using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Data.OracleClient;
using System.Linq;

namespace WillemseFranceMP.Models
{
    // On peut ajouter des données de profil pour l'utilisateur en ajoutant plus de propriétés à notre classe ApplicationUser
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nom")]
        [MaxLength(256)]
        public string Nom { get; set; }
        [Required]
        [Display(Name = "Prenom")]
        [MaxLength(256)]
        public string Prenom { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        [MaxLength(256)]
        public override string Email { get; set; }
        public string Telephone { get; set; }
        [MaxLength(256)]
        public string Societe { get; set; }
        [MaxLength(256)]
        public string Siret { get; set; }
        [MaxLength(500)]
        public string Adresse { get; set; }
        [MaxLength(256)]
        public string CodePostal { get; set; }
        [MaxLength(256)]
        public string Ville { get; set; }
        [MaxLength(256)]
        public string Pays { get; set; }
        [MaxLength(2000)]
        public string Message { get; set; }
        // Boolean
        // Ici on effectue un controle pour savoir si le compte du fournisseur a été validé.
        [MaxLength(256)]
        public string Sigtie { get; set; }
        [MaxLength(256)]
        public string IdFou { get; set; }
        // Répresente le code Images : traitement dans \\rho\ektas\VracAtraiter
        [MaxLength(256)]
        public string Valzn1 { get; set; }
        [Display(Name = "Reçoit des notifications")]
        [MaxLength(256)]
        public string ValZn2 { get; set; }
        [MaxLength(256)]
        public string ValZn3 { get; set; }
        [Display(Name ="FTP Login")]
        [MaxLength(100)]
        public string FTP_LOGIN { get; set; }
        [Display(Name = "FTP Password")]
        [MaxLength(100)]
        public string FTP_PASS { get; set; }
        [Display(Name = "Activé")]
        [MaxLength(100)]
        public string ACTIVE { get; set; }

        // Détermine si l'utilisateur connecté est un admin
        public static  Boolean estAdmin(ApplicationUser user)
        {
            if (user != null && user.Roles.Any(r => r.RoleId == "1")) return true;
            return false;
        }

        // Détermine si l'utilisateur connecté est un super admin
        public static Boolean estSuperAdmin(ApplicationUser user)
        {
            if (user != null && user.Roles.Any(r => r.RoleId == "3")) return true;
            return false;
        }

        // Détermine si le compte du fournisseur a été validé
        public static Boolean estEnAttente(ApplicationUser user)
        {
            if (user.Sigtie.Equals("Non") || string.IsNullOrEmpty(user.Sigtie)) return true;
            return false;
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Notez qu'authenticationType doit correspondre à l'élément défini dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Ajouter les revendications personnalisées de l’utilisateur ici
            return userIdentity;
        }
    }

    public class ApplicationDbOracleContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbOracleContext()
           : base("name=" + new Parametres().env, throwIfV1Schema: false)
        {
        }
        public static ApplicationDbOracleContext Create()
        {

            return new ApplicationDbOracleContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("WF");
            base.OnModelCreating(modelBuilder);
        }   
    }
}