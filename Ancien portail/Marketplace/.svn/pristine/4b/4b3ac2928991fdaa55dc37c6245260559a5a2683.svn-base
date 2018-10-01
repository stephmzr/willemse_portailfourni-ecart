using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WillemseFranceMP.Models;
using System.Net.Mail;
using System.IO;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using System.IO.Compression;
using System.Web;

namespace WillemseFranceMP.Controllers
{
    // Gestion des offres de produits
    [Authorize]
    public class OffresController : Controller
    {
        private ProduitDbOracleContext db = new ProduitDbOracleContext();
        private ApplicationDbOracleContext dbA = new ApplicationDbOracleContext();
        private Parametres p = new Parametres();
        private UtilNotifications notif = new UtilNotifications();

        // Récupère l'identité de l'utilisateur connecté 
        private ApplicationUser CurrentUser
        {
            get
            {
                string currentUserId = User.Identity.GetUserId();
                return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
            }
        }

        // GET: Offres 
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Index(int ? id)
        {
            if(id==null)
            {
                return View("Error");
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            if (!ApplicationUser.estAdmin(CurrentUser) && (produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();

            Offre offre = db.Offres.Find(id);
            if (offre ==null)
            {
                ViewBag.Id = id;
                return View(offre);
            }
            offre.Tva = FormatDonnees.transformTva(offre.Tva);
            return View("Details",offre);
        }


        // GET: Offres/Create
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Create(int ? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            if (!ApplicationUser.estAdmin(CurrentUser) && (produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            return View();
        }


        // POST: Offres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Create(int? id,CreateOffreViewModel model, string Tva)
        {
            Offre offre = new Offre();
            if (ModelState.IsValid)
            {
                offre = new Offre
                {
                    ProduitID = db.Produits.Single(p => p.ProduitID == id).ProduitID,
                    PrixAchtHT = FormatDonnees.transformeDecimal(model.PrixAchtHT),
                    PrixAchtTsp = FormatDonnees.transformeDecimal(model.PrixAchtHT),
                    PrixVtTtc = FormatDonnees.transformeDecimal(model.PrixVtTtc),
                    FraisLiv = FormatDonnees.transformeDecimal(model.FraisLiv),
                    Delailiv = model.Delailiv,
                    EcoTaxe= FormatDonnees.transformeDecimal(model.EcoTaxe),
                    Dispo = model.Dispo,
                    Tva = Tva,
                    DateCre = DateTime.Now, DateMod = DateTime.Now,FicOrForm="Oui",
                    ValdFou = "Oui", ValdWill="Non"
                };

                if(offre.PrixAchtTsp == null)
                {
                    if (!String.IsNullOrEmpty(offre.FraisLiv))
                    {
                        offre.PrixAchtTsp = System.Convert.ToString(decimal.Parse(offre.PrixAchtHT) + decimal.Parse(offre.FraisLiv));
                    }else
                    {
                        offre.PrixAchtTsp = offre.PrixAchtHT;
                    }
                }
                string[] line = { "", "", "", "", "", "", "", "", "", "", "", "", ""};
                line[0] = CurrentUser.IdFou;
                line[1] = db.Produits.Single(p => p.ProduitID == id).CodProFou;
                line[2] = db.Produits.Single(p => p.ProduitID == id).DesignationPro;
                line[3] = offre.PrixAchtHT;
                line[4] = "";
                line[5] = offre.FraisLiv;
                line[6] = offre.PrixAchtTsp;
                line[7] = "";
                line[8] = offre.PrixVtTtc;
                line[9] = offre.EcoTaxe;
                line[10] = FormatDonnees.transformTva(offre.Tva);
                line[11] = offre.Delailiv;
                line[12] = offre.Dispo;
                notif.NewOffres(line);
                db.Offres.Add(offre);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = offre.ProduitID });
            }
            return View(model);
        }

         // GET: Offres/Edit/id
         // Modification d'une offre
         public ActionResult Edit(int? id)
         {
            // Test privilèges modification
            if (ApplicationUser.estAdmin(CurrentUser) || ApplicationUser.estSuperAdmin(CurrentUser))
            {
                ViewBag.Message = "a";
                if (User.IsInRole("Super"))
                {
                    ViewBag.Message = "p";
                }
            }
            if (id == null)
             {
                 return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
             }
             Offre offre = db.Offres.Find(id);
             if (offre == null)
             {
                 return HttpNotFound();
             }
            if (!ApplicationUser.estAdmin(CurrentUser) && (offre.Produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            var model = new CreateOffreViewModel
            {
                PrixAchtHT = offre.PrixAchtHT,
                PrixVtTtc = offre.PrixVtTtc,
                PrixAchtTsp = offre.PrixAchtTsp,
                FraisLiv = offre.FraisLiv,
                Tva = offre.Tva,
                Delailiv = offre.Delailiv,
                Dispo = offre.Dispo,
                EcoTaxe = offre.EcoTaxe
            };
            return View(model);
         }

         // POST: Offres/Edit/id
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Edit(int ?id, CreateOffreViewModel model, string Tva)
         {
            Offre offre = db.Offres.Find(id);
            string[] line = {"", "", "", "", "", "", "", "", "", "", "", "", ""};
            line[0] = CurrentUser.IdFou;
            if (!ApplicationUser.estAdmin(CurrentUser) && (offre.Produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            if (ModelState.IsValid)
             {
                line[1] = offre.Produit.CodProFou;
                line[2] = offre.Produit.DesignationPro;
                line[3] = offre.PrixAchtHT;
                offre.PrixAchtHT = FormatDonnees.transformeDecimal(model.PrixAchtHT);
                line[4] = offre.PrixAchtHT;
                offre.FraisLiv = FormatDonnees.transformeDecimal(model.FraisLiv);
                line[5] = offre.FraisLiv;
                line[6] = offre.PrixAchtTsp;
                offre.PrixAchtTsp = FormatDonnees.transformeDecimal(model.PrixAchtTsp);
                line[7] = offre.PrixAchtTsp;
                offre.PrixVtTtc = FormatDonnees.transformeDecimal(model.PrixVtTtc);
                line[8] = offre.PrixVtTtc;
                offre.EcoTaxe = FormatDonnees.transformeDecimal(model.EcoTaxe);
                line[9] = offre.EcoTaxe;
                offre.Tva = Tva;
                line[10] = FormatDonnees.transformTva(offre.Tva);
                offre.Delailiv = model.Delailiv;
                line[11] = offre.Delailiv;
                if (String.Equals(offre.Dispo, "Oui") && !String.Equals(model.Dispo, offre.Dispo))
                {
                    notif.ProduitsEpuises(CurrentUser.IdFou, offre.Produit.CodProERP, offre.Produit.CodProFou, offre.Produit.DesignationPro, offre.Dispo);
                }
                offre.Dispo = model.Dispo;
                line[12] = offre.Dispo;
                offre.FicOrForm = "Oui";
                offre.DateMod = DateTime.Now;
                offre.ValdFou = "Oui";
                offre.ValdWill = "Non";
                offre.Produit.FlagExportErp = "0";
                offre.Produit.ValdWill = "Non";
                db.Entry(offre).State = EntityState.Modified;
                db.SaveChanges();
                db.Entry(offre.Produit).State = EntityState.Modified;
                db.SaveChanges();
                notif.UpdateOffres(line);
                return RedirectToAction("Index", new { id = offre.ProduitID });
            }
            ViewBag.ProduitID = new SelectList(db.Produits, "ProduitID", "IdFou", offre.ProduitID);
            
            return View(model);
        }

        // GET: Offres/Delete/id
        // Suppression d'une offre
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                return HttpNotFound();
            }
            if (!ApplicationUser.estAdmin(CurrentUser) && (offre.Produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            return View(offre);
        }

        // POST: Offres/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilisateur")]
        public ActionResult DeleteConfirmed(int id)
        {
            Offre offre = db.Offres.Find(id);
            if (!ApplicationUser.estAdmin(CurrentUser) && (offre!=null) && (offre.Produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            db.Offres.Remove(offre);
            db.SaveChanges();
            return RedirectToAction("Index", "Produits");   
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
