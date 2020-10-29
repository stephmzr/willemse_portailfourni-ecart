using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PortailFournisseur.Models;

namespace PortailFournisseur.Controllers
{


    


    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ListeCommandesEntities db = new ListeCommandesEntities();
        public ListeFournisseursEntities DB = new ListeFournisseursEntities(); //* table avec la liste des fournisseurs
        private ApplicationDbContext dbA = new ApplicationDbContext();



        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // Récupérer l'identité de l'utilisateur connecté
        private ApplicationUser CurrentUser
        {
            get
            {
                string currentUserId = User.Identity.GetUserId();
                return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
            }
        }

        // GET: Admin
        public ActionResult Index(int? page)
        {

            var fournisseur = DB.F_COMPTET.Where(d => d.CT_Num == "629" || d.CT_Num == "622" || d.CT_Num == "608" || d.CT_Num == "611" || d.CT_Num == "017" || d.CT_Num == "352" || d.CT_Num == "326" || d.CT_Num == "393" || d.CT_Num == "347" || d.CT_Num == "356" || d.CT_Num == "309" || d.CT_Num == "660" || d.CT_Num == "316" || d.CT_Num == "610" || d.CT_Num == "613" || d.CT_Num == "624" || d.CT_Num == "310" || d.CT_Num == "353" || d.CT_Num == "334" || d.CT_Num == "632" || d.CT_Num == "382").OrderBy(d => d.CT_Num).ToList();
           
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            

            if (User.IsInRole("Admin"))
            {
                return View(fournisseur.ToPagedList(pageNumber, pageSize));

            }
            else return HttpNotFound();
        }

        // Afficher la liste de commandes
        [HttpGet]
        public ActionResult SuiviCommandesAdmin(int? page, string id, string num, string pro, DateTime? datecm, short? type)
        {
            var commandes = db.F_DOCENTETE.Where(d => d.DO_Tiers == id && d.DO_Domaine == 1 /*&& d.DO_Piece.Contains("CF")*/).OrderByDescending(d => d.DO_Date).ToList();
            int pageSize = 25;
            if (pro != null) // recherche a partir du code produit
            {
                if (!string.IsNullOrEmpty(pro) && (!string.IsNullOrWhiteSpace(pro)))
                commandes = db.F_DOCENTETE.Where(d => d.DO_Piece == pro /*&& d.DO_Tiers == id*/).OrderByDescending(d => d.DO_Date).ToList();
                pageSize = commandes.Count();
            }

            if (datecm != null) //commandes à partir d'une date
            {
                commandes = db.F_DOCENTETE.Where(d => d.DO_Date >= datecm && d.DO_Tiers == id).OrderByDescending(d => d.DO_Date).ToList();
                pageSize = commandes.Count();
            }

            if (pageSize == 0)
                pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(commandes.ToPagedList(pageNumber, pageSize));
        }


        //appel d'une procédure stockée pour afficher les détails d'une commande
        public static class DBCommandesAdmin
        {
            public static List<F_DOCLIGNE> SuiviCommandes(string id)
            {
                List<F_DOCLIGNE> mlist = new List<F_DOCLIGNE>();
                SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseInfo"].ConnectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "p_GetDetailsCommande";
                cmd.Parameters.Add("@id", System.Data.SqlDbType.NVarChar);
                cmd.Parameters["@Id"].Value = id;
                cmd.Connection = con;
                SqlDataReader ds = cmd.ExecuteReader();
                while (ds.Read())
                {
                    F_DOCLIGNE arttemp = new F_DOCLIGNE();


                    arttemp.AF_RefFourniss = ds["AF_RefFourniss"].ToString();
                    arttemp.DL_Design = ds["DL_Design"].ToString();
                    //arttemp.PRIX_VENTE_TTC = Decimal.Parse(ds["PRIX_VENTE_TTC"].ToString()==""?"0": ds["PRIX_VENTE_TTC"].ToString());
                    arttemp.DL_MontantHT = Decimal.Parse(ds["DL_MontantHT"].ToString());
                    arttemp.DL_PrixUnitaire = Decimal.Parse(ds["DL_PrixUnitaire"].ToString());
                    arttemp.EU_Qte = Decimal.Parse(ds["EU_Qte"].ToString());
                    arttemp.EU_Enumere = (ds["EU_Enumere"].ToString());



                    mlist.Add(arttemp);

                }
                con.Close();
                return mlist;
            }
        }


        public ActionResult DetailsCommandeAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<F_DOCLIGNE> items = DBCommandesAdmin.SuiviCommandes(id);

            if (items == null)
            {
                return HttpNotFound();
            }

            return View(items);

        }

        public ActionResult NouveauProduit(string id)
        {
            return View();
        }



    }
}
