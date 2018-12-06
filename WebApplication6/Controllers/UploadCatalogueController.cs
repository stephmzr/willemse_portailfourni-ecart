using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;
using System.IO.Compression;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication6.Controllers
{
    public class UploadCatalogueController : Controller
    {
        private ApplicationDbContext dbA = new ApplicationDbContext();
        MappingCatalogueEntities mapping = new MappingCatalogueEntities();
        //private string dossiersFournisseurs;

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
        public ActionResult PostFichier(HttpPostedFileBase file, string id, string pathCSV)
        {
            if (file != null && file.ContentLength > 0)
            {
                var extensionFichier = new[] { ".csv", "" };
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
                ViewBag.ErreurFormat = "Nous n'acceptons que les fichiers au format CSV";
                }
            }
            else
            {
                ViewBag.PasDeFichier = "Vous n'avez choisi aucun fichier";
            }


            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseWillemse"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PF_TELECHARGE_CSV";
            cmd.Parameters.Add("@FOURNI", SqlDbType.NVarChar);
            cmd.Parameters["@FOURNI"].Value = id;
            cmd.Parameters.Add("@FICHIER", SqlDbType.NVarChar);
            cmd.Parameters["@FICHIER"].Value = pathCSV;

            cmd.Connection = con;
           
            con.Close();
            

            return View("Index");
        }


        public ActionResult PostArchiveImg(HttpPostedFileBase imagesZip)
        {
            if (ModelState.IsValid)
            {

                if (imagesZip != null && imagesZip.ContentLength > 0)
                {
                    var extensionFichier = new[] { ".zip", ".rar", "" };
                    var checkextension = Path.GetExtension(imagesZip.FileName).ToLower();

                    if (extensionFichier.Contains(checkextension))
                    {

                        string idfou = CurrentUser.Id;
                        string extractPath = Server.MapPath("/" + CurrentUser.Id + "/Images");
                        string zipPath = Path.Combine(extractPath, imagesZip.FileName);
                        string unzipPath = Path.Combine(extractPath, (Path.GetFileNameWithoutExtension(imagesZip.FileName)));
                        if (Directory.Exists(unzipPath)) Directory.Delete(unzipPath, true);
                        if (System.IO.File.Exists(zipPath)) System.IO.File.Delete(zipPath);
                        imagesZip.SaveAs(zipPath);
                        // Forcer à remplacer anciennes images:
                        using (var zip = ZipFile.OpenRead(zipPath))
                        {
                            foreach (ZipArchiveEntry e in zip.Entries)
                            {
                            e.ExtractToFile(Path.Combine(extractPath, e.Name), true);
                            }
                        }
                        System.IO.File.Delete(zipPath);
                        ViewBag.MessageZip = "Archive envoyée avec succès.";
                    }
                    else 
                    {
                        ViewBag.Erreur = "Nous n'acceptons que les formats d'archive .zip ou .rar";
                    }
                }
                else
                {
                    ViewBag.Erreur = "Vous n'avez choisi aucun fichier";
                }
            }
            return View("Index");
        }

        public ActionResult MappingChamps()
        {

            return View();
        }

        public static MappingCatalogueEntities InsertBulkCSV(string id, string pathCSV)
        {
            MappingCatalogueEntities arttemp = new MappingCatalogueEntities();
            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseInfo"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PF_TELECHARGE_CSV";
            cmd.Parameters.Add("@FOURNI", SqlDbType.NVarChar);
            cmd.Parameters["@FOURNI"].Value = id;
            cmd.Parameters.Add("@FICHIER", SqlDbType.NVarChar);
            cmd.Parameters["@FOURNI"].Value = pathCSV;

            cmd.Connection = con;

            con.Close();

            return arttemp;

        }


        }



    }
