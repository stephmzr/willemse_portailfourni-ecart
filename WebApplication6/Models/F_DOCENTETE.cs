//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class F_DOCENTETE
    {
        public Nullable<short> DO_Domaine { get; set; }
        public Nullable<short> DO_Type { get; set; }
        public string DO_Piece { get; set; }
        public byte[] cbDO_Piece { get; set; }
        public Nullable<System.DateTime> DO_Date { get; set; }
        public string DO_Ref { get; set; }
        public string DO_Tiers { get; set; }
        public byte[] cbDO_Tiers { get; set; }
        public Nullable<int> CO_No { get; set; }
        public Nullable<int> cbCO_No { get; set; }
        public Nullable<short> DO_Period { get; set; }
        public Nullable<short> DO_Devise { get; set; }
        public Nullable<decimal> DO_Cours { get; set; }
        public Nullable<int> DE_No { get; set; }
        public Nullable<int> cbDE_No { get; set; }
        public Nullable<int> LI_No { get; set; }
        public Nullable<int> cbLI_No { get; set; }
        public string CT_NumPayeur { get; set; }
        public byte[] cbCT_NumPayeur { get; set; }
        public Nullable<short> DO_Expedit { get; set; }
        public Nullable<short> DO_NbFacture { get; set; }
        public Nullable<short> DO_BLFact { get; set; }
        public Nullable<decimal> DO_TxEscompte { get; set; }
        public Nullable<short> DO_Reliquat { get; set; }
        public Nullable<short> DO_Imprim { get; set; }
        public string CA_Num { get; set; }
        public byte[] cbCA_Num { get; set; }
        public string DO_Coord01 { get; set; }
        public string DO_Coord02 { get; set; }
        public string DO_Coord03 { get; set; }
        public string DO_Coord04 { get; set; }
        public Nullable<short> DO_Souche { get; set; }
        public Nullable<System.DateTime> DO_DateLivr { get; set; }
        public Nullable<short> DO_Condition { get; set; }
        public Nullable<short> DO_Tarif { get; set; }
        public Nullable<short> DO_Colisage { get; set; }
        public Nullable<short> DO_TypeColis { get; set; }
        public Nullable<short> DO_Transaction { get; set; }
        public Nullable<short> DO_Langue { get; set; }
        public Nullable<decimal> DO_Ecart { get; set; }
        public Nullable<short> DO_Regime { get; set; }
        public Nullable<short> N_CatCompta { get; set; }
        public Nullable<short> DO_Ventile { get; set; }
        public Nullable<int> AB_No { get; set; }
        public Nullable<System.DateTime> DO_DebutAbo { get; set; }
        public Nullable<System.DateTime> DO_FinAbo { get; set; }
        public Nullable<System.DateTime> DO_DebutPeriod { get; set; }
        public Nullable<System.DateTime> DO_FinPeriod { get; set; }
        public string CG_Num { get; set; }
        public byte[] cbCG_Num { get; set; }
        public Nullable<short> DO_Statut { get; set; }
        public string DO_Heure { get; set; }
        public Nullable<int> CA_No { get; set; }
        public Nullable<int> cbCA_No { get; set; }
        public Nullable<int> CO_NoCaissier { get; set; }
        public Nullable<int> cbCO_NoCaissier { get; set; }
        public Nullable<short> DO_Transfere { get; set; }
        public Nullable<short> DO_Cloture { get; set; }
        public string DO_NoWeb { get; set; }
        public Nullable<short> DO_Attente { get; set; }
        public Nullable<short> DO_Provenance { get; set; }
        public string CA_NumIFRS { get; set; }
        public Nullable<int> MR_No { get; set; }
        public Nullable<short> DO_TypeFrais { get; set; }
        public Nullable<decimal> DO_ValFrais { get; set; }
        public Nullable<short> DO_TypeLigneFrais { get; set; }
        public Nullable<short> DO_TypeFranco { get; set; }
        public Nullable<decimal> DO_ValFranco { get; set; }
        public Nullable<short> DO_TypeLigneFranco { get; set; }
        public Nullable<decimal> DO_Taxe1 { get; set; }
        public Nullable<short> DO_TypeTaux1 { get; set; }
        public Nullable<short> DO_TypeTaxe1 { get; set; }
        public Nullable<decimal> DO_Taxe2 { get; set; }
        public Nullable<short> DO_TypeTaux2 { get; set; }
        public Nullable<short> DO_TypeTaxe2 { get; set; }
        public Nullable<decimal> DO_Taxe3 { get; set; }
        public Nullable<short> DO_TypeTaux3 { get; set; }
        public Nullable<short> DO_TypeTaxe3 { get; set; }
        public Nullable<short> DO_MajCpta { get; set; }
        public string DO_Motif { get; set; }
        public string CT_NumCentrale { get; set; }
        public byte[] cbCT_NumCentrale { get; set; }
        public string DO_Contact { get; set; }
        public Nullable<short> DO_FactureElec { get; set; }
        public Nullable<short> DO_TypeTransac { get; set; }
        public Nullable<System.DateTime> DO_DateLivrRealisee { get; set; }
        public Nullable<System.DateTime> DO_DateExpedition { get; set; }
        public string DO_FactureFrs { get; set; }
        public byte[] cbDO_FactureFrs { get; set; }
        public string DO_PieceOrig { get; set; }
        public byte[] cbDO_PieceOrig { get; set; }
        public Nullable<System.Guid> DO_GUID { get; set; }
        public Nullable<short> DO_EStatut { get; set; }
        public Nullable<short> DO_DemandeRegul { get; set; }
        public Nullable<int> ET_No { get; set; }
        public Nullable<int> cbET_No { get; set; }
        public Nullable<short> DO_Valide { get; set; }
        public Nullable<short> DO_Coffre { get; set; }
        public string DO_CodeTaxe1 { get; set; }
        public string DO_CodeTaxe2 { get; set; }
        public string DO_CodeTaxe3 { get; set; }
        public Nullable<decimal> DO_TotalHT { get; set; }
        public Nullable<short> DO_StatutBAP { get; set; }
        public Nullable<short> cbProt { get; set; }
        public int cbMarq { get; set; }
        public string cbCreateur { get; set; }
        public Nullable<System.DateTime> cbModification { get; set; }
        public Nullable<int> cbReplication { get; set; }
        public Nullable<short> cbFlag { get; set; }
        public string URGENT { get; set; }
        public string SOCIETE_FACTURATION { get; set; }
        public string CIVILITE_FACTURATION { get; set; }
        public string NOM_FACTURATION { get; set; }
        public string PRENOM_FACTURATION { get; set; }
        public string EMAIL_FACTURATION { get; set; }
        public string TELEPHONE_FACTURATION { get; set; }
        public string ADRESSE_FACTURATION { get; set; }
        public string COMPLEMENT_FACTURATION { get; set; }
        public string CODE_POSTAL_FACTURATION { get; set; }
        public string VILLE_FACTURATION { get; set; }
        public string PAYS_FACTURATION { get; set; }
        public string POINT_RETRAIT_RELAIS { get; set; }
        public string SOCIETE_LIVRAISON { get; set; }
        public string CIVILITE_LIVRAISON { get; set; }
        public string NOM_LIVRAISON { get; set; }
        public string PRENOM_LIVRAISON { get; set; }
        public string EMAIL_LIVRAISON { get; set; }
        public string TELEPHONE_LIVRAISON { get; set; }
        public string ADRESSE_LIVRAISON { get; set; }
        public string COMPLEMENT_LIVRAISON { get; set; }
        public string CODE_POSTAL_LIVRAISON { get; set; }
        public string VILLE_LIVRAISON { get; set; }
        public string PAYS_LIVRAISON { get; set; }
        public string GESTION_COLIS { get; set; }
        public string GENERATION_CONTREMARQUE { get; set; }
        public string LIEN_TRACKING_2 { get; set; }
        public Nullable<System.DateTime> DATE_EXPEDITION { get; set; }
        public string NUMERO_TRACKING { get; set; }
        public string CODE_AVANTAGE { get; set; }
        public Nullable<decimal> DECAGNOTTAGE { get; set; }
        public string LIEN_TRACKING_3 { get; set; }
        public string DATE__LIVRAISON_MOTO { get; set; }
        public Nullable<decimal> TTC_TX10 { get; set; }
        public Nullable<decimal> TTC_TX20 { get; set; }
        public Nullable<decimal> TTC_TXM { get; set; }
        public string LIEN_TRACKING_1 { get; set; }
        public string CODE_PARTENAIRE { get; set; }
        public string MOTIFS_RMBT_RECL { get; set; }
        public string MOTIFS_RETOUR { get; set; }
        public string SAISON { get; set; }
    }
}
