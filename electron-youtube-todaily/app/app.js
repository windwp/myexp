window.$ = window.jQuery = require('./vendor/jquery-2.2.0.min.js');
var YouTube = require('youtube-node');
var fs = require('fs');
var path = require('path');
var youtubedl = require('youtube-dl');

var electron = require('electron');
var jetpack = require('fs-jetpack');

var app;
if (process.type === 'renderer') {
	app = require('electron').remote.app;
} else {
	app = require('electron').app;
}



//End Log
var manifest = jetpack.cwd(app.getAppPath()).read('package.json', 'json');
var env = manifest.env;
var appDir = app.getAppPath();
var ytdlBinary = path.join(app.getAppPath(), 'vendor', 'youtube-dl');
if (env.name === 'production') {
	appDir = path.join(app.getAppPath(), "../../");
}

//Log config
var logger = require("./vendor/logger").createLogger(path.join(appDir, "log.txt"));

process.on('uncaughtException', function(err) {
	logger.log('info', 'Caught exception: ' + err);
});

console.log = (function() {
	var error = console.log;

	return function(exception) {
		logger.log('info', exception);
		if (typeof exception.stack !== 'undefined') {
			error.call(console, exception.stack);
		} else {
			error.apply(console, arguments);
		}
	};
})();
//end log config
var DOWNLOAD_FOLDER = path.join(appDir, 'download');
var USER_KEY_JSON = path.join(appDir, "dm.key.json");
var YOUTUBE_KEY = "AIzaSyB1OOSpTREs85WUMvIgJvLTZKye4BVsoFU";
var DMClient = require('dailymotion-sdk').client;

var DM_CLIENTID = '7263aad9966cac32c307'; // Fill yours
var DM_SECRET = '3f083885a5a9ac99da08c9dcea5ec7cd87f9ca1b'; // Fill yours
var DM_SCOPE = [
	//'desired scopes',
	//'refer to API documentation',
	'email',
	'userinfo',
	'feed',
	'manage_videos'
];
//Web
var videoApp = angular.module('dmApp', ['ngRoute']);
videoApp.config(['$routeProvider', '$locationProvider',
	function($routeProvider, $locationProvider) {
		$routeProvider.when('/', {
				templateUrl: 'view/search.html',
				controller: 'SearchController',
				controllerAs: 'ctrl'
			})
			.when('/download', {
				templateUrl: 'view/download.html',
				controller: 'DownloadController',
				controllerAs: 'ctrl'
			})
			.when('/upload', {
				templateUrl: 'view/upload.html',
				controller: 'UploadController',
				controllerAs: 'ctrl'
			});

		$routeProvider.otherwise({
			redirectTo: '/'
		});
	}
]);
videoApp.service('DownloadService', ['$rootScope', 'UploadService', function($rootScope, UploadService) {
	var self = this;
	self.maxDownloadConnection = 3;
	self.ListDownLoad = [];
	/**
	 * add video to list download
	 * @param {[type]} videoInfo video download
	 */
	self.addVideo = function(videoInfo) {
		if (!videoInfo || !videoInfo.id) return;
		if (videoInfo.isLocked) return;
		for (var i = self.ListDownLoad.length - 1; i >= 0; i--) {
			if (videoInfo.id === self.ListDownLoad[i].id) {
				console.log("video already download");
				return;
			}
		}
		self.percent = 0;
		self.isLocked = false;
		self.ListDownLoad.push(videoInfo);
		self.autoDownload();
	};
	/**
	 * Start download video
	 * @param  {VideoInfo} videoInfo Video to download
	 */
	self.startDownload = function(videoInfo) {
		if (videoInfo.isLocked) return;
		videoInfo.isLocked = true;
		//write json file
		fs.writeFile(path.join(DOWNLOAD_FOLDER, videoInfo.id + ".json"), JSON.stringify(videoInfo), function(error) {
			if (error) throw error;
		});
		var video = youtubedl('http://www.youtube.com/watch?v=' + videoInfo.id, [], {
			cwd: DOWNLOAD_FOLDER,
			ytdl: ytdlBinary
		});
		video.on('info', function(info) {
			videoInfo.size = info.size;
			videoInfo.percent = 1;
		});
		var pos = 0;
		video.on('data', function(data) {
			pos += data.length;
			if (videoInfo.size) {
				var percent = (pos / videoInfo.size * 100).toFixed(2);
				videoInfo.percent = percent;
				$rootScope.$digest();
			}
		});
		video.on('end', function(data) {
			self.removeVideo(videoInfo);
		});
		video.pipe(fs.createWriteStream(path.join(DOWNLOAD_FOLDER, videoInfo.id + '.mp4')));
	};
	/**
	 * [autoDownload auto download next video when download finish
	 * @return {[type]} [description]
	 */
	self.autoDownload = function() {
		var numVideoDownloading = 0;
		var newUpload;
		for (var i = self.ListDownLoad.length - 1; i >= 0; i--) {
			var vid = self.ListDownLoad[i];
			if (vid.isLocked) {
				numVideoDownloading += 1;
			} else {
				newUpload = vid;
			}
		}
		if (newUpload && numVideoDownloading < self.maxDownloadConnection) {
			self.startDownload(newUpload);
		}
	};
	self.removeVideo = function(video) {
		video.isLocked = false;
		//remove video from list;
		for (var i = self.ListDownLoad.length; i--;) {
			if (self.ListDownLoad[i].id === video.id) {
				self.ListDownLoad.splice(i, 1);
			}
		}
		self.autoDownload();
		//add video to upload
		UploadService.addVideo(video);
		$rootScope.$digest();
	};
	self.getVideoDownloads = function() {
		return self.ListDownLoad;
	};
}]);


videoApp.service('UploadService', ['$rootScope', function($rootScope) {
	var self = this;
	self.ListUpload = [];
	self.clientDM = {};
	self.username = '';
	self.password = '';
	self.maxUploadConnection = 1;
	self.isLogin = false;
	self.lastTimeUpload = 0;
	self.waitNextUpload = 10000;
	self.isLastUploadFinish = true;
	self.loginCredentials = function() {
		if (self.username !== '' && self.password !== '') {
			self.clientDM = new DMClient(DM_CLIENTID, DM_SECRET, DM_SCOPE);
			self.clientDM.setCredentials(DMClient.GRANT_TYPES.PASSWORD, {
				username: self.username,
				password: self.password
			});
			self.clientDM.createToken(function() {
				self.isLogin = true;
				console.log("Login Finish");
			});
		}
	};

	self.addVideo = function(videoInfo) {
		if (!videoInfo) return;
		for (var i = self.ListUpload.length - 1; i >= 0; i--) {
			if (videoInfo.id === self.ListUpload[i].id) {
				console.log("video already download");
				return;
			}
		}
		videoInfo.isLocked = false;
		videoInfo.percent = 0;
		self.ListUpload.push(videoInfo);

	};

	self.startUpload = function(videoInfo) {
		if (videoInfo.isLocked) return;
		videoInfo.isLocked = true;
		if (self.isLastUploadFinish && Date.now() - self.lastTimeUpload > self.waitNextUpload) {
			self.isLastUploadFinish = false;
			self.lastTimeUpload = Date.now();
			logger.log('info', "Upload new file :" + videoInfo.id);
			//self.loginCredentials();
			self.clientDM.upload({
				filepath: path.join(DOWNLOAD_FOLDER, videoInfo.id + ".mp4"),
				meta: {
					title: videoInfo.title,
					description: videoInfo.description,
					channel: videoInfo.uploadChannel,
					tags: videoInfo.tags,
					published: true
				},
				progress: function(error, r, result) {
					if (error) {
						console.log(error);
						console.log(r);
						console.log(result);
						self.removeVideo(videoInfo);
					}
					videoInfo.percent = result.progress.toFixed(2);
					$rootScope.$digest();
				},
				done: function() {
					self.removeVideo(videoInfo);
				}
			});

		} else {
			alert("You need wait for next upload");
			setTimeout(self.autoUpload, self.waitNextUpload);
		}

	};

	//check for can upload next video
	self.autoUpload = function() {
		var numVideoUploading = 0;
		var newUpload;
		for (var i = self.ListUpload.length - 1; i >= 0; i--) {
			var vid = self.ListUpload[i];
			if (vid.isLocked) {
				numVideoUploading += 1;
			} else {
				newUpload = vid;
			}

		}
		if (newUpload && numVideoUploading < self.maxUploadConnection) {
			self.startUpload(newUpload);
		}

	};
	/**
	 * [removeVideo description]
	 * @param  {[type]} video [description]
	 * @return {[type]}       [description]
	 */
	self.removeVideo = function(video) {
		video.isLocked = false;
		//remove video from list;
		for (var i = self.ListUpload.length; i--;) {
			if (self.ListUpload[i].id === video.id) {
				self.ListUpload.splice(i, 1);
				self.isLastUploadFinish = true;
			}
		}
		// wait for stream unload 
		setTimeout(function() {
			try {
				fs.unlinkSync(path.join(DOWNLOAD_FOLDER, video.id + ".mp4"));
				fs.unlinkSync(path.join(DOWNLOAD_FOLDER, video.id + ".json"));
			} catch (error) {
				logger.log('info', "video :" + video.id + " :" + error);
			}
		}, 1000);
		self.autoUpload();
		$rootScope.$digest();
	};
	self.getVideoUploads = function() {
		return self.ListUpload;
	};

}]);
videoApp.controller('AppController', ['$scope', 'UploadService', function($scope, UploadService) {
	var self = this;
	self.username = '';
	self.password = '';
	self.intiLoginDM = function() {
		//create folder download
		fs.exists(appDir + "/download", function(error) {
			if (!error) {
				fs.mkdir(appDir + "/download", function(args) {});

			}
		});
		//reload json file information
		fs.readFile(USER_KEY_JSON, "utf-8", function(error, text) {
			if (error) return;
			var obj = JSON.parse(text);
			self.username = (obj.username) ? obj.username : '';
			self.password = (obj.password) ? obj.password : '';
			UploadService.username = self.username;
			UploadService.password = self.password;
			UploadService.loginCredentials();

		});
	};
	self.intiLoginDM();

}]);

videoApp.controller('SearchController', ['$scope', 'DownloadService', function($scope, DownloadService) {
	var self = this;
	self.searchCount = 16;
	self.ListVideo = [];
	self.wordSearch = '';
	self.nextPageToken = '';
	self.prevPageToken = '';
	self.currentToken = '';
	self.allTags = "";
	self.uploadChannel = "";

	self.clickSearch = function() {
		self.currentToken = '';
		self.search();
	};
	self.loadNext = function() {
		self.currentToken = self.nextPageToken;
		self.search();

	};
	self.loadPrev = function() {
		self.currentToken = self.prevPageToken;
		self.search();
	};
	self.search = function() {
		self.ListVideo = [];
		var youTube = new YouTube();
		youTube.setKey(YOUTUBE_KEY);
		youTube.search(self.wordSearch, self.searchCount, function(error, result) {
			if (error) {
				console.log(error);
			} else {
				for (var i = result.items.length - 1; i >= 0; i--) {
					var resultInfo = result.items[i];
					var videoInfo = new VideoInfo();

					videoInfo.title = resultInfo.snippet.title;
					videoInfo.description = removeLink(resultInfo.snippet.description);
					videoInfo.publishedAt = getYTtime(resultInfo.snippet.publishedAt);
					videoInfo.channelTitle = (resultInfo.snippet.channelTitle) ? resultInfo.snippet.channelTitle : resultInfo.snippet.channelId;
					videoInfo.image = resultInfo.snippet.thumbnails.medium.url;
					videoInfo.id = resultInfo.id.videoId;
					self.prevPageToken = (result.prevPageToken) ? result.prevPageToken : '';
					self.nextPageToken = (result.nextPageToken) ? result.nextPageToken : '';
					//loading video view
					self.loadingView(videoInfo);
					self.ListVideo.push(videoInfo);
				}

				$scope.$digest();
			}
		}, self.currentToken);
	};
	/**
	 * Get view count and like count of video
	 * because search function can't get view count property,
	 * @param  {[type]} video [description]
	 */
	self.loadingView = function(video) {
		if (!video) return;
		var youTube = new YouTube();
		youTube.setKey(YOUTUBE_KEY);
		youTube.getById(video.id, function(error, result) {
			if (error) {
				alert(error);
			} else {
				if (result.items && result.items.length == 1) {
					video.publishedAt = getYTtime(result.items[0].snippet.publishedAt);
					video.tags = result.items[0].snippet.tags.join(',');
					video.duration = minutesFromIsoDuration(result.items[0].contentDetails.duration);
					video.licensedContent = result.items[0].contentDetails.licensedContent;
					video.numberView = result.items[0].statistics.viewCount;
					video.numberLike = result.items[0].statistics.likeCount;
				}
			}
			$scope.$digest();
		});
	};
	self.addToDownload = function(video) {
		//add information of video
		if(self.allTags&&self.allTags!=='')video.tags = self.allTags;
		if(self.uploadChannel&&self.uploadChannel!=='')video.uploadChannel = self.uploadChannel;
		DownloadService.addVideo(video);
	};

}]);
videoApp.controller('DownloadController', ['$scope', 'DownloadService', 'UploadService', function($scope, DownloadService, UploadService) {
	var self = this;
	self.ytLink = "";
	self.ListDownloadVideo = [];
	self.ListLocalVideo = [];
	self.maxDownloadConnection = 0;
	self.loadDownloadVideo = function() {
		self.maxDownloadConnection = DownloadService.maxDownloadConnection;
		self.ListDownloadVideo = DownloadService.getVideoDownloads();
	};
	self.loadLocalVideo = function() {
		self.ListLocalVideo = [];
		fs.readdir(DOWNLOAD_FOLDER, function(error, files) {
			if (error) return;
			// body
			files.forEach(function(file) {
				if (file.endsWith(".mp4")) {
					//check json file exits
					var jsonfile = path.join(DOWNLOAD_FOLDER, file.replace(".mp4", ".json"));
					fs.exists(jsonfile, function() {
						fs.readFile(jsonfile, "utf-8", function(error, text) {
							if (error) return;
							self.ListLocalVideo.push(JSON.parse(text));
							$scope.$digest();
						});

					});
				}
			});
		});
	};

	self.updateDownloadInfo = function() {
		DownloadService.maxDownloadConnection = self.maxDownloadConnection;
	};

	self.addYoutubeVideo = function() {
		var youtubeid = youtube_parser(self.ytLink);
		var youTube = new YouTube();
		youTube.setKey(YOUTUBE_KEY);
		youTube.getById(youtubeid, function(error, result) {
			if (error) {
				alert(error);
			} else {
				if (result.items && result.items.length == 1) {
					var item = result.items[0];
					var video = new VideoInfo();
					video.id = youtubeid;
					video.title = item.snippet.title;
					video.tags=item.snippet.tags.join(',');
					video.description = removeLink(item.snippet.description);
					video.publishedAt = getYTtime(item.snippet.publishedAt);
					video.channelTitle = item.snippet.channelTitle;
					video.duration = minutesFromIsoDuration(item.contentDetails.duration);
					video.licensedContent = result.items.contentDetails.licensedContent;
					video.image = item.snippet.thumbnails.medium.url;
					video.numberView = item.statistics.viewCount;
					video.numberLike = item.statistics.likeCount;
					DownloadService.addVideo(video);
					$scope.$digest();
				}
			}
			$scope.$digest();
		});
	};
	self.download = function(video) {
		DownloadService.startDownload(video);
	};
	self.upload = function(video) {
		UploadService.addVideo(video);
	};
	self.loadDownloadVideo();
	self.loadLocalVideo();
}]);
videoApp.controller('UploadController', ['$scope', 'UploadService', function($scope, UploadService) {
	var self = this;
	self.maxUploadConnection = 0;
	self.ListUploadVideo = [];
	self.username = '';
	self.password = '';
	self.loginDM = function() {
		UploadService.username = self.username;
		UploadService.password = self.password;
		UploadService.loginCredentials();
	};
	self.saveFile = function() {
		var saveText = JSON.stringify({
			username: self.username,
			password: self.password
		});
		fs.writeFile(USER_KEY_JSON, saveText, function(error) {
			if (error) throw error;

		});
	};
	self.loadUploadVideo = function() {
		self.ListUploadVideo = UploadService.getVideoUploads();
		self.username = UploadService.username;
		self.password = UploadService.password;
		self.maxUploadConnection = UploadService.maxUploadConnection;
	};


	self.updateVideoInfo = function() {
		UploadService.maxUploadConnection = self.maxUploadConnection;
	};
	self.upload = function(videoInfo) {
		UploadService.startUpload(videoInfo);
	};

	self.removeVideo = function(videoInfo) {
		UploadService.removeVideo(videoInfo);
	};
	/**
	 * Save info to  json file
	 * @param  {Object} videoI nfo  file
	 */
	self.saveTag = function(videoInfo) {
		fs.writeFile(path.join(DOWNLOAD_FOLDER, videoInfo.id + ".json"), JSON.stringify(videoInfo), function(error) {
			if (error) throw error;
			// body
		});
	};
	// self.saveAllTag = function() {
	// 	self.ListUploadVideo.forEach(function(videoInfo) {
	// 		videoInfo.tags = self.allTags;
	// 		videoInfo.uploadChannel = self.allCategory;
	// 		self.saveTag(videoInfo);
	// 	});
	// };

	self.loadUploadVideo();

}]);

function VideoInfo() {
	this.numberView = "";
	this.numberLike = "";

	this.id = "";
	this.path = "";
	this.title = "";
	this.channelTitle = "";
	this.description = "";
	this.tags = "";
	this.publishedAt = "";
	this.image = "";
	this.duration = "";
	this.uploadChannel = "";
	this.licensedContent = false;
	this.size = 0;
	this.percent = 0;
	this.isLocked = false;
}

var regex = /P((([0-9]*\.?[0-9]*)Y)?(([0-9]*\.?[0-9]*)M)?(([0-9]*\.?[0-9]*)W)?(([0-9]*\.?[0-9]*)D)?)?(T(([0-9]*\.?[0-9]*)H)?(([0-9]*\.?[0-9]*)M)?(([0-9]*\.?[0-9]*)S)?)?/

function minutesFromIsoDuration(duration) {
	var matches = duration.match(regex);

	return parseFloat(matches[14]) || 0;
}

function youtube_parser(url) {
	var regExp = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;
	var match = url.match(regExp);
	if (match && match[7].length == 11) {
		return match[7];
	} else {
		alert("Link YouTube Không đúng");
		return '';
	}
}

function copyFile(source, target, cb) {
	var cbCalled = false;

	var rd = fs.createReadStream(source);
	rd.on("error", function(err) {
		done(err);
	});
	var wr = fs.createWriteStream(target);
	wr.on("error", function(err) {
		done(err);
	});
	wr.on("close", function(ex) {
		done();
	});
	rd.pipe(wr);

	function done(err) {
		if (cb && !cbCalled) {
			cb(err);
			cbCalled = true;
		}
	}
}


function getYTtime(timeString) {
	var dt = timeString.replace("T", " ").replace(/\..+/g, "");
	dt = new Date(dt);
	return dt.toLocaleDateString();
}


function removeLink(description) {
	var reg = /((https?|ftp):\/\/|www\.)[^\s/$.?#].[^\s]*/ig;
	return description.replace(reg, "");
}