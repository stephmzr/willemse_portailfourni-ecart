using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WillemseFranceMP.Models;

namespace WillemseFranceMP.Controllers
{
    
    // Ce controller gère les différentes fonctionnalités produits
    [Authorize]
    public  class ProduitsController : Controller
    {
        private ProduitDbOracleContext db = new ProduitDbOracleContext();
        private ApplicationDbOracleContext dbA = new ApplicationDbOracleContext();
        private CommandeDbOracleContext dbc = new CommandeDbOracleContext();
        private Parametres p = new Parametres();
        private UtilNotifications notif = new UtilNotifications();
        private string dossiersFournisseurs, ektas;

        public ProduitsController()
        {
            this.dossiersFournisseurs = p.DossiersFournisseurs;
            this.ektas = p.ektas;
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


        // La page d'accueil du catalogue de produits (liste tous les produits d'un fournisseur)
        [HttpGet]
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Index(string currentFilter,string recherche,int ? page)
        {
            if (ApplicationUser.estEnAttente(CurrentUser)) 
                return RedirectToAction("Attente");
            if (recherche != null)
            {
                page = 1;
            }
            else
            {
                recherche = currentFilter;
            }
            ViewBag.CurrentFilter = recherche;
            var produits = db.Produits.Where(d => d.IdFou == CurrentUser.IdFou && d.ACTIVE == "Oui").OrderByDescending(x => x.DateMod).ToList();
            if (!string.IsNullOrEmpty(recherche)&&!string.IsNullOrWhiteSpace(recherche))
            {
                produits = produits.Where(d => ((d.IdFou == CurrentUser.IdFou) && (d.CodProFou.Equals(recherche)))).ToList() ;
            }
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(produits.ToPagedList(pageNumber,pageSize));
        }


        // Page en attente de validation du compte par un administrateur
        [HttpGet]
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Attente()
        {
             return View("AttenteValidation");
        }


        // GET: Produits/Details/id (détails d'un produit)
        public ActionResult Details(int? id)
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
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            // Règles de sécurité : eviter qu'un fournisseur ne consulte des produits qui lui appartiennent pas
            if (!ApplicationUser.estAdmin(CurrentUser) && !ApplicationUser.estSuperAdmin(CurrentUser) && (produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();

            produit.CatArborIn = Arb4.libelleArbor(produit.CodeSecteur);
            produit.PerPlant = FormatDonnees.getCheckboxValues(produit.PerPlant, 1);
            produit.PerFlo = FormatDonnees.getCheckboxValues(produit.PerFlo, 1);
            produit.PerSemis = FormatDonnees.getCheckboxValues(produit.PerSemis, 1);
            produit.PerRecolte = FormatDonnees.getCheckboxValues(produit.PerRecolte, 1);
            produit.PerLiv = FormatDonnees.getCheckboxValues(produit.PerLiv, 1);
            produit.TypSol = FormatDonnees.getCheckboxValues(produit.TypSol, 2);
            produit.Exposition = FormatDonnees.getCheckboxValues(produit.Exposition,3);
            produit.TypUtil = FormatDonnees.getCheckboxValues(produit.TypUtil, 4);
            ViewBag.TYPUSER = ApplicationUser.estAdmin(CurrentUser);
            return View(produit);
        }


        // Gère les listes déroulantes dynamiques de l'arborescence produit
        // Niveau 1
        public ActionResult Arb1List()
        {
            IQueryable arb1 = Arb1.GetArb1();
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new SelectList(
                                arb1,
                                "Arb1Code",
                                "Arb1Name"), JsonRequestBehavior.AllowGet
                                );

                }              
            return View(arb1);         
        }
        // Niveau 2
        public ActionResult Arb2List(string Arb1Code)
        {
            IQueryable arb2 = Arb2.GetArb2(Arb1Code.Replace("xxx", "&"));
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new SelectList(arb2,"Arb2ID","Arb2Name"), JsonRequestBehavior.AllowGet);
                }
            }
            return View(arb2);
        }
         // Niveau 3
        public ActionResult Arb3List(string Arb1Code,  string Arb2Code)
        {
            IQueryable arb3 = Arb3.GetArb3(Arb1Code.Replace("xxx", "&"), Arb2Code.Replace("xxx","&"));
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new SelectList(arb3, "Arb3ID", "Arb3Name"), JsonRequestBehavior.AllowGet);
                }

            }
            return View(arb3);
        }
        // Niveau 4
        public ActionResult Arb4List(string Arb1Code, string Arb2Code, string Arb3Code)
        {
            IQueryable arb4 = Arb4.GetArb4(Arb1Code.Replace("xxx", "&"), Arb2Code.Replace("xxx", "&"), Arb3Code.Replace("xxx", "&"));
            {
                if (HttpContext.Request.IsAjaxRequest())
                {
                    return Json(new SelectList(arb4, "Arb4ID", "Arb4Name"), JsonRequestBehavior.AllowGet);
                }
            }
            return View(arb4);
        }
        
        // Formulaire de création d'un nouveau produit 
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Create()
        {
            if (ApplicationUser.estEnAttente(CurrentUser))
                return RedirectToAction("Attente");
            return View();
        }

        // POST: Produits/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Create(CreateProduitViewModel model,string[] Perplant,string[] Perflo,string[] Perrec, string[] Persem, string[] Perliv, string[] Expo, string[] Typutil, string[] Typsol) {
            Produit produit;       
            if (ModelState.IsValid)
            {
                string idfou = CurrentUser.IdFou;
                string sigimg = CurrentUser.Valzn1;
                // Test de l'unicité du code produit fournisseur pour chaque fournisseur 
                produit = db.Produits.SingleOrDefault(u => (u.IdFou == idfou && u.CodProFou == model.CodProFou));
                if (produit == null)
                {
                    produit = new Produit();
                }
                else
                {
                    ModelState.AddModelError("CodProFou", "Vous disposez déjà d'un produit avec le code fournisseur : " + model.CodProFou);
                    return View(model);
                }
                string fich1 = null; string fich2 = null; string fich3 = null; string fich4 = null; string fich5 = null; string fich6 = null;
                string fichpdf = null;
                string codeSect = "0";
                NewArbor sect = new NewArbor();
                string sect1 = model.Arb1;
                string sect2 = model.Arb2;
                DirectoryInfo res = new DirectoryInfo(@ektas); // l'image est  copiée dans \\Rho pour traitement automatique
                if (string.IsNullOrEmpty(sect1) || string.IsNullOrWhiteSpace(sect1))
                {
                    sect1 = sect2 = "";
                }
                if (string.IsNullOrEmpty(sect2) || string.IsNullOrWhiteSpace(sect2))
                {
                    sect2 = "";
                }
                sect = db.NewArbors.FirstOrDefault(u => (u.Secteur == sect1 && u.SousSecteur == sect2));
                if (sect != null)
                {
                    codeSect = sect.CodeSecteur;
                }
                else
                {
                    codeSect = "-1";
                }
                string pplant = null; string pflo = null; string psemis=null; string prec = null; string pliv = null;  string typsol = null;
                string expo = null; string typutil = null;
                if (model.ImgPrinc != null && model.ImgPrinc.ContentLength>0)
                {
                    if(!String.IsNullOrEmpty(sigimg))
                        fich1 = sigimg + "-" + Path.GetFileName(model.ImgPrinc.FileName) ;
                    else
                        fich1 = Path.GetFileName(model.ImgPrinc.FileName);
                    var path_f1 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich1);
                    model.ImgPrinc.SaveAs(path_f1);
                    System.IO.File.Copy(path_f1, Path.Combine(@ektas, fich1), true);
                }
                if (model.ImgSecond1 != null && model.ImgSecond1.ContentLength > 0)
                {
                    if (!String.IsNullOrEmpty(sigimg))
                        fich2 = sigimg + "-" + Path.GetFileName(model.ImgSecond1.FileName);
                    else
                        fich2 = Path.GetFileName(model.ImgSecond1.FileName);
                    var path_f2 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich2);
                    model.ImgSecond1.SaveAs(path_f2);
                    System.IO.File.Copy(path_f2, Path.Combine(@ektas, fich2), true);

                }
                if (model.ImgSecond2 != null && model.ImgSecond2.ContentLength > 0)
                {
                    if (!String.IsNullOrEmpty(sigimg))
                        fich3 = sigimg + "-"  + Path.GetFileName(model.ImgSecond2.FileName);
                    else
                        fich3 = Path.GetFileName(model.ImgSecond2.FileName);
                    var path_f3 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich3);
                    model.ImgSecond2.SaveAs(path_f3);
                    System.IO.File.Copy(path_f3, Path.Combine(@ektas, fich3), true);
                }
                if (model.ImgSecond3 != null && model.ImgSecond3.ContentLength > 0)
                {
                    if (!String.IsNullOrEmpty(sigimg))
                        fich4 = sigimg + "-" + Path.GetFileName(model.ImgSecond3.FileName);
                    else
                        fich4 = Path.GetFileName(model.ImgSecond3.FileName);
                    var path_f4 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich4);
                    model.ImgSecond3.SaveAs(path_f4);
                    System.IO.File.Copy(path_f4, Path.Combine(@ektas, fich4), true);
                }
                if (model.ImgSecond4 != null && model.ImgSecond4.ContentLength > 0)
                {
                    if (!String.IsNullOrEmpty(sigimg))
                        fich5 = sigimg + "-" + Path.GetFileName(model.ImgSecond4.FileName);
                    else
                        fich5 = Path.GetFileName(model.ImgSecond4.FileName);
                    var path_f5 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich5);
                    model.ImgSecond4.SaveAs(path_f5);
                    System.IO.File.Copy(path_f5, Path.Combine(@ektas, fich5), true);
                }
                if (model.ImgSecond5 != null && model.ImgSecond5.ContentLength > 0)
                {
                    if (!String.IsNullOrEmpty(sigimg))
                        fich6 = sigimg + "-" + Path.GetFileName(model.ImgSecond5.FileName);
                    else
                        fich6 = Path.GetFileName(model.ImgSecond5.FileName);
                    var path_f6 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich6);
                    model.ImgSecond5.SaveAs(path_f6);
                    System.IO.File.Copy(path_f6, Path.Combine(@ektas, fich6), true);
                }
                if (model.FichePDF != null && model.FichePDF.ContentLength > 0)
                {
                        fichpdf = Path.GetFileName(model.FichePDF.FileName);
                        var path_pdf = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/"), fichpdf);
                        model.FichePDF.SaveAs(path_pdf);
                        System.IO.File.Copy(path_pdf, Path.Combine(@"\\rho\EKTAS\Vrac\Fiches infos\PDF", fichpdf), true);
                }
                if(Perplant !=null)
                {
                    pplant = string.Join("-", Perplant.ToArray());
                }
                if (Perflo != null)
                {
                    pflo = string.Join("-", Perflo.ToArray());
                }
                if (Perrec != null)
                {
                    prec = string.Join("-", Perrec.ToArray());
                }
                if (Perliv != null)
                {
                    pliv = string.Join("-", Perliv.ToArray());
                }
                if (Typsol != null)
                {
                    typsol = string.Join("-", Typsol.ToArray());
                }
                
                if (Typutil != null)
                {
                    typutil = string.Join("-", Typutil.ToArray());
                }
                if (Expo != null)
                {
                    expo = string.Join("-", Expo.ToArray());
                }
                if (Persem != null)
                {
                    psemis = string.Join("-", Persem.ToArray());
                }               
                produit = new Produit
                {
                    IdFou = idfou, CodProFou = model.CodProFou, DesignationPro = model.DesignationPro, LibBonLiv = model.LibBonLiv, DescPro = FormatDonnees.echapp(model.DescPro),
                    DureeGarantie = model.DureeGarantie, CodeSecteur = codeSect, Slogan = model.Slogan, QuaLiv = model.QuaLiv, Couleur = model.Couleur,
                    EAN = model.EAN, Marque = model.Marque, DesignationLat = model.DesignationLat,
                    DFO = model.DFO, FicOrForm = "Oui",DateCre=DateTime.Now,DateMod=DateTime.Now,
                    NbrePcsPaq= model.NbrePcsPaq.ToString(),Hauteur= model.Hauteur,PlusProd1= model.PlusProd1,PlusProd2= model.PlusProd2,PlusProd3=model.PlusProd3,
                    ImgPrinc=fich1,ImgSecond1=fich2,ImgSecond2=fich3,ImgSecond3=fich4,ImgSecond4=fich5,ImgSecond5=fich6,FichePDF=fichpdf,LienYoutube=model.LienYoutube,
                    ValdFou="Oui",ValdWill="Non", FlagExportErp = "0",
                    PerPlant = pplant,PerFlo=pflo,PerSemis=psemis,PerLiv=pliv,Exposition=expo,TypUtil=typutil,TypSol=typsol,PerRecolte=prec,ACTIVE="Oui"
                };

                string[] line = { "", "", ""};
                line[0] = idfou;
                line[1] = produit.CodProFou;
                line[2] = produit.DesignationPro;
                notif.NewProduits(line);

                db.Produits.Add(produit);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(model);
        }
       

        // GET: Produits/Edit/id
        // Formulaire de modification d'un produit
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
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            if (!ApplicationUser.estAdmin(CurrentUser) && !ApplicationUser.estSuperAdmin(CurrentUser) && (produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            var model = new EditProduitViewModel
            {
                CodProFou = produit.CodProFou,
                EAN = produit.EAN,
                DesignationPro = produit.DesignationPro,
                DesignationLat = produit.DesignationLat,
                LibBonLiv = produit.LibBonLiv,
                DescPro = produit.DescPro,
                DureeGarantie = produit.DureeGarantie,
                Slogan = produit.Slogan,
                QuaLiv = produit.QuaLiv,
                Couleur = produit.Couleur,
                Hauteur = produit.Hauteur,
                PlusProd1 = produit.PlusProd1,
                PlusProd2 = produit.PlusProd2,
                PlusProd3 = produit.PlusProd3,
                LienYoutube = produit.LienYoutube,
                Marque = produit.Marque,
                ActualArbor = Arb4.libelleArbor(produit.CodeSecteur),
                //checkboxes
                PerPlant = produit.PerPlant !=null ? produit.PerPlant : "",
                PerFlo = produit.PerFlo !=null ? produit.PerFlo : "",
                TypUtil = produit.TypUtil !=null ? produit.TypUtil : "",
                PerSemis = produit.PerSemis != null ? produit.PerSemis : "",
                PerLiv = produit.PerLiv != null ? produit.PerLiv : "",
                TypSol = produit.TypSol != null  ? produit.TypSol : "",
                Exposition = produit.Exposition != null ? produit.Exposition : "",
                PerRecolte = produit.PerRecolte != null ? produit.PerRecolte : ""
            };
            if (string.IsNullOrEmpty(produit.NbrePcsPaq) || string.IsNullOrWhiteSpace(produit.NbrePcsPaq))
            {
                model.NbrePcsPaq = 0;
            }
            else
            {
                model.NbrePcsPaq = int.Parse(produit.NbrePcsPaq);
            }

           
            return View(model);
        }

        // POST: Produits/Edit/id (appliquer et enregistrer les modifications)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int ? id,EditProduitViewModel model, string[] Perplant, string[] Perflo, string[] Perrec, string[] Persem, string[] Perliv, string[] Expo, string[] Typutil, string[] Typsol)
        {
            Produit produit = db.Produits.Find(id);
            if (!ApplicationUser.estAdmin(CurrentUser) && !ApplicationUser.estSuperAdmin(CurrentUser) && (produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            if (ModelState.IsValid)
            {
                string[] line = { "", "", "", ""};
                string idfou = CurrentUser.IdFou;
                line[0] = idfou;
                string sigimg = CurrentUser.Valzn1;
                string fich1 = produit.ImgPrinc ; string fich2 = produit.ImgSecond1; string fich3 = produit.ImgSecond2; string fich4 = produit.ImgSecond3;
                string fich5 = produit.ImgSecond4; string fich6 = produit.ImgSecond5; string fichpdf = produit.FichePDF;
                string codeSect = produit.CodeSecteur;
                NewArbor sect = new NewArbor();
                string arb1 = model.Arb1; string arb2 = model.Arb2;
                string pplant = produit.PerPlant; string pflo = produit.PerFlo; string psemis = produit.PerSemis; string prec = produit.PerRecolte;
                string pliv = produit.PerLiv; string typsol = produit.TypSol;string expo = produit.Exposition; string typutil = produit.TypUtil;       
                if (string.IsNullOrEmpty(arb1) || string.IsNullOrWhiteSpace(arb1))
                {
                    arb1 = arb2 = "";
                }
                if (string.IsNullOrEmpty(arb2) || string.IsNullOrWhiteSpace(arb2))
                {
                    arb2 = "";
                }
                sect = db.NewArbors.FirstOrDefault(u => (u.Secteur == arb1 && u.SousSecteur == arb2));
                if (sect != null)
                {
                    codeSect = sect.CodeSecteur;
                }
                else
                {
                    codeSect = produit.CodeSecteur;
                }
                if (model.ImgPrinc != null && model.ImgPrinc.ContentLength > 0)
                {
                    fich1 = sigimg + "-" + Path.GetFileName(model.ImgPrinc.FileName);
                    var path_f1 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich1);
                    model.ImgPrinc.SaveAs(path_f1);
                    System.IO.File.Copy(path_f1, Path.Combine(@ektas, fich1), true);
                }
                if (model.ImgSecond1 != null && model.ImgSecond1.ContentLength > 0)
                {
                    fich2 = sigimg + "-" + Path.GetFileName(model.ImgSecond1.FileName);
                    var path_f2 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich2);
                    model.ImgSecond1.SaveAs(path_f2);
                    System.IO.File.Copy(path_f2, Path.Combine(@ektas, fich2), true);

                }
                if (model.ImgSecond2 != null && model.ImgSecond2.ContentLength > 0)
                {
                    fich3 = sigimg + "-" + Path.GetFileName(model.ImgSecond2.FileName);
                    var path_f3 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich3);
                    model.ImgSecond2.SaveAs(path_f3);
                    System.IO.File.Copy(path_f3, Path.Combine(@ektas, fich3), true);
                }
                if (model.ImgSecond3 != null && model.ImgSecond3.ContentLength > 0)
                {
                    fich4 = sigimg + "-" + Path.GetFileName(model.ImgSecond3.FileName);
                    var path_f4 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich4);
                    model.ImgSecond3.SaveAs(path_f4);
                    System.IO.File.Copy(path_f4, Path.Combine(@ektas, fich4), true);
                }
                if (model.ImgSecond4 != null && model.ImgSecond4.ContentLength > 0)
                {
                    fich5 = sigimg + "-" + Path.GetFileName(model.ImgSecond4.FileName);
                    var path_f5 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich5);
                    model.ImgSecond4.SaveAs(path_f5);
                    System.IO.File.Copy(path_f5, Path.Combine(@ektas, fich5), true);
                }
                if (model.ImgSecond5 != null && model.ImgSecond5.ContentLength > 0)
                {
                    fich6 = sigimg + "-" + Path.GetFileName(model.ImgSecond5.FileName);
                    var path_f6 = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/Images"), fich6);
                    model.ImgSecond5.SaveAs(path_f6);
                    System.IO.File.Copy(path_f6, Path.Combine(@ektas, fich6), true);
                }
                if (model.FichePDF != null && model.FichePDF.ContentLength > 0)
                {
                    fichpdf = Path.GetFileName(model.FichePDF.FileName);
                    var path_pdf = Path.Combine(Server.MapPath(dossiersFournisseurs + "/" + idfou + "/"), fichpdf);
                    model.FichePDF.SaveAs(path_pdf);
                    System.IO.File.Copy(path_pdf, Path.Combine(@"\\rho\EKTAS\Vrac\Fiches infos\PDF", fichpdf), true);
                }
                if (Perplant != null)
                {
                    pplant = string.Join("-", Perplant.ToArray());
                }
                if (Perflo != null)
                {
                    pflo = string.Join("-", Perflo.ToArray());
                }
                if (Perrec != null)
                {
                    prec = string.Join("-", Perrec.ToArray());
                }
                if (Perliv != null)
                {
                    pliv = string.Join("-", Perliv.ToArray());
                }
                if (Typsol != null)
                {
                    typsol = string.Join("-", Typsol.ToArray());
                }

                if (Typutil != null)
                {
                    typutil = string.Join("-", Typutil.ToArray());
                }
                if (Expo != null)
                {
                    expo = string.Join("-", Expo.ToArray());
                }
                if (Persem != null)
                {
                    psemis = string.Join("-", Persem.ToArray());
                }


                line[1] = produit.CodProFou;
                line[2] = produit.DesignationPro;

                if (!String.Equals(produit.DesignationPro, model.DesignationPro))
                    line[3] += " - Designation produit";
                produit.DesignationPro = model.DesignationPro;

                if (!String.Equals(produit.DesignationLat, model.DesignationLat))
                    line[3] += " - Designation latine";
                produit.DesignationLat = model.DesignationLat;

                if (!String.Equals(produit.EAN, model.EAN))
                    line[3] += " - EAN";
                produit.EAN = model.EAN;

                if (!String.Equals(produit.LibBonLiv, model.LibBonLiv))
                    line[3] += " - Libelle bon livraison";
                produit.LibBonLiv = model.LibBonLiv;

                if (!String.Equals(produit.DescPro, model.DescPro))
                    line[3] += " - Description du produit";
                produit.DescPro = FormatDonnees.echapp(model.DescPro);

                if (!String.Equals(produit.DureeGarantie, model.DureeGarantie))
                    line[3] += " - Duree garantie";
                produit.DureeGarantie = model.DureeGarantie;

                if (!String.Equals(produit.CodeSecteur, codeSect))
                    line[3] += " - Secteur";
                produit.CodeSecteur = codeSect;

                if (!String.Equals(produit.Slogan, model.Slogan))
                    line[3] += " - Slogan";
                produit.Slogan = model.Slogan;

                if (!String.Equals(produit.QuaLiv, model.QuaLiv))
                    line[3] += " - Qualitee livree";
                produit.QuaLiv = model.QuaLiv;

                if (!String.Equals(produit.Couleur, model.Couleur))
                    line[3] += " - Couleur";
                produit.Couleur = model.Couleur;

                if (!String.Equals(produit.DFO, model.DFO))
                    line[3] += " - Type de livraison";
                produit.DFO = model.DFO;

                produit.FicOrForm = "Oui";
                produit.DateMod = DateTime.Now;

                if (!String.Equals(produit.NbrePcsPaq, model.NbrePcsPaq))
                    line[3] += " - Nombre de pieces par paquet";
                produit.NbrePcsPaq = model.NbrePcsPaq.ToString();

                if (!String.Equals(produit.Hauteur, model.Hauteur))
                    line[3] += " - Dimension";
                produit.Hauteur = model.Hauteur;

                if (!String.Equals(produit.PlusProd1, model.PlusProd1))
                    line[3] += " - Les plus du produit 1";
                produit.PlusProd1 = model.PlusProd1;

                if (!String.Equals(produit.PlusProd2, model.PlusProd2))
                    line[3] += " - Les plus du produit 2";
                produit.PlusProd2 = model.PlusProd2;

                if (!String.Equals(produit.PlusProd3, model.PlusProd3))
                    line[3] += " - Les plus du produit 3";
                produit.PlusProd3 = model.PlusProd3;

                if (!String.Equals(produit.ImgPrinc, model.ImgPrinc))
                    line[3] += " - Image principale";
                produit.ImgPrinc = fich1;

                if (!String.Equals(produit.ImgSecond1, fich2))
                    line[3] += " - Image secondaire 1";
                produit.ImgSecond1 = fich2;

                if (!String.Equals(produit.ImgSecond2, fich3))
                    line[3] += " - Image secondaire 2";
                produit.ImgSecond2 = fich3;

                if (!String.Equals(produit.ImgSecond3, fich4))
                    line[3] += " - Image secondaire 3";
                produit.ImgSecond3 = fich4;

                if (!String.Equals(produit.ImgSecond4, fich5))
                    line[3] += " - Image secondaire 4";
                produit.ImgSecond4 = fich5;

                if (!String.Equals(produit.ImgSecond5, fich6))
                    line[3] += " - Image secondaire 5";
                produit.ImgSecond5 = fich6;

                if (!String.Equals(produit.FichePDF, fichpdf))
                    line[3] += " - Fiche PDF";
                produit.FichePDF = fichpdf;

                if (!String.Equals(produit.LienYoutube, model.LienYoutube))
                    line[3] += " - Lien Youtube";
                produit.LienYoutube = model.LienYoutube;

                if (!String.Equals(produit.Marque, model.Marque))
                    line[3] += " - Marque";
                produit.Marque = model.Marque;


                produit.ValdFou = "Oui";
                produit.ValdWill = "Non";
                produit.FlagExportErp = "0";

                if (!String.Equals(produit.PerPlant, pplant))
                    line[3] += " - Periode plantation";
                produit.PerPlant = pplant;

                if (!String.Equals(produit.PerFlo, pflo))
                    line[3] += " - Periode floraison";
                produit.PerFlo = pflo;

                if (!String.Equals(produit.PerSemis, psemis))
                    line[3] += " - Periode semis";
                produit.PerSemis = psemis;

                if (!String.Equals(produit.PerLiv, pliv))
                    line[3] += " - Periode livraison";
                produit.PerLiv = pliv;

                if (!String.Equals(produit.Exposition, expo))
                    line[3] += " - Exposition";
                produit.Exposition = expo;

                if (!String.Equals(produit.TypUtil, typutil))
                    line[3] += " - Type d'utilisation";
                produit.TypUtil = typutil;

                if (!String.Equals(produit.TypSol, typsol))
                    line[3] += " - Type de sol";
                produit.TypSol = typsol;

                if (!String.Equals(produit.PerRecolte, prec))
                    line[3] += " - Periode de recolte";
                produit.PerRecolte = prec;
                
                // Test de privilèges pour validation directe des modifications
                if (ApplicationUser.estAdmin(CurrentUser) || ApplicationUser.estSuperAdmin(CurrentUser)) 
                {
                    ViewBag.Message = "a";
                    if (User.IsInRole("Super"))
                    {
                        ViewBag.Message = "p";
                    }
                    produit.ValdWill = "Oui";
                    db.Entry(produit).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ValidPro", "Admin", new { recherche = produit.IdFou });                  
                }
                else
                {
                    notif.UpdateProduits(line);
                    if (produit.Offre != null) produit.Offre.ValdWill = "Non"; produit.FlagExportErp = "0";
                }
                db.Entry(produit).State = EntityState.Modified;
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Suppression des produits
        // Le produit n'est pas supprimé de la base de données, mais il ne sera plus affiché dans le portail
        // GET: Produits/Delete/id
        [Authorize(Roles = "Utilisateur")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produit produit = db.Produits.Find(id);
            if (produit == null)
            {
                return HttpNotFound();
            }
            if (!ApplicationUser.estAdmin(CurrentUser) && !ApplicationUser.estSuperAdmin(CurrentUser) && (produit.IdFou != CurrentUser.IdFou))
                return HttpNotFound();
            return View(produit);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Utilisateur")]
        public ActionResult DeleteConfirmed(int id)
        {
            string idfou = CurrentUser.IdFou;
            Offre offre = db.Offres.Find(id);
            Produit produit = db.Produits.Find(id);
            produit.ACTIVE = "Non"; // Changer en "Oui" dans la table wf.mp_produits pour réafficher le produit
            produit.Offre.Dispo = "Epuisé/Supprimé";
            db.Entry(produit).State = EntityState.Modified;
            db.SaveChanges();
            notif.DeleteProduits(CurrentUser.IdFou, produit.CodProERP, produit.CodProFou, produit.DesignationPro);
            return RedirectToAction("Index");
        } 

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                dbA.Dispose();
            }
            base.Dispose(disposing);
        }
        
        //Envoi d'un email aux administrateurs avec un rapport sur les dernières modifications
        public void Validate()
        {
            try
            {
                notif.MailAdmins(CurrentUser.IdFou, CurrentUser.Societe, CurrentUser.Nom, CurrentUser.Prenom);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught while sending mail ",
                  ex.ToString());
            }
        }


        // Lorsque le fournisseur effectue des modifications dans le portail, les modifications sont ajoutées dans des fichiers au fur et à mesure, ceux-ci sont envoyés 
        // aux administrateur quand le fournisseur clique sur "demander la validation des modifications", puis supprimés du répertoire du fournisseur,
        // ceci dans le but d'avoir une seule notification pour l'ensemble des modifications au lieu d'un email pour chaque produit modifié.
        public bool ChangeOccured()
        {
            if(System.IO.File.Exists(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Modifications_Offres_" + CurrentUser.IdFou + ".csv"))
                || System.IO.File.Exists(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Modifications_Produits_" + CurrentUser.IdFou + ".csv"))
                || System.IO.File.Exists(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Nouveaux_Produits_" + CurrentUser.IdFou + ".csv"))
                || System.IO.File.Exists(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Nouvelles_Offres_" + CurrentUser.IdFou + ".csv"))
                || System.IO.File.Exists(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Dispo_Produits_" + CurrentUser.IdFou + ".csv"))
                || System.IO.File.Exists(Server.MapPath(dossiersFournisseurs + "/" + CurrentUser.IdFou + "/Suppression_Produits_" + CurrentUser.IdFou + ".csv")))
                return true;
            return false;
        }
              
    }
}