using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WillemseFranceMP.Models
{
    public class CreateProduitViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Code produit Fournisseur ")]
        public string CodProFou { get; set; }

        [Required]
        [Display(Name = "Désignation produit (Nom commercial, pas de nom latin)", Description = "maximum 40 caractères")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string DesignationPro { get; set; }

        [Display(Name = "Désignation latine (végétal)", Description = "maximum 40 caractères")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string DesignationLat { get; set; }

        [Required]
        [Display(Name = "Libellé Bon de Livraison ", Description = "maximum 40 caractères")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string LibBonLiv { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "Maximum 600 Caractères")]
        [Display(Name = "Descriptif du produit ", Description = "maximum 600 caractères")]
        public string DescPro { get; set; }

        [Required]
        [Display(Name = "Durée garantie")]
        public string DureeGarantie { get; set; }

        [Required]
        [Display(Name = "Nombre Pièces/paquet (Unité de vente)")]
        [Range(1,100)]
        public int NbrePcsPaq { get; set; }

        [Display(Name = "Periode de récolte")]
        public string PerRecolte { get; set; }      

        [Required]
        [Display(Name = "Categorie Arborescence")]
        public string Arb1 { get; set; }

        [Required]
        [Display(Name = "Sous Arborescence 1")]
        public string Arb2 { get; set; }

        [Display(Name = "Sous Arborescence 2")]
        public string Arb3 { get; set; }

        [Display(Name = "Sous Arborescence 3")]
        public string Arb4 { get; set; }

        [Display(Name = "Slogan/Avantages")]
        [StringLength(200, ErrorMessage = "Maximum 200 Caractères")]
        public string Slogan { get; set; }

        [Required]
        [StringLength(40,ErrorMessage ="Maximum 40 Caractères")]
        [Display(Name = "Qualité Livrée (contenance ou matière)", Description = "maximum 40 caractères")]
        public string QuaLiv { get; set; }

        [Required]
        [Display(Name = "Couleur")]
        public string Couleur { get; set; }

        [Required]
        [Display(Name ="Type de Livraison")]
        public string DFO { get; set; }

        [Required]
        [Display(Name = "Dimension produit / hauteur plante adulte (en cm)")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string Hauteur { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 Caractères")]
        [Display(Name = "Les plus du produit (1)", Description = "maximum 30 caractères")]
        public string PlusProd1 { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 Caractères")]
        [Display(Name = "Les plus du produit (2)", Description = "maximum 30 caractères")]
        public string PlusProd2 { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 Caractères")]
        [Display(Name = "Les plus du produit (3)", Description = "maximum 30 caractères")]
        public string PlusProd3 { get; set; }

        [Required(ErrorMessage ="Une image principale est obligatoire")]
        [Display(Name = "Image Principale", Description = "Le format optimum de la photo est de : 572 X 450")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImgPrinc { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 1", Description = "Le format optimum de la photo est de : 572 X 450")]
        public HttpPostedFileBase ImgSecond1 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 2", Description = "Le format optimum de la photo est de : 572 X 450")]
        public HttpPostedFileBase ImgSecond2 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 3", Description = "Le format optimum de la photo est de : 572 X 450")]
        public HttpPostedFileBase ImgSecond3 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 4", Description = "Le format optimum de la photo est de : 572 X 450")]
        public HttpPostedFileBase ImgSecond4 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 5", Description = "Le format optimum de la photo est de : 572 X 450")]
        public HttpPostedFileBase ImgSecond5 { get; set; }

        [Display(Name = "Video Lien Youtube")]
        public string LienYoutube { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Fiche PDF")]
        public HttpPostedFileBase FichePDF { get; set; }
                
        [Display(Name = "Periode de plantation")]
        public string PerPlant { get; set; }

        [Display(Name = "Periode de floraison")]
        public string PerFlo { get; set; }

        [Display(Name = "Periode de semis")]
        public string PerSemis { get; set; }

        [Required]
        [Display(Name = "Periode de livraison")]
        public string PerLiv { get; set; }

        [Display(Name = "Type de Sol")]
        public string TypSol { get; set; }

        [Display(Name = "Exposition")]
        public string Exposition { get; set; }

        [Display(Name = "Type d'utilisation")]
        public string TypUtil { get; set; }

        // Les nouveaux champs :
        [Display(Name = "EAN")]
    //    [MaxLength(13, ErrorMessage = "Identifiant numérique de 13 chiffres")]
    //    [MinLength(13, ErrorMessage = "Identifiant numérique de 13 chiffres")]
        [RegularExpression("^[0-9]*", ErrorMessage = "Identifiant numérique de 13 chiffres")]
        public string EAN { get; set; }

        [Display(Name = "Marque")]
        [MaxLength(100, ErrorMessage = "Maximum 100 caractères")]
        public string Marque { get; set; }
        

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
                string[] i1 = GestionErreurImage.ImageExtension(ImgPrinc, new string[] { ".jpg", ".jpeg" },"image principale","ImgPrinc");
                if (i1 != null) yield return new ValidationResult(i1[0], new[] { i1[1] });

                string[] i2 = GestionErreurImage.ImageExtension(ImgSecond1, new string[] { ".jpg", ".jpeg" },"Image secondaire 1" ,"ImgSecond1");
                if (i2 != null) yield return new ValidationResult(i2[0], new[] { i2[1] });

                string[] i3 = GestionErreurImage.ImageExtension(ImgSecond2, new string[] { ".jpg", ".jpeg" }, "Image secondaire 2", "ImgSecond2");
                if (i3 != null) yield return new ValidationResult(i3[0], new[] { i3[1] });

                string[] i4 = GestionErreurImage.ImageExtension(ImgSecond3, new string[] { ".jpg", ".jpeg" }, "Image secondaire 3", "ImgSecond3");
                if (i4 != null) yield return new ValidationResult(i4[0], new[] { i4[1] });

                string[] i5 = GestionErreurImage.ImageExtension(ImgSecond4, new string[] { ".jpg", ".jpeg" }, "Image secondaire 4", "ImgSecond4");
                if (i5 != null) yield return new ValidationResult(i5[0], new[] { i5[1] });

                string[] i6 = GestionErreurImage.ImageExtension(ImgSecond5, new string[] { ".jpg", ".jpeg" }, "Image secondaire 5", "ImgSecond4");
                if (i6 != null) yield return new ValidationResult(i6[0], new[] { i6[1] });

                string[] i7 = GestionErreurImage.ImageExtension(FichePDF, new string[] { ".pdf"},"fiche PDF" ,"FichePDF");
                if (i7 != null) yield return new ValidationResult(i7[0], new[] { i7[1] });
        }
        
    }

    public class EditProduitViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Code produit Fournisseur ")]
        public string CodProFou { get; set; }
        
        [Required]
        [Display(Name = "Désignation produit (Nom commercial)", Description = "maximum 40 caractères")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string DesignationPro { get; set; }

        [Display(Name = "Désignation latine (végétal)", Description = "maximum 40 caractères")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string DesignationLat { get; set; }

        [Required]
        [Display(Name = "Libellé Bon de Livraison ", Description = "maximum 40 caractères")]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        public string LibBonLiv { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "Maximum 600 Caractères")]
        [Display(Name = "Descriptif du produit ", Description = "maximum 600 caractères")]
        public string DescPro { get; set; }

        [Required]
        [Display(Name = "Durée garantie")]
        public string DureeGarantie { get; set; }

        [Required]
        [Display(Name = "Nombre Pièces/paquet (Unité de vente)")]
        [Range(1, 100)]
        public int NbrePcsPaq { get; set; }

        [Display(Name = "Periode de récolte")]
        public string PerRecolte { get; set; }

        [Display(Name = "Arborescence Actuelle")]
        public string ActualArbor { get; set; }

        [Display(Name = "Categorie Arborescence")]
        public string Arb1 { get; set; }

        [Display(Name = "Sous Arborescence 1")]
        public string Arb2 { get; set; }

        [Display(Name = "Sous Arborescence 2")]
        public string Arb3 { get; set; }

        [Display(Name = "Sous Arborescence 3")]
        public string Arb4 { get; set; }

        [Display(Name = "Slogan/Avantages")]
        [StringLength(200, ErrorMessage = "Maximum 200 Caractères")]
        public string Slogan { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Maximum 40 Caractères")]
        [Display(Name = "Qualité Livrée (contenance ou matière)", Description = "maximum 40 caractères")]
        public string QuaLiv { get; set; }

        [Required]
        [Display(Name = "Couleur")]
        public string Couleur { get; set; }

        [Required]
        [Display(Name = "Type de Livraison")]
        public string DFO { get; set; }

        [Required]
        [Display(Name = "Dimension produit / hauteur plante adulte (en cm)")]
        [StringLength(100, ErrorMessage = "Maximum 40 Caractères")]
        public string Hauteur { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 Caractères")]
        [Display(Name = "Plus (1) du produit", Description = "maximum 30 caractères")]
        public string PlusProd1 { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 Caractères")]
        [Display(Name = "Plus (2) du produit", Description = "maximum 30 caractères")]
        public string PlusProd2 { get; set; }

        [StringLength(30, ErrorMessage = "Maximum 30 Caractères")]
        [Display(Name = "Plus (3) du produit", Description = "maximum 30 caractères")]
        public string PlusProd3 { get; set; }

        [Display(Name = "Image Principale", Description = "Le format optimum de la photo est de : 572 X 450")]
        [DataType(DataType.Upload)]
        // [FileExtensions(ErrorMessage ="L'extension de l'image doit être en .jpeg ou .jpg",Extensions = ".jpg,.jpeg")]
        public HttpPostedFileBase ImgPrinc { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 1", Description = "Le format optimum de la photo est de : 572 X 450")]
        //  [FileExtensions(ErrorMessage = "L'extension de l'image doit être un .jpeg ou .jpg", Extensions = ".jpg,.jpeg")]
        public HttpPostedFileBase ImgSecond1 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 2", Description = "Le format optimum de la photo est de : 572 X 450")]
        // [FileExtensions(ErrorMessage = "L'extension de l'image doit être un .jpeg ou .jpg", Extensions = ".jpg,.jpeg")]
        public HttpPostedFileBase ImgSecond2 { get; set; }

        [DataType(DataType.Upload)]
        //  [FileExtensions(ErrorMessage = "L'extension de l'image doit être un .jpeg ou .jpg", Extensions = ".jpg,.jpeg")]
        [Display(Name = "Image Secondaire 3", Description = "Le format optimum de la photo est de : 572 X 450")]
        public HttpPostedFileBase ImgSecond3 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 4", Description = "Le format optimum de la photo est de : 572 X 450")]
        //    [FileExtensions(ErrorMessage = "L'extension de l'image doit être un .jpeg ou .jpg", Extensions = ".jpg,.jpeg")]
        public HttpPostedFileBase ImgSecond4 { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Image Secondaire 5", Description = "Le format optimum de la photo est de : 572 X 450")]
        //    [FileExtensions(ErrorMessage = "L'extension de l'image doit être un .jpeg ou .jpg", Extensions = ".jpg,.jpeg")]
        public HttpPostedFileBase ImgSecond5 { get; set; }

        [Display(Name = "Video Lien Youtube")]
        public string LienYoutube { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Fiche PDF")]
        //   [FileExtensions(ErrorMessage = "L'extension de fichier doit être un .pdf", Extensions = ".pdf")]
        public HttpPostedFileBase FichePDF { get; set; }

        [Display(Name = "Periode de plantation")]
        public string PerPlant { get; set; }

        [Display(Name = "Periode de floraison")]
        public string PerFlo { get; set; }

        [Display(Name = "Periode de semis")]
        public string PerSemis { get; set; }

        [Required]
        [Display(Name = "Periode de livraison")]
        public string PerLiv { get; set; }

        [Display(Name = "Type de Sol")]
        public string TypSol { get; set; }

        [Display(Name = "Exposition")]
        public string Exposition { get; set; }

        [Display(Name = "Type d'utilisation")]
        public string TypUtil { get; set; }


        // Les nouveaux champs :
        [Display(Name = "EAN")]
        //    [MaxLength(13, ErrorMessage = "Identifiant numérique de 13 chiffres")]
        //    [MinLength(13, ErrorMessage = "Identifiant numérique de 13 chiffres")]
        [RegularExpression("^[0-9]*", ErrorMessage = "Identifiant numérique de 13 chiffres")]
        public string EAN { get; set; }


        [Display(Name = "Marque")]
        [MaxLength(100, ErrorMessage = "Maximum 100 caractères")]
        public string Marque { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            string[] i1 = GestionErreurImage.ImageExtension(ImgPrinc, new string[] { ".jpg", ".jpeg" }, "image principale", "ImgPrinc");
            if (i1 != null) yield return new ValidationResult(i1[0], new[] { i1[1] });

            string[] i2 = GestionErreurImage.ImageExtension(ImgSecond1, new string[] { ".jpg", ".jpeg" }, "Image secondaire 1", "ImgSecond1");
            if (i2 != null) yield return new ValidationResult(i2[0], new[] { i2[1] });

            string[] i3 = GestionErreurImage.ImageExtension(ImgSecond2, new string[] { ".jpg", ".jpeg" }, "Image secondaire 2", "ImgSecond2");
            if (i3 != null) yield return new ValidationResult(i3[0], new[] { i3[1] });

            string[] i4 = GestionErreurImage.ImageExtension(ImgSecond3, new string[] { ".jpg", ".jpeg" }, "Image secondaire 3", "ImgSecond3");
            if (i4 != null) yield return new ValidationResult(i4[0], new[] { i4[1] });

            string[] i5 = GestionErreurImage.ImageExtension(ImgSecond4, new string[] { ".jpg", ".jpeg" }, "Image secondaire 4", "ImgSecond4");
            if (i5 != null) yield return new ValidationResult(i5[0], new[] { i5[1] });

            string[] i6 = GestionErreurImage.ImageExtension(ImgSecond5, new string[] { ".jpg", ".jpeg" }, "Image secondaire 5", "ImgSecond4");
            if (i6 != null) yield return new ValidationResult(i6[0], new[] { i6[1] });

            string[] i7 = GestionErreurImage.ImageExtension(FichePDF, new string[] { ".pdf" }, "fiche PDF", "FichePDF");
            if (i7 != null) yield return new ValidationResult(i7[0], new[] { i7[1] });
        }

    }

    public class FicheProduitViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Il faut obligatoirement  un fichier")]
        [Display(Name = "Fichier")]
        [DataType(DataType.Upload)]
       //[FileExtensions(ErrorMessage = "L'extension du fichier doit etre en .xlsx ou en .xls", Extensions = "xlsx")]
        public HttpPostedFileBase FicPro { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FicPro != null && FicPro.ContentLength > 0)
            {
                string ext = System.IO.Path.GetExtension(FicPro.FileName);
                if (!ext.Equals(".xlsx"))
                {
                    yield return new ValidationResult("Le Fichier est incorrect. Il doit être en '.xlsx' ", new[] { "FicPro" });
                }
            }
        }   
    }
    
    public class CreateOffreViewModel 
    {      
        [Display(Name = "Prix de Cession HT (€)")]
        [Required]
        public string PrixAchtHT { get; set; }

        [Display(Name = "Prix de Vente Conseillé")]
        public string PrixVtTtc { get; set; }
        
        [Display(Name = "Prix d'achat + frais (€)")]
        public string PrixAchtTsp { get; set; }

        [Display(Name = "Frais de Livraison (€)")]
        public string FraisLiv { get; set; }


        [Display(Name = "Ecotaxe")]
        public string EcoTaxe { get; set; }

        [Required]
        [Display(Name ="TVA")]
        public string Tva { get; set; }

        [Required]
        [Display(Name ="Delai de livraison")]
        public string Delailiv { get; set; }

        [Required]
        [Display(Name = "Produit Disponible")]
        public string Dispo { get; set; }

    }

    public class DisplayProduitViewModel
    {
        [Required]
        [Display(Name = "Fournisseur ")]
        public string IdFou { get; set; }

        [Required]
        [Display(Name = "Code Produit ")]
        public string CodProFou { get; set; }

        [Required]
        [Display(Name = "Désignation")]
        [StringLength(40)]
        public string DesignationPro { get; set; }

        [Required]
        [Display(Name = "Bon de Livraison ", Description = "maximum 40 caractères")]
        [StringLength(40)]
        public string  LibBonLiv { get; set; }
        /*
        [Required]
        [StringLength(600)]
        [Display(Name = "Descriptif du produit ", Description = "maximum 600 caractères")]
        public string DescPro { get; set; }
        
        [Required]
        [Display(Name = "Durée garantie")]
        public string DureeGarantie { get; set; }

        [Display(Name = "Nombre de Pièces par paquet")]
        public int NbrePcsPaq { get; set; }
        */
        [Display(Name = "Periode de récolte")]
        public string PerRecolte { get; set; }
    }
}

    
