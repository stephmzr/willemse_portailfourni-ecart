using Ecartcommande_cheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Ecartcommande_cheque.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string numeroclient, string numeroOxatis, int? page, string sortOrder)
        {

            EcartReglementEntities db = new EcartReglementEntities();

            var tableau = db.ecart_reglement.Where(d => d.ER_statut != "Cloturé").OrderByDescending(d => d.ER_date_piece).ToList();


            if (numeroclient != null)
            {
                tableau = db.ecart_reglement.Where(d => d.ER_Tiers == numeroclient).ToList();
                
            }
            if (numeroOxatis != null)
            {
                tableau = db.ecart_reglement.Where(d => d.ER_Oxatis == numeroOxatis).ToList();
            }

            ViewBag.EcartSort = String.IsNullOrEmpty(sortOrder) ? "EcartSort" : "";


            switch (sortOrder)
            {
                case "EcartSort":
                    tableau = db.ecart_reglement.Where(d => d.ER_statut != "Cloturé").OrderByDescending(d => d.ER_ecart).ToList();
                    break;
            }


            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(tableau.ToPagedList(pageNumber, pageSize));
            
        } 

        public ActionResult ActionCloturer(string commentaire, string numcommande)
        {

            EcartReglementEntities db = new EcartReglementEntities();
            
            var tableau = db.ecart_reglement.Where(d => d.ER_piece == numcommande && d.ER_statut != "Cloturé");



            foreach (var ligne in tableau)
            {

                ligne.ER_statut = "Cloturé";
                ligne.ER_Commentaire = commentaire;
                ligne.ER_Derniere_action = DateTime.Now;

            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Handle ex
            }

            return RedirectToAction("Index");
        }

    }
}