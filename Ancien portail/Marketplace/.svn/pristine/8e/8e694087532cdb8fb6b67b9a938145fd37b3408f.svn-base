using System;
using System.Data;
using System.Data.Entity;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using PagedList;
using WillemseFranceMP.Models;
using Microsoft.AspNet.Identity;
using System.Globalization;

namespace WillemseFranceMP.Controllers
{
    public class CommandesController : Controller
    {
        private CommandeDbOracleContext db = new CommandeDbOracleContext();
        private ApplicationDbOracleContext dbA = new ApplicationDbOracleContext();
        private Parametres p = new Parametres();
        private UtilCommandes utils = new UtilCommandes();



        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // ----------------------------------------------------------------------- Vue Administrateur -----------------------------------------------------------------------//
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        // GET: Lister toutes les Commandes
        [HttpGet]
        [Authorize(Roles = "Administrateur, Super")]
        public ActionResult Index(string fou, int? page, string com, string pro, DateTime? datecm, string maj)
        {
            // vue admin/super admin
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            var commandes = db.Commandes.OrderBy(d => d.datecmnd).ToList();
            int pageSize = 50;
            // recherche
            if (fou != null)
            {
                if (!string.IsNullOrEmpty(fou) && (!string.IsNullOrWhiteSpace(fou)))
                    commandes = db.Commandes.Where(d => d.idfou == fou).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if(com != null)
            {
                if (!string.IsNullOrEmpty(com) && (!string.IsNullOrWhiteSpace(com)))
                    commandes = db.Commandes.Where(d => d.numcmnd == com).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (pro != null)
            {
                if (!string.IsNullOrEmpty(pro) && (!string.IsNullOrWhiteSpace(pro)))
                    commandes = db.Commandes.Where(d => d.codproerp == pro).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (datecm != null)
            {
                commandes = db.Commandes.Where(d => d.datecmnd >= datecm).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (pageSize == 0)
                pageSize = 1;
            int pageNumber = (page ?? 1);
            // filtrer le résultat
            if(maj != null)
            {
                if(maj == "true")
                {
                    commandes = commandes.Where(c => c.datappliv.HasValue || (!string.IsNullOrEmpty(c.tracking) && !string.IsNullOrWhiteSpace(c.tracking))).ToList();
                }
                else if(maj == "false")
                {
                    commandes = commandes.Where(c => !c.datecmnd.HasValue && (c.tracking == null || c.tracking == "")).ToList();
                }
            }
            commandes = commandes.Where(c => c.AFFICHER != "Non").ToList();
            return View(commandes.ToPagedList(pageNumber, pageSize));
        }



        // GET: Détails d'une commande
        [Authorize(Roles = "Administrateur, Super")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            return View(commande);
        }
        

        // GET: Editer une commande vue administrateur (tous les champs sont modifiables)
        [Authorize(Roles = "Administrateur, Super")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            return View(commande);
        }


        // POST: Appliquer les modifications sur les commandes
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur, Super")]
        public ActionResult Edit(Commande commande)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commande).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commande);
        }


        // GET: Commandes/Hide/5
        // Page de confirmation pour ne plus afficher une commande dans la liste vue administrateur
        [Authorize(Roles = "Administrateur, Super")]
        public ActionResult Hide(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commande commande = db.Commandes.Find(id);
            if (commande == null)
            {
                return HttpNotFound();
            }
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            return View(commande);
        }



        // POST: Commandes/Hide
        [HttpPost, ActionName("Hide")]
        [Authorize(Roles = "Administrateur, Super")]
        [ValidateAntiForgeryToken]
        public ActionResult HideConfirmed(int id)
        {
            Commande commande = db.Commandes.Find(id);
            commande.AFFICHER = "Non";
            db.Entry(commande).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------//
        // ----------------------------------------------------------------------- Vue Fournisseur---------------------------------------------------------------------------//
        // ------------------------------------------------------------------------------------------------------------------------------------------------------------------//


        // Récupérer l'identifiant du fournisseur connecté
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
        public ActionResult SuiviCommandes(int? page, int? id, string num, string pro, DateTime? datecm)
        {
            var commandes = db.Commandes.Where(d => d.idfou == CurrentUser.IdFou).OrderBy(d => d.datappliv).ToList();
            int pageSize = 50;
            if (id != null)
            {
                commandes = db.Commandes.Where(d => d.idcmnd == id && d.idfou == CurrentUser.IdFou).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (pro != null)
            {
                if (!string.IsNullOrEmpty(pro) && (!string.IsNullOrWhiteSpace(pro)))
                    commandes = db.Commandes.Where(d => d.reffou == pro && d.idfou == CurrentUser.IdFou).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (num != null)
            {
                if (!string.IsNullOrEmpty(num) && (!string.IsNullOrWhiteSpace(num)))
                    commandes = db.Commandes.Where(d => d.numcmnd == num && d.idfou == CurrentUser.IdFou).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (datecm != null)
            {
                commandes = db.Commandes.Where(d => d.datecmnd >= datecm && d.idfou == CurrentUser.IdFou).OrderBy(d => d.datecmnd).ToList();
                pageSize = commandes.Count();
            }
            if (pageSize == 0)
                pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(commandes.ToPagedList(pageNumber, pageSize));
        }


        // Afficher les détails d'une commande
        [HttpGet]
        public ActionResult SuiviCommande(int? id)
        {
            if (id != null)
            {
                var commande = db.Commandes.Where(d => d.idcmnd == id && d.idfou == CurrentUser.IdFou).SingleOrDefault();
                if (commande != null)
                    return View(commande);
                else
                {
                    ViewBag.message = "Commande indisponible.";
                    return View();
                }
            }
            return RedirectToAction("Commandes");
        }
        

        // Mise à jour du suivi de commandes : tracking, livraison, retour...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MAJCommandes(Commande com)
        {
            if (ModelState.IsValid)
            {
                var c = db.Commandes.Where(d => d.idfou == CurrentUser.IdFou && d.idcmnd == com.idcmnd).FirstOrDefault();
                string[] line = { "", "", "", "", "", "", "", "", "", "" };
                bool changed = false;
                line[0] = c.idfou;
                line[1] = c.numcmnd;
                line[2] = c.codproerp;
                if (!string.IsNullOrEmpty(com.REMARQUE))
                {
                    c.REMARQUE = com.REMARQUE;
                    changed = true;
                }
                if (!string.IsNullOrEmpty(com.NUMFACTFOU))
                {
                    c.NUMFACTFOU = com.NUMFACTFOU;
                    changed = true;
                }
                if (com.datappliv.HasValue)
                {
                    c.datappliv = com.datappliv;
                    line[3] = com.datappliv.Value.ToString("dd|MM|yyyy", CultureInfo.InvariantCulture);
                    changed = true;
                }
                if (com.datexp.HasValue)
                {
                    c.datexp = com.datexp;
                    line[4] = com.datexp.Value.ToString("dd|MM|yyyy", CultureInfo.InvariantCulture);
                    changed = true;
                }
                if (!string.IsNullOrEmpty(com.tracking))
                {
                    c.tracking = com.tracking;
                    line[5] = com.tracking;
                    changed = true;
                }
                if (com.datrec.HasValue)
                {
                    c.datrec = com.datrec;
                    line[6] = com.datrec.Value.ToString("dd|MM|yyyy", CultureInfo.InvariantCulture);
                    changed = true;
                }
                if (!string.IsNullOrEmpty(com.motifretour))
                {
                    c.motifretour = com.motifretour;
                    line[7] = com.motifretour;
                    changed = true;
                }
                if (!string.IsNullOrEmpty(com.colisretour))
                {
                    c.colisretour = com.colisretour;
                    line[8] = com.colisretour;
                    changed = true;
                }
                if (!string.IsNullOrEmpty(com.transporteur))
                {
                    c.transporteur = com.transporteur;
                    line[9] = com.transporteur;
                    changed = true;
                }
                if (!string.IsNullOrEmpty(com.ACTIONRETOUR))
                {
                    c.ACTIONRETOUR = com.ACTIONRETOUR;
                    changed = true;
                }
                if (changed)
                {
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    //WriteCommandesChanges(line);
                }
            }


            // Appel de la procédure stockée pour mise à jour du fichier de commandes en FTP
            
            System.Data.OracleClient.OracleConnection oc = new System.Data.OracleClient.OracleConnection(p.oracleConnection);
            System.Data.OracleClient.OracleCommand cmd = new System.Data.OracleClient.OracleCommand();
            cmd.Connection = oc;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "WF.PKG_WF_FLUX_MP_COMMANDES.P_WF_MAJ_FICHIERS";
            //cmd.Parameters.Add("sigfou", OracleDbType.Varchar2).Value = idfou;
            System.Data.OracleClient.OracleParameter sigfou = new System.Data.OracleClient.OracleParameter("sigfou", OracleType.VarChar);
            sigfou.IsNullable = true;
            sigfou.Value = CurrentUser.IdFou;
            sigfou.Direction = ParameterDirection.Input;
            sigfou.Size = 10;
            cmd.Parameters.Add(sigfou);
            System.Data.OracleClient.OracleParameter nomfou = new System.Data.OracleClient.OracleParameter("nomfou", OracleType.VarChar);
            nomfou.IsNullable = true;
            nomfou.Value = CurrentUser.Societe.Replace(" ","_");
            nomfou.Direction = ParameterDirection.Input;
            nomfou.Size = 50;
            cmd.Parameters.Add(nomfou);
            System.Data.OracleClient.OracleParameter login = new System.Data.OracleClient.OracleParameter("login", OracleType.VarChar);
            login.IsNullable = true;
            login.Value = CurrentUser.FTP_LOGIN;
            login.Direction = ParameterDirection.Input;
            login.Size = 20;
            cmd.Parameters.Add(login);
            System.Data.OracleClient.OracleParameter pass = new System.Data.OracleClient.OracleParameter("pass", OracleType.VarChar);
            pass.IsNullable = true;
            pass.Value = CurrentUser.FTP_PASS;
            pass.Direction = ParameterDirection.Input;
            pass.Size = 20;
            cmd.Parameters.Add(pass);
            System.Data.OracleClient.OracleParameter retour = new System.Data.OracleClient.OracleParameter("retour", OracleType.VarChar);
            retour.IsNullable = true;
            retour.Direction = ParameterDirection.Output;
            retour.Size = 5;
            cmd.Parameters.Add(retour);
            try
            {
                oc.Open();
                cmd.ExecuteNonQuery();
                if ((string)retour.Value != "0")
                    throw new Exception("La mise à jour du suivi de commande n'a pas été effectué.");
                //copier le fichier mis à jour dans le repertoire des relations clients
                string filename = "Test_Preprod_Commandes_" + CurrentUser.IdFou + "_" + CurrentUser.Societe.Replace(" ","").ToUpper() + ".csv";
                System.IO.File.Copy(Path.Combine(p.DirOutPath, filename), Path.Combine(p.suiviCDE, filename), true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception: {0}", ex.ToString());
                System.Console.WriteLine("Exception: {0}", ex.ToString());
                ViewBag.errormessage = "Erreur : " + ex.ToString();
            }
            oc.Close();
            return RedirectToAction("SuiviCommandes");
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
