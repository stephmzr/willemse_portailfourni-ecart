using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WillemseFranceMP.Models
{
    public class GestionErreurFiche
    {
        private static ProduitDbOracleContext dbp = new ProduitDbOracleContext();
        
        // Rapport d'erreur sur les fichiers images manquants
        public static void ecrireRapport(string pathFiches, string idfou)
        {
            string nomfic = @"/Rapport_Erreur_Fiches.txt";
            using (StreamWriter file = new StreamWriter(File.Open(pathFiches + nomfic, FileMode.Create), Encoding.GetEncoding("iso-8859-1")))
            {
                file.WriteLine("########  -- Journal des Erreurs : Fiches PDF   " + DateTime.Now.ToString(CultureInfo.GetCultureInfo("fr-FR")) + " --  ##########\n");
                file.WriteLine(checkfile(pathFiches, idfou));
            }
        }

        // Génération du fichier de rapport d'erreurs
        public static string checkfile(string pathFiches, string idfou)
        {
            string rapport = "";
            var mesprod = dbp.Produits.Where(u => u.IdFou == idfou).ToList();
            var lesfichiersPdf = Directory.GetFiles(pathFiches + @"/").Select(x => Path.GetFileName(x));

            foreach (Produit p in mesprod)
            {
                if ((!string.IsNullOrEmpty(p.ImgPrinc) && (!string.IsNullOrWhiteSpace(p.ImgPrinc)) && (!lesfichiersPdf.Contains(p.FichePDF))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé de fiche PDF avec " + p.FichePDF + " \n";
                }
            }
            rapport += "\n-------------------------------  FIN  -----------------------------------\n";
            return rapport;
        }
    }



    public class GestionErreurImage
    {
        private static ProduitDbOracleContext dbp = new ProduitDbOracleContext();

        // Rapport d'erreur sur les fichiers images manquants
        public static void ecrireRapport(string pathImages,string idfou)
        {
            string nomfic = @"/Rapport_Erreur_Image.txt";
            using (StreamWriter file = new StreamWriter(File.Open(pathImages + nomfic, FileMode.Create), Encoding.GetEncoding("iso-8859-1")))
            {
               file.WriteLine("########  -- Journal des Erreurs : Images   " + DateTime.Now.ToString(CultureInfo.GetCultureInfo("fr-FR")) + " --  ##########\n");
               file.WriteLine(checkfile(pathImages,idfou));
            }
        }
        // Génération du fichier de rapport d'erreurs
        public static string checkfile(string pathImages, string idfou)
        {
            string rapport = "";
            var mesprod = dbp.Produits.Where(u => u.IdFou == idfou).ToList();
            var lesfichiersImages = Directory.GetFiles(pathImages+@"/Images").Select(x=>Path.GetFileName(x));
            
            foreach (Produit p in mesprod)
            {
                if ((!string.IsNullOrEmpty(p.ImgPrinc) && (!string.IsNullOrWhiteSpace(p.ImgPrinc)) && (!lesfichiersImages.Contains(p.ImgPrinc))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé d'image principale avec " + p.ImgPrinc.Substring( p.ImgPrinc.IndexOf('-') + 1 ) + " \n";
                }
                if ((!string.IsNullOrEmpty(p.ImgSecond1) && (!string.IsNullOrWhiteSpace(p.ImgSecond1)) && (!lesfichiersImages.Contains(p.ImgSecond1))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé d'image sécondaire 1 avec  " + p.ImgSecond1.Substring(p.ImgSecond1.IndexOf('-') + 1) + " \n";
                }
                if ((!string.IsNullOrEmpty(p.ImgSecond2) && (!string.IsNullOrWhiteSpace(p.ImgSecond2)) && (!lesfichiersImages.Contains(p.ImgSecond2))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé d'image sécondaire 2 avec  " + p.ImgSecond2.Substring(p.ImgSecond2.IndexOf('-') + 1) + " \n";
                }
                if ((!string.IsNullOrEmpty(p.ImgSecond3) && (!string.IsNullOrWhiteSpace(p.ImgSecond3)) && (!lesfichiersImages.Contains(p.ImgSecond3))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé d'image sécondaire 3 avec  " + p.ImgSecond3.Substring(p.ImgSecond3.IndexOf('-') + 1) + " \n";
                }
                if ((!string.IsNullOrEmpty(p.ImgSecond4) && (!string.IsNullOrWhiteSpace(p.ImgSecond4)) && (!lesfichiersImages.Contains(p.ImgSecond4))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé d'image sécondaire 4 avec  " + p.ImgSecond4.Substring(p.ImgSecond4.IndexOf('-') + 1) + " \n";
                }
                if ((!string.IsNullOrEmpty(p.ImgSecond5) && (!string.IsNullOrWhiteSpace(p.ImgSecond5)) && (!lesfichiersImages.Contains(p.ImgSecond5))))
                {
                    rapport += "Le produit " + p.CodProFou + " n'a pas trouvé d'image sécondaire 5 avec  " + p.ImgSecond5.Substring(p.ImgSecond5.IndexOf('-') + 1) + " \n";
                }
            }
            rapport += "\n------------------------  FIN  -----------------------------------\n";
            return rapport;
        }

        // Utiliser par le CreateViewModel pour tester l'extension des images - génère un petit message
        public static string[]  ImageExtension(HttpPostedFileBase file,string[] extensions,string display,string nomchamp )
        {
            if(file != null && file.ContentLength > 0)
            {
                string ext = System.IO.Path.GetExtension(file.FileName);
                if(!extensions.Contains(ext) && !extensions.Contains(ext.ToLower()))
                {
                    return new string[] { "L'extension du fichier dans " + display + " doit être :   " + string.Join(" , ", extensions) + " \n", nomchamp };                  
                }
            }
            return null;
        }
    }

    // Controlle les décimales dans les prix  
    public class FormatDonnees
    {
        public static string transformeDecimal(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return "";
            string res = "";
            int i = -1;
            foreach (char c in s.ToArray())
            {
                if (i == 0 || i == 1) i += 1;
                if(char.IsNumber(c)) res+=c;
                if (c.Equals(',') || c.Equals('.'))
                {
                    res += "."; i += 1;
                }
                if (i == 2) break;
            }
            try
            {
                res= Convert.ToDecimal(res).ToString();
            }
            catch(Exception)
            {
                return "";
            }         
            return res;           
        }

        // Affiche la bonne TVA en fonction du code tva - utile pour l'affichage dans le site web
        public static string transformTva(string codeTVa)
        {
            if(!string.IsNullOrEmpty(codeTVa)&&(codeTVa.Equals("6"))) return "Taux normal (20%)";
            return "Taux réduit (10%)";
        }
        // Permet d'éviter les quotes et les valeurs NULL dans la réquête SQL
        public static string echapp(string s)
        {
            if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s)) return string.Empty;
            s.Replace(Environment.NewLine, " ");
            s = Regex.Replace(s, @"\t|\n|\r", " ");
            return s.Replace(";", ",");
        }


        // retourne l'équivalent des checkbox selectionnés . Exple : avec i = 1; 01-12 retourne janvier|decembre
        public static string getCheckboxValues(string s, int i)
        {
            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s)) return s;

            Dictionary<string, string> dic1 = new Dictionary<string, string>();
            string res = "";
            switch (i)
            {
                case 1:
                    dic1 = new Dictionary<string, string>();
                    dic1.Add("00", "Toute l'année"); dic1.Add("01", "Janvier"); dic1.Add("02", "Fevrier"); dic1.Add("03", "Mars");
                    dic1.Add("04", "Avril"); dic1.Add("05", "Mai"); dic1.Add("06", "Juin"); dic1.Add("07", "Juillet"); dic1.Add("08", "Aout");
                    dic1.Add("09", "Septembre"); dic1.Add("10", "Octombre"); dic1.Add("11", "Novembre"); dic1.Add("12", "Decembre");
                    break;
                case 2:
                    dic1 = new Dictionary<string, string>();
                    dic1.Add("1", "Tout Type"); dic1.Add("2", "Argileux"); dic1.Add("3", "Calcaire");
                    dic1.Add("4", "Sableux"); dic1.Add("5", "Humide"); dic1.Add("6", "Sol Sec"); dic1.Add("7", "Sol Acide");
                    break;
                case 3:
                    dic1 = new Dictionary<string, string>();
                    dic1.Add("13", "Plein Soleil"); dic1.Add("14", "Mi-Ombre"); dic1.Add("15", "Ombre");
                    break;
                case 4:
                    dic1 = new Dictionary<string, string>();
                    dic1.Add("1", "Bordure"); dic1.Add("2", "Couvre-sol"); dic1.Add("3", "Decoration Interieure"); dic1.Add("8", "Massif"); dic1.Add("9", "Rocaille");
                    dic1.Add("4", "Fleur à couper"); dic1.Add("5", "Grimpante"); dic1.Add("6", "Haie"); dic1.Add("7", "Isolé"); dic1.Add("10", "Jardinière/pot");
                    break;
                default: break;

            }         
            foreach(string key in s.Split('-'))
            {
                if (dic1.ContainsKey(key)) res +=";"+ dic1[key];
            }
            if (String.IsNullOrEmpty(res) || String.IsNullOrWhiteSpace(res)) return s;
            return res.Substring(1);
        }


    }

   
}




