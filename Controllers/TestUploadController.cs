using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jquery_ajax_unobtrusive_fix_sample.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace jquery_ajax_unobtrusive_fix_sample.Controllers
{
   public class TestUploadController : Controller
    {
        private IHostingEnvironment _env;

        public TestUploadController(IHostingEnvironment env)
        {
            _env = env;
        }


        //
        // GET: /TestUpload/NormalPost

        public ActionResult NormalPost()
        {
            return View(
                new SimpleModel() { 
                    Id = 1,
                    Name = "Abel"
                }
            );
        }

        //
        // POST: /TestUpload/NormalPost

        [HttpPost]
        public ActionResult NormalPost(SimpleModel model)
        {
            var id = model.Id;
            var name = model.Name;
            var guid = Guid.NewGuid().ToString();
            var filename = guid + Path.GetExtension(model.File.FileName);
            var filePath = Path.Combine(_env.WebRootPath, "_temp_upload", filename); // ~/wwwroot/_temp_upload/
            var inputStream = model.File.OpenReadStream();

            SaveFile(inputStream, filePath);
            var html = "download <a href=\"" + Url.Action("Download", new { file = filename }) + "\">" + filename + "</a>";

            return Content(html, "text/html");
        }

        private void SaveFile(Stream inputStream, string filePath)
        {
            var buffer = new byte[Request.ContentLength.Value];

            using (var fs = System.IO.File.OpenWrite(filePath))
            {
                int read = 0;
                while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fs.Write(buffer, 0, read);
                }
            }
        }

        //
        // GET: /TestUpload/Download?file=file1.ext

        public ActionResult Download(string file)
        {
            return File("~/_temp_upload/" + file, "application/octet-stream", file);
        }

        //
        // GET: /TestUpload/AjaxPost

        public ActionResult AjaxPost()
        {
            return View(
                new SimpleModel()
                {
                    Id = 1,
                    Name = "Abel"
                }
            );
        }

        //
        // POST: /TestUpload/AjaxPost

        [HttpPost]
        public ActionResult AjaxPost(SimpleModel model)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if (!isAjax)
                return new NotFoundResult();

            try
            {
                var id = model.Id;
                var name = model.Name;
                var guid = Guid.NewGuid().ToString();
                var filename = guid + Path.GetExtension(model.File.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "_temp_upload", filename); // ~/wwwroot/_temp_upload/
                var inputStream = model.File.OpenReadStream();

                SaveFile(inputStream, filePath);

                var content = "<div>id = " + id + "</div>";
                content += "<div>name = " + name + "</div>";
                content += "<div>" + "download <a href=\"" + Url.Action("Download", new { file = filename }) + "\">" + filename + "</a>" + "</div>";

                if (!string.IsNullOrEmpty(Request.Form["SaveImage.x"]) && !string.IsNullOrEmpty(Request.Form["SaveImage.y"]))
                {
                    content += "<div>SaveImage.x = " + Request.Form["SaveImage.x"] + "</div>";
                    content += "<div>SaveImage.y = " + Request.Form["SaveImage.y"] + "</div>";
                }

                return Content(content);
            }
            catch (Exception ex)
            {
                return Content(ex.Message + "<br />" + ex.StackTrace);
            }
        }

        //
        // GET: /TestUpload/GetHTMLContent

        public ActionResult GetHTMLContent() 
        {
            return Content("<h4>text from the server</h4>");
        }
    }
}
