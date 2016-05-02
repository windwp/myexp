import cheerio=require('cheerio');
import request=require('request');
import _=require('lodash');
import async=require('async');
import {DanhNgon} from "./danhngon";
import fs = require("fs");
const siteUrl = "http://danhngoncuocsong.vn/";
var listDanhNgon:Array<any> = [];

function getPage(category:string, pageIndex:number, callback:Function) {
  var reqUrl = `${siteUrl}chu-de/${category}_${pageIndex}.html`;
  console.log("reqUrl" + reqUrl);
  request(reqUrl, (error:any, response:any, body:any)=> {
    try {
      var $ = cheerio.load(body);
      var listitem = $('.bqQuoteLink');
      _.each(listitem, (item:any)=> {
        var danhngon = new DanhNgon();
        danhngon.url = $(item).find('a').first().attr("href");
        danhngon.image = $(item).find('img').first().attr('src');
        danhngon.noidung = $(item).find('a').first().text().trim();
        danhngon.tacgia = $(item).next().find('a').first().text().trim();
        danhngon.phanloai = category;
        // console.log("dn " + danhngon);
        listDanhNgon.push(danhngon);
        return;
      })
    } catch (e) {
      console.log(`Error ${e} page :${pageIndex}`);
    } finally {
      if (callback)callback();
    }
  })
}

var queues = async.queue((obj:{page:number,category:string}, callback:Function)=> {
  getPage(obj.category, obj.page, callback)
}, 4);
queues.drain = ()=> {
  console.log("Every thing done");
  fs.writeFile('./danhngoncs.txt',
    JSON.stringify({
        name: "danh ngon",
        content: listDanhNgon
      }
    ), 'utf8');
};

function pushQueue(category:string, totalPage:number) {
  for (var i = 1; i < totalPage; i++) {
    queues.push({
      page: i,
      category: category
    });
  }
}

pushQueue('danh-ngon-cuoc-song', 78);
pushQueue('danh-ngon-gia-dinh', 10);
pushQueue('danh-ngon-cong-viec', 22);
pushQueue('danh-ngon-giao-duc', 9);
pushQueue('danh-ngon-hai-huoc', 12);
pushQueue('danh-ngon-hanh-dong', 11);
pushQueue('danh-ngon-hanh-phuc', 12);
pushQueue('danh-ngon-nghe-thuat', 10);
pushQueue('danh-ngon-phu-nu', 10);
pushQueue('danh-ngon-dan-ong', 6);
pushQueue('danh-ngon-su-nghiep', 18);
pushQueue('danh-ngon-tam-hon', 13);
pushQueue('danh-ngon-thoi-gian', 9);
pushQueue('danh-ngon-tien-bac', 9);
pushQueue('danh-ngon-tinh-ban', 11);
pushQueue('danh-ngon-tinh-cach', 22);
pushQueue('danh-ngon-tinh-yeu', 22);
pushQueue('danh-ngon-tri-tue', 21);
pushQueue('danh-ngon-van-hoa', 13);
