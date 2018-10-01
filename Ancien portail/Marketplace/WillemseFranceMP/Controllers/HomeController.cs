using System;
using System.IO;
using System.Web.Mvc;
using WillemseFranceMP.Models;

namespace WillemseFranceMP.Controllers
{
    // Controller : Page générale du site internet
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            try
            {
                if (User.IsInRole("Utilisateur"))
                {
                    ViewBag.Message = "u";
                }
                return View();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        // Foire aux Questions - Voir la vue
        public ActionResult About()
        {
            ViewBag.Message = "Le portail fournisseur - Willemsefrance";

            return View();
        }

        // Page "en construction" 
        public ActionResult Loading()
        {
            ViewBag.Message = "Cette page est en cours de construction";

            return View();
        }

        // Desactiver pour l'instant - En fonction de l'ergonomie du site
        public ActionResult Contact()
        {
            ViewBag.Message = "Page de contact";

            return View();
        }



        public FileResult Download()
        {
            if (User.IsInRole("Utilisateur"))
            {
                Parametres p = new Parametres();
                string filename = "GuideFournisseur.pdf";
                return File(Path.Combine(@p.appData,filename), "application/pdf", filename);
            }
            return null;
        }


    }
}