using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebApplication6.Models;
using System.Text;

public class BonLivraisonPDFController : Controller
{

    private ApplicationDbContext dbA = new ApplicationDbContext();
    private ListeCommandesEntities DB = new ListeCommandesEntities();


    private ApplicationUser CurrentUser
    {
        get
        {
            string currentUserId = User.Identity.GetUserId();
            return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
        }
    }


    public ActionResult BonLivraisonPDF(string id)
    {

          string pdfTemplate = @"C:\TPL\templateachat.pdf";
        //  string pdfTemplate = @"C:\Users\SM\Desktop\templateachat.pdf";
        string newFile = @"C:\BL\" + id + ".pdf";


        PdfReader pdfReader = new PdfReader(pdfTemplate);



        if (System.IO.File.Exists(newFile))
        {

            return File(newFile, "application /pdf");

        }

        else
        {
            ListeCommandesEntities DB = new ListeCommandesEntities();
            ListeFournisseursEntities DBF = new ListeFournisseursEntities();
      
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.CreateNew, FileAccess.ReadWrite));
                

            var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Piece == id && d.DO_Domaine == 1 && d.DO_Type == 12 && !d.DO_Ref.Equals("")).ToList(); 
            var F_DOCLIGNE = DB.F_DOCLIGNE.Where(d => d.DO_Piece == id).ToList();

            AcroFields form = pdfStamper.AcroFields;

            //remplit les champs d'information client et fournisseur
            foreach (var infoclient in F_DOCENTETE)
            {
                if (!string.IsNullOrEmpty(infoclient.POINT_RETRAIT_RELAIS))
                {
                    form.SetField("numero", infoclient.DO_Piece);
                    form.SetField("date", infoclient.DO_Date.ToString().Remove(10, 9));
                    form.SetField("telephone", infoclient.TELEPHONE_FACTURATION);
                    form.SetField("tel2", infoclient.TELEPHONE_LIVRAISON);
                    form.SetField("nomclient", infoclient.CIVILITE_FACTURATION + " " + infoclient.NOM_FACTURATION + " " + infoclient.PRENOM_FACTURATION);
                    form.SetField("adresseclient", infoclient.ADRESSE_FACTURATION);
                    form.SetField("adresse2client", infoclient.COMPLEMENT_FACTURATION);
                    form.SetField("cpclient", infoclient.CODE_POSTAL_FACTURATION);
                    form.SetField("villeclient", infoclient.VILLE_FACTURATION);
                    form.SetField("paysclient", infoclient.PAYS_FACTURATION);
                    form.SetField("emailclient", infoclient.EMAIL_FACTURATION);
                }
                else
                {
                    form.SetField("numero", infoclient.DO_Piece);
                    form.SetField("date", infoclient.DO_Date.ToString().Remove(10, 9));
                    form.SetField("telephone", infoclient.TELEPHONE_LIVRAISON);
                    form.SetField("tel2", infoclient.TELEPHONE_FACTURATION);
                    form.SetField("nomclient", infoclient.CIVILITE_LIVRAISON + " " + infoclient.NOM_LIVRAISON + " " + infoclient.PRENOM_LIVRAISON);
                    form.SetField("adresseclient", infoclient.ADRESSE_LIVRAISON);
                    form.SetField("adresse2client", infoclient.COMPLEMENT_LIVRAISON);
                    form.SetField("cpclient", infoclient.CODE_POSTAL_LIVRAISON);
                    form.SetField("villeclient", infoclient.VILLE_LIVRAISON);
                    form.SetField("paysclient", infoclient.PAYS_LIVRAISON);
                    form.SetField("emailclient", infoclient.EMAIL_LIVRAISON);
                }



                var F_COMPTET = DBF.F_COMPTET.Where(d => d.CT_Num == infoclient.DO_Tiers && d.CT_Type == 1).ToList();
                foreach (var infofourni in F_COMPTET)
                {
                    form.SetField("nomfournisseur", infofourni.CT_Intitule);
                    form.SetField("adressefournisseur", infofourni.CT_Adresse);
                    form.SetField("adresse2fournisseur", infofourni.CT_Complement);
                    form.SetField("cpfournisseur", infofourni.CT_CodePostal);
                    form.SetField("villefournisseur", infofourni.CT_Ville);
                }
            }

            
            //remplit chaque ligne de commande
            foreach (var produit in F_DOCLIGNE)
            {
                
                // a passer en boucle for pour réduire taille du code
                //for (produit.DL_Ligne = 0; produit.DL_Ligne <= 10000; produit.DL_Ligne++1000)
                if (produit.DL_Ligne == 1000)
                {
                    form.SetField("ref1", produit.AR_Ref);
                    form.SetField("reffrs1", produit.AF_RefFourniss);
                    form.SetField("refdesign1", produit.DL_Design);
                    form.SetField("qte1", produit.EU_Qte.ToString());
                    form.SetField("cond1", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 2000)
                {
                    form.SetField("ref2", produit.AR_Ref);
                    form.SetField("reffrs2", produit.AF_RefFourniss);
                    form.SetField("refdesign2", produit.DL_Design);
                    form.SetField("qte2", produit.EU_Qte.ToString());
                    form.SetField("cond2", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 3000)
                {
                    form.SetField("ref3", produit.AR_Ref);
                    form.SetField("reffrs3", produit.AF_RefFourniss);
                    form.SetField("refdesign3", produit.DL_Design);
                    form.SetField("qte3", produit.EU_Qte.ToString());
                    form.SetField("cond3", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 4000)
                {
                    form.SetField("ref4", produit.AR_Ref);
                    form.SetField("reffrs4", produit.AF_RefFourniss);
                    form.SetField("refdesign4", produit.DL_Design);
                    form.SetField("qte4", produit.EU_Qte.ToString());
                    form.SetField("cond4", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 5000)
                {
                    form.SetField("ref5", produit.AR_Ref);
                    form.SetField("reffrs5", produit.AF_RefFourniss);
                    form.SetField("refdesign5", produit.DL_Design);
                    form.SetField("qte5", produit.EU_Qte.ToString());
                    form.SetField("cond5", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 6000)
                {
                    form.SetField("ref6", produit.AR_Ref);
                    form.SetField("reffrs6", produit.AF_RefFourniss);
                    form.SetField("refdesign6", produit.DL_Design);
                    form.SetField("qte6", produit.EU_Qte.ToString());
                    form.SetField("cond6", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 7000)
                {
                    form.SetField("ref7", produit.AR_Ref);
                    form.SetField("reffrs7", produit.AF_RefFourniss);
                    form.SetField("refdesign7", produit.DL_Design);
                    form.SetField("qte7", produit.EU_Qte.ToString());
                    form.SetField("cond7", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 8000)
                {
                    form.SetField("ref8", produit.AR_Ref);
                    form.SetField("reffrs8", produit.AF_RefFourniss);
                    form.SetField("refdesign8", produit.DL_Design);
                    form.SetField("qte8", produit.EU_Qte.ToString());
                    form.SetField("cond8", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 9000)
                {
                    form.SetField("ref9", produit.AR_Ref);
                    form.SetField("reffrs9", produit.AF_RefFourniss);
                    form.SetField("refdesign9", produit.DL_Design);
                    form.SetField("qte9", produit.EU_Qte.ToString());
                    form.SetField("cond9", produit.EU_Enumere);
                }
                else if (produit.DL_Ligne == 10000)
                {
                    form.SetField("ref10", produit.AR_Ref);
                    form.SetField("reffrs10", produit.AF_RefFourniss);
                    form.SetField("refdesign10", produit.DL_Design);
                    form.SetField("qte10", produit.EU_Qte.ToString());
                    form.SetField("cond10", produit.EU_Enumere);
                }
            }

            pdfStamper.FormFlattening = true;

            // close the pdf
            pdfStamper.Close();

            //afficher le fichier dans le navigateur web
            return File(newFile, "application /pdf");

        }




    }



    public ActionResult GenererTousLesBons(string Id)
    {

        string pdfTemplate = @"C:\TPL\templateachat.pdf";
    
        ListeCommandesEntities DB = new ListeCommandesEntities();
        ListeFournisseursEntities DBF = new ListeFournisseursEntities();

        var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Domaine == 1 && d.DO_Type == 12 && !d.DO_Ref.Equals("")).ToList();

        //remplit les champs d'information client et fournisseur
        foreach (var infoclient in F_DOCENTETE)
        {

            string newFile = @"C:\BL\" + infoclient.DO_Piece + ".pdf";


            if (System.IO.File.Exists(newFile))
            {
            }

            else
            {

            PdfReader pdfReader = new PdfReader(pdfTemplate);

            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.CreateNew, FileAccess.ReadWrite));

            AcroFields form = pdfStamper.AcroFields;

                if (!string.IsNullOrEmpty(infoclient.POINT_RETRAIT_RELAIS))
                {
                    form.SetField("numero", infoclient.DO_Piece);
                    form.SetField("date", infoclient.DO_Date.ToString().Remove(10, 9));
                    form.SetField("telephone", infoclient.TELEPHONE_FACTURATION);
                    form.SetField("tel2", infoclient.TELEPHONE_LIVRAISON);
                    form.SetField("nomclient", infoclient.CIVILITE_FACTURATION + " " + infoclient.NOM_FACTURATION + " " + infoclient.PRENOM_FACTURATION);
                    form.SetField("adresseclient", infoclient.ADRESSE_FACTURATION);
                    form.SetField("adresse2client", infoclient.COMPLEMENT_FACTURATION);
                    form.SetField("cpclient", infoclient.CODE_POSTAL_FACTURATION);
                    form.SetField("villeclient", infoclient.VILLE_FACTURATION);
                    form.SetField("paysclient", infoclient.PAYS_FACTURATION);
                    form.SetField("emailclient", infoclient.EMAIL_FACTURATION);
                }
                else
                {
                    form.SetField("numero", infoclient.DO_Piece);
                    form.SetField("date", infoclient.DO_Date.ToString().Remove(10, 9));
                    form.SetField("telephone", infoclient.TELEPHONE_LIVRAISON);
                    form.SetField("tel2", infoclient.TELEPHONE_FACTURATION);
                    form.SetField("nomclient", infoclient.CIVILITE_LIVRAISON + " " + infoclient.NOM_LIVRAISON + " " + infoclient.PRENOM_LIVRAISON);
                    form.SetField("adresseclient", infoclient.ADRESSE_LIVRAISON);
                    form.SetField("adresse2client", infoclient.COMPLEMENT_LIVRAISON);
                    form.SetField("cpclient", infoclient.CODE_POSTAL_LIVRAISON);
                    form.SetField("villeclient", infoclient.VILLE_LIVRAISON);
                    form.SetField("paysclient", infoclient.PAYS_LIVRAISON);
                    form.SetField("emailclient", infoclient.EMAIL_LIVRAISON);
                }


                // données du client
                var F_COMPTET = DBF.F_COMPTET.Where(d => d.CT_Num == infoclient.DO_Tiers && d.CT_Type == 1).ToList();
            foreach (var infofourni in F_COMPTET)
            {
            form.SetField("nomfournisseur", infofourni.CT_Intitule);
            form.SetField("adressefournisseur", infofourni.CT_Adresse);
            form.SetField("adresse2fournisseur", infofourni.CT_Complement);
            form.SetField("cpfournisseur", infofourni.CT_CodePostal);
            form.SetField("villefournisseur", infofourni.CT_Ville);
            }

            // données des produits
             var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(infoclient.DO_Piece)).ToList();
            foreach (var produit in F_DOCLIGNE)
                {

                    // a passer en boucle for pour réduire taille du code
                    //for (produit.DL_Ligne = 0; produit.DL_Ligne <= 10000; produit.DL_Ligne++1000)
                    if (produit.DL_Ligne == 1000)
                    {
                        form.SetField("ref1", produit.AR_Ref);
                        form.SetField("reffrs1", produit.AF_RefFourniss);
                        form.SetField("refdesign1", produit.DL_Design);
                        form.SetField("qte1", produit.EU_Qte.ToString());
                        form.SetField("cond1", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 2000)
                    {
                        form.SetField("ref2", produit.AR_Ref);
                        form.SetField("reffrs2", produit.AF_RefFourniss);
                        form.SetField("refdesign2", produit.DL_Design);
                        form.SetField("qte2", produit.EU_Qte.ToString());
                        form.SetField("cond2", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 3000)
                    {
                        form.SetField("ref3", produit.AR_Ref);
                        form.SetField("reffrs3", produit.AF_RefFourniss);
                        form.SetField("refdesign3", produit.DL_Design);
                        form.SetField("qte3", produit.EU_Qte.ToString());
                        form.SetField("cond3", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 4000)
                    {
                        form.SetField("ref4", produit.AR_Ref);
                        form.SetField("reffrs4", produit.AF_RefFourniss);
                        form.SetField("refdesign4", produit.DL_Design);
                        form.SetField("qte4", produit.EU_Qte.ToString());
                        form.SetField("cond4", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 5000)
                    {
                        form.SetField("ref5", produit.AR_Ref);
                        form.SetField("reffrs5", produit.AF_RefFourniss);
                        form.SetField("refdesign5", produit.DL_Design);
                        form.SetField("qte5", produit.EU_Qte.ToString());
                        form.SetField("cond5", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 6000)
                    {
                        form.SetField("ref6", produit.AR_Ref);
                        form.SetField("reffrs6", produit.AF_RefFourniss);
                        form.SetField("refdesign6", produit.DL_Design);
                        form.SetField("qte6", produit.EU_Qte.ToString());
                        form.SetField("cond6", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 7000)
                    {
                        form.SetField("ref7", produit.AR_Ref);
                        form.SetField("reffrs7", produit.AF_RefFourniss);
                        form.SetField("refdesign7", produit.DL_Design);
                        form.SetField("qte7", produit.EU_Qte.ToString());
                        form.SetField("cond7", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 8000)
                    {
                        form.SetField("ref8", produit.AR_Ref);
                        form.SetField("reffrs8", produit.AF_RefFourniss);
                        form.SetField("refdesign8", produit.DL_Design);
                        form.SetField("qte8", produit.EU_Qte.ToString());
                        form.SetField("cond8", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 9000)
                    {
                        form.SetField("ref9", produit.AR_Ref);
                        form.SetField("reffrs9", produit.AF_RefFourniss);
                        form.SetField("refdesign9", produit.DL_Design);
                        form.SetField("qte9", produit.EU_Qte.ToString());
                        form.SetField("cond9", produit.EU_Enumere);
                    }
                    else if (produit.DL_Ligne == 10000)
                    {
                        form.SetField("ref10", produit.AR_Ref);
                        form.SetField("reffrs10", produit.AF_RefFourniss);
                        form.SetField("refdesign10", produit.DL_Design);
                        form.SetField("qte10", produit.EU_Qte.ToString());
                        form.SetField("cond10", produit.EU_Enumere);
                    }
                }
                
            // fermeture du template
            pdfStamper.FormFlattening = true;
            pdfStamper.Close();

            }

        }
        //afficher le fichier dans le navigateur web
        //   return File(newFile, "application /pdf");
        return null;
 
    }
}



