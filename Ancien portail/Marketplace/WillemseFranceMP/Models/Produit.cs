using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace WillemseFranceMP.Models
{
    [Table("MP_PRODUITS")]
    public class Produit
    {        
        public int ProduitID { get; set; }
        [MaxLength(256)]
        public string IdFou { get; set; }
        [MaxLength(256)]
        public string CodProFou { get; set; }
        [MaxLength(256)]
        public string CodProERP { get; set; }
        [MaxLength(256)]
        public string DesignationPro { get; set; }
        [MaxLength(256)]
        public string DesignationLat { get; set; }
        [MaxLength(256)]
        public string LibBonLiv { get; set; }
        [MaxLength(2000)]
        public string DescPro { get; set; }
        [MaxLength(256)]
        public string DureeGarantie { get; set; }
        [MaxLength(256)]
        public string PerRecolte { get; set; }
        [MaxLength(256)]
        public string CatArborIn { get; set; }
        [MaxLength(256)]
        public string Slogan { get; set; }
        [MaxLength(256)]
        public string QuaLiv { get; set; }
        [MaxLength(256)]
        public string Couleur { get; set; }
        [MaxLength(256)]
        public string NbrePcsPaq { get; set; }
        [MaxLength(256)]
        public string Hauteur { get; set; }
        [MaxLength(2000)]
        public string PlusProd1 { get; set; }
        [MaxLength(2000)]
        public string PlusProd2 { get; set; }
        [MaxLength(2000)]
        public string PlusProd3 { get; set; }
        [MaxLength(2000)]
        public string ImgPrinc { get; set; }
        [MaxLength(2000)]
        public string ImgSecond1 { get; set; }
        [MaxLength(2000)]
        public string ImgSecond2 { get; set; }
        [MaxLength(2000)]
        public string ImgSecond3 { get; set; }
        [MaxLength(2000)]
        public string ImgSecond4 { get; set; }
        [MaxLength(2000)]
        public string ImgSecond5 { get; set; }
        [MaxLength(2000)]
        public string LienYoutube { get; set; }
        [MaxLength(2000)]
        public string FichePDF { get; set; }
        [MaxLength(2000)]
        public string PerPlant { get; set; }
        [MaxLength(250)]
        public string PerFlo { get; set; }
        [MaxLength(250)]
        public string PerSemis { get; set; }
        [MaxLength(250)]
        public string PerLiv { get; set; }
        [MaxLength(250)]
        public string TypSol { get; set; }
        [MaxLength(250)]
        public string Exposition { get; set; }
        [MaxLength(250)]
        public string TypUtil { get; set; }
        [MaxLength(250)]
        public string ValdFou { get; set; }
        [MaxLength(250)]
        public string ValdWill { get; set; }
        [MaxLength(250)]
        public string FicOrForm { get; set; }
        [MaxLength(250)]
        public string DFO { get; set; }
        [MaxLength(10)]
        public string FlagExportErp { get; set; }
        [MaxLength(10)]
        public string FlagExportBE { get; set; }
        [MaxLength(10)]
        public string ACTIVE { get; set; }
        public DateTime DateCre { get; set; }
        public DateTime DateMod { get; set; }
        public virtual Offre Offre { get; set; }

        // lES NOUVEAUX CHAMPS DANS LES TABLES 
        [MaxLength(250)]
        public string EAN { get; set; }

        [MaxLength(250)]
        public string EcoTaxe { get; set; }

        [MaxLength(250)]
        public string Marque { get; set; }

        [MaxLength(250)]
        public string ModeLiv { get; set; }

        [MaxLength(250)]
        public string Poids { get; set; }
        [MaxLength(10)]
        public string CodeSecteur { get; set; }

        // Ces champs seront utilisés pour les prochaines infos

        [MaxLength(250)]
        public string ValZn1 { get; set; }

        [MaxLength(250)]
        public string ValZn2 { get; set; }


    }

    [Table("MP_ARBORS")]
    public class Arbor
    {
        public int ID { get; set; }
        [MaxLength(250)]
        public string CodeArbor { get; set; }
        [MaxLength(250)]
        public string Secteur { get; set; }
        [MaxLength(250)]
        public string SousArb1 { get; set; }
        [MaxLength(250)]
        public string SousArb2 { get; set; }
        [MaxLength(250)]
        public string SousArb3 { get; set; }
    }

    [Table("MP_NEW_ARBORS")]
    public class NewArbor
    {
        [Key]
        [MaxLength(250)]
        public string CodeSecteur { get; set; }
        [MaxLength(250)]
        public string Secteur { get; set; }
        [MaxLength(250)]
        public string SousSecteur { get; set; }
    }


    public class ProduitDbOracleContext : DbContext
    {
        public ProduitDbOracleContext()
         : base("name="+new Parametres().env)
        {
        }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Arbor> Arbors { get; set; }
        public DbSet<NewArbor> NewArbors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WF");
            base.OnModelCreating(modelBuilder);
        }
    }
}



