using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace prjFileUpload.Controllers
{
    public class FileUploadController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase photo)
        {
            //上傳圖檔名字
            string fileName = "";
            //檔案上傳
            if (photo != null)
            {
                if (photo.ContentLength > 0)
                //取得圖檔名稱
                {
                    fileName = Path.GetFileName(photo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Photos"), fileName);
                    photo.SaveAs(path);
                }

            }
            //導View
            return RedirectToAction("ShowPhotos");
        }
        //ShowPhotos 方法使用ContentResult 傳回HTML
        //可顯示 Photos資料夾下所有圖檔
        public ContentResult ShowPhotos()//ContentResult的使用回傳特定文字
        {
            string strHtml = "";
            //建立可操作Photos 資料夾的dir
            DirectoryInfo dir =
                new DirectoryInfo(Server.MapPath("~/Photos"));
            //取得dir物件下的所有檔案(Photos)放入finfo陣列
            FileInfo[] fInfo = dir.GetFiles();
            //再把finfo檔案陣列內圖檔丟給strHtml
            foreach (FileInfo result in fInfo)
            {
                //將顯示圖的HTML字串指定給strHtml
                strHtml += $"<a href='../Photos/{result.Name}'  target='_blank'>" +
                        $"<img src ='../Photos/{result.Name}' width='150' height='120' border='0'>" + $"</a>";
            }
            //strHtml變數再加上'返回' Create()動作方法的連結
            strHtml += "<p><a href='Create'>返回</a></p>";
            return Content(strHtml, "text/html", System.Text.Encoding.UTF8);
        }
    }

}