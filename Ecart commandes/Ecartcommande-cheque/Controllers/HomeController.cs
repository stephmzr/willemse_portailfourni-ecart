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
        public ActionResult Index(string numeroclient, string numerocommande, int? page)
        {

            EcartReglementEntities db = new EcartReglementEntities();

            var tableau = db.ecart_reglement.OrderByDescending(d => d.ER_date_piece).ToList();


            if (numeroclient != null)
            {
                tableau = db.ecart_reglement.Where(d => d.ER_Tiers == numeroclient).ToList();
                
            }
            if (numerocommande != null)
            {
                tableau = db.ecart_reglement.Where(d => d.ER_piece == numerocommande).ToList();
            }




            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(tableau.ToPagedList(pageNumber, pageSize));
            
        } 

        //[HttpPost]
        public ActionResult ActionCloturer(string commentaire, string numcommande)
        {

            EcartReglementEntities db = new EcartReglementEntities();

            var tableau = db.ecart_reglement.Where(d => d.ER_piece == numcommande && d.ER_statut != "Cloturé");



            foreach (var ligne in tableau)
            {

                ligne.ER_statut = "Cloturé";
                ligne.ER_Commentaire = commentaire;
                ligne.ER_date_piece = DateTime.Now;

            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Handle ex
            }
            return View("Index");


        }

    }
}