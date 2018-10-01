namespace WillemseFranceMP.ProduitContextMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WillemseFranceMP.Models.ProduitDbOracleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"ProduitContextMigrations";
        }

        protected override void Seed(WillemseFranceMP.Models.ProduitDbOracleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }

    internal sealed class ConfigurationCommande : DbMigrationsConfiguration<WillemseFranceMP.Models.CommandeDbOracleContext>
    {
        public ConfigurationCommande()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"CommandeContextMigrations";
        }

        protected override void Seed(WillemseFranceMP.Models.CommandeDbOracleContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
