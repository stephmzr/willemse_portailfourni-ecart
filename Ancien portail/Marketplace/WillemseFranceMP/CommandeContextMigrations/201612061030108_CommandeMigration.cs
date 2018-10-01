namespace WillemseFranceMP.ProduitContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommandeMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "WF.MP_COMMANDES",
                c => new
                    {
                        idcmnd = c.Decimal(nullable: false, precision: 10, scale: 0, identity: true),
                        numcmnd = c.String(maxLength: 256),
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
                        solder = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.idcmnd);
            
        }
        
        public override void Down()
        {
            DropTable("WF.MP_COMMANDES");
        }
    }
}
