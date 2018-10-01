using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WillemseFranceMP.Models
{
    [Table("MP_COMMANDES")]
    public class Commande
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idcmnd { get; set; }
        [Display(Name = "Numéro de commande")]
        [MaxLength(256)]
        public string numcmnd { get; set; }
        [Display(Name = "Date de commande")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? datecmnd { get; set; }
        [Display(Name = "Date de facturation")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? datefact { get; set; }
        [Display(Name = "Civilité")]
        [MaxLength(256)]
        public string civilite { get; set; }
        [Display(Name = "Nom du client")]
        [MaxLength(256)]
        public string nomc { get; set; }
        [Display(Name = "Prénom du client")]
        [MaxLength(256)]
        public string prenomc { get; set; }
        [Display(Name = "Adresse de livraison")]
        [MaxLength(500)]
        public string adrliv { get; set; }
        [Display(Name = "Complément d'adresse")]
        [MaxLength(500)]
        public string compadr { get; set; }
        [Display(Name = "BP / Lieu-dit")]
        [MaxLength(256)]
        public string bplieu { get; set; }
        [Display(Name = "Code postal")]
        [MaxLength(256)]
        public string codpost { get; set; }
        [Display(Name = "Ville")]
        [MaxLength(256)]
        public string ville { get; set; }
        [Display(Name = "Pays")]
        [MaxLength(256)]
        public string pays { get; set; }
        [Display(Name = "Téléphone fixe")]
        [MaxLength(256)]
        public string telfix { get; set; }
        [Display(Name = "Téléphone portable")]
        [MaxLength(256)]
        public string telport { get; set; }
        [Display(Name = "E-mail du client")]
        [MaxLength(256)]
        public string emailc { get; set; }
        [Display(Name = "Code produit (ERP)")]
        [MaxLength(256)]
        public string codproerp { get; set; }
        [Display(Name = "Prix d'achat HT")]
        [MaxLength(256)]
        public string prixht { get; set; }
        [Display(Name = "Quantité")]
        [MaxLength(256)]
        public string qt { get; set; }
        [Display(Name = "Référence produit")]
        [MaxLength(256)]
        public string reffou { get; set; }
        [Display(Name = "Date approximative de livraison")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? datappliv { get; set; }
        [Display(Name = "Date d'expédition")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? datexp { get; set; }
        [Display(Name = "Numéro de tracking")]
        [MaxLength(256)]
        public string tracking { get; set; }
        [Display(Name = "Transporteur")]
        [MaxLength(256)]
        public string transporteur { get; set; }
        [Display(Name = "Date de réception (client)")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? datrec { get; set; }
        [Display(Name = "Colis en retour")]
        [MaxLength(256)]
        public string colisretour { get; set; }
        [Display(Name = "Motif retour")]
        [MaxLength(256)]
        public string motifretour { get; set; }
        [Display(Name = "Soldé")]
        [MaxLength(256)]
        public string solder { get; set; }
        [Display(Name = "Id fournisseur")]
        [MaxLength(50)]
        public string idfou { get; set; }

        [Display(Name = "Afficher")]
        [MaxLength(50)]
        public string AFFICHER { get; set; }

        [Display(Name ="Commande exportée au fournisseur")]
        [MaxLength(5)]
        public string EXPORTER { get; set; }

        [Display(Name ="Date export au fournisseur")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DATEXPORT { get; set; }

        [Display(Name = "Commande expédiée par le fournisseur")]
        [MaxLength(5)]
        public string EXPEDIER { get; set; }

        [Display(Name = "Remarque sur produit")]
        [MaxLength(256)]
        public string REMARQUE { get; set; }

        [Display(Name = "N° facture fournisseur")]
        [MaxLength(256)]
        public string NUMFACTFOU { get; set; }


        [Display(Name = "Action après retour")]
        [MaxLength(256)]
        public string ACTIONRETOUR { get; set; }
        

        public Commande(int index)
        {
            this.idcmnd = index;
        }

        public Commande()
        {
        }
    }


    public partial class CommandeMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "WF.MP_COMMANDES",
                c => new
                {
                    idcmnd = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                    numcmnd = c.String(maxLength: 256, nullable: false),
                    datecmnd = c.DateTime(),
                    datefact = c.DateTime(),
                    civilite = c.String(maxLength: 256),
                    nomc = c.String(maxLength: 256),
                    prenomc = c.String(maxLength: 256),
                    adrliv = c.String(maxLength: 500),
                    compadr = c.String(maxLength: 500),
                    bplieu = c.String(maxLength: 256),
                    codpost = c.String(maxLength: 256),
                    ville = c.String(maxLength: 256),
                    pays = c.String(maxLength: 256),
                    telfix = c.String(maxLength: 256),
                    telport = c.String(maxLength: 256),
                    emailc = c.String(maxLength: 256),
                    codproerp = c.String(maxLength: 256),
                    prixht = c.String(maxLength: 256),
                    qt = c.String(maxLength: 256),
                    reffou = c.String(maxLength: 256),
                    datappliv = c.DateTime(),
                    datexp = c.DateTime(),
                    tracking = c.String(maxLength: 256),
                    transporteur = c.String(maxLength: 256),
                    datrec = c.DateTime(),
                    colisretour = c.String(maxLength: 256),
                    motifretour = c.String(maxLength: 256),
                    solder = c.String(maxLength: 256)
                })
               .PrimaryKey(t => t.idcmnd)
               .Index(t => t.idcmnd);
        }

        public override void Down()
        {
            DropIndex("WF.MP_COMMANDES", new[] { "idcmnd" });
            DropTable("WF.MP_COMMANDES");

        }
    }

    public class CommandeDbOracleContext : DbContext
    {
        public CommandeDbOracleContext()
           : base("name=" + new Parametres().env)
        {
        }
        public DbSet<Commande> Commandes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Commande>().HasKey(e => e.idcmnd);
            modelBuilder.Entity<Commande>().Property(e => e.idcmnd).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.HasDefaultSchema("WF");
            base.OnModelCreating(modelBuilder);
        }
        
    }
    

}