using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebApplication6.Models;
using System.Net;
using PagedList;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace WebApplication6.Controllers
{
    [Authorize]
    public class ProduitsController : Controller
    {



        public ListeProduitsFournisseursEntities DB = new ListeProduitsFournisseursEntities(); //* table des produits fournisseurs (model : produitfournisseur )
        private ApplicationDbContext dbA = new ApplicationDbContext();
        public INFORMATIQUEEntities v = new INFORMATIQUEEntities();

        // Récupérer l'identité de l'utilisateur connecté
        private ApplicationUser CurrentUser
        {
            get
            {
                string currentUserId = User.Identity.GetUserId();
                return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
            }
        }


        // La page d'accueil du catalogue de produits (liste tous les produits d'un fournisseur)
        [HttpGet]
        public ActionResult Index(string currentFilter, string recherche, int? page, string buttonNonDispo, string buttonDispo)
        {

            if (recherche != null)
            {
                page = 1;
            }
            else
            {
                recherche = currentFilter;
            }
            ViewBag.CurrentFilter = recherche;
            var produits = v.v_MP_ArtFournisseur.Where(d => d.CT_Num == CurrentUser.Id).OrderByDescending(x => x.CT_Num).ToList();
            var filtreProduitsNonDispo = v.v_MP_ArtFournisseur.Where(d => d.CODE_STATUT == "Epuisé" && d.CT_Num == CurrentUser.Id).OrderByDescending(x => x.CT_Num).ToList();
            if (!string.IsNullOrEmpty(recherche) && !string.IsNullOrWhiteSpace(recherche))
            {
                produits = produits.Where(d => ((d.CT_Num == CurrentUser.Id) && (d.AR_Ref.Equals(recherche)))).ToList();
            }
            if (buttonNonDispo != null)
            {
                produits = v.v_MP_ArtFournisseur.Where(d => d.CODE_STATUT == "Epuisé" && d.CT_Num == CurrentUser.Id).OrderByDescending(x => x.CT_Num).ToList();

            }
            if (buttonDispo != null)
            {
                produits = v.v_MP_ArtFournisseur.Where(d => d.CODE_STATUT == "Commercialisé" && d.CT_Num == CurrentUser.Id).OrderByDescending(x => x.CT_Num).ToList();

            }

            int pageSize = 25;
            int pageNumber = (page ?? 1);
            return View(produits.ToPagedList(pageNumber, pageSize));


        }
    

        // GET: Produits/Details/id (détails d'un produit)
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
       
            F_ARTICLE item = DBArticles.GetarticleView(id);
            if (item == null)
            {
                return HttpNotFound();
            }

            return View(item);

        }

        public ActionResult FiltrerProduitsDispo()
        {
            return Redirect("Home/Index");
        }

        // appel de procédure stockée pour afficher détails d'un produit
        public static class DBArticles
        {
            public static F_ARTICLE GetarticleView(string id)
            {
                F_ARTICLE arttemp = new F_ARTICLE();
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseInfo"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "p_GetArticleByRef";
                cmd.Parameters.Add("@id", SqlDbType.NVarChar);
                cmd.Parameters["@Id"].Value = id;
                cmd.Connection = con;
                SqlDataReader ds = cmd.ExecuteReader();
                ds.Read();
                arttemp.AR_Design = ds["AR_Design"].ToString();
                arttemp.AR_Ref = ds["AR_Ref"].ToString();
                arttemp.AR_PrixAch = Decimal.Parse(ds["AR_PrixAch"].ToString());
               
                con.Close();

                return arttemp;

            }
        }

        //public ActionResult FilterProduitsDispo(int? page)

        //{
        //    var filtreProduitsNonDispo = v.v_MP_ArtFournisseur.Where(d => d.CODE_STATUT == "Epuisé" && d.CT_Num == CurrentUser.Id).OrderByDescending(x => x.CT_Num).ToList(); 

        //    int pageSize = 25;
        //    int pageNumber = (page ?? 1);
        //    return View("Index", filtreProduitsNonDispo.ToPagedList(pageNumber, pageSize));


        //}
       

    }


}


