﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication6.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MappingCatalogueEntities : DbContext
    {
        public MappingCatalogueEntities()
            : base("name=MappingCatalogueEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<pf_colonne_csv> pf_colonne_csv { get; set; }
        public virtual DbSet<pf_contenu_csv> pf_contenu_csv { get; set; }
        public virtual DbSet<pf_csv> pf_csv { get; set; }
        public virtual DbSet<hierarchie_be> hierarchie_be { get; set; }
    }
}
