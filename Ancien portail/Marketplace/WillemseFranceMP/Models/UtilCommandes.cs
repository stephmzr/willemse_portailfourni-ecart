using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WillemseFranceMP.Models
{

    // Cette classe n'est pas utilisée actuellement, elle sert à ajouter des commandes et leurs suivis par fichier (comme pour les catalogues produit)
    // il faudra implémenter une procédure oracle derrière pour créer et metre à jour des commandes dans l'ERP
    public class UtilCommandes
    {
        private CommandeDbOracleContext db = new CommandeDbOracleContext();
        private Parametres p = new Parametres();


        public int TraitementCSVCommandes(string path, string idfou)
        {
            string pathOnly = Path.GetDirectoryName(path);
            string filename = Path.GetFileName(path);
            try
            {
                DataTable dt = ConvertCSVToDataTable(path);
                if (dt.Columns.Count != 27)
                    return 1;
                if (dt.Rows.Count == 0)
                    return 2;
                else
                    if (!InsertInDBCommandes(dt, idfou))
                    return 3;
                else
                    return 0;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return 3;
            }
        }

        public int TraitementCSVSuiviCommandes(string path, string idfou)
        {
            // TODO
            string pathOnly = Path.GetDirectoryName(path);
            string filename = Path.GetFileName(path);
            try
            {
                DataTable dt = ConvertCSVToDataTable(path);
                if (dt.Columns.Count != 9)
                    return 1;
                if (dt.Rows.Count == 0)
                    return 2;
                else
                    if (!InsertInDBCommandesSuivi(dt, idfou))
                    return 3;
                else
                    return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 3;
            }
        }

        //Convertir les données du fichier en une table de données
        public DataTable ConvertCSVToDataTable(string filename)
        {
            StreamReader DataStreamReader = new StreamReader(@filename, Encoding.Default, true);
            string[] columnNames = DataStreamReader.ReadLine().Split(new string[] { ";" }, StringSplitOptions.None);
            DataTable table = new DataTable("Commandes");
            DataSet dset = new DataSet();
            DataColumn column = null;
            for (int i = 0; i < columnNames.Length; i++)
            {
                column = new DataColumn(columnNames[i]);
                table.Columns.Add(column);
            }
            dset.Tables.Add(table);
            while (!DataStreamReader.EndOfStream)
            {
                dset.Tables["Commandes"].Rows.Add(DataStreamReader.ReadLine().Split(new string[] { ";" }, StringSplitOptions.None));
            }
            DataStreamReader.Close();
            return table;
        }





        public Boolean InsertInDBCommandes(DataTable dt, string idfou)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                Commande commande = new Commande(db.Commandes.Count() + 1);
                if (row[0].ToString().Equals("") || row[0].ToString() == null)
                    return false;
                commande.numcmnd = row[0].ToString();
                if (row[1].ToString().Equals("") || row[1].ToString().Equals(null))
                    return false;
                commande.datecmnd = DateTime.ParseExact(row[1].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                if (row[2].ToString().Equals("") || row[2].ToString() == null)
                    return false;
                commande.datefact = DateTime.ParseExact(row[2].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                if (row[3].ToString().Equals("") || row[3].ToString() == null)
                    return false;
                commande.civilite = row[3].ToString();
                if (row[4].ToString().Equals("") || row[4].ToString() == null)
                    return false;
                commande.nomc = row[4].ToString();
                if (row[5].ToString().Equals("") || row[5].ToString() == null)
                    return false;
                commande.prenomc = row[5].ToString();
                if (row[6].ToString().Equals("") || row[6].ToString() == null)
                    return false;
                commande.adrliv = row[6].ToString();
                commande.compadr = row[7].ToString();
                commande.bplieu = row[8].ToString();
                if (row[9].ToString().Equals("") || row[9].ToString() == null)
                    return false;
                commande.codpost = row[9].ToString();
                if (row[10].ToString().Equals("") || row[10].ToString() == null)
                    return false;
                commande.ville = row[10].ToString();
                if (row[11].ToString().Equals("") || row[11].ToString() == null)
                    return false;
                commande.pays = row[11].ToString();
                commande.telfix = row[12].ToString();
                commande.telport = row[13].ToString();
                if (row[14].ToString().Equals("") || row[14].ToString() == null)
                    return false;
                commande.emailc = row[14].ToString();
                commande.codproerp = row[15].ToString();
                commande.prixht = row[16].ToString();
                commande.qt = row[17].ToString();
                commande.reffou = row[18].ToString();
                if (!row[19].ToString().Equals(null) && !row[19].ToString().Equals(""))
                    commande.datappliv = DateTime.ParseExact(row[19].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                if (!row[20].ToString().Equals(null) && !row[20].ToString().Equals(""))
                    commande.datexp = DateTime.ParseExact(row[20].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                commande.tracking = row[21].ToString();
                commande.transporteur = row[22].ToString();
                if (!row[23].ToString().Equals(null) && !row[23].ToString().Equals(""))
                    commande.datrec = DateTime.ParseExact(row[23].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);
                commande.colisretour = row[24].ToString();
                commande.motifretour = row[25].ToString();
                commande.solder = row[26].ToString();
                commande.idfou = idfou;
                //vérifier si la commande existe déjà : update
                var com = db.Commandes.Where(c => c.idfou == commande.idfou && c.numcmnd == commande.numcmnd && c.codproerp == commande.codproerp && c.datecmnd == commande.datecmnd && c.reffou == commande.reffou && c.qt == commande.qt && c.prixht == commande.prixht).SingleOrDefault();                
                try
                {
                    if(com == null)
                    {
                        db.Commandes.Add(commande);
                    }else
                    {
                        db.Entry(com).State = System.Data.Entity.EntityState.Modified;
                    }
                    db.SaveChanges();
                }catch(DbUpdateException e)
                {
                    Console.WriteLine(HandleDbUpdateException(e).Message);
                    return false;
                }
            }
            return true;
        }

        public Boolean InsertInDBCommandesSuivi(DataTable dt, string idfou)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
                // numéro de commande
                if (row[0].ToString().Equals("") || row[0].ToString() == null)
                    return false;
                string numcmnd = row[0].ToString();
                // référence produit
                if (row[1].ToString().Equals("") || row[0].ToString() == null)
                    return false;
                string reffou = row[1].ToString();

                var commande = db.Commandes.Where(c => c.numcmnd == numcmnd && c.reffou == reffou && c.idfou == idfou).SingleOrDefault();
                if(commande != null)
                {
                    DateTime dateTime;
                    //Date approximative de livraison
                    if (!row[2].ToString().Equals(null) && !row[2].ToString().Equals(""))
                    {
                        //commande.datappliv = DateTime.ParseExact(row[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if(!DateTime.TryParseExact(row[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        {
                            DateTime.TryParseExact(row[2].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                        }
                        commande.datappliv = dateTime;
                    }
                    //Date expédition réelle
                    if (!row[3].ToString().Equals(null) && !row[3].ToString().Equals(""))
                    {
                        //commande.datappliv = DateTime.ParseExact(row[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (!DateTime.TryParseExact(row[3].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        {
                            DateTime.TryParseExact(row[3].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                        }
                        commande.datexp = dateTime;
                    }
                    //N° Tracking
                    commande.tracking = row[4].ToString();
                    //Nom transporteur
                    commande.transporteur = row[5].ToString();
                    //Date reception client
                    if (!row[6].ToString().Equals(null) && !row[6].ToString().Equals(""))
                    {
                        //commande.datappliv = DateTime.ParseExact(row[2].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        if (!DateTime.TryParseExact(row[6].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                        {
                            DateTime.TryParseExact(row[6].ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                        }
                        commande.datrec = dateTime;
                    }
                    //Colis en retour
                    commande.colisretour = row[7].ToString();
                    //Motif retour
                    commande.motifretour = row[8].ToString();
                    // update commande
                    try
                    {
                        db.Entry(commande).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (DbUpdateException e)
                    {
                        Console.WriteLine(HandleDbUpdateException(e).Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }




        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");
            if (!(dbu.InnerException is System.Data.Entity.Core.UpdateException) ||
                !(dbu.InnerException.InnerException is System.Data.SqlClient.SqlException))
            {
                try
                {
                    foreach (var result in dbu.Entries)
                    {
                        builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                    }
                }
                catch (Exception e)
                {
                    builder.Append("Error parsing DbUpdateException: " + e);
                }
            }
            else
            {
                var sqlException = (System.Data.SqlClient.SqlException)dbu.InnerException.InnerException;
                for (int i = 0; i < sqlException.Errors.Count; i++)
                {
                    builder.AppendLine("    SQL Message: " + sqlException.Errors[i].Message);
                    builder.AppendLine("    SQL procedure: " + sqlException.Errors[i].Procedure);
                }
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }
    }
}