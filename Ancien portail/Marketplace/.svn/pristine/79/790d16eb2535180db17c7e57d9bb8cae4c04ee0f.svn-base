using Microsoft.AspNet.Identity;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WillemseFranceMP.Models;

namespace WillemseFranceMP.Controllers
{
    // Ce controller gère tous les traitements de fichiers et dossiers
    [Authorize(Roles = "Utilisateur")]
    public class FichierProduitsController : Controller
    {
        private ProduitDbOracleContext db = new ProduitDbOracleContext();
        private ApplicationDbOracleContext dbA = new ApplicationDbOracleContext();
        private ImportExcelFile FicPro = new ImportExcelFile();
        private Parametres p = new Parametres();
        UtilNotifications notif = new UtilNotifications();
        private string dossiersFournisseurs;
        private string appData;

        public FichierProduitsController()
        {
            this.dossiersFournisseurs = p.DossiersFournisseurs;
            this.appData = p.appData;
        }

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
            if (ApplicationUser.estEnAttente(CurrentUser))
                return RedirectToAction("Attente", "Produits");
            return View();
        }

        // Permet de télécharger un modèle de fichiers produits dans un document excel 
        public FileResult Download(string id)
        {
            if (!string.IsNullOrEmpty(id)&&id.Equals("Pro"))
            {
              string  filename = "MP_" + CurrentUser.Societe.Replace(" ",string.Empty) + "_Produits.xlsx";
              return File(Path.Combine(Server.MapPath(appData),p.modeleProduits), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            return null;
        }

        // Permet de télécharger un modèle de fichiers offres dans un document excel 
        // tout en considérant les références produit fournisseur et la désignation commerciale des produits enregistrés
        public FileResult DownloadOffExcel(string id)
        {
            if (!string.IsNullOrEmpty(id) && id.Equals("Off"))
            {
                string sourcepath = Path.Combine(Server.MapPath(appData), p.modeleOffres);
                string filename = "MP_" + this.CurrentUser.Societe.Replace(" ",string.Empty) + "_Offres.xlsx";
                string destpath = Server.MapPath(dossiersFournisseurs +"/" + CurrentUser.IdFou + "/" + filename);
                System.IO.File.Copy(sourcepath, destpath, true);
                // On peut utiliser l'appel asynchrone 
                this.FicPro.CreateExcelFileOff(destpath, CurrentUser.IdFou);
                return File(destpath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
            }
            return null;

        }


        // Permet de traiter un fichier de produits chargé par un fournisseur et crée un fichier de rapports d'erreurs
        [HttpPost]
        public ActionResult GenereFicPro(FicheProduitViewModel model)
        {
            DataTable dtResult = null;
            int nbErrors = 0; 
            if (ModelState.IsValid)
            {
                if (model.FicPro != null && model.FicPro.ContentLength > 0)
                {
                    try
                    {
                        string filename = "MP_" + CurrentUser.Societe.Replace(" ",string.Empty) + "_Produits.xlsx";
                        string filePath = Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filename);
                        model.FicPro.SaveAs(Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou), filename));   
                        dtResult = FicPro.ConvertExcelToDataTable(filePath, 1);
                        nbErrors = FicPro.checkFile(dtResult, Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou), CurrentUser.IdFou, CurrentUser.Valzn1, 1);
                        System.Threading.Thread.Sleep(1000);
                        notif.MailAdmins(CurrentUser.IdFou, CurrentUser.Societe, CurrentUser.Nom, CurrentUser.Prenom);
                        if (nbErrors > 0)
                            return RedirectToAction("EtatChargement", new { status = 4 });
                    }
                    catch (Exception e)
                    {
                        Console.Out.WriteLine(e.StackTrace);
                        return RedirectToAction("EtatChargement", new { status = -1 });
                    }
                    
                    return RedirectToAction("EtatChargement", new { status = 1 });
                }
                
            }
            return View("Index");
        }


        // Permet de traiter un fichier offres chargé par un fournisseur et créer un fichier de rapports d'erreurs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GenereFicOff(FicheProduitViewModel model)
        {
            DataTable dtResult = null;
            int nbErrors = 0;
            if (ModelState.IsValid)
            {
                if (model.FicPro != null && model.FicPro.ContentLength > 0)
                {
                    try
                    {
                        string filename = "MP_" + this.CurrentUser.Societe.Replace(" ",string.Empty) + "_Offres.xlsx";
                        string filePath = Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filename);
                        model.FicPro.SaveAs(Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou), filename));
                        // On transforme le fichier excel dans une table pour récuperer les données 
                        // le 2ème paramètre : indique que le type de fichiers offres ou produits à traiter
                        dtResult = FicPro.ConvertExcelToDataTable(filePath, 2);
                        nbErrors = this.FicPro.checkFile(dtResult, Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou), this.CurrentUser.IdFou, this.CurrentUser.Valzn1, 2);
                        System.Threading.Thread.Sleep(1000);
                        notif.MailAdmins(CurrentUser.IdFou, CurrentUser.Societe, CurrentUser.Nom, CurrentUser.Prenom);
                        if (nbErrors > 0)
                            return RedirectToAction("EtatChargement", new { status = 4 });
                    }
                    catch(Exception )
                    {
                        return RedirectToAction("EtatChargement", new { status = -1 });
                    } 
                    return RedirectToAction("EtatChargement", new { status = 2 });
                }
                
            }
            return View("Index");
        }

        // Gestion du chargement des images 
        // En entrée un fichier ZIP contenant toutes les images 
        // Dézippe le fichier pour récuperer les images et les copies dans le dossier Images du fournisseur
        public ActionResult ChargerImages(HttpPostedFileBase imagesZip)
        {
            if(ModelState.IsValid)
            {
                if(imagesZip!=null && imagesZip.ContentLength>0)
                {
                    try
                    {
                        string idfou = CurrentUser.IdFou;
                        string imgsig = CurrentUser.Valzn1;
                        string extractPath = Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Images");
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
                                e.ExtractToFile(Path.Combine(extractPath, imgsig + '-' + e.Name), true);
                                // copie dans répertoire ektas à traiter
                                System.IO.File.Copy(Path.Combine(extractPath, imgsig + '-' + e.Name), Path.Combine(@p.ektas, imgsig + '-' + e.Name), true);
                            }
                        }
                        System.IO.File.Delete(zipPath);
                        GestionErreurImage.ecrireRapport(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou), idfou);
                    }
                    catch (Exception  )
                    {
                        return RedirectToAction("EtatChargement", new { status = -1 });
                    }
                    return RedirectToAction("EtatChargement", new { status = 3 });

                }
                    
            }
            return View();
        }


        // Gestion du chargement des fiches infos PDF
        // En entrée un fichier ZIP contenant tous les documents 
        // Dézippe le fichier pour récuperer les fiches et les copies dans le dossier du fournisseur
        public ActionResult ChargerFiches(HttpPostedFileBase PdfZip)
        {
            if (ModelState.IsValid)
            {
                if (PdfZip != null && PdfZip.ContentLength > 0)
                {
                    try
                    {
                        string idfou = CurrentUser.IdFou;
                        string extractPath = Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/");
                        string zipPath = Path.Combine(extractPath, PdfZip.FileName);
                        string unzipPath = Path.Combine(extractPath, (Path.GetFileNameWithoutExtension(PdfZip.FileName)));
                        if (Directory.Exists(unzipPath)) Directory.Delete(unzipPath, true);
                        if (System.IO.File.Exists(zipPath)) System.IO.File.Delete(zipPath);
                        PdfZip.SaveAs(zipPath);
                        using (var zip = ZipFile.OpenRead(zipPath))
                        {
                            foreach (ZipArchiveEntry e in zip.Entries)
                            {
                                e.ExtractToFile(Path.Combine(extractPath, e.Name), true);
                                // copie dans répertoire ektas à traiter
                                System.IO.File.Copy(Path.Combine(extractPath, e.Name), Path.Combine(@p.ektas, e.Name), true);
                            }
                        }
                        System.IO.File.Delete(zipPath);
                        GestionErreurFiche.ecrireRapport(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou), idfou);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("EtatChargement", new { status = -1 });
                    }
                    return RedirectToAction("EtatChargement", new { status = 3 });

                }

            }
            return View();
        }

        

        // retourne les différents rapports d'erreurs 
        public FileResult RapportErreur(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string filename = p.rapportErreursProduits;
                string filepath = dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filename;
                if (id.Equals("Off"))
                {
                    filename = p.rapportErreursOffres;
                    filepath = dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filename;
                }
                else if (id.Equals("Img"))
                {
                    filename = p.rapportErreursImages;
                    filepath = dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filename;
                    GestionErreurImage.ecrireRapport(Server.MapPath(@dossiersFournisseurs +"/"+ CurrentUser.IdFou), CurrentUser.IdFou);
                }
                else if (id.Equals("Pdf"))
                {
                    filename = p.rapportErreursFiches;
                    filepath = dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filename;
                    GestionErreurFiche.ecrireRapport(Server.MapPath(@dossiersFournisseurs + "/" + CurrentUser.IdFou), CurrentUser.IdFou);
                }

                // Controle sur le cas où le fournisseur n'a pas encore soumis de fichiers 
                if (!System.IO.File.Exists(Server.MapPath(filepath)))
                {
                    System.IO.File.Copy(Server.MapPath(appData+"/"+p.rapportErreursModele), Server.MapPath(filepath) );
                    return File(Server.MapPath(filepath), "application/octet-stream", filename);
                }
                return File(Server.MapPath(filepath), "application/octet-stream", filename);
            }
            return null;
        }

        public ActionResult EtatChargement(string status)
        {
            if (string.IsNullOrEmpty(status) || string.IsNullOrWhiteSpace(status)) //&& !new string[] { "1", "2", "3" }.Contains(status))
                return View("Error");
            if (!string.IsNullOrEmpty(status) && status.Equals("4"))
            {
                ViewBag.Error = true;
                ViewBag.StatusFile = "Certains éléments n'ont pa été pris, veuillez consulter le rapport d'erreurs.";
            } 
            if (!string.IsNullOrEmpty(status) && status.Equals("3")) ViewBag.StatusFile = "Le fichier Images a été chargé avec succès";
           if (!string.IsNullOrEmpty(status) && status.Equals("2")) ViewBag.StatusFile = "Le fichier Offres a été chargé avec succès";
           if (!string.IsNullOrEmpty(status) && status.Equals("1")) ViewBag.StatusFile = "Le fichier Produits a été chargé avec Succès";
            if (!string.IsNullOrEmpty(status) && status.Equals("-1"))
            {
                ViewBag.IncorrectFile = true;
                ViewBag.StatusFile = "Le format du fichier que vous essayez de charger n'est pas correct !";
            }
            return View();
        }

        // Charger les produits directement de la BD dans un CSV 
        public FileResult chargerBDProduits() 
        {
            string filepathsource = appData+"/"+p.produitsCSV;
            string filenamedes = "MP_" + CurrentUser.Societe.Replace(" ",string.Empty) + "_ProduitsCSV.csv";
            string filePathdes = dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filenamedes;
            System.IO.File.Copy(Server.MapPath(filepathsource), Server.MapPath(filePathdes), true);
            FicPro.CreateCsvFilePro(Server.MapPath(filePathdes), CurrentUser.IdFou);
            return File(Server.MapPath(filePathdes), "text/csv", filenamedes);
        }
         
        // Charger les offres directement de la BD dans un CSV
        public FileResult chargerBDOffres()
        {
            string filepathsource = appData + "/" + p.offresCSV;
            string filenamedes = "MP_" + CurrentUser.Societe.Replace(" ",string.Empty) + "_BddOffres.csv";
            string filePathdes = dossiersFournisseurs + "/" + CurrentUser.IdFou + "/" + filenamedes;
            System.IO.File.Copy(Server.MapPath(filepathsource), Server.MapPath(filePathdes), true);
            FicPro.CreateCsvFileOff(Server.MapPath(filePathdes), CurrentUser.IdFou);
            return File(Server.MapPath(filePathdes), "text/csv", filenamedes);
        }

        public ActionResult testView()
        {

            string query = @"select p.codpro as CodPro,p.nompro as Idfou from soc1.pro p where p.codpro in (:id1, :id2)";
            IEnumerable<TestView> data = db.Database.SqlQuery<TestView>(query,new OracleParameter("id1","012804"),new OracleParameter("id2", "012809"));

            int l = data.Count();

            return View("TestExcel", data.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dbA.Dispose();
            }
            base.Dispose(disposing);
        }


    }
}
