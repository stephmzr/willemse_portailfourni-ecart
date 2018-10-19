using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class CommandesController : Controller
    {


        private ApplicationDbContext dbA = new ApplicationDbContext();
        private ListeCommandesEntities db = new ListeCommandesEntities();


        // Récupérer l'identité de l'utilisateur connecté
        private ApplicationUser CurrentUser
        {
            get
            {
                string currentUserId = User.Identity.GetUserId();
                return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
            }
        }

        // Afficher la liste de commandes
        [HttpGet]
        public ActionResult Index(int? page, int? id, string num, string pro, DateTime? datecm, short? type)
        {
            var commandes = db.F_DOCENTETE.Where(d => d.DO_Tiers == CurrentUser.Id && d.DO_Domaine == 1 && d.DO_Piece.Contains("CF")).OrderByDescending(d => d.DO_Date).ToList();
            int pageSize = 25;
            if (num != null)
            {
                commandes = db.F_DOCENTETE.Where(d => d.DO_Tiers == CurrentUser.Id).OrderByDescending(d => d.DO_Date).ToList();
                pageSize = commandes.Count();
            }
            if (pro != null) // recherche a partir du code produit
            {
                if (!string.IsNullOrEmpty(pro) && (!string.IsNullOrWhiteSpace(pro)))
                    commandes = db.F_DOCENTETE.Where(d => d.DO_Piece == pro && d.DO_Tiers == CurrentUser.Id).OrderByDescending(d => d.DO_Date).ToList();
                pageSize = commandes.Count();
            }

            if (datecm != null) //commandes à partir d'une date
            {
                commandes = db.F_DOCENTETE.Where(d => d.DO_Date >= datecm && d.DO_Tiers == CurrentUser.Id).OrderByDescending(d => d.DO_Date).ToList();
                pageSize = commandes.Count();
            }
            if (type != null) //recherche de l'etat d'une commande
            {
                commandes = db.F_DOCENTETE.Where(d => d.DO_Type == type && d.DO_Tiers == CurrentUser.Id).OrderByDescending(d => d.DO_Date).ToList();
                pageSize = commandes.Count();

            }

            if (pageSize == 0)
                pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(commandes.ToPagedList(pageNumber, pageSize));
        }

        //appel d'une procédure stockée pour afficher les détails d'une commande
        public static class DBCommandes
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


        public ActionResult SuiviCommandes(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            List<F_DOCLIGNE> items = DBCommandes.SuiviCommandes(id);
            
            if (items == null)
            {
                return HttpNotFound();
            }

            return View(items);

        }


        public ActionResult ActionAjoutTracking(string numcommande, string lientracking, DateTime dateExped)
        {

            string cnnString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseInfo"].ConnectionString;

            SqlConnection cnn = new SqlConnection(cnnString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cnn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "TEST_MAJ";
            cmd.Parameters.Add(new SqlParameter("@numcommande", SqlDbType.NVarChar));
            cmd.Parameters["@numcommande"].Value = numcommande;
            cmd.Parameters.Add(new SqlParameter("@lientracking", SqlDbType.NVarChar));
            cmd.Parameters["@lientracking"].Value = lientracking;
            cmd.Parameters.Add(new SqlParameter("@dateExped", SqlDbType.DateTime));
            cmd.Parameters["@dateExped"].Value = dateExped;
            cnn.Open();
            object o = cmd.ExecuteScalar();
            cnn.Close();
            return RedirectToAction("Index");

        }

    }
}
