using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers
{
    public class UploadCatalogueController : Controller
    {



        public ActionResult UploadCatalogue()
        {
            return View();
        }




        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            var extensionFichier = new[] { ".xls", ".csv", ".xlsx" };
            var checkextension = Path.GetExtension(file.FileName).ToLower();


            if (extensionFichier.Contains(checkextension))
            {

                if (file != null && file.ContentLength > 0)
                {




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
                }

                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }


            }
            else
            {

                ViewBag.Message = "Nous n'acceptons que les formats CSV, XLS ou XLSX";
            }

            return RedirectToAction("Index", "Produits");
                


        }

    }
}