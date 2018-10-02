﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ExportToCSVController : Controller
    {

        private ApplicationDbContext dbA = new ApplicationDbContext();

        private ApplicationUser CurrentUser
        {
            get
            {
                string currentUserId = User.Identity.GetUserId();
                return dbA.Users.FirstOrDefault(x => x.Id == currentUserId);
            }
        }

        public void ExportToCSV(string id)
        {
            StringWriter sw = new StringWriter();
            
            sw.WriteLine("\"Référence\";\"Désignation\";\"Quantité\";\"Prix unitaire\";\"Société\";\"Nom\";\"Prénom\";\"Email\";\"Adresse Livraison\";\"Téléphone\";\"Téléphone2\";\"Code postal\";\"Ville\"");

            Response.ClearContent();
            Response.ContentEncoding = Encoding.UTF8;
            Response.AddHeader("content-disposition", "attachment;filename=commande_" + id + ".csv");
            Response.ContentType = "application/octet-stream";
            

            ListeCommandesEntities DB = new ListeCommandesEntities();

            var F_DOCLIGNE = DB.F_DOCLIGNE.Where(d => d.DO_Piece == id && d.AF_RefFourniss!="").ToList();
            var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Piece == id).ToList();

            foreach (var ligne in F_DOCLIGNE)
            {
                
                foreach (var commande in F_DOCENTETE)
                {
                    if (String.IsNullOrEmpty(commande.POINT_RETRAIT_RELAIS))
                    {
                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\"",


                            ligne.AF_RefFourniss,
                            ligne.DL_Design,
                            ligne.DL_Qte,
                            ligne.DL_PrixUnitaire,
                            commande.SOCIETE_LIVRAISON,
                            commande.NOM_LIVRAISON,
                            commande.PRENOM_LIVRAISON,
                            commande.EMAIL_LIVRAISON,
                            commande.ADRESSE_LIVRAISON,
                            commande.TELEPHONE_LIVRAISON,
                            commande.TELEPHONE_FACTURATION,
                            commande.CODE_POSTAL_LIVRAISON,
                            commande.VILLE_LIVRAISON


                            ));
                    }
                    else
                    {

                        sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\"",

                            ligne.AF_RefFourniss,
                            ligne.DL_Design,
                            ligne.DL_Qte,
                            ligne.DL_PrixUnitaire,
                            commande.SOCIETE_FACTURATION,
                            commande.NOM_FACTURATION,
                            commande.PRENOM_FACTURATION,
                            commande.EMAIL_FACTURATION,
                            commande.ADRESSE_FACTURATION,
                            commande.TELEPHONE_LIVRAISON,
                            commande.TELEPHONE_FACTURATION,
                            commande.CODE_POSTAL_FACTURATION,
                            commande.VILLE_FACTURATION


                            ));
                    }

                 /*   sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\"",

                    
                    commande.AF_RefFourniss,
                    commande.DL_Design,
                    commande.DL_Qte,
                    commande.DL_PrixUnitaire,
                    infoclient.SOCIETE_LIVRAISON,
                    infoclient.NOM_LIVRAISON,
                    infoclient.PRENOM_LIVRAISON,
                    infoclient.EMAIL_LIVRAISON,
                    infoclient.ADRESSE_LIVRAISON,
                    infoclient.TELEPHONE_LIVRAISON,
                    infoclient.TELEPHONE_FACTURATION,
                    infoclient.CODE_POSTAL_LIVRAISON,
                    infoclient.VILLE_LIVRAISON 

                    ));*/
                }
            }
            Response.ContentEncoding = Encoding.Unicode;
            Response.Write(sw.ToString());
            Response.End();
        }





        public void GenererTousLesCSV()

        {
            
            ListeCommandesEntities DB = new ListeCommandesEntities();

            var F_DOCENTETE = DB.F_DOCENTETE.Where(d => d.DO_Domaine == 1 && d.DO_Type == 12 && !d.DO_Ref.Equals("")).ToList();

            foreach (var commande in F_DOCENTETE)
            {
                string newFile = @"C:\CSV\" + commande.DO_Piece + ".csv";   
                //string newFile = @"C:\Users\SM\source\Portail fournisseur Willemse France\CSV\" + commande.DO_Piece + ".csv";

                if (!System.IO.File.Exists(newFile))
                {

                    Response.ClearContent();                  
                    //Response.AddHeader("content-disposition", "attachment;filename=commande_" + commande.DO_Piece + ".csv");
                    //Response.ContentType = "application/octet-stream";

                    StreamWriter sw = new StreamWriter(new FileStream(newFile, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8);


                    sw.WriteLine("\"Référence\";\"Désignation\";\"Quantité\";\"Prix unitaire\";\"Société\";\"Nom\";\"Prénom\";\"Email\";\"Adresse Livraison\";\"Téléphone\";\"Téléphone2\";\"Code postal\";\"Ville\"");
          

                    // données des produits
                    var F_DOCLIGNE = DB.F_DOCLIGNE.Where(ligne => ligne.DO_Piece.Contains(commande.DO_Piece)).ToList();
                    foreach (var ligne in F_DOCLIGNE)
                    {
                        if (String.IsNullOrEmpty(commande.POINT_RETRAIT_RELAIS))
                        {
                            sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\"",


                                ligne.AF_RefFourniss,
                                ligne.DL_Design,
                                ligne.DL_Qte,
                                ligne.DL_PrixUnitaire,
                                commande.SOCIETE_LIVRAISON,
                                commande.NOM_LIVRAISON,
                                commande.PRENOM_LIVRAISON,
                                commande.EMAIL_LIVRAISON,
                                commande.ADRESSE_LIVRAISON,
                                commande.TELEPHONE_LIVRAISON,
                                commande.TELEPHONE_FACTURATION,
                                commande.CODE_POSTAL_LIVRAISON,
                                commande.VILLE_LIVRAISON


                                ));
                        }
                        else
                        {

                            sw.WriteLine(string.Format("\"{0}\";\"{1}\";\"{2}\";\"{3}\";\"{4}\";\"{5}\";\"{6}\";\"{7}\";\"{8}\";\"{9}\";\"{10}\";\"{11}\";\"{12}\"",

                                ligne.AF_RefFourniss,
                                ligne.DL_Design,
                                ligne.DL_Qte,
                                ligne.DL_PrixUnitaire,
                                commande.SOCIETE_FACTURATION,
                                commande.NOM_FACTURATION,
                                commande.PRENOM_FACTURATION,
                                commande.EMAIL_FACTURATION,
                                commande.ADRESSE_FACTURATION,
                                commande.TELEPHONE_LIVRAISON,
                                commande.TELEPHONE_FACTURATION,
                                commande.CODE_POSTAL_FACTURATION,
                                commande.VILLE_FACTURATION


                                ));
                        }



                    };


                    Response.Write(sw.ToString());
                    Response.End();
                    

                    sw.Close();
                }
                
            }
           
            return;
            
        }
    }
}
    