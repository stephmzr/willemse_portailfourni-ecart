using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WillemseFranceMP.Models
{
    //Cette classe n'est (peut-être) pas necessaire, une autre classe ApplicationUsers ou users récupère les mêmes données
    [Table("ASPNETUSERS")]
    public class Fournisseur : IdentityUser
    {
        [MaxLength(250)]
        public string Nom { get; set; }
        [MaxLength(250)]
        public string Prenom { get; set; }
        [MaxLength(250)]
        public override string Email { get; set; }
        [MaxLength(250)]
        public string Telephone { get; set; }
        [MaxLength(250)]
        public string PassWord { get; set; }
        [MaxLength(250)]
        public string Societe { get; set; }
        [MaxLength(250)]
        public string Adresse { get; set; }
        [MaxLength(250)]
        public string CodePostal { get; set; }
        [MaxLength(250)]
        public string Ville { get; set; }
        [MaxLength(250)]
        public string Pays { get; set; }
        [MaxLength(250)]
        public string Message { get; set; }
        [MaxLength(250)]
        public string Sigtie { get; set; }
        [MaxLength(250)]
        public string IdFou { get; set; }

        public async Task<ClaimsIdentity> GenerateFournisseur(UserManager<Fournisseur> four ) 
        {
            var fournisseurIdentity = await four.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return fournisseurIdentity;

        }
        /*
        public Fournisseur(string nom, string prenom,string email,string mdp)
        {
            Nom = nom;
            Prenom = prenom;
            Email = email;
            PassWord = mdp;
        }
        */
    }


    public class FournisseurDbOracleContext : DbContext
    {
        public FournisseurDbOracleContext() 
            : base("name=" + new Parametres().env)
        {
        }
        //public DbSet<Fournisseur> Fournisseurs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WF");
            base.OnModelCreating(modelBuilder);
        }
    }
}
/*
namespace WillemseFranceMP.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à votre classe ApplicationUser ; consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.
    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Societe { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public string Message { get; set; }
        public string Sigtie { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}*/