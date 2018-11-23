using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class UploadCatalogueController : Controller
    {
        private ApplicationDbContext dbA = new ApplicationDbContext();

        private ApplicationUser CurrentUser
        {

            get
            {
                string currentUserId = User.Identity.GetUserId();
                return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostFichier(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {

                var extensionFichier = new[] { ".xls", ".csv", ".xlsx", "" };
                var checkextension = Path.GetExtension(file.FileName).ToLower();

                if (extensionFichier.Contains(checkextension))
                {
                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        //var path = Path.Combine(Server.MapPath("/UploadsCatalogue"), fileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                        file.SaveAs(path);
                        ViewBag.Message = "Fichier envoyé avec succès.";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }

                 else
            {
                ViewBag.ErreurFormat = "Nous n'acceptons que les formats CSV, XLS ou XLSX";
            }
            }
            else
            {
                ViewBag.Message = "Vous n'avez choisi aucun fichier";
            }
            return View("Index");
        }


    }
}