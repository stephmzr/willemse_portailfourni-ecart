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
    
    public partial class ListeProduitsFournisseursEntities : DbContext
    {
        public ListeProduitsFournisseursEntities()
            : base("name=ListeProduitsFournisseursEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<F_ARTFOURNISS> F_ARTFOURNISS { get; set; }
        public virtual DbSet<F_ARTICLE> F_ARTICLE { get; set; }
    }
}
