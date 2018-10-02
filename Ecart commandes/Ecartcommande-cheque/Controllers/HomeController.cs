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

            TableEcartReglementEntities DB = new TableEcartReglementEntities();

            var tableau = DB.ecart_reglement.OrderByDescending(d => d.ER_date_piece).ToList();


            if (numeroclient != null)
            {
                tableau = DB.ecart_reglement.Where(d => d.ER_Tiers == numeroclient).ToList();
                
            }
            if (numerocommande != null)
            {
                tableau = DB.ecart_reglement.Where(d => d.ER_piece == numerocommande).ToList();
            }




            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(tableau.ToPagedList(pageNumber, pageSize));
            
        } 
 
    }
}