using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml;
using System.Web.Mvc;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace WillemseFranceMP.Models
{
    // Cette classe permet à un administrateur informatique de modifier les paramètres de mail, chemins etc. 
    // à partir du fichier parametres.xml
    public class Parametres
    {
        private XmlDocument doc = new XmlDocument();

        public Parametres()
        {
            try
            {
                this.doc.Load(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
            catch(XmlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Display(Name = "Host")]
        public String Host
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/mail/host").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/mail/host").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }
        [Display(Name = "Port")]
        public int Port
        {
            get
            {
                return int.Parse(this.doc.SelectSingleNode("/parametres/mail/port").InnerText);
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/mail/port").InnerText = ""+value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }
        [Display(Name = "Adresse de messagerie")]
        public String MailUser
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/mail/credentials").Attributes["name"].Value;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/mail/credentials").Attributes["name"].Value = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }
        [Display(Name = "Mot de passe de messagerie")]
        public String MailPass
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/mail/credentials").Attributes["pass"].Value;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/mail/credentials").Attributes["pass"].Value = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }
        [Display(Name = "Adresse d'expédition")]
        public String MailMP
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/mail/mpMail").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/mail/mpMail").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }
        [Display(Name = "Nom affiché")]
        public String MailMpName
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/mail/mpMailName").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/mail/mpMailName").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Liste des emails administrateurs")]
        public List<String> Recipients
        {
            get
            {
                ApplicationDbOracleContext dbF = new ApplicationDbOracleContext();
                var administrateurs = dbF.Users.Where(d => (d.Roles.Any(r => r.RoleId == "1" || r.RoleId == "3") && d.ValZn2 == "Oui")).ToList();
                List<String> l = new List<string>();
                foreach (var f in administrateurs)
                {
                    l.Add(f.Email);
                }
                return l;
            }
        }

        [Display(Name = "Email pour les produits  épuisés")]
        public String ProdEpuises
        {
            get
            {
                ApplicationDbOracleContext dbF = new ApplicationDbOracleContext();
                var admin = dbF.Users.Where(d => (d.Roles.Any(r => r.RoleId == "1" || r.RoleId == "3") && d.Email.Contains("marlene"))).FirstOrDefault();
                return admin.Email;
            }
        }

        [Display(Name = "URL FTP")]
        public String FTP
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/FTP").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/FTP").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Chemin des données du projet")]
        public String appData
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/appData").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/appData").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Chemin des dossiers fournisseurs")]
        public String DossiersFournisseurs
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/DossiersFournisseurs").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/DossiersFournisseurs").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Chemin d'export des données vers l'ERP")]
        public String exportErpPath
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/exportERPPath").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/exportERPPath").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Chemin de sortie")]
        public String DirOutPath
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/DirOutPath").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/DirOutPath").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Fichier d'export des données vers l'ERP")]
        public String exportErpFile
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/exportERPFile").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/exportERPFile").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Fichier modèle de produits")]
        public String modeleProduits
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/produits").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/produits").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Fichier modèle d'offres")]
        public String modeleOffres
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/offres").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/offres").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Images à traiter")]
        public String ektas
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/ektas").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/ektas").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Répertoire Relations Clients pour les fichiers de suivi de commandes")]
        public String suiviCDE
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/relationsclients").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/relationsclients").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Rapport erreurs produits")]
        public String rapportErreursProduits
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursProduits").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursProduits").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Rapport erreurs offres")]
        public String rapportErreursOffres
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursOffres").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursOffres").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Rapport erreurs images")]
        public String rapportErreursImages
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursImages").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursImages").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Rapport erreurs fiches")]
        public String rapportErreursFiches
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursFiches").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursFiches").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Rapport erreurs modele")]
        public String rapportErreursModele
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursModele").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/rapportErreursModele").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "CSV produits")]
        public String produitsCSV
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/produitsCSV").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/produitsCSV").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "CSV Offres")]
        public String offresCSV
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/offresCSV").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/offresCSV").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Modèle de fichier de commandes")]
        public String modeleCommandes
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/commandes").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/commandes").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Modèle de fichier de suivi de commandes")]
        public String modeleSuiviCommandes
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/fichiers/suivi").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/fichiers/suivi").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Répertoire d'archivage")]
        public String repArchivage
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/DirOutPath").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/DirOutPath").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Répertoire de fichiers de commandes")]
        public String repCommandes
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/chemins/repCommandes").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/chemins/repCommandes").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Environnement de developpement")]
        public String env
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/environnement").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/environnement").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }

        [Display(Name = "Connexion Oracle")]
        public String oracleConnection
        {
            get
            {
                return this.doc.SelectSingleNode("/parametres/oracleConnection").InnerText;
            }
            set
            {
                this.doc.SelectSingleNode("/parametres/oracleConnection").InnerText = value;
                this.doc.Save(HttpContext.Current.Server.MapPath("~/parametres.xml"));
            }
        }
    }
}