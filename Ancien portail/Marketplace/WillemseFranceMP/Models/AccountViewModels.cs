using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WillemseFranceMP.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Mémoriser ce navigateur ?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Courrier électronique")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Display(Name = "Mémoriser le mot de passe ?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nom")]
        [StringLength(100, ErrorMessage = "Trop long Maximum 100 Caractères")]
        public string Nom { get; set; }

        [Required]
        [Display(Name = "Prenom ")]
        [StringLength(100, ErrorMessage = "Trop long Maximum 100 Caractères")]
        public string Prenom { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        [StringLength(100, ErrorMessage = "Trop long Maximum 100 Caractères")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe ")]
        [Compare("Password", ErrorMessage = "Le mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        // const string pattern = @"^(\+\s?)?((?<!\+.*)\(\+?\d+([\s\-\.]?\d+)?\)|\d+)([\s\-\.]?(\(\d+([\s\-\.]?\d+)?\)|\d+))*(\s?(x|ext\.?)\s?\d+)?$";
        // C'est l'expression régulière qui est utilisée pour valider les numéros de téléphones
        [Required]
        [Phone(ErrorMessage = "Le numéro de telephone n'est pas valide")]
        [Display(Name = "Numéro de téléphone")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "Nom Société ")]
        [StringLength(100, ErrorMessage = "Trop long Maximum 100 Caractères")]
        public string Societe { get; set; }

        [Required]
        [Display(Name = "Numéro SIRET ")]
        [MaxLength(14, ErrorMessage = "Identifiant numérique de 14 chiffres")]
        [MinLength(14, ErrorMessage = "Identifiant numérique de 14 chiffres")]
        [RegularExpression("^[0-9]*",ErrorMessage = "Identifiant numérique de 14 chiffres")]
        public string Siret { get; set; }

        [Display(Name = "N° et Nom de rue ")]
        [StringLength(200, ErrorMessage = "Trop long Maximum 200 Caractères")]
        public string Rue { get; set; }

        [Display(Name = "Complement adresse ")]
        [StringLength(200, ErrorMessage = "Trop long Maximum 200 Caractères")]
        public string Complement { get; set; }

        [Display(Name = "Code Postal ")]
        [StringLength(100, ErrorMessage = "Trop long Maximum 100 Caractères")]
        public string CodePostal { get; set; }

        [Display(Name = "Ville ")]
        public string Ville { get; set; }

        [Display(Name = "PAYS ")]
        [StringLength(100, ErrorMessage = "Trop long Maximum 100 Caractères")]
        public string Pays { get; set; }

        [Display(Name = "Message ")]
        [StringLength(600, ErrorMessage = "Le message ne doit pas depasser 600 caractères")]
        public string Message { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Courrier électronique")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
