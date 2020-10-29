using iTextSharp.text;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text.factories;
using iText.Layout;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PortailFournisseur.Models;
using System.Text;
using System;
using System.Drawing;
using System.Collections.Generic;

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
        string pdfTemplate = @"C:\TPL\templatedelivraisonOK5.pdf";

        string dateString = Id;
        DateTime dateCommande = DateTime.Parse(dateString);


        //var dateCommande = new DateTime(2020, 1, 1);
        //toutes les commandes appartenant a do_domaine = 0 et do_type = 6 ou 7 et à partir du 01/01/2020
        var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Domaine == 0 && !d.DO_Ref.Equals("") && d.DO_Date == dateCommande && d.DO_Type == 7 && d.DO_Souche == 0
                                                    || d.DO_Domaine == 0 && !d.DO_Ref.Equals("") && d.DO_Date == dateCommande && d.DO_Type == 6 && d.DO_Souche == 0).ToList();
        //table qui contient les informations de la société(nom, adresse, activité, etc)
        var P_DOSSIER = DB.P_DOSSIER.ToList();


        foreach (var infoclient in F_DOCENTETE)
        {

            string newFile = @"C:\FACTUREPDF\" + infoclient.DO_Piece + ".pdf";
            //sélectionner chaque ligne de commande
            var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(infoclient.DO_Piece)).ToList();


            if (System.IO.File.Exists(newFile))
            {
            }

            else
            {

                PdfReader pdfReader = new PdfReader(pdfTemplate);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.CreateNew, FileAccess.ReadWrite));


                AcroFields form = pdfStamper.AcroFields;

                form.SetFieldProperty("adresse", "textsize", 8f, null);
                form.SetFieldProperty("activite", "textsize", 8f, null);


                form.SetFieldProperty("do_piece", "textsize", 8f, null);
                form.SetFieldProperty("do_date", "textsize", 8f, null);
                form.SetFieldProperty("do_ref", "textsize", 8f, null);
                form.SetFieldProperty("societe_facturation", "textsize", 8f, null);
                form.SetFieldProperty("civilite+nom+prenom_facturation", "textsize", 8f, null);
                form.SetFieldProperty("adresse_facturation", "textsize", 8f, null);
                form.SetFieldProperty("complement_facturation", "textsize", 8f, null);
                form.SetFieldProperty("cp + ville_facturation", "textsize", 8f, null);
                form.SetFieldProperty("pays_facturation", "textsize", 8f, null);

                form.SetFieldProperty("societe_livraison", "textsize", 8f, null);
                form.SetFieldProperty("civilite+nom+prenom_livraison", "textsize", 8f, null);
                form.SetFieldProperty("adresse_livraison", "textsize", 8f, null);
                form.SetFieldProperty("complement_livraison", "textsize", 8f, null);
                form.SetFieldProperty("cp + ville_livraison", "textsize", 8f, null);
                form.SetFieldProperty("pays_livraison", "textsize", 8f, null);

                form.SetFieldProperty("do_totalht", "textsize", 8f, null);
                form.SetFieldProperty("do_taxe1", "textsize", 8f, null);
                form.SetFieldProperty("do_totalttc", "textsize", 8f, null);
                form.SetFieldProperty("do_valfrais", "textsize", 8f, null);

                //taille police reference
                form.SetFieldProperty("ar_ref1", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref2", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref3", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref4", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref5", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref6", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref7", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref8", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref9", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref10", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref11", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref12", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref13", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref14", "textsize", 6f, null);
                form.SetFieldProperty("ar_ref15", "textsize", 6f, null);

                //taille police designation
                form.SetFieldProperty("dl_design1", "textsize", 6f, null);
                form.SetFieldProperty("dl_design2", "textsize", 6f, null);
                form.SetFieldProperty("dl_design3", "textsize", 6f, null);
                form.SetFieldProperty("dl_design4", "textsize", 6f, null);
                form.SetFieldProperty("dl_design5", "textsize", 6f, null);
                form.SetFieldProperty("dl_design6", "textsize", 6f, null);
                form.SetFieldProperty("dl_design7", "textsize", 6f, null);
                form.SetFieldProperty("dl_design8", "textsize", 6f, null);
                form.SetFieldProperty("dl_design9", "textsize", 6f, null);
                form.SetFieldProperty("dl_design10", "textsize", 6f, null);
                form.SetFieldProperty("dl_design11", "textsize", 6f, null);
                form.SetFieldProperty("dl_design12", "textsize", 6f, null);
                form.SetFieldProperty("dl_design13", "textsize", 6f, null);
                form.SetFieldProperty("dl_design14", "textsize", 6f, null);
                form.SetFieldProperty("dl_design15", "textsize", 6f, null);

                //taille police qte
                form.SetFieldProperty("dl_qte1", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte2", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte3", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte4", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte5", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte6", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte7", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte8", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte9", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte10", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte11", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte12", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte13", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte14", "textsize", 6f, null);
                form.SetFieldProperty("dl_qte15", "textsize", 6f, null);

                //taille police prix ttc
                form.SetFieldProperty("dl_puttc1", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc2", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc3", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc4", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc5", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc6", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc7", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc8", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc9", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc10", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc11", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc12", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc13", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc14", "textsize", 6f, null);
                form.SetFieldProperty("dl_puttc15", "textsize", 6f, null);

                //taille police remise
                form.SetFieldProperty("dl_remise1", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise2", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise3", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise4", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise5", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise6", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise7", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise8", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise9", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise10", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise11", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise12", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise13", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise14", "textsize", 6f, null);
                form.SetFieldProperty("dl_remise15", "textsize", 6f, null);

                //taille police montant total ttc
                form.SetFieldProperty("dl_montantttc1", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc2", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc3", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc4", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc5", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc6", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc7", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc8", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc9", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc10", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc11", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc12", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc13", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc14", "textsize", 6f, null);
                form.SetFieldProperty("dl_montantttc15", "textsize", 6f, null);
                form.SetFieldProperty("page", "textsize", 8f, null);

                //numéro page
                form.SetField("page", "Page   1");


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
                
                if (F_DOCLIGNE.Count <= 15)
                {

                    form.SetField("do_totalht", infoclient.DO_TotalHT.ToString().Substring(0, infoclient.DO_TotalHT.ToString().Length - 3));
                        //equivaut au Do_totalTTC - le DO total HT net
                    form.SetField("do_taxe1", (infoclient.TTC_TX10 - infoclient.DO_TotalHT).ToString().Substring(0, (infoclient.TTC_TX10 - infoclient.DO_TotalHT).ToString().Length - 3).ToString());
                    form.SetField("do_totalttc", infoclient.TTC_TX10.ToString().Substring(0, infoclient.TTC_TX10.ToString().Length - 3));
                    form.SetField("do_valfrais", infoclient.DO_ValFrais.ToString().Substring(0, infoclient.DO_ValFrais.ToString().Length - 3));
                }

                int compteur_ligne = 0;
                foreach (var infocommande in F_DOCLIGNE)
                {
                    compteur_ligne++;
                    if (compteur_ligne == 1)
                    {
                        form.SetField("ar_ref1", infocommande.AR_Ref);
                        form.SetField("dl_design1", infocommande.DL_Design);
                        form.SetField("dl_qte1", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc1", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise1", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc1", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));

                    }
                    else if (compteur_ligne == 2)
                    {
                        form.SetField("ar_ref2", infocommande.AR_Ref);
                        form.SetField("dl_design2", infocommande.DL_Design);
                        form.SetField("dl_qte2", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc2", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise2", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc2", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 3)
                    {
                        form.SetField("ar_ref3", infocommande.AR_Ref);
                        form.SetField("dl_design3", infocommande.DL_Design);
                        form.SetField("dl_qte3", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc3", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise3", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc3", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 4)
                    {
                        form.SetField("ar_ref4", infocommande.AR_Ref);
                        form.SetField("dl_design4", infocommande.DL_Design);
                        form.SetField("dl_qte4", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc4", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise4", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc4", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 5)
                    {
                        form.SetField("ar_ref5", infocommande.AR_Ref);
                        form.SetField("dl_design5", infocommande.DL_Design);
                        form.SetField("dl_qte5", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc5", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise5", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc5", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 6)
                    {
                        form.SetField("ar_ref6", infocommande.AR_Ref);
                        form.SetField("dl_design6", infocommande.DL_Design);
                        form.SetField("dl_qte6", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc6", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise6", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc6", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 7)
                    {
                        form.SetField("ar_ref7", infocommande.AR_Ref);
                        form.SetField("dl_design7", infocommande.DL_Design);
                        form.SetField("dl_qte7", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc7", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise7", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc7", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 8)
                    {
                        form.SetField("ar_ref8", infocommande.AR_Ref);
                        form.SetField("dl_design8", infocommande.DL_Design);
                        form.SetField("dl_qte8", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc8", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise8", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc8", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 9)
                    {
                        form.SetField("ar_ref9", infocommande.AR_Ref);
                        form.SetField("dl_design9", infocommande.DL_Design);
                        form.SetField("dl_qte9", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc9", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise9", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc9", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 10)
                    {
                        form.SetField("ar_ref10", infocommande.AR_Ref);
                        form.SetField("dl_design10", infocommande.DL_Design);
                        form.SetField("dl_qte10", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc10", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise10", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc10", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 11)
                    {
                        form.SetField("ar_ref11", infocommande.AR_Ref);
                        form.SetField("dl_design11", infocommande.DL_Design);
                        form.SetField("dl_qte11", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc11", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise11", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc11", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 12)
                    {
                        form.SetField("ar_ref12", infocommande.AR_Ref);
                        form.SetField("dl_design12", infocommande.DL_Design);
                        form.SetField("dl_qte12", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc12", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise12", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc12", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 13)
                    {
                        form.SetField("ar_ref13", infocommande.AR_Ref);
                        form.SetField("dl_design13", infocommande.DL_Design);
                        form.SetField("dl_qte13", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc13", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise13", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc13", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 14)
                    {
                        form.SetField("ar_ref14", infocommande.AR_Ref);
                        form.SetField("dl_design14", infocommande.DL_Design);
                        form.SetField("dl_qte14", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc14", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise14", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc14", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }
                    else if (compteur_ligne == 15)
                    {
                        form.SetField("ar_ref15", infocommande.AR_Ref);
                        form.SetField("dl_design15", infocommande.DL_Design);
                        form.SetField("dl_qte15", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                        form.SetField("dl_puttc15", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                        if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                        {
                            form.SetField("dl_remise15", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                        }
                        form.SetField("dl_montantttc15", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
                    }

                    if (compteur_ligne == 16)
                    {
                        //fermeture du template
                        pdfStamper.FormFlattening = true;
                        pdfStamper.Close();

                        
                        if (F_DOCLIGNE.Count >= 16 && F_DOCLIGNE.Count <= 38)
                        {
                            string[] pages = { newFile, GenerationPage2(Id, infocommande.DO_Piece, 2, infocommande.DL_Ligne.ToString()) };
                            MergePages(@"C:\FACTUREPDF\" + infoclient.DO_Piece + "(" + pages.Count() + ").pdf", pages);
                        }
                        if (F_DOCLIGNE.Count >= 39 && F_DOCLIGNE.Count <= 61)
                        {
                            string[] pages = { newFile, GenerationPage2(Id, infocommande.DO_Piece, 3, infocommande.DL_Ligne.ToString()) };
                            MergePages(@"C:\FACTUREPDF\" + infoclient.DO_Piece + "(" + pages.Count() + ").pdf", pages);
                        }
                        else if (F_DOCLIGNE.Count >= 62 && F_DOCLIGNE.Count <= 84)
                        {
                            string[] pages = { newFile, GenerationPage2(Id, infocommande.DO_Piece, 4, infocommande.DL_Ligne.ToString()) };
                            MergePages(@"C:\FACTUREPDF\" + infoclient.DO_Piece + "(" + pages.Count() + ").pdf", pages);
                        }
                        else if (F_DOCLIGNE.Count >= 85 && F_DOCLIGNE.Count <= 107)
                        {
                            string[] pages = { newFile, GenerationPage2(Id, infocommande.DO_Piece, 5, infocommande.DL_Ligne.ToString()) };
                            MergePages(@"C:\FACTUREPDF\" + infoclient.DO_Piece + "(" + pages.Count() + ").pdf", pages);
                        }
                        else if (F_DOCLIGNE.Count >= 108 && F_DOCLIGNE.Count <= 130)
                        {
                            string[] pages = { newFile, GenerationPage2(Id, infocommande.DO_Piece, 6, infocommande.DL_Ligne.ToString()) };
                            MergePages(@"C:\FACTUREPDF\" + infoclient.DO_Piece + "(" + pages.Count() + ").pdf", pages);
                        }


                    }
                }

                 //fermeture du template
                pdfStamper.FormFlattening = true;
                pdfStamper.Close();
            }

        }
        DirectoryInfo di = new DirectoryInfo(@"C:\FACTUREPDF\");

        foreach (FileInfo file in di.GetFiles())
        {
            if (file.Name.Contains("_"))
            {
                file.Delete();

            }
        }
        return null;

    }

    public string GenerationPage2(string Id, string do_piece, int numero_page, string derniere_page1)
    {

        string dateString = Id;
        DateTime dateCommande = DateTime.Parse(dateString);

        //table qui contient les informations de la société(nom, adresse, activité, etc)
        var P_DOSSIER = DB.P_DOSSIER.ToList();
        //sélectionner chaque ligne de commande
        /*
        int debut = (((numero_page - 1) * 23) - 7) * 1000;
        int fin = (((numero_page - 1) * 23) + 15) * 1000;
        var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(do_piece) && ligne.DL_Ligne >= debut && ligne.DL_Ligne <= fin ).ToList();
        */
        int fin = int.Parse(derniere_page1);
        var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(do_piece) && ligne.DL_Ligne >= fin).ToList();

        //toutes les commandes appartenant a do_domaine = 0 et do_type = 6 ou 7 et à partir du 01/01/2020
        var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Piece == do_piece).ToList();

        string pdfTemplatePage2 = @"C:\TPL\templatedelivraisonpage3ok.pdf";
        string newFilePage2 = @"C:\FACTUREPDF\" + do_piece +"_"+ numero_page+".pdf";
        PdfReader pdfReaderPage2 = new PdfReader(pdfTemplatePage2);
        PdfStamper pdfStamperPage2 = new PdfStamper(pdfReaderPage2, new FileStream(newFilePage2, FileMode.CreateNew, FileAccess.ReadWrite));
        AcroFields form2 = pdfStamperPage2.AcroFields;

        form2.SetFieldProperty("d_telephonepage2", "textsize", 10f, null);
        form2.SetFieldProperty("d_sitepage2", "textsize", 10f, null);
        form2.SetFieldProperty("d_siretpage2", "textsize", 10f, null);
        form2.SetFieldProperty("d_apepage2", "textsize", 10f, null);
        form2.SetFieldProperty("d_identifiantpage2", "textsize", 10f, null);

        form2.SetFieldProperty("do_totalht", "textsize", 8f, null);
        form2.SetFieldProperty("do_taxe1", "textsize", 8f, null);
        form2.SetFieldProperty("do_totalttc", "textsize", 8f, null);
        form2.SetFieldProperty("do_valfrais", "textsize", 8f, null);

        //taille police reference
        form2.SetFieldProperty("ar_ref16", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref17", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref18", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref19", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref20", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref21", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref22", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref23", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref24", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref25", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref26", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref27", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref28", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref29", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref30", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref31", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref32", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref33", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref34", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref35", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref36", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref37", "textsize", 6f, null);
        form2.SetFieldProperty("ar_ref38", "textsize", 6f, null);

        //taille police designation
        form2.SetFieldProperty("dl_design16", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design17", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design18", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design19", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design20", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design21", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design22", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design23", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design24", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design25", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design26", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design27", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design28", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design29", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design30", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design31", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design32", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design33", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design34", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design35", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design36", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design37", "textsize", 6f, null);
        form2.SetFieldProperty("dl_design38", "textsize", 6f, null);

        //taille police qte
        form2.SetFieldProperty("dl_qte16", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte17", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte18", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte19", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte20", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte21", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte22", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte23", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte24", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte25", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte26", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte27", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte28", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte29", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte30", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte31", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte32", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte33", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte34", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte35", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte36", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte37", "textsize", 6f, null);
        form2.SetFieldProperty("dl_qte38", "textsize", 6f, null);

        //taille police prix ttc
        form2.SetFieldProperty("dl_puttc16", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc17", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc18", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc19", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc20", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc21", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc22", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc23", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc24", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc25", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc26", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc27", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc28", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc29", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc30", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc31", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc32", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc33", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc34", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc35", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc36", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc37", "textsize", 6f, null);
        form2.SetFieldProperty("dl_puttc38", "textsize", 6f, null);

        //taille police remise
        form2.SetFieldProperty("dl_remise16", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise17", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise18", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise19", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise20", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise21", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise22", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise23", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise24", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise25", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise26", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise27", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise28", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise29", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise30", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise31", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise32", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise33", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise34", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise35", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise36", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise37", "textsize", 6f, null);
        form2.SetFieldProperty("dl_remise38", "textsize", 6f, null);

        //taille police montant total ttc
        form2.SetFieldProperty("dl_montantttc16", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc17", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc18", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc19", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc20", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc21", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc22", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc23", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc24", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc25", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc26", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc27", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc28", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc29", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc30", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc31", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc32", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc33", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc34", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc35", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc36", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc37", "textsize", 6f, null);
        form2.SetFieldProperty("dl_montantttc38", "textsize", 6f, null);
        form2.SetFieldProperty("page", "textsize", 8f, null);

        //numéro page
        form2.SetField("page", "Page   " + numero_page.ToString());

        foreach (var infosociete in P_DOSSIER)
        {
            form2.SetField("d_telephonepage2", infosociete.D_Telephone);
            form2.SetField("d_sitepage2", infosociete.D_Site);
            form2.SetField("d_siretpage2", infosociete.D_Siret);
            form2.SetField("d_apepage2", infosociete.D_Ape);
            form2.SetField("d_identifiantpage2", infosociete.D_Identifiant);

        }
        /*
        string numero_ligne = ((infocommande.DL_Ligne/1000) + ((numero_page - 2) * 23)).ToString();
        if (numero <= "38")
        {
            foreach (var infoclient in F_DOCENTETE)
            {
                try
                {
                    form2.SetField("do_totalht", infoclient.DO_TotalHTNet.ToString().Substring(0, infoclient.DO_TotalHT.ToString().Length - 3));
                    //equivaut au Do_totalTTC - le DO total HT net
                    form2.SetField("do_taxe1", (infoclient.DO_TotalTTC - infoclient.DO_TotalHTNet).ToString().Substring(0, (infoclient.DO_TotalTTC - infoclient.DO_TotalHTNet).ToString().Length - 3).ToString());

                    form2.SetField("do_totalttc", infoclient.DO_TotalTTC.ToString().Substring(0, infoclient.DO_TotalTTC.ToString().Length - 3));
                }
                catch
                {

                }

                form2.SetField("do_valfrais", infoclient.DO_ValFrais.ToString().Substring(0, infoclient.DO_ValFrais.ToString().Length - 3));
            }
        }*/

        int compteur_ligne = 15;
        foreach (var infocommande in F_DOCLIGNE)
        {
            compteur_ligne++;    
            string numero_ligne = (compteur_ligne + ((numero_page - 2) * 23)).ToString();
            string derniere_ligne = (F_DOCLIGNE.Count+15).ToString();

            if (derniere_ligne==numero_ligne)
            {
                foreach (var infoclient in F_DOCENTETE)
                {
                    form2.SetField("do_totalht", infoclient.DO_TotalHT.ToString().Substring(0, infoclient.DO_TotalHT.ToString().Length - 3));
                    //equivaut au Do_totalTTC - le DO total HT net
                    form2.SetField("do_taxe1", (infoclient.TTC_TX10 - infoclient.DO_TotalHT).ToString().Substring(0, (infoclient.TTC_TX10 - infoclient.DO_TotalHT).ToString().Length - 3).ToString());
                    form2.SetField("do_totalttc", infoclient.TTC_TX10.ToString().Substring(0, infoclient.TTC_TX10.ToString().Length - 3));
                    form2.SetField("do_valfrais", infoclient.DO_ValFrais.ToString().Substring(0, infoclient.DO_ValFrais.ToString().Length - 3));
                }
            }



            if (numero_ligne == "16" )
            {
                form2.SetField("ar_ref16", infocommande.AR_Ref);
                form2.SetField("dl_design16", infocommande.DL_Design);
                form2.SetField("dl_qte16", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc16", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise16", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc16", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "17")
            {
                form2.SetField("ar_ref17", infocommande.AR_Ref);
                form2.SetField("dl_design17", infocommande.DL_Design);
                form2.SetField("dl_qte17", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc17", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise17", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc17", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "18")
            {
                form2.SetField("ar_ref18", infocommande.AR_Ref);
                form2.SetField("dl_design18", infocommande.DL_Design);
                form2.SetField("dl_qte18", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc18", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise18", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc18", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "19")
            {
                form2.SetField("ar_ref19", infocommande.AR_Ref);
                form2.SetField("dl_design19", infocommande.DL_Design);
                form2.SetField("dl_qte19", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc19", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise19", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc19", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "20")
            {
                form2.SetField("ar_ref20", infocommande.AR_Ref);
                form2.SetField("dl_design20", infocommande.DL_Design);
                form2.SetField("dl_qte20", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc20", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise20", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc20", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "21")
            {
                form2.SetField("ar_ref21", infocommande.AR_Ref);
                form2.SetField("dl_design21", infocommande.DL_Design);
                form2.SetField("dl_qte21", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc21", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise21", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc21", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "22")
            {
                form2.SetField("ar_ref22", infocommande.AR_Ref);
                form2.SetField("dl_design22", infocommande.DL_Design);
                form2.SetField("dl_qte22", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc22", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise22", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc22", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "23")
            {
                form2.SetField("ar_ref23", infocommande.AR_Ref);
                form2.SetField("dl_design23", infocommande.DL_Design);
                form2.SetField("dl_qte23", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc23", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise23", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc23", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "24")
            {
                form2.SetField("ar_ref24", infocommande.AR_Ref);
                form2.SetField("dl_design24", infocommande.DL_Design);
                form2.SetField("dl_qte24", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc24", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise24", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc24", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));

            }
            if (numero_ligne == "25")
            {
                form2.SetField("ar_ref25", infocommande.AR_Ref);
                form2.SetField("dl_design25", infocommande.DL_Design);
                form2.SetField("dl_qte25", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc25", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise25", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc25", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "26")
            {
                form2.SetField("ar_ref26", infocommande.AR_Ref);
                form2.SetField("dl_design26", infocommande.DL_Design);
                form2.SetField("dl_qte26", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc26", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise26", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc26", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "27")
            {
                form2.SetField("ar_ref27", infocommande.AR_Ref);
                form2.SetField("dl_design27", infocommande.DL_Design);
                form2.SetField("dl_qte27", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc27", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise27", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc27", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "28")
            {
                form2.SetField("ar_ref28", infocommande.AR_Ref);
                form2.SetField("dl_design28", infocommande.DL_Design);
                form2.SetField("dl_qte28", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc28", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise28", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc28", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "29")
            {
                form2.SetField("ar_ref29", infocommande.AR_Ref);
                form2.SetField("dl_design29", infocommande.DL_Design);
                form2.SetField("dl_qte29", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc29", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise29", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc29", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "30")
            {
                form2.SetField("ar_ref30", infocommande.AR_Ref);
                form2.SetField("dl_design30", infocommande.DL_Design);
                form2.SetField("dl_qte30", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc30", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise30", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc30", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "31")
            {
                form2.SetField("ar_ref31", infocommande.AR_Ref);
                form2.SetField("dl_design31", infocommande.DL_Design);
                form2.SetField("dl_qte31", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc31", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise31", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc31", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "32")
            {
                form2.SetField("ar_ref32", infocommande.AR_Ref);
                form2.SetField("dl_design32", infocommande.DL_Design);
                form2.SetField("dl_qte32", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc32", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise32", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc32", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "33")
            {
                form2.SetField("ar_ref33", infocommande.AR_Ref);
                form2.SetField("dl_design33", infocommande.DL_Design);
                form2.SetField("dl_qte33", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc33", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise33", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc33", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "34")
            {
                form2.SetField("ar_ref34", infocommande.AR_Ref);
                form2.SetField("dl_design34", infocommande.DL_Design);
                form2.SetField("dl_qte34", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc34", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise34", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc34", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "35")
            {
                form2.SetField("ar_ref35", infocommande.AR_Ref);
                form2.SetField("dl_design35", infocommande.DL_Design);
                form2.SetField("dl_qte35", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc35", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise35", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc35", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "36")
            {
                form2.SetField("ar_ref36", infocommande.AR_Ref);
                form2.SetField("dl_design36", infocommande.DL_Design);
                form2.SetField("dl_qte36", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc36", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise36", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc36", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "37")
            {
                form2.SetField("ar_ref37", infocommande.AR_Ref);
                form2.SetField("dl_design37", infocommande.DL_Design);
                form2.SetField("dl_qte37", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc37", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise37", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc37", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "38")
            {
                form2.SetField("ar_ref38", infocommande.AR_Ref);
                form2.SetField("dl_design38", infocommande.DL_Design);
                form2.SetField("dl_qte38", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form2.SetField("dl_puttc38", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form2.SetField("dl_remise38", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form2.SetField("dl_montantttc38", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (numero_ligne == "39")
            {
                // fermeture du template
                pdfStamperPage2.FormFlattening = true;
                pdfStamperPage2.Close();

                //GenerationPage3(Id, infocommande.DO_Piece);
                return newFilePage2;

          
            }
        }
        // fermeture du template
        pdfStamperPage2.FormFlattening = true;
        pdfStamperPage2.Close();
        return newFilePage2;

    }
    /*
    public string GenerationPage3(string Id, string do_piece)
    {

        string dateString = Id;
        DateTime dateCommande = DateTime.Parse(dateString);

        //table qui contient les informations de la société(nom, adresse, activité, etc)
        var P_DOSSIER = DB.P_DOSSIER.ToList();
        //sélectionner chaque ligne de commande

        var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(do_piece) && ligne.DL_Ligne >= 16000).ToList();

        //toutes les commandes appartenant a do_domaine = 0 et do_type = 6 ou 7 et à partir du 01/01/2020
        var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Piece == do_piece).ToList();

        string pdfTemplatePage3 = @"C:\TPL\templatedelivraisonpage3ok.pdf";
        string newFilePage3 = @"C:\FACTUREPDF\" + do_piece + "Page3.pdf";
        PdfReader pdfReaderPage3 = new PdfReader(pdfTemplatePage3);
        PdfStamper pdfStamperPage3 = new PdfStamper(pdfReaderPage3, new FileStream(newFilePage3, FileMode.CreateNew, FileAccess.ReadWrite));
        AcroFields form3 = pdfStamperPage3.AcroFields;

        form3.SetFieldProperty("d_telephonepage2", "textsize", 10f, null);
        form3.SetFieldProperty("d_sitepage2", "textsize", 10f, null);
        form3.SetFieldProperty("d_siretpage2", "textsize", 10f, null);
        form3.SetFieldProperty("d_apepage2", "textsize", 10f, null);
        form3.SetFieldProperty("d_identifiantpage2", "textsize", 10f, null);

        form3.SetFieldProperty("do_totalht", "textsize", 8f, null);
        form3.SetFieldProperty("do_taxe1", "textsize", 8f, null);
        form3.SetFieldProperty("do_totalttc", "textsize", 8f, null);
        form3.SetFieldProperty("do_valfrais", "textsize", 8f, null);

        //taille police reference
        form3.SetFieldProperty("ar_ref16", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref17", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref18", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref19", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref20", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref21", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref22", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref23", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref24", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref25", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref26", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref27", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref28", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref29", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref30", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref31", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref32", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref33", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref34", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref35", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref36", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref37", "textsize", 6f, null);
        form3.SetFieldProperty("ar_ref38", "textsize", 6f, null);

        //taille police designation
        form3.SetFieldProperty("dl_design16", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design17", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design18", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design19", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design20", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design21", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design22", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design23", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design24", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design25", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design26", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design27", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design28", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design29", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design30", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design31", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design32", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design33", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design34", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design35", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design36", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design37", "textsize", 6f, null);
        form3.SetFieldProperty("dl_design38", "textsize", 6f, null);

        //taille police qte
        form3.SetFieldProperty("dl_qte16", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte17", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte18", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte19", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte20", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte21", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte22", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte23", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte24", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte25", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte26", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte27", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte28", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte29", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte30", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte31", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte32", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte33", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte34", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte35", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte36", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte37", "textsize", 6f, null);
        form3.SetFieldProperty("dl_qte38", "textsize", 6f, null);

        //taille police prix ttc
        form3.SetFieldProperty("dl_puttc16", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc17", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc18", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc19", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc20", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc21", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc22", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc23", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc24", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc25", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc26", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc27", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc28", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc29", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc30", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc31", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc32", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc33", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc34", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc35", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc36", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc37", "textsize", 6f, null);
        form3.SetFieldProperty("dl_puttc38", "textsize", 6f, null);

        //taille police remise
        form3.SetFieldProperty("dl_remise16", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise17", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise18", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise19", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise20", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise21", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise22", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise23", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise24", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise25", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise26", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise27", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise28", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise29", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise30", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise31", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise32", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise33", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise34", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise35", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise36", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise37", "textsize", 6f, null);
        form3.SetFieldProperty("dl_remise38", "textsize", 6f, null);

        //taille police montant total ttc
        form3.SetFieldProperty("dl_montantttc16", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc17", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc18", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc19", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc20", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc21", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc22", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc23", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc24", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc25", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc26", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc27", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc28", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc29", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc30", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc31", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc32", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc33", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc34", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc35", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc36", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc37", "textsize", 6f, null);
        form3.SetFieldProperty("dl_montantttc38", "textsize", 6f, null);

        foreach (var infosociete in P_DOSSIER)
        {
            form3.SetField("d_telephonepage2", infosociete.D_Telephone);
            form3.SetField("d_sitepage2", infosociete.D_Site);
            form3.SetField("d_siretpage2", infosociete.D_Siret);
            form3.SetField("d_apepage2", infosociete.D_Ape);
            form3.SetField("d_identifiantpage2", infosociete.D_Identifiant);

        }

        foreach (var infoclient in F_DOCENTETE)
        {
            try
            {
                form3.SetField("do_totalht", infoclient.DO_TotalHTNet.ToString().Substring(0, infoclient.DO_TotalHT.ToString().Length - 3));
                //equivaut au Do_totalTTC - le DO total HT net
                form3.SetField("do_taxe1", (infoclient.DO_TotalTTC - infoclient.DO_TotalHTNet).ToString().Substring(0, (infoclient.DO_TotalTTC - infoclient.DO_TotalHTNet).ToString().Length - 3).ToString());

                form3.SetField("do_totalttc", infoclient.DO_TotalTTC.ToString().Substring(0, infoclient.DO_TotalTTC.ToString().Length - 3));
            }
            catch
            {

            }

            form3.SetField("do_valfrais", infoclient.DO_ValFrais.ToString().Substring(0, infoclient.DO_ValFrais.ToString().Length - 3));
        }
        foreach (var infocommande in F_DOCLIGNE)
        {
            if (infocommande.DL_Ligne == 39000)
            {
                form3.SetField("ar_ref16", infocommande.AR_Ref);
                form3.SetField("dl_design16", infocommande.DL_Design);
                form3.SetField("dl_qte16", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc16", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise16", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc16", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 40000)
            {
                form3.SetField("ar_ref17", infocommande.AR_Ref);
                form3.SetField("dl_design17", infocommande.DL_Design);
                form3.SetField("dl_qte17", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc17", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise17", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc17", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 41000)
            {
                form3.SetField("ar_ref18", infocommande.AR_Ref);
                form3.SetField("dl_design18", infocommande.DL_Design);
                form3.SetField("dl_qte18", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc18", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise18", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc18", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 42000)
            {
                form3.SetField("ar_ref19", infocommande.AR_Ref);
                form3.SetField("dl_design19", infocommande.DL_Design);
                form3.SetField("dl_qte19", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc19", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise19", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc19", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 43000)
            {
                form3.SetField("ar_ref20", infocommande.AR_Ref);
                form3.SetField("dl_design20", infocommande.DL_Design);
                form3.SetField("dl_qte20", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc20", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise20", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc20", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 44000)
            {
                form3.SetField("ar_ref21", infocommande.AR_Ref);
                form3.SetField("dl_design21", infocommande.DL_Design);
                form3.SetField("dl_qte21", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc21", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise21", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc21", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 45000)
            {
                form3.SetField("ar_ref22", infocommande.AR_Ref);
                form3.SetField("dl_design22", infocommande.DL_Design);
                form3.SetField("dl_qte22", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc22", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise22", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc22", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 46000)
            {
                form3.SetField("ar_ref23", infocommande.AR_Ref);
                form3.SetField("dl_design23", infocommande.DL_Design);
                form3.SetField("dl_qte23", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc23", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise23", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc23", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 47000)
            {
                form3.SetField("ar_ref24", infocommande.AR_Ref);
                form3.SetField("dl_design24", infocommande.DL_Design);
                form3.SetField("dl_qte24", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc24", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise24", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc24", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));

            }
            if (infocommande.DL_Ligne == 48000)
            {
                form3.SetField("ar_ref25", infocommande.AR_Ref);
                form3.SetField("dl_design25", infocommande.DL_Design);
                form3.SetField("dl_qte25", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc25", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise25", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc25", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 49000)
            {
                form3.SetField("ar_ref26", infocommande.AR_Ref);
                form3.SetField("dl_design26", infocommande.DL_Design);
                form3.SetField("dl_qte26", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc26", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise26", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc26", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 50000)
            {
                form3.SetField("ar_ref27", infocommande.AR_Ref);
                form3.SetField("dl_design27", infocommande.DL_Design);
                form3.SetField("dl_qte27", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc27", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise27", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc27", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 51000)
            {
                form3.SetField("ar_ref28", infocommande.AR_Ref);
                form3.SetField("dl_design28", infocommande.DL_Design);
                form3.SetField("dl_qte28", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc28", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise28", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc28", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 52000)
            {
                form3.SetField("ar_ref29", infocommande.AR_Ref);
                form3.SetField("dl_design29", infocommande.DL_Design);
                form3.SetField("dl_qte29", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc29", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise29", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc29", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 53000)
            {
                form3.SetField("ar_ref30", infocommande.AR_Ref);
                form3.SetField("dl_design30", infocommande.DL_Design);
                form3.SetField("dl_qte30", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc30", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise30", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc30", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 54000)
            {
                form3.SetField("ar_ref31", infocommande.AR_Ref);
                form3.SetField("dl_design31", infocommande.DL_Design);
                form3.SetField("dl_qte31", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc31", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise31", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc31", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 55000)
            {
                form3.SetField("ar_ref32", infocommande.AR_Ref);
                form3.SetField("dl_design32", infocommande.DL_Design);
                form3.SetField("dl_qte32", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc32", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise32", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc32", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 56000)
            {
                form3.SetField("ar_ref33", infocommande.AR_Ref);
                form3.SetField("dl_design33", infocommande.DL_Design);
                form3.SetField("dl_qte33", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc33", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise33", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc33", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 57000)
            {
                form3.SetField("ar_ref34", infocommande.AR_Ref);
                form3.SetField("dl_design34", infocommande.DL_Design);
                form3.SetField("dl_qte34", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc34", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise34", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc34", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 58000)
            {
                form3.SetField("ar_ref35", infocommande.AR_Ref);
                form3.SetField("dl_design35", infocommande.DL_Design);
                form3.SetField("dl_qte35", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc35", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise35", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc35", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 59000)
            {
                form3.SetField("ar_ref63", infocommande.AR_Ref);
                form3.SetField("dl_design36", infocommande.DL_Design);
                form3.SetField("dl_qte36", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc36", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise36", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc36", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 60000)
            {
                form3.SetField("ar_ref37", infocommande.AR_Ref);
                form3.SetField("dl_design37", infocommande.DL_Design);
                form3.SetField("dl_qte37", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc37", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise37", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc37", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 61000)
            {
                form3.SetField("ar_ref38", infocommande.AR_Ref);
                form3.SetField("dl_design38", infocommande.DL_Design);
                form3.SetField("dl_qte38", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form3.SetField("dl_puttc38", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form3.SetField("dl_remise38", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form3.SetField("dl_montantttc38", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 62000)
            {
                // fermeture du template
                pdfStamperPage3.FormFlattening = true;
                pdfStamperPage3.Close();
                return newFilePage3;

            }
        }
        // fermeture du template
        pdfStamperPage3.FormFlattening = true;
        pdfStamperPage3.Close();
        return newFilePage3;
    }

    public string GenerationPage4(string Id, string do_piece)
    {

        string dateString = Id;
        DateTime dateCommande = DateTime.Parse(dateString);

        //table qui contient les informations de la société(nom, adresse, activité, etc)
        var P_DOSSIER = DB.P_DOSSIER.ToList();
        //sélectionner chaque ligne de commande

        var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(do_piece) && ligne.DL_Ligne >= 16000).ToList();

        //toutes les commandes appartenant a do_domaine = 0 et do_type = 6 ou 7 et à partir du 01/01/2020
        var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Piece == do_piece).ToList();

        string pdfTemplatePage4 = @"C:\TPL\templatedelivraisonpage3ok.pdf";
        string newFilePage4 = @"C:\FACTUREPDF\" + do_piece + "Page4.pdf";
        PdfReader pdfReaderPage4 = new PdfReader(pdfTemplatePage4);
        PdfStamper pdfStamperPage4 = new PdfStamper(pdfReaderPage4, new FileStream(newFilePage4, FileMode.CreateNew, FileAccess.ReadWrite));
        AcroFields form4 = pdfStamperPage4.AcroFields;

        form4.SetFieldProperty("d_telephonepage2", "textsize", 10f, null);
        form4.SetFieldProperty("d_sitepage2", "textsize", 10f, null);
        form4.SetFieldProperty("d_siretpage2", "textsize", 10f, null);
        form4.SetFieldProperty("d_apepage2", "textsize", 10f, null);
        form4.SetFieldProperty("d_identifiantpage2", "textsize", 10f, null);

        form4.SetFieldProperty("do_totalht", "textsize", 8f, null);
        form4.SetFieldProperty("do_taxe1", "textsize", 8f, null);
        form4.SetFieldProperty("do_totalttc", "textsize", 8f, null);
        form4.SetFieldProperty("do_valfrais", "textsize", 8f, null);

        //taille police reference
        form4.SetFieldProperty("ar_ref16", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref17", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref18", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref19", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref20", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref21", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref22", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref23", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref24", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref25", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref26", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref27", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref28", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref29", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref30", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref31", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref32", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref33", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref34", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref35", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref36", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref37", "textsize", 6f, null);
        form4.SetFieldProperty("ar_ref38", "textsize", 6f, null);

        //taille police designation
        form4.SetFieldProperty("dl_design16", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design17", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design18", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design19", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design20", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design21", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design22", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design23", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design24", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design25", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design26", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design27", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design28", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design29", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design30", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design31", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design32", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design33", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design34", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design35", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design36", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design37", "textsize", 6f, null);
        form4.SetFieldProperty("dl_design38", "textsize", 6f, null);

        //taille police qte
        form4.SetFieldProperty("dl_qte16", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte17", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte18", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte19", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte20", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte21", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte22", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte23", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte24", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte25", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte26", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte27", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte28", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte29", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte30", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte31", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte32", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte33", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte34", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte35", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte36", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte37", "textsize", 6f, null);
        form4.SetFieldProperty("dl_qte38", "textsize", 6f, null);

        //taille police prix ttc
        form4.SetFieldProperty("dl_puttc16", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc17", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc18", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc19", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc20", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc21", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc22", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc23", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc24", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc25", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc26", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc27", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc28", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc29", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc30", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc31", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc32", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc33", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc34", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc35", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc36", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc37", "textsize", 6f, null);
        form4.SetFieldProperty("dl_puttc38", "textsize", 6f, null);

        //taille police remise
        form4.SetFieldProperty("dl_remise16", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise17", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise18", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise19", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise20", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise21", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise22", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise23", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise24", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise25", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise26", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise27", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise28", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise29", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise30", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise31", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise32", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise33", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise34", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise35", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise36", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise37", "textsize", 6f, null);
        form4.SetFieldProperty("dl_remise38", "textsize", 6f, null);

        //taille police montant total ttc
        form4.SetFieldProperty("dl_montantttc16", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc17", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc18", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc19", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc20", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc21", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc22", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc23", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc24", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc25", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc26", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc27", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc28", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc29", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc30", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc31", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc32", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc33", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc34", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc35", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc36", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc37", "textsize", 6f, null);
        form4.SetFieldProperty("dl_montantttc38", "textsize", 6f, null);

        foreach (var infosociete in P_DOSSIER)
        {
            form4.SetField("d_telephonepage2", infosociete.D_Telephone);
            form4.SetField("d_sitepage2", infosociete.D_Site);
            form4.SetField("d_siretpage2", infosociete.D_Siret);
            form4.SetField("d_apepage2", infosociete.D_Ape);
            form4.SetField("d_identifiantpage2", infosociete.D_Identifiant);

        }

        foreach (var infoclient in F_DOCENTETE)
        {
            try
            {
                form4.SetField("do_totalht", infoclient.DO_TotalHTNet.ToString().Substring(0, infoclient.DO_TotalHT.ToString().Length - 3));
                //equivaut au Do_totalTTC - le DO total HT net
                form4.SetField("do_taxe1", (infoclient.DO_TotalTTC - infoclient.DO_TotalHTNet).ToString().Substring(0, (infoclient.DO_TotalTTC - infoclient.DO_TotalHTNet).ToString().Length - 3).ToString());

                form4.SetField("do_totalttc", infoclient.DO_TotalTTC.ToString().Substring(0, infoclient.DO_TotalTTC.ToString().Length - 3));
            }
            catch
            {

            }

            form4.SetField("do_valfrais", infoclient.DO_ValFrais.ToString().Substring(0, infoclient.DO_ValFrais.ToString().Length - 3));
        }
        foreach (var infocommande in F_DOCLIGNE)
        {
            if (infocommande.DL_Ligne == 62000)
            {
                form4.SetField("ar_ref16", infocommande.AR_Ref);
                form4.SetField("dl_design16", infocommande.DL_Design);
                form4.SetField("dl_qte16", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc16", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise16", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc16", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 63000)
            {
                form4.SetField("ar_ref17", infocommande.AR_Ref);
                form4.SetField("dl_design17", infocommande.DL_Design);
                form4.SetField("dl_qte17", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc17", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise17", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc17", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 64000)
            {
                form4.SetField("ar_ref18", infocommande.AR_Ref);
                form4.SetField("dl_design18", infocommande.DL_Design);
                form4.SetField("dl_qte18", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc18", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise18", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc18", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 65000)
            {
                form4.SetField("ar_ref19", infocommande.AR_Ref);
                form4.SetField("dl_design19", infocommande.DL_Design);
                form4.SetField("dl_qte19", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc19", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise19", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc19", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 66000)
            {
                form4.SetField("ar_ref20", infocommande.AR_Ref);
                form4.SetField("dl_design20", infocommande.DL_Design);
                form4.SetField("dl_qte20", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc20", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise20", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc20", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 67000)
            {
                form4.SetField("ar_ref21", infocommande.AR_Ref);
                form4.SetField("dl_design21", infocommande.DL_Design);
                form4.SetField("dl_qte21", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc21", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise21", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc21", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 68000)
            {
                form4.SetField("ar_ref22", infocommande.AR_Ref);
                form4.SetField("dl_design22", infocommande.DL_Design);
                form4.SetField("dl_qte22", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc22", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise22", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc22", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 69000)
            {
                form4.SetField("ar_ref23", infocommande.AR_Ref);
                form4.SetField("dl_design23", infocommande.DL_Design);
                form4.SetField("dl_qte23", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc23", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise23", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc23", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 70000)
            {
                form4.SetField("ar_ref24", infocommande.AR_Ref);
                form4.SetField("dl_design24", infocommande.DL_Design);
                form4.SetField("dl_qte24", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc24", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise24", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc24", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));

            }
            if (infocommande.DL_Ligne == 71000)
            {
                form4.SetField("ar_ref25", infocommande.AR_Ref);
                form4.SetField("dl_design25", infocommande.DL_Design);
                form4.SetField("dl_qte25", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc25", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise25", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc25", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 72000)
            {
                form4.SetField("ar_ref26", infocommande.AR_Ref);
                form4.SetField("dl_design26", infocommande.DL_Design);
                form4.SetField("dl_qte26", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc26", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise26", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc26", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 73000)
            {
                form4.SetField("ar_ref27", infocommande.AR_Ref);
                form4.SetField("dl_design27", infocommande.DL_Design);
                form4.SetField("dl_qte27", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc27", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise27", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc27", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 74000)
            {
                form4.SetField("ar_ref28", infocommande.AR_Ref);
                form4.SetField("dl_design28", infocommande.DL_Design);
                form4.SetField("dl_qte28", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc28", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise28", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc28", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 75000)
            {
                form4.SetField("ar_ref29", infocommande.AR_Ref);
                form4.SetField("dl_design29", infocommande.DL_Design);
                form4.SetField("dl_qte29", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc29", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise29", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc29", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 76000)
            {
                form4.SetField("ar_ref30", infocommande.AR_Ref);
                form4.SetField("dl_design30", infocommande.DL_Design);
                form4.SetField("dl_qte30", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc30", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise30", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc30", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 77000)
            {
                form4.SetField("ar_ref31", infocommande.AR_Ref);
                form4.SetField("dl_design31", infocommande.DL_Design);
                form4.SetField("dl_qte31", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc31", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise31", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc31", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 78000)
            {
                form4.SetField("ar_ref32", infocommande.AR_Ref);
                form4.SetField("dl_design32", infocommande.DL_Design);
                form4.SetField("dl_qte32", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc32", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise32", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc32", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 79000)
            {
                form4.SetField("ar_ref33", infocommande.AR_Ref);
                form4.SetField("dl_design33", infocommande.DL_Design);
                form4.SetField("dl_qte33", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc33", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise33", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc33", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 80000)
            {
                form4.SetField("ar_ref34", infocommande.AR_Ref);
                form4.SetField("dl_design34", infocommande.DL_Design);
                form4.SetField("dl_qte34", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc34", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise34", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc34", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 81000)
            {
                form4.SetField("ar_ref35", infocommande.AR_Ref);
                form4.SetField("dl_design35", infocommande.DL_Design);
                form4.SetField("dl_qte35", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc35", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise35", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc35", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 82000)
            {
                form4.SetField("ar_ref63", infocommande.AR_Ref);
                form4.SetField("dl_design36", infocommande.DL_Design);
                form4.SetField("dl_qte36", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc36", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise36", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc36", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 83000)
            {
                form4.SetField("ar_ref37", infocommande.AR_Ref);
                form4.SetField("dl_design37", infocommande.DL_Design);
                form4.SetField("dl_qte37", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc37", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise37", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc37", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }
            if (infocommande.DL_Ligne == 84000)
            {
                form4.SetField("ar_ref38", infocommande.AR_Ref);
                form4.SetField("dl_design38", infocommande.DL_Design);
                form4.SetField("dl_qte38", infocommande.DL_Qte.ToString().Substring(0, infocommande.DL_Qte.ToString().Length - 7));
                form4.SetField("dl_puttc38", infocommande.DL_PUTTC.ToString().Substring(0, infocommande.DL_PUTTC.ToString().Length - 3));
                if (infocommande.DL_Remise01REM_Valeur.ToString() != "0,000000")
                {
                    form4.SetField("dl_remise38", infocommande.DL_Remise01REM_Valeur.ToString().Substring(0, infocommande.DL_Remise01REM_Valeur.ToString().Length - 3));
                }
                form4.SetField("dl_montantttc38", infocommande.DL_MontantTTC.ToString().Substring(0, infocommande.DL_MontantTTC.ToString().Length - 3));
            }

        }
        // fermeture du template
        pdfStamperPage4.FormFlattening = true;
        pdfStamperPage4.Close();
        return newFilePage4;
    }
    */

    private void MergePages(string outPutFilePath, params string[] filesPath)
    {
        List<PdfReader> readerList = new List<PdfReader>();
        foreach (string filePath in filesPath)
        {
            PdfReader pdfReader = new PdfReader(filePath);
            readerList.Add(pdfReader);
        }

        //Define a new output document and its size, type
        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 0, 0, 0, 0);
        //Create blank output pdf file and get the stream to write on it.
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.CreateNew, FileAccess.ReadWrite));
        document.Open();

        foreach (PdfReader reader in readerList)
        {
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                document.Add(iTextSharp.text.Image.GetInstance(page));
            }
        }
        document.Close();
    }
}
