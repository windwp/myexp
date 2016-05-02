"use strict";
var cheerio = require('cheerio');
var request = require('request');
var _ = require('lodash');
var async = require('async');
var danhngon_1 = require("./danhngon");
var fs = require("fs");
var siteUrl = "http://khotangdanhngon.com/page/";
var listDanhNgon = [];
function getPage(pageIndex, callback) {
    request(siteUrl + pageIndex, function (error, response, body) {
        var pageQueue = async.queue(getDanhNgon, 5);
        pageQueue.drain = function () {
            console.log("Finish page" + pageIndex);
            callback();
        };
        var $ = cheerio.load(body);
        var listitem = $(".entry-content");
        _.each(listitem, function (item) {
            var url = $(item).find("a").attr("href");
            pageQueue.push(url);
            return;
        });
    });
}
function getDanhNgon(url, callback) {
    request(url, function (error, response, body) {
        if (!error && response.statusCode === 200) {
            var $ = cheerio.load(body);
            var danhngon = new danhngon_1.DanhNgon();
            danhngon.url = url.replace('http://khotangdanhngon.com/', '');
            danhngon.noidung = $('.entry-content > p > span').first().text();
            danhngon.tacgia = $('.entry-author > a').first().text();
            danhngon.phanloai = $('.posts-same-term > span >a ').first().text();
            listDanhNgon.push(danhngon);
            console.log("Them danh ngon");
        }
        else {
            console.log("Khong time thay bai viet " + url);
        }
        callback();
    });
}
var queues = async.queue(function (task, callback) {
    getPage(task, callback);
}, 4);
queues.drain = function () {
    console.log("Every thing done");
    fs.writeFile('./danhngon.txt', JSON.stringify({
        name: "danh ngon",
        content: listDanhNgon
    }), 'utf8');
};
for (var i = 1; i < 218; i++) {
    queues.push(i);
}
