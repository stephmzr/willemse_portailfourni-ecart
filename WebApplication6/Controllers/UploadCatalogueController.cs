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

        //Poster fichier CSV + appel procédure stockée TELECHARGE_CSV
        [HttpPost]
        public ActionResult PostFichier(HttpPostedFileBase file)
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
                        var path = Path.Combine(Server.MapPath("/App_Data/uploads"), fileName);
                        //var path = Path.Combine(Server.MapPath("~/Dossier_fournisseurs/"+ CurrentUser.Id), fileName);
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
            cmd.CommandText = "[OW_BP].[PF_TELECHARGE_CSV]";
            cmd.Parameters.Add("@FOURNI", SqlDbType.NVarChar);
            cmd.Parameters["@FOURNI"].Value = CurrentUser.Id;
            cmd.Parameters.Add("@FICHIER", SqlDbType.NVarChar);
            cmd.Parameters["@FICHIER"].Value = file.FileName;

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();


            return View("Index");
        }

        //Poster archive d'images et décompresse dans le dossier du fournisseur
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

        //Page correspondance des champs
        public ActionResult MappingChamps()
        {
            var champs = mapping.pf_colonne_csv.ToList();

            List<SelectListItem> colonnelist = (from p in mapping.pf_colonne_csv.Where(x => x.id_fournisseur == CurrentUser.Id).AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.colonne,
                                                     Value = p.id.ToString()
                                                 }).ToList();

            colonnelist.Insert(0, new SelectListItem { Text = "--Selectionnez les champs--", Value = "" });

            return View(colonnelist);
        }

        //Action envoyer la correspondance des champs et les inscrit dans la table colonne_csv
        [HttpPost]
        public ActionResult SubmitMapping(string refproduit, string libproduit, string pvht, string fport, string pvttc, string categorie,
            string delailiv, string datedelivr, string nbpiecepaquet, string longueur, string dimensions, string statut, string codebarre, string poids, string argcommercial,
            string slogan, string couleur, string qualitelivree, string garantie, string photo1, string photo2, string photo3, string photo4, string photo5, string photo6, string photo7,
            string photo8, string photo9, string photo10, string feuillage, string parfume, string arrosage, string niveausoin, string distanceplantation, string periodefloraison,
            string perioderecolte, string periodesemis, string periodeplantation, string climat, string typesol, string typeutil, string ensoleillement, string gelif, string culturepot )
        {
            var champs = mapping.pf_colonne_csv.ToList();
            if (!string.IsNullOrEmpty(refproduit))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(refproduit));
                if( lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Référence produit";
                }
            }
            if (!string.IsNullOrEmpty(libproduit))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(libproduit));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Libellé produit";
                }
            }
            if (!string.IsNullOrEmpty(pvht))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(pvht));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Prix de vente HT";
                }
            }
            if (!string.IsNullOrEmpty(fport))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(fport));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Frais de port";
                }
            }
            if (!string.IsNullOrEmpty(pvttc))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(pvttc));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Prix de vente conseillé TTC";
                }
            }
            if (!string.IsNullOrEmpty(categorie))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(categorie));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Catégorie";
                }
            }
            if (!string.IsNullOrEmpty(delailiv))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(delailiv));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Délais de livraison";
                }
            }
            if (!string.IsNullOrEmpty(datedelivr))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(datedelivr));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Date de délivrabilité";
                }
            }
            if (!string.IsNullOrEmpty(nbpiecepaquet))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(nbpiecepaquet));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Nombre de pièce par paquet";
                }
            }
            if (!string.IsNullOrEmpty(longueur))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(longueur));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Longueur du produit";
                }
            }
            if (!string.IsNullOrEmpty(dimensions))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(dimensions));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Dimensions";
                }
            }
            if (!string.IsNullOrEmpty(statut))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(statut));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Statut";
                }
            }
            if (!string.IsNullOrEmpty(codebarre))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(codebarre));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Code barres";
                }
            }
            if (!string.IsNullOrEmpty(poids))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(poids));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Poids brut";
                }
            }
            if (!string.IsNullOrEmpty(argcommercial))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(argcommercial));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Argumentaire commercial";
                }
            }
            if (!string.IsNullOrEmpty(slogan))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(slogan));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Slogan";
                }
            }
            if (!string.IsNullOrEmpty(couleur))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(couleur));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Couleur";
                }
            }
            if (!string.IsNullOrEmpty(qualitelivree))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(qualitelivree));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Qualité livrée";
                }
            }
            if (!string.IsNullOrEmpty(garantie))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(garantie));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Garantie";
                }
            }
            if (!string.IsNullOrEmpty(photo1))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo1));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo principale";
                }
            }
            if (!string.IsNullOrEmpty(photo2))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo2));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 2";
                }
            }
            if (!string.IsNullOrEmpty(photo3))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo3));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 3";
                }
            }
            if (!string.IsNullOrEmpty(photo4))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo4));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 4";
                }
            }
            if (!string.IsNullOrEmpty(photo5))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo5));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 5";
                }
            }
            if (!string.IsNullOrEmpty(photo6))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo6));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 6";
                }
            }
            if (!string.IsNullOrEmpty(photo7))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo7));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 7";
                }
            }
            if (!string.IsNullOrEmpty(photo8))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo8));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 8";
                }
            }
            if (!string.IsNullOrEmpty(photo9))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo9));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 9";
                }
            }
            if (!string.IsNullOrEmpty(photo10))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(photo10));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Photo 10";
                }
            }
            if (!string.IsNullOrEmpty(feuillage))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(feuillage));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Feuillage persistant";
                }
            }
            if (!string.IsNullOrEmpty(parfume))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(parfume));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Parfumé oui ou non";
                }
            }
            if (!string.IsNullOrEmpty(arrosage))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(arrosage));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Arrosage";
                }
            }
            if (!string.IsNullOrEmpty(niveausoin))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(niveausoin));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Niveau de soin";
                }
            }
            if (!string.IsNullOrEmpty(distanceplantation))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(distanceplantation));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Distance de plantation";
                }
            }
            if (!string.IsNullOrEmpty(periodefloraison))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(periodefloraison));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Période de floraison ";
                }
            }
            if (!string.IsNullOrEmpty(perioderecolte))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(perioderecolte));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Période de récolte ";
                }
            }
            if (!string.IsNullOrEmpty(periodesemis))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(periodesemis));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Période de semis";
                }
            }
            if (!string.IsNullOrEmpty(periodefloraison))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(periodeplantation));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Période de plantation";
                }
            }
            if (!string.IsNullOrEmpty(climat))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(climat));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Climat";
                }
            }
            if (!string.IsNullOrEmpty(typesol))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(typesol));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Type de sol";
                }
            }
            if (!string.IsNullOrEmpty(typeutil))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(typeutil));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Type d’utilisation";
                }
            }
            if (!string.IsNullOrEmpty(ensoleillement))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(libproduit));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Ensoleillement";
                }
            }
            if (!string.IsNullOrEmpty(gelif))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(gelif));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Gélif ";
                }
            }
            if (!string.IsNullOrEmpty(culturepot))
            {
                var lacolonne = champs.SingleOrDefault(x => x.id == Int32.Parse(culturepot));
                if (lacolonne != null)
                {
                    lacolonne.colonnewillemse = "Culture possible en pot";
                }
            }
            mapping.SaveChanges();

            return View("Index");
        }


        //Page correspondance des catégories
        public ActionResult MappingCategories()
        {
            var champs = mapping.pf_champ_willemse.ToList();

            return View(champs);

        }
    }
}
