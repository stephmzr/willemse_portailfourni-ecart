using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using PagedList;
using System.Web.Mvc;
using WillemseFranceMP.Models;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace WillemseFranceMP.Controllers
{
    // Ce controller s'occupe de la gestion des fonctionnalités administrateur
    [Authorize(Roles = "Administrateur, Super")]
    public class AdminController : Controller
    {
        private ProduitDbOracleContext db = new ProduitDbOracleContext();
        private ApplicationDbOracleContext dbF = new ApplicationDbOracleContext();
        private Parametres p = new Parametres();
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

        // GET: Page d'accueil administrateur
        public ActionResult Index()
        {
            ViewBag.Message = "a";
            var fournisseurs = dbF.Users.Where(d => (d.Roles.Any(r => r.RoleId == "2") && d.Sigtie == "Oui")).OrderBy(d => d.Societe).ToList();
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            return View(fournisseurs);
        }

        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------Parametrage------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//


        // GET: Affichage des paramètres
        public ActionResult Parametrage()
        {
            ViewBag.Message = "a";
            //Si super admin: redirection vers page de parametrage
            if (User.IsInRole("Administrateur"))
            {
                return RedirectToAction("Index");
            }
            ViewBag.Message = "p";
            return View(p);
        }

       // POST: Modifier les paramètres de l'application
        [HttpPost]
        public ActionResult EditParams(Parametres pam)
        {
            p.Host = pam.Host;
            p.Port = pam.Port;
            p.MailUser = pam.MailUser;
            p.MailPass = pam.MailPass;
            p.MailMP = pam.MailMP;
            p.MailMpName = pam.MailMpName;
            p.FTP = pam.FTP;
            return Redirect("Parametrage");
        }

        //Download le catalogue produits
        public FileResult DownloadCatPro()
        {
            string filepath = p.appData + "/" + p.modeleProduits;
            return File(Server.MapPath(filepath), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", p.modeleProduits);
        }
        //Download le catalogue offres
        public FileResult DownloadCatOffres()
        {
            string filepath = p.appData + "/" + p.modeleOffres;
            return File(Server.MapPath(filepath), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", p.modeleOffres);
        }
        //Download le modèle de commandes
        public FileResult DownloadFichierSuiviCommandes()
        {
            string filepath = p.appData + "/" + p.modeleSuiviCommandes;
            return File(Server.MapPath(filepath), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", p.modeleSuiviCommandes);
        }
        //Upload un nouveau modèle de catalogue produits
        [HttpPost]
        public ActionResult changerCatProduits()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                p.modeleProduits = file.FileName;
                file.SaveAs(Path.Combine(Server.MapPath(p.appData), p.modeleProduits));
                return RedirectToAction("Parametrage");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        //Upload un nouveau modèle de catalogue offres
        [HttpPost]
        public ActionResult changerCatOffres()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                p.modeleOffres = file.FileName;
                file.SaveAs(Path.Combine(Server.MapPath(p.appData), p.modeleOffres));
                return RedirectToAction("Parametrage");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        //Upload un nouveau modèle de catalogue commandes
        [HttpPost]
        public ActionResult changerCatCommandes()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                p.modeleCommandes = file.FileName;
                file.SaveAs(Path.Combine(Server.MapPath(p.appData), p.modeleCommandes));
                return RedirectToAction("Parametrage");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }



        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------Administrateurs--------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//



        //GET : Gestion des comptes administrateurs
        [HttpGet]
        [Authorize(Roles = "Super")]
        public ActionResult Administrateurs(string nom)
        {
            ViewBag.Message = "a";
            if (!User.IsInRole("Super"))
            {
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(nom) && (!string.IsNullOrWhiteSpace(nom)))
            {
                return RedirectToAction("EditAdmin");
            }
            var administrateurs = dbF.Users.Where(d => (d.Roles.Any(r => r.RoleId == "1" || r.RoleId == "3"))).ToList();
            ViewBag.Message = "p";
            return View(administrateurs);
        }

        [HttpGet]
        [Authorize(Roles = "Super")]
        public ActionResult EditAdmin(String nom)
        {
            ViewBag.Message = "p";
            var administrateur = dbF.Users.Where(d => (d.Nom.Equals(nom))).FirstOrDefault();
            return View(administrateur);
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        public ActionResult SaveAdmin(String id, String nom, String prenom, String email, String valzn2)
        {
            var a = dbF.Users.Find(id);
            a.Nom = nom;
            a.Prenom = prenom;
            a.Email = email;
            a.ValZn2 = valzn2;
            dbF.Entry(a).State = EntityState.Modified;
            dbF.SaveChanges();
            return Redirect("Administrateurs");
        }
        
        public ActionResult DeleteAdmin(string email)
        {
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var administrateur = dbF.Users.Where(d => d.Email == email).SingleOrDefault();
            if (administrateur != null)
            {
                dbF.Entry(administrateur).State = EntityState.Deleted;
                dbF.SaveChanges();
            }
            return RedirectToAction("Administrateurs");
        }

        public ActionResult CreateAdmin()
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateAdmin(RegisterViewModel model, string valzn2)
        {
            var admin = new ApplicationUser
            {
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                ValZn2 = valzn2,
                UserName = model.Email,
                Telephone = model.Telephone,
                Societe = model.Societe,
                Adresse = model.Rue + model.Complement,
                CodePostal = model.CodePostal,
                Ville = model.Ville,
                Pays = model.Pays,
                Message = FormatDonnees.echapp(model.Message),
                Siret = model.Siret,
                Sigtie = "oui",
                IdFou = "admin"
            };
            var result = await UserManager.CreateAsync(admin, model.Password);
            if (result.Succeeded)
            {
                var curentUser = UserManager.FindByName(admin.UserName);
                var roleresult = UserManager.AddToRole(curentUser.Id, "Administrateur");
            }
            
            return RedirectToAction("Administrateurs");
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------Fournisseurs-----------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//


        // GET : Gestion des comptes fournisseurs
        public ActionResult ValidFouFinal(string id, string idfou)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            ApplicationUser fournisseur = new ApplicationUser();
            if (idfou == null && id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (id == null)
                fournisseur = dbF.Users.Where(d => d.IdFou == idfou).SingleOrDefault();
            else
                fournisseur = dbF.Users.Find(id);
            if (fournisseur == null)
            {
                return HttpNotFound();
            }
            return View(fournisseur);
        }

        //Editer un compte fournisseur et/ou le valider
        [HttpPost]
        [ValidateAntiForgeryToken,ActionName("ValidFouFinal")]
        public ActionResult ValidFouFinal(string id, string idfou,string sigimg, string nom, string prenom, string email, string societe, string tel, string siret, string ftplog, string ftppass)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            if (idfou == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fournisseur = dbF.Users.Find(id);
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(idfou) || string.IsNullOrWhiteSpace(idfou) )
                {
                    idfou = null;
                    return View(fournisseur);
                }

                fournisseur.Nom = nom;
                fournisseur.Prenom = prenom;
                fournisseur.Email = email;
                fournisseur.Telephone = tel;
                fournisseur.Societe = societe;
                fournisseur.Siret = siret;
                fournisseur.FTP_LOGIN = ftplog;
                fournisseur.FTP_PASS = ftppass;

                //Si première validation du compte fournisseur
                if (fournisseur.Sigtie == "Non")
                {
                    //affectation du code fournisseur, celui-ci ne devrait pas être modifié par la suite
                    fournisseur.IdFou = idfou;
                    fournisseur.Valzn1 = sigimg;

                    /// Création d'un repertoire pour le fournisseur
                    var path = Path.Combine(Server.MapPath(this.p.DossiersFournisseurs),idfou);
                    DirectoryInfo di = new DirectoryInfo(path);
                    if (di.Exists == false) di.Create();
                    DirectoryInfo im = di.CreateSubdirectory("Images");

                    // envoi du mail de validation compte fournisseur s'il vient d'être validé
                    MailMessage mailContact = new MailMessage();
                    mailContact.IsBodyHtml = true;
                    mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
                    mailContact.To.Add(fournisseur.Email);
                    mailContact.Subject = "Willemsefrance - Validation de votre compte";
                    string textmsg = "Bonjour " + "<b>" + fournisseur.Prenom + " " + fournisseur.Nom + "  ! </b><br/><br/> ";
                    textmsg += "Nous vous remercions d'avoir créé un compte fournisseur chez Willemse France.<br/>";
                    textmsg += "Votre compte a été validé. <br/>";
                    textmsg += "Retrouvez-nous très vite sur  http://market.partage-willemse.com:8888/ <br/><br/>";
                    textmsg += "L'équipe Willemse France vous adresse ses meilleures salutations. <br/><br/>";
                    mailContact.Body = textmsg;
                    var smtp = new SmtpClient
                    {
                        Host = p.Host,
                        Port = p.Port,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Timeout = 10000,
                        Credentials = new NetworkCredential(p.MailUser, p.MailPass)
                    };
                    smtp.Send(mailContact);
                }
                fournisseur.Sigtie = "Oui";
                dbF.Entry(fournisseur).State = EntityState.Modified;
                dbF.SaveChanges();

                return RedirectToAction("");
            }
            return View(fournisseur);
        }

        // Supprimer un fournisseur dans la liste d'attente de validation
        public ActionResult ValidFouDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fournisseur = dbF.Users.Where(d => d.Id == id).SingleOrDefault();
            if (fournisseur != null)
            {
                dbF.Entry(fournisseur).State = EntityState.Deleted;
                dbF.SaveChanges();
            }
            return RedirectToAction("ValidFou");
        }

        // Désactiver un compte fournisseur
        public ActionResult DesactiveFou(string idfou)
        {
            if (idfou == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fournisseur = dbF.Users.Where(d => d.IdFou == idfou).SingleOrDefault();
            if (fournisseur != null)
            {
                fournisseur.ACTIVE = "Non";
                dbF.Entry(fournisseur).State = EntityState.Modified;
                dbF.SaveChanges();

                var produits = db.Produits.Where(p => p.IdFou == fournisseur.IdFou).ToList();
                foreach(Produit p in produits)
                {
                    p.ACTIVE = "Non";
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // Désactiver un compte fournisseur
        public ActionResult ActiveFou(string idfou)
        {
            if (idfou == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fournisseur = dbF.Users.Where(d => d.IdFou == idfou).SingleOrDefault();
            if (fournisseur != null)
            {
                fournisseur.ACTIVE = "Oui";
                dbF.Entry(fournisseur).State = EntityState.Modified;
                dbF.SaveChanges();

                var produits = db.Produits.Where(p => p.IdFou == fournisseur.IdFou).ToList();
                foreach (Produit p in produits)
                {
                    p.ACTIVE = "Oui";
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        // Validation des fournisseurs en attente
        public ActionResult ValidFou()
        {
            ViewBag.Message = "a";
            var fournisseurs = dbF.Users.Where((d => ((d.Sigtie == "Non" || d.Sigtie == null) && d.Nom != "WillemseFrance")));
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            return View(fournisseurs.ToList());
        }





        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //------------------------------------------------------------------------Produits et offres---------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Modification et validation d'une offre par l'administrateur du site
        public ActionResult EditOffreAdmin(int? id,string idFou)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            ViewBag.ExisteOffre = true;
            if (!string.IsNullOrEmpty(idFou)) ViewBag.IDFOU = idFou;
            if (id == null)
            {
                 return View("Error");
            }
            Offre offre = db.Offres.Find(id);
            if (offre == null)
            {
                ViewBag.ExisteOffre = false;
                return View();
            }
            var model = new CreateOffreViewModel
            {
                PrixAchtHT = offre.PrixAchtHT,
                PrixVtTtc = offre.PrixVtTtc,
                PrixAchtTsp = offre.PrixAchtTsp,
                EcoTaxe = offre.EcoTaxe,
                FraisLiv = offre.FraisLiv,
                Tva = FormatDonnees.transformTva(offre.Tva),
                Delailiv = offre.Delailiv,
                Dispo = offre.Dispo
            };
            return View(model);
        }

        // POST: Offres/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOffreAdmin(int? id, CreateOffreViewModel model, string Tva)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            Offre offre = db.Offres.Find(id);
            if (ModelState.IsValid)
            {
                offre.PrixAchtHT = FormatDonnees.transformeDecimal(model.PrixAchtHT);
                offre.PrixVtTtc = FormatDonnees.transformeDecimal(model.PrixVtTtc);
                offre.PrixAchtTsp = FormatDonnees.transformeDecimal(model.PrixAchtTsp);
                offre.FraisLiv = FormatDonnees.transformeDecimal(model.FraisLiv);
                offre.EcoTaxe = model.EcoTaxe;
                offre.Delailiv = model.Delailiv;
                offre.Dispo = model.Dispo;
                offre.FicOrForm = "Oui";
                offre.DateMod = DateTime.Now;
                offre.Tva = Tva;
                offre.ValdFou = "Oui";
                offre.ValdWill = "Oui";
                offre.Produit.FlagExportErp = "0";
                offre.Produit.FlagExportBE = "0";
                db.Entry(offre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ValidPro", "Admin", new { recherche = offre.Produit.IdFou });
            }
            ViewBag.ProduitID = new SelectList(db.Produits, "ProduitID", "IdFou", offre.ProduitID);
            return View(model);
        }

        // Validation d'un produit par l'administrateur
        public ActionResult ValidProAdmin(int? id)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            if (id == null) return View("Error");
            Produit p = db.Produits.Find(id);
            if (p == null) return View("Error");
            p.ValdFou = "Oui";
            p.ValdWill = "Oui";
            p.FlagExportErp = "0";
            p.FlagExportBE = "0";
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ValidPro", "Admin", new { recherche = p.IdFou });
        }

        // Validation d'une offre par l'administrateur
        public ActionResult ValidOffAdmin(int? id)
        {
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            if (id == null) return View("Error");
            Offre o = db.Offres.Find(id);
            if (o == null) return View("Error");
            o.ValdFou = "Oui";
            o.ValdWill = "Oui";
            o.Produit.FlagExportErp = "0";
            o.Produit.FlagExportBE = "0";
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ValidPro", "Admin", new { recherche = o.Produit.IdFou });
        }

        // Affichage d'une offre et validation par l'administrateur du site
        public ActionResult DetailsOffreAdmin(int? id)
        {
            ViewBag.Message = "a";
            if (id == null) return View("Error");
            Produit p = db.Produits.Find(id);
            if (p == null) return View("Error");
            Offre offre = p.Offre;
            ViewBag.ExisteOffre = true;
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            if (offre != null)
            {
                offre.Tva = FormatDonnees.transformTva(offre.Tva);
                return View(offre);
            }
            ViewBag.ExisteOffre = false;
            ViewBag.idFou = p.IdFou;
            return View();
        }

        // Validation des produits
        [HttpGet]
        public ActionResult ValidPro(string currentFilter, string recherche, string recherchePro, string rechercheProErp, int? page, string valid, string nonvalid)
        {
            ViewBag.Message = "a";
            if (recherche != null)
            {
                page = 1;
            }
            else
            {
                recherche = currentFilter;
            }
            int pageSize = 100;
            ViewBag.CurrentFilter = recherche;
            var produits = db.Produits.Where(d => d.ACTIVE == "Oui").OrderBy(x => x.ValdWill).ToList(); ;
            if (!string.IsNullOrEmpty(valid) && string.IsNullOrEmpty(nonvalid))
            {
                if (valid == "true")
                {
                    produits = produits.Where(d => d.ValdWill == "Oui" && d.Offre.ValdWill == "Oui").ToList();
                }
            }
            if (!string.IsNullOrEmpty(valid) && !string.IsNullOrEmpty(nonvalid))
            {
                if (valid == "true" && nonvalid =="false")
                {
                    produits = produits.Where(d => d.ValdWill == "Oui" && d.Offre.ValdWill == "Oui").ToList();
                }
            }
            if (string.IsNullOrEmpty(valid) && !string.IsNullOrEmpty(nonvalid))
            {
                if (nonvalid == "true")
                {
                    produits = produits.Where(d => d.ValdWill == "Non" || d.Offre.ValdWill == "Non").ToList();
                }
            }
            if (!string.IsNullOrEmpty(valid) && !string.IsNullOrEmpty(nonvalid))
            {
                if (valid == "false" && nonvalid == "true")
                {
                    produits = produits.Where(d => d.ValdWill == "Non" || d.Offre.ValdWill == "Non").ToList();
                }
            }
            if (!string.IsNullOrEmpty(recherche) && (!string.IsNullOrWhiteSpace(recherche)))
            {
                produits = produits.Where(d => (d.IdFou.Equals(recherche)) && d.ACTIVE == "Oui").OrderBy(x => x.ValdWill).ToList();
                pageSize = 1000;
            }
            if (!string.IsNullOrEmpty(recherchePro) && (!string.IsNullOrWhiteSpace(recherchePro)))
            {
                produits = produits.Where(d => (d.CodProFou.Equals(recherchePro)) && d.ACTIVE == "Oui").OrderBy(x => x.ValdWill).ToList();
            }
            if (!string.IsNullOrEmpty(rechercheProErp) && (!string.IsNullOrWhiteSpace(rechercheProErp)))
            {
                produits = db.Produits.Where(d => (d.CodProERP.Equals(rechercheProErp)) && d.ACTIVE == "Oui").OrderBy(x => x.ValdWill).ToList();
            }


            int pageNumber = (page ?? 1);
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }

            // liste de fournisseurs pour afficher le nom de la société à qui appartient chaque produit
            ViewBag.fournisseurs = dbF.Users.Where(u => u.IdFou.Length == 3 && u.ACTIVE == "Oui").ToList();

            return View(produits.ToPagedList(pageNumber, pageSize));
        }

        //méthode pour valider plusieurs produits à la fois
        [HttpPost]
        public ActionResult ValidAll(string[] validPro)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            string idredirect = "";
            try
            {
                foreach (string f in validPro ?? new string[0])
                {
                    string i1 = f.Split(new string[] { "$$$" }, StringSplitOptions.None)[0];
                    string i2 = f.Split(new string[] { "$$$" }, StringSplitOptions.None)[1];
                    var p = db.Produits.Where(d => ((d.IdFou.Equals(i1)) && (d.CodProFou.Equals(i2)))).SingleOrDefault();
                    if (p != null)
                    {
                        p.ValdWill = "Oui";
                        p.FlagExportErp = "0";
                        p.FlagExportBE = "0";
                        var o = db.Offres.Where(d => d.Produit.IdFou == p.IdFou && d.Produit.CodProFou == p.CodProFou).SingleOrDefault();
                        o.ValdWill = "Oui";
                        db.Entry(o).State = EntityState.Modified;
                        db.Entry(p).State = EntityState.Modified;
                        db.SaveChanges();
                        idredirect = i1;
                    }
                }
                //si tout ok
                return RedirectToAction("ValidPro");
            }
            catch(Exception e)
            {
                throw e;
            }
        }



        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //----------------------------------------------------------------------------Export ERP-------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------------------------------------------------------------------------------//

        // Donne la liste des produits qui doivent être traités par l'ERP
        // En lien avec la base de données Oracle et les procédures stockées pour la market place
        [HttpGet]
        public ActionResult EnvoieERP(string currentFilter, string recherche, int? page)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            if (recherche != null)
            {
                page = 1;
            }
            else
            {
                recherche = currentFilter;
            }
            int pageSize = 50;
            ViewBag.CurrentFilter = recherche;
            var produits = new List<Produit>();
            if (!string.IsNullOrEmpty(recherche) && (!string.IsNullOrWhiteSpace(recherche)))
            {
                //changé flagexporterp contition à "0"
                produits = db.Produits.Where(d => (d.IdFou.Equals(recherche) && (d.ValdWill.Equals("Oui")) && (d.Offre.ValdWill.Equals("Oui")) && d.FlagExportErp.Equals("0"))).OrderBy(x => x.DateMod).ToList();
                pageSize = 1000;
            }
            int pageNumber = (page ?? 1);

            ViewBag.fournisseurs = dbF.Users.Where(u => u.IdFou.Length == 3 && u.ACTIVE == "Oui").ToList();

            return View(produits.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult EnvoieERPPost(string[] FluxERP)
        {
            ViewBag.Message = "a";
            if (User.IsInRole("Super"))
            {
                ViewBag.Message = "p";
            }
            string idredirect = "";
            foreach (string f in FluxERP ?? new string[0])
            {
                string i1 = f.Split(new string[] { "$$$" }, StringSplitOptions.None)[0];
                string i2 = f.Split(new string[] { "$$$" }, StringSplitOptions.None)[1];
                var p = db.Produits.Where(d => ((d.IdFou.Equals(i1)) && (d.CodProFou.Equals(i2)))).SingleOrDefault();
                if (p != null)
                {
                    p.FlagExportErp = "1";
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    idredirect = i1;
                }
            }
            if (!string.IsNullOrEmpty(idredirect))
            {
                string ficin = p.exportErpFile;
                string path_in = Path.Combine(Server.MapPath(p.appData), ficin);

                string path_out = Path.Combine(@p.exportErpPath, ficin);

                System.IO.File.Copy(path_in, path_out, true);
            }
            return RedirectToAction("EnvoieERP", "Admin", new { recherche = idredirect });
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dbF.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}