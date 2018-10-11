using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers
{
    public class GetFournisseurDataController : Controller
    {
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}