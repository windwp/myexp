import cheerio=require('cheerio');
import request=require('request');
import _=require('lodash');
import async=require('async');
import {DanhNgon} from "./danhngon";
import fs = require("fs");
const siteUrl = "http://khotangdanhngon.com/page/";
var listDanhNgon:Array<any> = [];

function getPage(pageIndex:number, callback:Function) {

  request(siteUrl + pageIndex, (error:any, response:any, body:any)=> {
    var pageQueue = async.queue(getDanhNgon, 5);
    pageQueue.drain = ()=> {
      console.log("Finish page" + pageIndex);
      callback();
    };
    var $ = cheerio.load(body);
    var listitem = $(".entry-content");
    _.each(listitem, (item:any)=> {
      var url = $(item).find("a").attr("href");
      pageQueue.push(url);
      return;
    })
  })
}
function getDanhNgon(url:string, callback:Function) {
  request(url, (error:any, response:any, body:any)=> {
    if (!error && response.statusCode === 200) {
      var $ = cheerio.load(body);
      var danhngon = new DanhNgon();
      danhngon.url = url.replace('http://khotangdanhngon.com/', '');
      danhngon.noidung = $('.entry-content > p > span').first().text();
      danhngon.tacgia = $('.entry-author > a').first().text();
      danhngon.phanloai = $('.posts-same-term > span >a ').first().text();
      listDanhNgon.push(danhngon);
      console.log("Them danh ngon");
    } else {
      console.log("Khong time thay bai viet " + url);
    }
    callback()
  });
}

var queues = async.queue((task:number, callback:Function)=> {
  getPage(task, callback)
}, 4);
queues.drain = ()=> {
  console.log("Every thing done");
  fs.writeFile('./danhngon.txt',
    JSON.stringify({
        name: "danh ngon",
        content: listDanhNgon
      }
    ), 'utf8');
};


for (var i = 1; i < 218; i++) {
  queues.push(i);
}
