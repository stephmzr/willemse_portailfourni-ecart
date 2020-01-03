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
using System;

public class FacturePDFController : Controller
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

    public ActionResult GenerationFacturePDF(string Id)
    {
        string pdfTemplate = @"C:\TPL\templatedelivraisonOK3.pdf";

        var dateCommande = new DateTime(2020, 1, 1);
        var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Domaine == 0 && !d.DO_Ref.Equals("") && d.DO_Date >= dateCommande && d.DO_Type == 7
                                                    || d.DO_Domaine == 0 && !d.DO_Ref.Equals("") && d.DO_Date >= dateCommande && d.DO_Type == 6 ).ToList();
        var P_DOSSIER = DB.P_DOSSIER.ToList();


        foreach (var infoclient in F_DOCENTETE)
        {

            string newFile = @"C:\BL\" + infoclient.DO_Piece + ".pdf";
            var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(infoclient.DO_Piece)).ToList();


            if (System.IO.File.Exists(newFile))
            {
            }

            else
            {
                PdfReader pdfReader = new PdfReader(pdfTemplate);

                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.CreateNew, FileAccess.ReadWrite));

                AcroFields form = pdfStamper.AcroFields;

                foreach (var infosociete in P_DOSSIER)
                {
                    form.SetField("societe", infosociete.D_RaisonSoc);
                    form.SetField("activite", infosociete.D_Profession);
                    form.SetField("adresse", infosociete.D_Adresse);
                    form.SetField("complement", infosociete.D_Complement);
                    form.SetField("cp + ville", infosociete.D_CodePostal + " " + infosociete.D_Ville);
                    form.SetField("d_telephone", infosociete.D_Telephone);
                    form.SetField("d_site", infosociete.D_Site);
                    form.SetField("d_siret", infosociete.D_Siret);
                    form.SetField("d_ape", infosociete.D_Ape);
                    form.SetField("d_identifiant", infosociete.D_Identifiant);

                }

                form.SetField("do_piece", infoclient.DO_Piece);
                form.SetField("do_date", infoclient.DO_Date.ToString().Remove(10, 9));
                form.SetField("do_ref", infoclient.DO_Ref);
                form.SetField("societe_facturation", infoclient.SOCIETE_FACTURATION);
                form.SetField("civilite+nom+prenom_facturation", infoclient.CIVILITE_FACTURATION + " " + infoclient.NOM_FACTURATION + " " + infoclient.PRENOM_FACTURATION);
                form.SetField("adresse_facturation", infoclient.ADRESSE_FACTURATION);
                form.SetField("complement_facturation", infoclient.COMPLEMENT_FACTURATION);
                form.SetField("cp + ville_facturation", infoclient.CODE_POSTAL_FACTURATION + " " + infoclient.VILLE_FACTURATION);
                form.SetField("pays_facturation", infoclient.PAYS_FACTURATION);

                form.SetField("societe_livraison", infoclient.SOCIETE_LIVRAISON);
                form.SetField("civilite+nom+prenom_livraison", infoclient.CIVILITE_LIVRAISON + " " + infoclient.NOM_LIVRAISON + " " + infoclient.PRENOM_LIVRAISON);
                form.SetField("adresse_livraison", infoclient.ADRESSE_LIVRAISON);
                form.SetField("complement_livraison", infoclient.COMPLEMENT_LIVRAISON);
                form.SetField("cp + ville_livraison", infoclient.CODE_POSTAL_LIVRAISON + " " + infoclient.VILLE_LIVRAISON);
                form.SetField("pays_livraison", infoclient.PAYS_LIVRAISON);

                form.SetField("do_totalht", infoclient.DO_TotalHT.ToString().Substring(0, infoclient.DO_TotalHT.ToString().Length - 3));
                form.SetField("do_taxe1", infoclient.DO_Taxe1.ToString().Substring(0, infoclient.DO_Taxe1.ToString().Length - 3));
                try
                {
                    form.SetField("do_totalttc", infoclient.DO_TotalTTC.ToString().Substring(0, infoclient.DO_TotalTTC.ToString().Length - 3));
                }
                catch
                {

                }
                form.SetField("do_valfrais", infoclient.DO_ValFrais.ToString().Substring(0, infoclient.DO_ValFrais.ToString().Length - 3));

                foreach (var infocommande in F_DOCLIGNE )
                {
                    if (infocommande.DL_Ligne == 1000)
                    {
                        form.SetField("ar_ref1", infocommande.AR_Ref);
                        form.SetField("dl_design1", infocommande.DL_Design);
                        form.SetField("dl_qte1", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc1", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise1", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc1", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));

                    }
                    else if (infocommande.DL_Ligne == 2000)
                    {
                        form.SetField("ar_ref2", infocommande.AR_Ref);
                        form.SetField("dl_design2", infocommande.DL_Design);
                        form.SetField("dl_qte2", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc2", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise2", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc2", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 3000)
                    {
                        form.SetField("ar_ref3", infocommande.AR_Ref);
                        form.SetField("dl_design3", infocommande.DL_Design);
                        form.SetField("dl_qte3", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc3", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise3", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc3", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 4000)
                    {
                        form.SetField("ar_ref4", infocommande.AR_Ref);
                        form.SetField("dl_design4", infocommande.DL_Design);
                        form.SetField("dl_qte4", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc4", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise4", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc4", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 5000)
                    {
                        form.SetField("ar_ref5", infocommande.AR_Ref);
                        form.SetField("dl_design5", infocommande.DL_Design);
                        form.SetField("dl_qte5", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc5", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise5", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc5", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 6000)
                    {
                        form.SetField("ar_ref6", infocommande.AR_Ref);
                        form.SetField("dl_design6", infocommande.DL_Design);
                        form.SetField("dl_qte6", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc6", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise6", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc6", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 7000)
                    {
                        form.SetField("ar_ref7", infocommande.AR_Ref);
                        form.SetField("dl_design7", infocommande.DL_Design);
                        form.SetField("dl_qte7", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc7", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise7", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc7", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 8000)
                    {
                        form.SetField("ar_ref8", infocommande.AR_Ref);
                        form.SetField("dl_design8", infocommande.DL_Design);
                        form.SetField("dl_qte8", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc8", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise8", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc8", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 9000)
                    {
                        form.SetField("ar_ref9", infocommande.AR_Ref);
                        form.SetField("dl_design9", infocommande.DL_Design);
                        form.SetField("dl_qte9", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc9", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise9", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc9", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 10000)
                    {
                        form.SetField("ar_ref10", infocommande.AR_Ref);
                        form.SetField("dl_design10", infocommande.DL_Design);
                        form.SetField("dl_qte10", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc10", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise10", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc10", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 11000)
                    {
                        form.SetField("ar_ref11", infocommande.AR_Ref);
                        form.SetField("dl_design11", infocommande.DL_Design);
                        form.SetField("dl_qte11", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc11", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise11", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc11", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 12000)
                    {
                        form.SetField("ar_ref12", infocommande.AR_Ref);
                        form.SetField("dl_design12", infocommande.DL_Design);
                        form.SetField("dl_qte12", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc12", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise12", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc12", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 13000)
                    {
                        form.SetField("ar_ref13", infocommande.AR_Ref);
                        form.SetField("dl_design13", infocommande.DL_Design);
                        form.SetField("dl_qte13", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc13", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise13", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc13", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 14000)
                    {
                        form.SetField("ar_ref14", infocommande.AR_Ref);
                        form.SetField("dl_design14", infocommande.DL_Design);
                        form.SetField("dl_qte14", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc14", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise14", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc14", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (infocommande.DL_Ligne == 15000)
                    {
                        form.SetField("ar_ref15", infocommande.AR_Ref);
                        form.SetField("dl_design15", infocommande.DL_Design);
                        form.SetField("dl_qte15", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 3));
                        form.SetField("dl_puttc15", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        form.SetField("dl_remise15", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        form.SetField("dl_montantttc15", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                }


                // fermeture du template
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }



        }
        return null;
    }

}