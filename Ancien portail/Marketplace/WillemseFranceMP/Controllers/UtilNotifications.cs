using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using WillemseFranceMP.Models;
using System.IO;

namespace WillemseFranceMP.Controllers
{
    // Envoi de mails contenant les rapports d'ajout/modifications des produits et offres aux administrateurs
    public class UtilNotifications : Controller
    {
        private Parametres p = new Parametres();
        private SmtpClient smtp;
        private MailMessage mailContact;
        private string repFou;

        public UtilNotifications(){
            repFou = p.DossiersFournisseurs;
            smtp = new SmtpClient
            {
                Host = p.Host,
                Port = p.Port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Timeout = 10000,
                Credentials = new NetworkCredential(p.MailUser, p.MailPass)
            };
        }



        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //--------------------------------------------------------------------Création des Rapports-------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------------------------------------------------//



        // Ajoute une ligne au rapport des nouvelles offres
        public void NewOffres(string[] line)
        {
            string header = "Id Fournisseur;Code Produit Fournisseur;Libelle produit;Prix d'Achat HT;Frais de Livraison;Prix d'achat + frais;Prix de vente conseille;Ecotaxe;TVA;Delai de livraison;Disponible";
            string file = System.Web.HttpContext.Current.Server.MapPath(p.DossiersFournisseurs + "/" + line[0] + "/Nouvelles_Offres_" + line[0] + ".csv");
            Console.Out.WriteLine(file);
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = System.IO.File.AppendText(file))
                {
                    sw.WriteLine(header);
                }
            }
            using (StreamWriter sw = System.IO.File.AppendText(file))
            {
                sw.WriteLine(line[0] + ";" + line[1] + ";" + line[2] + ";" + line[4] + ";" + line[5] + ";" + line[7] + ";" + line[8] + ";" + line[9] + ";" + line[10] + ";" + line[11] + ";" + line[12]);
            }
        }

        // Ajoute une ligne au rapport de modifications d'une offre
        public void UpdateOffres(string[] line)
        {
            string header = "Id Fournisseur;Code Produit Fournisseur;Libelle produit;Prix d'Achat HT;Nouveau prix d'achat;Frais de Livraison;Prix d'achat + frais;Nouveau prix d'achat + fraix;Prix de vente conseille;Ecotaxe;TVA;Delai de livraison;Disponibilite";
            string file = System.Web.HttpContext.Current.Server.MapPath(p.DossiersFournisseurs + "/" + line[0] + "/Modifications_Offres_" + line[0] + ".csv");
            Console.Out.WriteLine(file);
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = System.IO.File.AppendText(file))
                {
                    sw.WriteLine(header);
                }
            }
            using (StreamWriter sw = System.IO.File.AppendText(file))
            {
                sw.WriteLine(line[0] + ";" + line[1] + ";" + line[2] + ";" + line[3] + ";" + line[4] + ";" + line[5] + ";" + line[6] + ";" + line[7] + ";" + line[8] + ";" + line[9] + ";" + line[10] + ";" + line[11] + ";" + line[12]);
            }
        }

        // nouveau produit
        public void NewProduits(string[] line)
        {
            string header = "Id Fournisseur;Code Produit Fournisseur;Designation Produit";
            string file = System.Web.HttpContext.Current.Server.MapPath(p.DossiersFournisseurs + "/" + line[0] + "/Nouveaux_Produits_" + line[0] + ".csv");
            Console.Out.WriteLine(file);
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = System.IO.File.AppendText(file))
                {
                    sw.WriteLine(header);
                }
            }
            using (StreamWriter sw = System.IO.File.AppendText(file))
            {
                sw.WriteLine(line[0] + ";" + line[1] + ";" + line[2]);
            }
        }


        // produits épuisés
        public void ProduitsEpuises(string idfou, string codproerp, string codprofou, string design, string dispo)
        {
            string header = "Id Fournisseur;Code Produit ERP;Code Produit Fournisseur;Designation Produit;Disponible";
            string file = System.Web.HttpContext.Current.Server.MapPath(p.DossiersFournisseurs + "/" + idfou + "/Dispo_Produits_" + idfou + ".csv");
            Console.Out.WriteLine(file);
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = System.IO.File.AppendText(file))
                {
                    sw.WriteLine(header);
                }
            }
            using (StreamWriter sw = System.IO.File.AppendText(file))
            {
                sw.WriteLine(idfou + ";" + codproerp + ";" + codprofou + ";" + design + ";" + dispo);
            }
        }


        // produits supprimés
        public void DeleteProduits(string idfou, string codproerp, string codprofou, string design)
        {
            string header = "Id Fournisseur;Code Produit ERP;Code Produit Fournisseur;Designation Produit";
            string file = System.Web.HttpContext.Current.Server.MapPath(p.DossiersFournisseurs + "/" + idfou + "/Suppression_Produits_" + idfou + ".csv");
            Console.Out.WriteLine(file);
            if (!System.IO.File.Exists(file))
            {
                using (StreamWriter sw = System.IO.File.AppendText(file))
                {
                    sw.WriteLine(header);
                }
            }
            using (StreamWriter sw = System.IO.File.AppendText(file))
            {
                sw.WriteLine(idfou + ";" + codproerp + ";" + codprofou + ";" + design);
            }
        }


        // produits modifiés
        public void UpdateProduits(string[] line)
        {
            try
            {
                string header = "Id Fournisseur;Code Produit Fournisseur;Designation Produit;Champs modifies";
                string file = System.Web.HttpContext.Current.Server.MapPath(p.DossiersFournisseurs + "/" + line[0] + "/Modifications_Produits_" + line[0] + ".csv");
                Console.Out.WriteLine(file);
                if (!System.IO.File.Exists(file))
                {
                    using (StreamWriter sw = System.IO.File.AppendText(file))
                    {
                        sw.WriteLine(header);
                    }
                }
                using (StreamWriter sw = System.IO.File.AppendText(file))
                {
                    sw.WriteLine(line[0] + ";" + line[1] + ";" + line[2] + ";" + line[3]);
                }
            }catch(Exception e)
            {
                Console.Out.WriteLine(e.StackTrace);
            }
            
        }





        //------------------------------------------------------------------------------------------------------------------------------------------------------------//
        //--------------------------------------------------------------------------Envoi des mails-------------------------------------------------------------------//
        //------------------------------------------------------------------------------------------------------------------------------------------------------------//

        public void MailProduitsEpuises(string idfou, string societe, string codproerp, string design, string path) // Signale les produits épuisés/indisponibles
        {
            mailContact = new MailMessage();
            mailContact.IsBodyHtml = true;
            mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
            foreach (String mail in p.Recipients)
            {
                mailContact.To.Add(mail);
            }
            mailContact.To.Add(p.ProdEpuises);
            mailContact.Subject = "[Portail Fournisseur] Produits " + societe + " épuisés";
            string textmsg = "Bonjour, <br/>";
            textmsg += "<br/>Vous trouverez ci-joint une liste des produits épuisés ou momentanément indisponibles du fournisseur " + idfou + " " + societe + ".";
            textmsg += "<br/>Veuillez effectuer les modification necessaires dans l'ERP.";
            textmsg += "<br/><br/>Portail Fournisseur";
            mailContact.Body = textmsg;
            Attachment attachment = new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath(@"~\DossierMP\" + idfou + "\\" + Path.GetFileName(path)));
            mailContact.Attachments.Add(attachment);
            try
            {
                smtp.Send(mailContact);
                mailContact.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du mail ",
                  ex.ToString());
            }
        }


        public void MailProduitsAjoutes(string idfou, string societe, string nom, string prenom, string path) // liste de produits ajoutés
        {
            mailContact = new MailMessage();
            mailContact.IsBodyHtml = true;
            mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
            foreach (String mail in p.Recipients)
            {
                mailContact.To.Add(mail);
            }
            mailContact.Subject = "[Portail Fournisseur] Nouveaux produits " + " " + idfou + " " + societe;
            string textmsg = "Bonjour, <br/>";
            textmsg += "<br/>Veuillez trouver ci-joint une liste des produits ajoutés par " + societe + " dans le portail fournisseur.";
            textmsg += "<br/>Vous devez vous rendre sur le portail fournisseur afin de valider ces produits.";
            textmsg += "<br/><br/>Portail Fournisseur";
            mailContact.Body = textmsg;
            Attachment attachment = new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath(@"~\DossierMP\" + idfou + "\\" + Path.GetFileName(path)));
            mailContact.Attachments.Add(attachment);
            try
            {
                smtp.Send(mailContact);
                mailContact.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du mail ",
                  ex.ToString());
            }
        }

        public void MailProduitsModifies(string idfou, string societe, string nom, string prenom, string path) // liste de produits modifiés
        {
            mailContact = new MailMessage();
            mailContact.IsBodyHtml = true;
            mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
            foreach (String mail in p.Recipients)
            {
                mailContact.To.Add(mail);
            }
            mailContact.Subject = "[Portail Fournisseur] Modifications des produits " + " " + idfou + " " + societe;
            string textmsg = "Bonjour, <br/>";
            textmsg += "<br/>Veuillez trouver ci-joint une liste des produits modifiés par " + societe + " dans le portail fournisseur.";
            textmsg += "<br/>Vous devez vous rendre sur le portail fournisseur afin de valider ces produits.";
            textmsg += "<br/><br/>Portail Fournisseur";
            mailContact.Body = textmsg;
            Attachment attachment = new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath(@"~\DossierMP\" + idfou + "\\" + Path.GetFileName(path)));
            mailContact.Attachments.Add(attachment);
            try
            {
                smtp.Send(mailContact);
                mailContact.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du mail ",
                  ex.ToString());
            }
        }

        public void MailDelete(string idfou, string societe, string path) // Lorsqu'un fournisseur supprime un produit directement dans le portail fournisseur au lieu d'indiquer qu'il est épuisé ou indisponible
        {
            mailContact = new MailMessage();
            mailContact.IsBodyHtml = true;
            mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
            foreach (String mail in p.Recipients)
            {
                mailContact.To.Add(mail);
            }
            mailContact.Subject = "[Portail Fournisseur] Suppression de produits " + idfou + " " + societe;
            string textmsg = "Bonjour, <br/>";
            textmsg += "<br/><br/>Le fournisseur "+ societe + " a supprimé des produits de son catalogue.";
            textmsg += "<br/>Veuillez trouver ci-joint une liste de produits supprimés.";
            textmsg += "<br/>Les produits concernés ne seront plus affichés dans le portail, mais ils existent toujours dans l'ERP.";
            textmsg += "<br/>Veuillez effectuer les modification necessaires dans l'ERP.";
            textmsg += "<br/>S'il s'agit d'une erreur, veuillez contacter un administrateur informatique pour ré-activer les produits dans le portail.";
            textmsg += "<br/><br/>Portail Fournisseur";
            mailContact.Body = textmsg;
            Attachment attachment = new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath(@"~\DossierMP\" + idfou + "\\" + Path.GetFileName(path)));
            mailContact.Attachments.Add(attachment);
            try
            {
                smtp.Send(mailContact);
                mailContact.Attachments.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi du mail ",
                  ex.ToString());
            }
        }

        public void MailOffresAjoutees(string idfou, string societe, string nom, string prenom, string path) // Envoi un mail contenant les nouvelles offres
        {
            if (System.IO.File.Exists(path))
            {
                mailContact = new MailMessage();
                mailContact.IsBodyHtml = true;
                mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
                foreach (String mail in p.Recipients)
                {
                    mailContact.To.Add(mail);
                }
                mailContact.Subject = "[Portail Fournisseur] Nouvelles offres du fournisseur " + idfou + " " + societe;
                string textmsg = "Bonjour, <br/>";
                textmsg += "<br/>Veuillez trouver ci-joint les nouvelles offres de "+ societe + ".";
                textmsg += "<br/>Vous devez vous rendre sur le portail fournisseur afin de valider ces offres.";
                textmsg += "<br/><br/>Portail Fournisseur";
                mailContact.Body = textmsg;
                Attachment attachment  = new System.Net.Mail.Attachment(System.Web.HttpContext.Current.Server.MapPath(@"~\DossierMP\" + idfou + "\\" + Path.GetFileName(path)));
                mailContact.Attachments.Add(attachment);
                try
                {
                    smtp.Send(mailContact);
                    mailContact.Attachments.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'envoi du mail ",
                      ex.ToString());
                }
            }
        }

        public void MailOffresModifies(string idfou, string societe, string nom, string prenom, string path) // mail contenant les offres modifiés, vue sur les anciens et nouveaux prix
        {
            if (System.IO.File.Exists(path))
            {
                mailContact = new MailMessage();
                mailContact.IsBodyHtml = true;
                mailContact.From = new MailAddress(p.MailMP, p.MailMpName);
                foreach (String mail in p.Recipients)
                {
                    mailContact.To.Add(mail);
                }
                mailContact.Subject = "[Portail Fournisseur] Modifications offres fournisseur " + idfou + " " + societe;
                string textmsg = "Bonjour, <br/>";
                textmsg += "<br/>Veuillez trouver ci-joint toutes les offres de "+societe+" modifiées.";
                textmsg += "<br/>Vous devez vous rendre sur le portail fournisseur afin de valider ces offres.";
                textmsg += "<br/><br/>Portail Fournisseur";
                mailContact.Body = textmsg;
                Attachment attachment = new Attachment(System.Web.HttpContext.Current.Server.MapPath(@"~\DossierMP\"+idfou+"\\"+Path.GetFileName(path)));
                mailContact.Attachments.Add(attachment);
                try
                {
                    smtp.Send(mailContact);
                    mailContact.Attachments.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erreur lors de l'envoi du mail ",
                      ex.ToString());
                }
            }
        }


        public void MailAdmins(string idfou, string societe, string nom, string prenom)
        {
            string pathModifOffres = System.Web.HttpContext.Current.Server.MapPath(repFou + "/" + idfou + "/Modifications_Offres_" + idfou + ".csv");
            string pathModifProd = System.Web.HttpContext.Current.Server.MapPath(repFou + "/" + idfou + "/Modifications_Produits_" + idfou + ".csv");
            string pathAjoutOffres = System.Web.HttpContext.Current.Server.MapPath(repFou + "/" + idfou + "/Nouvelles_Offres_" + idfou + ".csv");
            string pathAjoutProd = System.Web.HttpContext.Current.Server.MapPath(repFou + "/" + idfou + "/Nouveaux_Produits_" + idfou + ".csv");
            string pathProdEpuise = System.Web.HttpContext.Current.Server.MapPath(repFou + "/" + idfou + "/Dispo_Produits_" + idfou + ".csv");
            string pathDeleteProd = System.Web.HttpContext.Current.Server.MapPath(repFou + "/" + idfou + "/Suppression_Produits_" + idfou + ".csv");
            if (System.IO.File.Exists(pathModifOffres))
            {
                MailOffresModifies(idfou, societe, nom, prenom, pathModifOffres);
                while (!IsFileReady(pathModifOffres))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.IO.File.Delete(pathModifOffres);
            }
            if (System.IO.File.Exists(pathAjoutOffres))
            {
                MailOffresAjoutees(idfou, societe, nom, prenom, pathAjoutOffres);
                while (!IsFileReady(pathAjoutOffres))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.IO.File.Delete(pathAjoutOffres);
            }
            if (System.IO.File.Exists(pathModifProd))
            {
                MailProduitsModifies(idfou, societe, nom, prenom, pathModifProd);
                while (!IsFileReady(pathModifProd))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.IO.File.Delete(pathModifProd);
            }
            if (System.IO.File.Exists(pathAjoutProd))
            {
                MailProduitsAjoutes(idfou, societe, nom, prenom, pathAjoutProd);
                while (!IsFileReady(pathAjoutProd))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.IO.File.Delete(pathAjoutProd);
            }
            if (System.IO.File.Exists(pathProdEpuise))
            {
                MailProduitsEpuises(idfou, societe, nom, prenom, pathProdEpuise);
                while (!IsFileReady(pathProdEpuise))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.IO.File.Delete(pathProdEpuise);
            }
            if (System.IO.File.Exists(pathDeleteProd))
            {
                MailDelete(idfou, societe, pathDeleteProd);
                while (!IsFileReady(pathDeleteProd))
                {
                    System.Threading.Thread.Sleep(1000);
                }
                System.IO.File.Delete(pathDeleteProd);
            }
        }

        public static bool IsFileReady(String sFilename)
        {
            try
            {
                using (FileStream inputStream = System.IO.File.Open(sFilename, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (inputStream.Length > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}