using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WillemseFranceMP.Models
{
    [Table("MP_OFFRES")]
    public class Offre
    {
        [Key,ForeignKey("Produit")]
        public int ProduitID { get; set; }
        [MaxLength(250)]
        public string PrixVteTtcNonRemise { get; set; }
        [MaxLength(250)]
        public string PourcRemise { get; set; }        
        [MaxLength(250)]
        public string PrixVtTtc { get; set; }
        [MaxLength(250)]
        public string EcoTaxe { get; set; }
        [MaxLength(250)]
        public string PrixAchtHT { get; set; }
        [MaxLength(250)]
        public string PrixAchtTsp { get; set; } // prix d'achat + frais transport
        [MaxLength(250)]
        public string Tva { get; set; }
        [MaxLength(250)]
        public string Delailiv { get; set; }
        [MaxLength(250)]
        public string Dispo { get; set; }
        [MaxLength(250)]
        public string ValdFou { get; set; }
        [MaxLength(250)]
        public string ValdWill { get; set; }
        [MaxLength(250)]
        public string FraisLiv { get; set; }
        [MaxLength(250)]
        public string  FicOrForm { get; set; }
        public DateTime DateCre { get; set; }
        public DateTime DateMod { get; set; }
        public virtual Produit Produit { get; set; }

        // Ces champs seront utilisés pour les prochaines infos

        [MaxLength(250)]
        public string ValZn1 { get; set; }
    }
}