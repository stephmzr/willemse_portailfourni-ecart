//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecartcommande_cheque.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ecart_reglement
    {
        public int ER_ID { get; set; }
        public string ER_CA_NUM { get; set; }
        public string ER_CODE_PARTENAIRE { get; set; }
        public string ER_Mode_paiement { get; set; }
        public Nullable<System.DateTime> ER_date_piece { get; set; }
        public string ER_Tiers { get; set; }
        public Nullable<short> ER_type { get; set; }
        public string ER_piece { get; set; }
        public string ER_Oxatis { get; set; }
        public Nullable<decimal> ER_TTC_piece { get; set; }
        public Nullable<decimal> ER_TTC_paiement { get; set; }
        public Nullable<decimal> ER_TTC_reglement { get; set; }
        public Nullable<decimal> ER_ecart { get; set; }
        public string ER_statut { get; set; }
        public Nullable<System.DateTime> ER_Derniere_action { get; set; }
        public string ER_mail_client { get; set; }
        public string ER_Tel_client { get; set; }
        public string ER_portable_client { get; set; }
        public Nullable<decimal> Er_Montant_cagnotte { get; set; }
        public string ER_Commentaire { get; set; }
    }

}
