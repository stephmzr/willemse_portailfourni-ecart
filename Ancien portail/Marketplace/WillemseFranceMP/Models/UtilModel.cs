using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.Entity;
using System.Globalization;
using System.Collections.Generic;
using WillemseFranceMP.Controllers;

namespace WillemseFranceMP.Models
{
    // Contient divers fonctions utiles dans le projet


    // Catégories (ancienne arborescence)
    public class Arb1 
    {
        public string Arb1Code { get; set; }
        public string Arb1Name { get; set; }
        private static ProduitDbOracleContext dbt = new ProduitDbOracleContext();
        public bool Equals(Arb1 p)
        {
            if ((object)p == null)    {   return false; }
            return (Arb1Code == p.Arb1Code) && (Arb1Name == p.Arb1Name);
        }
        public static IQueryable<Arb1> GetArb1 ()
        {
            var arb1 = dbt.NewArbors.Select(u => new Arb1
            {
                Arb1Code = u.Secteur, Arb1Name = u.Secteur
            }).Distinct();
            return  arb1.AsQueryable();        
        }     
    }

    public class TestView
    {
        public string CodPro { get; set; }
        public string Idfou { get; set; }
        
    }
    
    public class Arb2
    {
        public string Arb1Code { get; set; }
        public string Arb2ID { get; set; }
        public string Arb2Name { get; set; }
        private static ProduitDbOracleContext dbt = new ProduitDbOracleContext();
        public bool Equals(Arb2 p)
        {
            if ((object)p == null) {  return false; }

            return (Arb1Code == p.Arb1Code) && (Arb2Name == p.Arb2Name) && (Arb2ID == p.Arb2ID);
        }
        public static IQueryable<Arb2> GetArb2(string secteurnom)
        {
           var arb2 = dbt.NewArbors.Where(u => u.Secteur == secteurnom).Select(u => new Arb2
            { Arb2ID = u.SousSecteur, Arb1Code = u.Secteur, Arb2Name = u.SousSecteur }).Distinct();
           return arb2.AsQueryable();
        }
    }
    public class Arb3
    {
        public string Arb1Code { get; set; }
        public string Arb2Code { get; set; }
        public string Arb3ID { get; set; }
        public string Arb3Name { get; set; }
        private static ProduitDbOracleContext dbt = new ProduitDbOracleContext();
        public bool Equals(Arb3 p)
        {
            if ((object)p == null) { return false; }
            return (Arb1Code==p.Arb1Code)&& (Arb2Code == p.Arb2Code) && (Arb3Name == p.Arb3Name) && (Arb3ID == p.Arb3ID);
        }
        public static IQueryable<Arb3> GetArb3(string secteurnom,string arb1nom)
        { 
            var arb3 = dbt.Arbors.Where(u => (u.Secteur==secteurnom && u.SousArb1 == arb1nom )).Select(u => new Arb3
            { Arb1Code=u.Secteur, Arb2Code = u.SousArb1, Arb3ID = u.SousArb2, Arb3Name = u.SousArb2 }).Distinct();
            return arb3.AsQueryable();
        }
    }

    public class Arb4
    {
        public string Arb1Code { get; set; }
        public string Arb2Code { get; set; }
        public string Arb3Code { get; set; }
        public string Arb4ID { get; set; }
        public string Arb4Name { get; set; }
        private static ProduitDbOracleContext dbt = new ProduitDbOracleContext();
        public bool Equals(Arb4 p)
        {
            if ((object)p == null) { return false; }
            return (Arb1Code == p.Arb1Code) && (Arb2Code == p.Arb2Code) && (Arb3Code == p.Arb3Code) &&(Arb4Name == p.Arb4Name) && (Arb4ID == p.Arb4ID);
        }

        public static IQueryable<Arb4> GetArb4(string secteurnom, string arb1nom, string arb2nom)
        {
            var arb4 = dbt.Arbors.Where(u => (u.Secteur == secteurnom && u.SousArb1 == arb1nom && u.SousArb2 == arb2nom)).Select(u => new Arb4
            { Arb1Code = u.Secteur, Arb2Code = u.SousArb1,Arb3Code=u.SousArb2, Arb4ID = u.SousArb3, Arb4Name = u.SousArb3 }).Distinct();

            return arb4.AsQueryable();
        }

        // Rétourne le libellé en utilisant un code arborescnce
        public static string libelleArbor(string codArbor)
        {
            //Arbor arb = dbt.Arbors.SingleOrDefault(u => u.CodeArbor.Equals(codArbor));

            NewArbor arb = dbt.NewArbors.SingleOrDefault(u => u.CodeSecteur.Equals(codArbor));
            if (arb != null)
            {
                string[]  res = { arb.Secteur,arb.SousSecteur};
                return string.Join(" / ", res);
            }
            return "";
        }
    }

    public class ImportExcelFile
    {
        private string monfichier;
        private ProduitDbOracleContext db = new ProduitDbOracleContext();
        UtilNotifications notif = new UtilNotifications();
        // Gère le fichier excel : Initialisation par le constructeur 
        public ImportExcelFile(string fic)
        {
            this.monfichier = fic;
        }
        public ImportExcelFile()
        {
        }

        // Transformer le fichier excel en une DataTable
        // le 2ème paramètre : indique le type de fichiers ( offres ou produits ) à traiter
        // 1 : Fichier produits   || 2 : Fichier Offres
        public DataTable ConvertExcelToDataTable(string FileName,int i)
        {
           string typFic = "FluxProduit$";
           if (i==2)      typFic = "FluxOffre$";
           DataTable dtResult = null;
            int totalSheet = 0; 
            using (OleDbConnection objConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';"))
            {
                objConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = string.Empty;
                if (dt != null)
                {
                    var tempDataTable = (from dataRow in dt.AsEnumerable()
                                         where !dataRow["TABLE_NAME"].ToString().Contains("FilterDatabase")
                                         select dataRow).CopyToDataTable();
                    dt = tempDataTable;
                    totalSheet = dt.Rows.Count;
                    sheetName = dt.Rows[0]["TABLE_NAME"].ToString();
                }
                cmd.Connection = objConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM ["+typFic+"]";
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "excelData");
                dtResult = ds.Tables["excelData"];
                objConn.Close();
                return dtResult; //Returne une Datatable  
            }
        }

        // Vérifie que les modèles de fichier sont corrects - controle le nbre de colonnes
        public string verifnbreChamps(DataTable dtresult,int nbcol)
        {      
            if (dtresult.Columns.Count != nbcol) 
            {
                return "/!\\  Votre fichier n'est pas correct : "+"Vous devez avoir normalement " +nbcol +" Colonnes  dans votre fichier. Utilisez nos modèles de fichier\n";
            }
            return null;         
        }


        // Vérification du fichier téléchargé par le fournisseur
        public int checkFile(DataTable dtresult, string filename, string idfou,string sigimg, int typfile)
        {
            string nomfic = @"/Rapport_Erreur_Produit.txt";
            bool estprod = true;
            int nbErrors = 0;
            if (typfile == 2)
            {
                nomfic = @"/Rapport_Erreur_Offre.txt"; estprod = false;
            }
            int[] t = { 34, 9};
            string texte = "";
            using (StreamWriter file = new StreamWriter(File.Open(filename + nomfic, FileMode.Create), Encoding.GetEncoding("iso-8859-1")))
            {
                // On génère le rapport d'erreurs
                file.WriteLine("########  -- Journal des Erreurs  " + DateTime.Now.ToString(CultureInfo.GetCultureInfo("fr-FR")) + " --  ##########\n");
                if(verifnbreChamps(dtresult,t[typfile-1])!=null)
                {
                    file.WriteLine(verifnbreChamps(dtresult, t[typfile- 1]));
                    nbErrors++;
                }
                else
                {
                    for(int i=1; i<dtresult.Rows.Count;i++)
                    {
                        if (estprod)
                        {
                            texte = testlignePro(dtresult.Rows[i], i + 1, idfou, sigimg);
                            if(texte != "")
                                file.WriteLine(Environment.NewLine);
                        }
                        else
                        {
                            texte = testligneOff(dtresult.Rows[i], i + 1, idfou);
                            if (texte != "")
                                file.WriteLine(Environment.NewLine);
                        }
                        file.Write(texte);
                        if (texte != "")
                            nbErrors++;
                    }
                    //envoi rapport ajout/changement d'offre/dispo
                }

                file.WriteLine(Environment.NewLine);
                file.WriteLine("Fin");
            }
            return nbErrors;
        }
        // Vérifie que le champ n'est pas vide
        private string valideForOblig (string contenuchamp, string nomchamp)
        {
            if (string.IsNullOrEmpty(contenuchamp) || string.IsNullOrWhiteSpace(contenuchamp))
            {
                return "Le champ " + nomchamp.ToUpper() +  " ne peut pas être vide\n";
            }
            return null;
        }
        // Vérifie que les deux champs ne soient pas vides
        private string valideForOblig2(string contenuchamp1, string contenuchamp2)
        {
            if (string.IsNullOrEmpty(contenuchamp1) && string.IsNullOrWhiteSpace(contenuchamp1) && string.IsNullOrEmpty(contenuchamp2) && string.IsNullOrWhiteSpace(contenuchamp2))
            {
                return "Les champs 'Prix de Vente TTC du Produit' et 'Prix d'achat HT' sont tous les deux vides! \n ";
            }
            return null;
        }
        // Vérifie que le nombre de caractères ne dépasse pas le max
        private string valideForTaille(string contenuchamp, string nomchamp,int taillechamp)
        {
            if (contenuchamp.Length>taillechamp)
            {
                return "Le champ " + nomchamp.ToUpper() + " prend au maximum " +taillechamp+"  caractères\n";
            }
            return null;
        }
        // Vérifie que les champs à cocher(EX. Type sol, Per. recolt,...) respectent le format 01-03-05
        private string valideForRegex(string contenuchamp, string nomchamp)
        {
            Regex myRegex = new Regex(@"^[-0-9]*$");
            if (!string.IsNullOrEmpty(contenuchamp) && ! string.IsNullOrWhiteSpace(contenuchamp) && !myRegex.IsMatch(contenuchamp.Replace(" ", String.Empty)))
            {
                return "Le champ " + nomchamp.ToUpper() + " doit être sous la forme 01-03-05 par exemple. Evitez les espaces !\n";
            }
            return null;
        }
        // Vérifie que le champ contient bien un nombre
        private string validfornbre(string contenuchamp, string nomchamp)
        {
            double number1 = 0;
            if (contenuchamp.Contains(","))
                contenuchamp = contenuchamp.Replace(",", ".");
            bool canConvert = double.TryParse(contenuchamp, out number1);
            if (!string.IsNullOrEmpty(contenuchamp) && !string.IsNullOrWhiteSpace(contenuchamp) && !canConvert) 
            {
                return "Le champ " + nomchamp.ToUpper() + " doit être un nombre\n";
            }
            return null;
        }
        // Verifie que la restriction de valeurs sur certains champs est respectée
        private string validValPossible(string contenuchamp, string nomchamp, string[] choixVal )
        {
            if (!string.IsNullOrEmpty(contenuchamp) && !string.IsNullOrWhiteSpace(contenuchamp)&&!choixVal.Contains(contenuchamp))
            {
                
                return "Les valeurs possibles dans " + nomchamp.ToUpper() + " sont : [ " +string.Join(",",choixVal)+" ]\n";
            }
            return null;
        }

        // Vérifie l'extension d'un champ image du fichier
        private string ValidExtIm(string contenuchamp, string nomchamp,string extension)
        {
            char[] pt = { '.' };
            if(!string.IsNullOrEmpty(contenuchamp) && !string.IsNullOrWhiteSpace(contenuchamp) && !contenuchamp.Contains("."))
            {
                return "Le nom de fichier dans " + nomchamp.ToUpper() + " n'est pas correct.\n";
            }
            if(!string.IsNullOrEmpty(contenuchamp)&&!string.IsNullOrWhiteSpace(contenuchamp)&& contenuchamp.Length>4 && (!contenuchamp.Split(pt)[1].Equals(extension))&& (!contenuchamp.Split(pt)[1].Equals(extension.ToUpper())))
            {
                return "L'extension dans " + nomchamp.ToUpper() + " doit être en '" + extension + "' \n";
            }
            return null;
        }

        // Vérifie qu'il n'y a pas d'espace dans les noms d'images
        private string NomImgSansEsp(string contenuchamp, string nomchamp)
        {
            
            if (!string.IsNullOrEmpty(contenuchamp) && !string.IsNullOrWhiteSpace(contenuchamp) && contenuchamp.Contains(" "))
            {
                return "Le nom de fichier dans " + nomchamp.ToUpper() + " ne doit pas contenir d'espaces.\n";
            }           
            return null;
        }

        // vérifie qu'on enregistre bien une offre sur un produit déjà existant
        public string validCodPro(string idfou,string codpro)
        {
            if(!string.IsNullOrEmpty(idfou)&&!string.IsNullOrEmpty(codpro))
            {
                var p = db.Produits.SingleOrDefault(d => (d.IdFou.Equals(idfou) && d.CodProFou.Equals(codpro)));
                if (p == null)
                {
                    return "Le Code produit Fournisseur " + codpro + " n'existe pas .\n";
                }
            }       
           return null;
       }
        // Vérifie qu'une ligne du fichier (un produit) est bien valide et affiche les erreurs
        private string testlignePro(DataRow maligne,int i,string idfou,string sigimg)
        {
            string sstitre = "------------  Ligne " + (i+1) + " : Référence Produit *** " + maligne[0].ToString() + " *** ---------------\n";
            string res = sstitre;
           
            res += valideForOblig(maligne[0].ToString(), "Code produit Fournisseur");
            // RAS : EAN 1
            res += validfornbre(maligne[1].ToString(), "Code EAN");


            res += valideForOblig(maligne[2].ToString(), "Désignation du produit");
            res += valideForTaille(maligne[2].ToString(), "Désignation du produit",40);

            // RAS : Nom Latin

            res += valideForOblig(maligne[4].ToString(), "Libellé bon de livraison");
            res += valideForTaille(maligne[4].ToString(), "Libellé bon de livraison", 40);

            res += valideForOblig(maligne[5].ToString(), "Descriptif du produit");
            res += valideForTaille(maligne[5].ToString(), "Descriptif du produit", 600);

            res += valideForOblig(maligne[6].ToString(), "Durée de garantie");
            res += validValPossible(maligne[6].ToString(), "Durée garantie",  new string[] { "1 an", "2 ans", "3 ans", "4 ans", "5 ans"  });

            res += valideForOblig(maligne[7].ToString(), "Catégorie Arborescence");
            //Nouvelle arborescence est alphanumérique
            //res += validfornbre(maligne[7].ToString(), "Catégorie Arborescence");

            // RAS : Slogan 8

            res += valideForOblig(maligne[9].ToString(), "Qualité Livrée");
            res += valideForTaille(maligne[9].ToString(), "Qualité Livrée", 40);

            res += valideForOblig(maligne[10].ToString(), "Couleur");
            res += validValPossible(maligne[10].ToString(), "Couleur", new string[] { "Aucune", "Bleu", "Rouge", "Jaune", "Vert", "Rose", "Violet", "Orange", "Noir", "Blanc", "Multicolore", "Mélange", "Gris" });

            // RAS : Marque 11

            res += validfornbre(maligne[12].ToString(), "Nombre de Pièces/Paquet");

            // RAS : Hauteur adulte 13

            res += valideForTaille(maligne[14].ToString(), "Les Plus du produit (1)", 30);
            res += valideForTaille(maligne[15].ToString(), "Les Plus du produit (2)", 30);
            res += valideForTaille(maligne[16].ToString(), "Les Plus du produit (3)", 30);

            res += valideForOblig(maligne[17].ToString(), "Image principale");
            res += ValidExtIm(maligne[17].ToString(), "Image principale", "jpg");
            res += NomImgSansEsp(maligne[17].ToString(), "Image principale");

            res += ValidExtIm(maligne[18].ToString(), "Image Secondaire 1", "jpg");
            res += NomImgSansEsp(maligne[18].ToString(), "Image Secondaire 1");

            res += ValidExtIm(maligne[19].ToString(), "Image Secondaire 2", "jpg");
            res += NomImgSansEsp(maligne[19].ToString(), "Image Secondaire 2");

            res += ValidExtIm(maligne[20].ToString(), "Image Secondaire 3", "jpg");
            res += NomImgSansEsp(maligne[20].ToString(), "Image Secondaire 3");

            res += ValidExtIm(maligne[21].ToString(), "Image Secondaire 4", "jpg");
            res += NomImgSansEsp(maligne[21].ToString(), "Image Secondaire 4");

            res += ValidExtIm(maligne[22].ToString(), "Image Secondaire 5", "jpg");
            res += NomImgSansEsp(maligne[22].ToString(), "Image Secondaire 5");

            // RAS : Lien Youtube 23
            res += ValidExtIm(maligne[24].ToString(), "Fiche PDF", "pdf");

            res += valideForRegex(maligne[25].ToString(), "Période de plantation");
            res += valideForRegex(maligne[26].ToString(), "Période de floraison");
            res += valideForRegex(maligne[27].ToString(), "Période de semis");
            res += valideForRegex(maligne[28].ToString(), "Période de récolte");
            res += valideForRegex(maligne[29].ToString(), "Période de livraison");
            res += valideForRegex(maligne[30].ToString(), "Type de sol");
            res += valideForRegex(maligne[31].ToString(), "Exposition");
            res += valideForRegex(maligne[32].ToString(), "Type d'utilisation");

            res += valideForOblig(maligne[33].ToString(), "Type de Livraison");
            res += validValPossible(maligne[33].ToString(), "Type de Livraison", new string[] { "00-Depart Fournisseur", "01-Depart Willemse" });
            if(res.Equals(sstitre)) {
                insertInBDProd(maligne,idfou,sigimg);      
                return "";
            }
            return res;
        }

        // Valide une ligne de fichier d'offres
        private string testligneOff(DataRow maligne, int i,string idfou)
        {
            string sstitre = "------------  Ligne " + (i + 1) + " : Référence Produit *** " + maligne[0].ToString() + " *** ---------------\n";
            string res = sstitre;
            res += valideForOblig(maligne[0].ToString(), "Code Produit Fournisseur");
            res += validCodPro(idfou, maligne[0].ToString());

            res+= valideForOblig(maligne[1].ToString(), "Désignation du Produit");

            res += valideForOblig(maligne[2].ToString(), "Prix d'achat HT"); 
            res += validfornbre(maligne[2].ToString(), "Prix d'achat HT");

            // RAS : 3 Frais de livraison

            // RAS : 4 Prix de vente conseillé

            // RAS : 5 Ecotaxe

            res += valideForOblig(maligne[6].ToString(), "Tva");
            res += validValPossible(maligne[6].ToString(), "tva", new string[] { "Taux normal (20%)", "Taux réduit (10%)" });

            res += valideForOblig(maligne[7].ToString(), "Delai de livraison");
            res += validValPossible(maligne[7].ToString(), "Delai de livraison", new string[] { "1 Semaine", "1 à 2 Semaines", "2 à 3 Semaines", "3 à 4 Semaines", "4 à 6 Semaines", "6 à 8 Semaines" });

            res += valideForOblig(maligne[8].ToString(), "Produit Disponible");
            res += validValPossible(maligne[8].ToString(), "Produit Disponible", new string[] { "Oui", "Epuisé/Supprimé", "Momentanément indisponible" });
            if (res.Equals(sstitre))
            {
                insertInBDOff(maligne, idfou);
                return "";
            }
            return res;
        }


        //insertion des offres dans la base de données par fichier
        private void insertInBDOff(DataRow maligne, string idfou)
        {
            bool newoff = false;
            bool modif = false;
            string[] line = { "", "", "", "", "", "", "", "", "", "", "", "", ""};
            line[0] = idfou;
            Offre offre;
            string codpro = maligne[0].ToString();
            var pro = db.Produits.SingleOrDefault(u => (u.IdFou == idfou && u.CodProFou == codpro));
            offre = pro.Offre;
            if(offre==null)
            {
                newoff = true;
                offre = new Offre();
                offre.DateCre = DateTime.Now;
            }
            offre.ProduitID = pro.ProduitID;
            line[1] = pro.CodProFou; // code produit fournisseur, car si le produit n'a pas encore été exporté, le code ERP est null
            line[2] = pro.DesignationPro;
            line[3] = offre.PrixAchtHT; // le prix actuel
            if (offre.PrixAchtHT != FormatDonnees.transformeDecimal(maligne[2].ToString()))
                modif = true;
            offre.PrixAchtHT = FormatDonnees.transformeDecimal( maligne[2].ToString());
            line[4] = offre.PrixAchtHT; // le nouveau prix
            if (offre.FraisLiv != FormatDonnees.transformeDecimal(maligne[3].ToString()) && !String.IsNullOrEmpty(maligne[3].ToString()) && !String.IsNullOrWhiteSpace(maligne[3].ToString()))
                modif = true;
            offre.FraisLiv= FormatDonnees.transformeDecimal(maligne[3].ToString());
            line[5] = offre.FraisLiv;
            line[6] = offre.PrixAchtTsp; // prix + fraix actuels
            string achtrsp = "";
            if (!String.IsNullOrEmpty(offre.FraisLiv) && !String.IsNullOrWhiteSpace(offre.FraisLiv)) {
                achtrsp = System.Convert.ToString(decimal.Parse(offre.PrixAchtHT) + decimal.Parse(offre.FraisLiv));
            }else
            {
                achtrsp = offre.PrixAchtHT;
            }
            offre.PrixAchtTsp = achtrsp;
            line[7] = offre.PrixAchtTsp; // nouveau prix + frais
            offre.PrixVtTtc = FormatDonnees.transformeDecimal(maligne[4].ToString());
            line[8] = offre.PrixVtTtc; // prix de vente conseillé
            offre.EcoTaxe = FormatDonnees.transformeDecimal(maligne[5].ToString());
            line[9] = offre.EcoTaxe; // Ecotaxe
            offre.Tva = getTva(maligne[6].ToString());
            line[10] = maligne[6].ToString();
            offre.Delailiv = maligne[7].ToString();
            line[11] = offre.Delailiv;
            if (!newoff && String.Equals(offre.Dispo, "Oui") && !String.Equals(maligne[8].ToString(), offre.Dispo)) {
                notif.ProduitsEpuises(idfou, pro.CodProERP, pro.CodProFou, pro.DesignationPro, maligne[8].ToString());
            } 
            offre.Dispo = maligne[8].ToString();
            line[12] = offre.Dispo;
            offre.Produit = pro;
            offre.DateMod = DateTime.Now;offre.FicOrForm = "Non";
            if (newoff)
            {
                db.Offres.Add(offre);
                notif.NewOffres(line);
            }
            else
            {
                if(modif)
                    notif.UpdateOffres(line);
                db.Entry(offre).State = EntityState.Modified;
            }
            db.SaveChanges();
        }
        private string getTva(string tva)
        {
            if (tva.Equals("Taux normal (20%)")) return "6";
            return "5";
        }

        public string getLibTva(string tva)
        {
            if (tva.Equals("6")) return "Taux normal (20%)";
            return "Taux réduit (10%)";
        }

        // insert une ligne de produits du fichier valide dans la base de données
        private void insertInBDProd(DataRow maligne,string idfou,string sigimg)
        {
            bool newprod = false;
            bool modif = false;
            string[] line = { "", "", "", ""};
            string codpro = maligne[0].ToString().Replace(" ","");
            var produit = db.Produits.SingleOrDefault(u => (u.IdFou == idfou && u.CodProFou == codpro ));
            if (produit == null)
            {
                newprod = true;
                produit = new Produit();
            }
            produit.IdFou = idfou;
            line[0] = idfou;          
            produit.CodProFou = codpro;
            line[1] = codpro;

            if (!String.Equals(produit.EAN, maligne[1].ToString()) && !String.IsNullOrWhiteSpace(maligne[1].ToString()) && !String.IsNullOrEmpty(maligne[1].ToString())) {
                line[3] += " - EAN";
                modif = true;
            }
                
            produit.EAN = maligne[1].ToString();

            if (!String.Equals(produit.DesignationPro, maligne[2].ToString()) && !String.IsNullOrWhiteSpace(maligne[2].ToString()) && !String.IsNullOrEmpty(maligne[2].ToString())) {
                modif = true;
                line[3] += " - Designation produit";
            }
            produit.DesignationPro = maligne[2].ToString();
            line[2] = produit.DesignationPro;

            if (!String.Equals(produit.DesignationLat, maligne[3].ToString()) && !String.IsNullOrWhiteSpace(maligne[3].ToString()) && !String.IsNullOrEmpty(maligne[3].ToString()))
            {
                modif = true;
                line[3] += " - Designation latine";
            }
            produit.DesignationLat = maligne[3].ToString();

            if (!String.Equals(produit.LibBonLiv, maligne[4].ToString()) && !String.IsNullOrWhiteSpace(maligne[4].ToString()) && !String.IsNullOrEmpty(maligne[4].ToString()))
            {
                modif = true;
                line[3] += " - Libelle bon de livraison";
            }
            produit.LibBonLiv = maligne[4].ToString();

            if (!String.Equals(produit.DescPro, FormatDonnees.echapp(maligne[5].ToString())) && !String.IsNullOrWhiteSpace(maligne[5].ToString()) && !String.IsNullOrEmpty(maligne[5].ToString()))
            {
                modif = true;
                line[3] += " - Descriptif produit";
            }
            produit.DescPro = FormatDonnees.echapp(maligne[5].ToString());

            if (!String.Equals(produit.DureeGarantie, maligne[6].ToString()) && !String.IsNullOrWhiteSpace(maligne[6].ToString()) && !String.IsNullOrEmpty(maligne[6].ToString()))
            {
                modif = true;
                line[3] += " - Duree garantie";
            }
            produit.DureeGarantie = maligne[6].ToString();



            // Test anciene ou nouvelle arborescence
            Regex r = new Regex("^[a-zA-Z0-9]*[a-zA-Z]+$"); // si alphanumeric alors nouvelle arborescence
            if (r.IsMatch(maligne[7].ToString().Replace(" ","")))
            {
                if (!String.Equals(produit.CodeSecteur, maligne[7].ToString()) && !String.IsNullOrWhiteSpace(maligne[7].ToString()) && !String.IsNullOrEmpty(maligne[7].ToString()))
                {
                    modif = true;
                    line[3] += " - Categorie arborescence";
                }
                produit.CodeSecteur = maligne[7].ToString();
            }
            else
            {
                if (!String.Equals(produit.CatArborIn, maligne[7].ToString()) && !String.IsNullOrWhiteSpace(maligne[7].ToString()) && !String.IsNullOrEmpty(maligne[7].ToString()))
                {
                    modif = true;
                    line[3] += " - Categorie arborescence";
                }
                produit.CatArborIn = maligne[7].ToString();
            }
                       

            if (!String.Equals(produit.Slogan, maligne[8].ToString()) && !String.IsNullOrWhiteSpace(maligne[8].ToString()) && !String.IsNullOrEmpty(maligne[8].ToString()))
            {
                modif = true;
                line[3] += " - Slogan";
            }
            produit.Slogan = maligne[8].ToString();

            if (!String.Equals(produit.QuaLiv, maligne[9].ToString()) && !String.IsNullOrWhiteSpace(maligne[9].ToString()) && !String.IsNullOrEmpty(maligne[9].ToString()))
            {
                modif = true;
                line[3] += " - Qualite livree";
            }
            produit.QuaLiv = maligne[9].ToString();

            if (!String.Equals(produit.Couleur, maligne[10].ToString()) && !String.IsNullOrWhiteSpace(maligne[10].ToString()) && !String.IsNullOrEmpty(maligne[10].ToString()))
            {
                modif = true;
                line[3] += " - Couleur";
            }
            produit.Couleur = maligne[10].ToString();

            if (!String.Equals(produit.Marque, maligne[11].ToString()) && !String.IsNullOrWhiteSpace(maligne[11].ToString()) && !String.IsNullOrEmpty(maligne[11].ToString()))
            {
                modif = true;
                line[3] += " - Marque";
            }
            produit.Marque = maligne[11].ToString();

            if (!String.Equals(produit.NbrePcsPaq, maligne[12].ToString()) && !String.IsNullOrWhiteSpace(maligne[12].ToString()) && !String.IsNullOrEmpty(maligne[12].ToString()))
            {
                modif = true;
                line[3] += " - Nombre pirèces par paquet";
            }
            produit.NbrePcsPaq = maligne[12].ToString();

            if (!String.Equals(produit.Hauteur, maligne[13].ToString()) && !String.IsNullOrWhiteSpace(maligne[13].ToString()) && !String.IsNullOrEmpty(maligne[13].ToString()))
            {
                modif = true;
                line[3] += " - Hauteur";
            }
            produit.Hauteur = maligne[13].ToString();

            if (!String.Equals(produit.PlusProd1, maligne[14].ToString()) && !String.IsNullOrWhiteSpace(maligne[14].ToString()) && !String.IsNullOrEmpty(maligne[14].ToString()))
            {
                modif = true;
                line[3] += " - Plus du produit 1";
            }
            produit.PlusProd1 = maligne[14].ToString();

            if (!String.Equals(produit.PlusProd2, maligne[15].ToString()) && !String.IsNullOrWhiteSpace(maligne[15].ToString()) && !String.IsNullOrEmpty(maligne[15].ToString()))
            {
                modif = true;
                line[3] += " - Plus du produit 2";
            }
            produit.PlusProd2 = maligne[15].ToString();

            if (!String.Equals(produit.PlusProd3, maligne[16].ToString()) && !String.IsNullOrWhiteSpace(maligne[16].ToString()) && !String.IsNullOrEmpty(maligne[16].ToString()))
            {
                modif = true;
                line[3] += " - Plus du produit 2";
            }
            produit.PlusProd3 = maligne[16].ToString();

            // produit.EcoTaxe = FormatDonnees.transformeDecimal(maligne[16].ToString());


            /***************************************************************** IMAGES *********************************************************************/


            // Image principale
            if (!String.IsNullOrEmpty(sigimg))
            {
                if (!String.Equals(produit.ImgPrinc, sigimg + "-" + maligne[17].ToString().Replace(sigimg, "")))
                {
                    modif = true;
                    line[3] += " - Image principale";
                }
                produit.ImgPrinc = sigimg + "-" + maligne[17].ToString();
            }
            else {
                if (!String.Equals(produit.ImgPrinc, maligne[17].ToString()))
                {
                    modif = true;
                    line[3] += " - Image principale";
                }
                produit.ImgPrinc = maligne[17].ToString();
            }



            // Image secondaire 1
            if(!string.IsNullOrEmpty(maligne[18].ToString()) && !string.IsNullOrWhiteSpace(maligne[18].ToString()))
            {
                if (!String.IsNullOrEmpty(sigimg))
                {
                    if (!String.Equals(produit.ImgSecond1, sigimg + "-" + maligne[18].ToString().Replace(sigimg, "")))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 1";
                    }
                    produit.ImgSecond1 = sigimg + "-" + maligne[18].ToString().Replace(sigimg, "");
                }
                else
                {
                    if (!String.Equals(produit.ImgSecond1, maligne[18].ToString()))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 1";
                    }
                    produit.ImgSecond1 = maligne[18].ToString();
                }

            }


            // Image secondaire 2
            if (!string.IsNullOrEmpty(maligne[19].ToString()) && !string.IsNullOrWhiteSpace(maligne[19].ToString()))
            {
                if (!String.IsNullOrEmpty(sigimg))
                {
                    if (!String.Equals(produit.ImgSecond2, sigimg + "-" + maligne[19].ToString().Replace(sigimg, "")))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 2";
                    }
                    produit.ImgSecond2 = sigimg + "-" + maligne[19].ToString().Replace(sigimg, "");                 
                }
                else
                {
                    if (!String.Equals(produit.ImgSecond2, maligne[19].ToString()))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 2";
                    }
                    produit.ImgSecond2 = maligne[19].ToString();
                }                
            }



            // Image secondaire 3
            if (!string.IsNullOrEmpty(maligne[20].ToString()) && !string.IsNullOrWhiteSpace(maligne[20].ToString()))
            {
                if (!String.IsNullOrEmpty(sigimg))
                {
                    if (!String.Equals(produit.ImgSecond3, sigimg + "-" + maligne[20].ToString().Replace(sigimg, "")))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 3";
                    }
                    produit.ImgSecond3 = sigimg + "-" + maligne[20].ToString().Replace(sigimg, "");
                }
                else
                {
                    if (!String.Equals(produit.ImgSecond3, maligne[20].ToString()))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 3";
                    }
                    produit.ImgSecond3 = maligne[20].ToString();
                }
            }

            // Image secondaire 4
            if (!string.IsNullOrEmpty(maligne[21].ToString()) && !string.IsNullOrWhiteSpace(maligne[21].ToString()))
            {
                if (!String.IsNullOrEmpty(sigimg))
                {
                    if (!String.Equals(produit.ImgSecond4, sigimg + "-" + maligne[21].ToString().Replace(sigimg, "")))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 4";
                    }
                    produit.ImgSecond4 = sigimg + "-" + maligne[21].ToString().Replace(sigimg, "");
                }
                else
                {
                    if (!String.Equals(produit.ImgSecond4, maligne[21].ToString()))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 4";
                    }
                    produit.ImgSecond4 = maligne[21].ToString();
                }
            }


            // Image secondaire 5
            if (!string.IsNullOrEmpty(maligne[22].ToString()) && !string.IsNullOrWhiteSpace(maligne[22].ToString()))
            {
                if (!String.IsNullOrEmpty(sigimg))
                {
                    if (!String.Equals(produit.ImgSecond5, sigimg + "-" + maligne[22].ToString().Replace(sigimg, "")))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 5";
                    }
                    produit.ImgSecond5 = sigimg + "-" + maligne[22].ToString().Replace(sigimg, "");
                }
                else
                {
                    if (!String.Equals(produit.ImgSecond5, maligne[22].ToString()))
                    {
                        modif = true;
                        line[3] += " - Image secondaire 5";
                    }
                    produit.ImgSecond5 = maligne[22].ToString();
                }
            }




            // Lien Youtube
            if (!String.Equals(produit.LienYoutube, maligne[23].ToString()) && !String.IsNullOrWhiteSpace(maligne[23].ToString()) && !String.IsNullOrEmpty(maligne[23].ToString()))
            {
                modif = true;
                line[3] += " - Lien Youtube";
            }
            produit.LienYoutube = maligne[23].ToString();


            // Fiche PDF
            if (!String.Equals(produit.FichePDF, sigimg + "-" + maligne[24].ToString()) && !String.IsNullOrWhiteSpace(maligne[24].ToString()) && !String.IsNullOrEmpty(maligne[24].ToString()))
            {
                modif = true;
                line[3] += " - Fiche PDF";
            }
            if (!string.IsNullOrEmpty(maligne[24].ToString()) && !string.IsNullOrWhiteSpace(maligne[24].ToString()))
            {
                if (!maligne[24].ToString().Contains(sigimg))
                    produit.FichePDF = sigimg + "-" + maligne[24].ToString();
                else
                    produit.FichePDF = maligne[24].ToString();
            }


            /****************************************************** PERIODES ****************************************************************/

            if (!String.Equals(produit.PerPlant, maligne[25].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[25].ToString()) && !String.IsNullOrEmpty(maligne[25].ToString()))
            {
                modif = true;
                line[3] += " - Periode plantation";
            }
            produit.PerPlant = maligne[25].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.PerFlo, maligne[26].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[26].ToString()) && !String.IsNullOrEmpty(maligne[26].ToString()))
            {
                modif = true;
                line[3] += " - Periode floraison";
            }
            produit.PerFlo = maligne[26].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.PerSemis, maligne[27].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[27].ToString()) && !String.IsNullOrEmpty(maligne[27].ToString()))
            {
                modif = true;
                line[3] += " - Periode Semis";
            }
            produit.PerSemis = maligne[27].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.PerRecolte, maligne[28].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[28].ToString()) && !String.IsNullOrEmpty(maligne[28].ToString()))
            {
                modif = true;
                line[3] += " - Periode Recolte";
            }
            produit.PerRecolte = maligne[28].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.PerLiv, maligne[29].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[29].ToString()) && !String.IsNullOrEmpty(maligne[29].ToString()))
            {
                modif = true;
                line[3] += " - Periode livraison";
            }
            produit.PerLiv = maligne[29].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.TypSol, maligne[30].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[30].ToString()) && !String.IsNullOrEmpty(maligne[30].ToString()))
            {
                modif = true;
                line[3] += " - Type de sol";
            }
            produit.TypSol = maligne[30].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.Exposition, maligne[31].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[31].ToString()) && !String.IsNullOrEmpty(maligne[31].ToString()))
            {
                modif = true;
                line[3] += " - Exposition";
            }
            produit.Exposition = maligne[31].ToString().Replace(" ", string.Empty);



            if (!String.Equals(produit.TypUtil, maligne[32].ToString().Replace(" ", string.Empty)) && !String.IsNullOrWhiteSpace(maligne[32].ToString()) && !String.IsNullOrEmpty(maligne[32].ToString()))
            {
                modif = true;
                line[3] += " - Type utilisation";
            }
            produit.TypUtil = maligne[32].ToString().Replace(" ", string.Empty);


            // Type fournisseur DFO ou non DFO
            if (!String.Equals(produit.DFO, maligne[33].ToString()) && !String.IsNullOrWhiteSpace(maligne[33].ToString()) && !String.IsNullOrEmpty(maligne[33].ToString()))
            {
                modif = true;
                line[3] += " - Départ fournisseur";
            }
            produit.DFO = maligne[33].ToString();

            produit.FicOrForm = "Non"; 
            produit.ValdFou = "Oui";
            produit.ValdWill = "Non";
            produit.FlagExportErp = "0";
            produit.DateMod = DateTime.Now;
            produit.ACTIVE = "Oui";

            if (produit.Offre != null) produit.Offre.ValdWill = "Non";
            
            if(newprod)
            {
                produit.DateCre = DateTime.Now;
                db.Produits.Add(produit);
                notif.NewProduits(line);
            }
            else
            {
                db.Entry(produit).State = EntityState.Modified;
                if(modif)
                    notif.UpdateProduits(line);
            }
            db.SaveChanges();
        }

        // Génération d'un fichier CSV contenant tous les produits d'un fournisseur     
        public void CreateCsvFilePro(string fileName, string idFou)
        {
            // string sql;
             var pro = db.Produits.Where(u => u.IdFou.Equals(idFou)).ToList();
             string proline = "Code produit Fournisseur;EAN;Désignation du produit (NOM COMMERCIAL); Désignation Latine; Libellé Bon de livraison; Descriptif du produit; Durée garantie; Catégorie Arborescence Internet;" +
               "Slogan;Qualité livrée;Couleur;Marque;Nombre de pièces par paquet;Hauteur adulte;Les plus du produit (1);Les plus du produit (2);Les plus du produit (3);Image_principale;" +
               "Image secondaire 1;Image secondaire 2;Image secondaire 3;Image secondaire 4;Image secondaire 5;Vidéo Lien youtube;Fiche PDF;Période de plantation;" +
               "Période de floraison;Période de semis;Période de récolte;Période de livraison;Type de sol;Exposition;Type utilisation;Type de Livraison";
            
            using (StreamWriter wfile = new StreamWriter(File.Open(fileName, FileMode.Create), Encoding.GetEncoding("iso-8859-1")))
            {
                /* objConn.Open();
                 OleDbCommand cmd = new OleDbCommand();
                 cmd.Connection = objConn;
                 */
                wfile.WriteLine(proline);
                foreach (Produit p in pro)
                {
                    proline=fNQ(p.CodProFou) + ";" + fNQ(p.EAN) + ";" + fNQ(p.DesignationPro) + ";" + fNQ(p.DesignationLat) + ";" + fNQ(p.LibBonLiv) + ";" + fNQ(p.DescPro) + ";" + fNQ(p.DureeGarantie) + ";" + fNQ(p.CatArborIn) + ";" +
                    fNQ(p.Slogan) + ";" + fNQ(p.QuaLiv) + ";" + fNQ(p.Couleur) + ";" + fNQ(p.Marque) + ";" + fNQ(p.NbrePcsPaq) + ";" + fNQ(p.Hauteur) + ";" + fNQ(p.PlusProd1) + ";" + fNQ(p.PlusProd2) + ";" + fNQ(p.PlusProd3) + ";" + fNQ(p.EcoTaxe) + ";" +
                    fNQ(p.ImgPrinc) + ";" + fNQ(p.ImgSecond1) + ";" + fNQ(p.ImgSecond2) + ";" + fNQ(p.ImgSecond3) + ";" + fNQ(p.ImgSecond4) + ";" + fNQ(p.ImgSecond5) + ";" + fNQ(p.LienYoutube) + ";" + fNQ(p.FichePDF) + ";" +
                    fNQ(p.PerPlant) + ";" + fNQ(p.PerFlo) + ";" + fNQ(p.PerSemis) + ";" + fNQ(p.PerRecolte) + ";" + fNQ(p.PerLiv) + ";" + fNQ(p.TypSol) + ";" + fNQ(p.Exposition) + ";" + fNQ(p.TypUtil) + ";" + fNQ(p.DFO) ;

                    wfile.WriteLine(proline);
                 //   cmd.ExecuteNonQuery();
                }
                //  cmd.Dispose();
              //  objConn.Close();
            }
            db.Dispose();
        }

        // Créé un modèle de fichier excel en utilisant les code produit fournisseur et la désignation commerciale
        public void CreateExcelFileOff(string fileName, string idFou)
        {
            string sql2;
           // var off = db.Offres.Where(u => u.Produit.IdFou.Equals(idFou)).ToList();
            var pro = db.Produits.Where(u => u.IdFou.Equals(idFou)).ToList();
            string rq1 = "insert into [FluxOffre$] (Code_produit_Fournisseur,Désignation_produit)";
            using (OleDbConnection objConn2 = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Mode=ReadWrite;Extended Properties='Excel 12.0;HDR=YES;';"))
            {
                objConn2.Open();
                OleDbCommand cmd2 = new OleDbCommand();
                cmd2.Connection = objConn2;
                foreach (Produit o in pro)
                {
                    sql2 = "values('" + fNQ(o.CodProFou) + "','" + fNQ(o.DesignationPro) +  "')";
                    cmd2.CommandText = rq1 + sql2;
                    cmd2.ExecuteNonQuery();
                }
                cmd2.Dispose();
                objConn2.Close();
            }
            db.Dispose();
        }

        // Génération d'un fichier CSV contenant toutes les offres d'un fournisseur     
        public void CreateCsvFileOff(string fileName, string idFou)
        {
          //  string sql2;
            var off = db.Offres.Where(u => u.Produit.IdFou.Equals(idFou)).ToList();
            string offline = "Code produit Fournisseur;Désignation du produit (NOM COMMERCIAL);Prix d'achat HT;Frais de Livraison;Prix de vente conseillé TTC;Ecotaxe;TVA;Delai de livraison;Produit disponible";
            using(StreamWriter wfile = new StreamWriter(File.Open(fileName, FileMode.Create), Encoding.GetEncoding("iso-8859-1")))
            {
                wfile.WriteLine(offline);
                foreach (Offre o in off)
                {
                    offline = fNQ(o.Produit.CodProFou) + ";" + fNQ(o.Produit.DesignationPro) + ";" +  fNQ(o.PrixAchtHT) + ";" + fNQ(o.FraisLiv) + ";" + fNQ(o.PrixVtTtc) + ";" + fNQ(o.EcoTaxe) + ";" + fNQ(getLibTva(o.Tva)) + ";" + fNQ(o.Delailiv) + ";" + fNQ(o.Dispo);
                    wfile.WriteLine(offline);
                }
            }
            db.Dispose();
        }

        // Permet d'éviter les quotes et les valeurs NULL dans la réquête SQL
        // les virgules aussi pour eviter les conflits dans le csv
        private string fNQ(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return string.Empty;
            s.Replace(Environment.NewLine, " ");
            s=Regex.Replace(s, @"\t|\n|\r|'", " ");
            return s.Replace(";", ",");
        }
        // Vérifie que le fichier téléchargé respecte bien notre modèle
        // A completer
        // TODO
        public Boolean respecteModele(DataTable dt)
        {
            bool rep = true;
            return rep;
        }

    }
   





}