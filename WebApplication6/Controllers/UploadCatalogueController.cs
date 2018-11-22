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

            var extensionFichier = new[] { ".xls", ".csv", ".xlsx" };
            var checkextension = Path.GetExtension(file.FileName).ToLower();


            if (extensionFichier.Contains(checkextension))
            {

                if (file != null && file.ContentLength > 0)
                {

                    try
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        //var path = Path.Combine(Server.MapPath("/UploadsCatalogue"), fileName);
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


        public FileResult Download(string id)
        {
            if (!string.IsNullOrEmpty(id) && id.Equals("Pro"))
            {
                string filename = "MP_" + CurrentUser.Societe.Replace(" ", string.Empty) + "_Produits.xlsx";
                return File(Path.Combine(Server.MapPath(appData), p.modeleProduits), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            return null;
        }

        //fonction ancien portail
        //public class FicheProduitViewModel : IValidatableObject
        //{
        //    [Required(ErrorMessage = "Il faut obligatoirement  un fichier")]
        //    [Display(Name = "Fichier")]
        //    [DataType(DataType.Upload)]
        //    //[FileExtensions(ErrorMessage = "L'extension du fichier doit etre en .xlsx ou en .xls", Extensions = "xlsx")]
        //    public HttpPostedFileBase FicPro { get; set; }
        //    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //    {
        //        if (FicPro != null && FicPro.ContentLength > 0)
        //        {
        //            string ext = System.IO.Path.GetExtension(FicPro.FileName);
        //            if (!ext.Equals(".xlsx"))
        //            {
        //                yield return new ValidationResult("Le Fichier est incorrect. Il doit être en '.xlsx' ", new[] { "FicPro" });
        //            }
        //        }
        //    }
        //}

    }
}