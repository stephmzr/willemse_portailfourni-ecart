//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortailFournisseur.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class F_DOCLIGNE
    {
        public Nullable<short> DO_Domaine { get; set; }
        public short DO_Type { get; set; }
        public string CT_Num { get; set; }
        public byte[] cbCT_Num { get; set; }
        public string DO_Piece { get; set; }
        public byte[] cbDO_Piece { get; set; }
        public string DL_PieceBC { get; set; }
        public byte[] cbDL_PieceBC { get; set; }
        public string DL_PieceBL { get; set; }
        public byte[] cbDL_PieceBL { get; set; }
        public Nullable<System.DateTime> DO_Date { get; set; }
        public Nullable<System.DateTime> DL_DateBC { get; set; }
        public Nullable<System.DateTime> DL_DateBL { get; set; }
        public Nullable<int> DL_Ligne { get; set; }
        public string DO_Ref { get; set; }
        public byte[] cbDO_Ref { get; set; }
        public Nullable<short> DL_TNomencl { get; set; }
        public Nullable<short> DL_TRemPied { get; set; }
        public Nullable<short> DL_TRemExep { get; set; }
        public string AR_Ref { get; set; }
        public byte[] cbAR_Ref { get; set; }
        public string DL_Design { get; set; }
        public Nullable<decimal> DL_Qte { get; set; }
        public Nullable<decimal> DL_QteBC { get; set; }
        public Nullable<decimal> DL_QteBL { get; set; }
        public Nullable<decimal> DL_PoidsNet { get; set; }
        public Nullable<decimal> DL_PoidsBrut { get; set; }
        public Nullable<decimal> DL_Remise01REM_Valeur { get; set; }
        public Nullable<short> DL_Remise01REM_Type { get; set; }
        public Nullable<decimal> DL_Remise02REM_Valeur { get; set; }
        public Nullable<short> DL_Remise02REM_Type { get; set; }
        public Nullable<decimal> DL_Remise03REM_Valeur { get; set; }
        public Nullable<short> DL_Remise03REM_Type { get; set; }
        public Nullable<decimal> DL_PrixUnitaire { get; set; }
        public Nullable<decimal> DL_PUBC { get; set; }
        public Nullable<decimal> DL_Taxe1 { get; set; }
        public Nullable<short> DL_TypeTaux1 { get; set; }
        public Nullable<short> DL_TypeTaxe1 { get; set; }
        public Nullable<decimal> DL_Taxe2 { get; set; }
        public Nullable<short> DL_TypeTaux2 { get; set; }
        public Nullable<short> DL_TypeTaxe2 { get; set; }
        public Nullable<int> CO_No { get; set; }
        public Nullable<int> cbCO_No { get; set; }
        public Nullable<int> AG_No1 { get; set; }
        public Nullable<int> AG_No2 { get; set; }
        public Nullable<decimal> DL_PrixRU { get; set; }
        public Nullable<decimal> DL_CMUP { get; set; }
        public Nullable<short> DL_MvtStock { get; set; }
        public Nullable<int> DT_No { get; set; }
        public Nullable<int> cbDT_No { get; set; }
        public string AF_RefFourniss { get; set; }
        public byte[] cbAF_RefFourniss { get; set; }
        public string EU_Enumere { get; set; }
        public Nullable<decimal> EU_Qte { get; set; }
        public Nullable<short> DL_TTC { get; set; }
        public Nullable<int> DE_No { get; set; }
        public Nullable<int> cbDE_No { get; set; }
        public Nullable<short> DL_NoRef { get; set; }
        public Nullable<short> DL_TypePL { get; set; }
        public Nullable<decimal> DL_PUDevise { get; set; }
        public Nullable<decimal> DL_PUTTC { get; set; }
        public Nullable<int> DL_No { get; set; }
        public Nullable<System.DateTime> DO_DateLivr { get; set; }
        public string CA_Num { get; set; }
        public byte[] cbCA_Num { get; set; }
        public Nullable<decimal> DL_Taxe3 { get; set; }
        public Nullable<short> DL_TypeTaux3 { get; set; }
        public Nullable<short> DL_TypeTaxe3 { get; set; }
        public Nullable<decimal> DL_Frais { get; set; }
        public Nullable<short> DL_Valorise { get; set; }
        public string AR_RefCompose { get; set; }
        public byte[] cbAR_RefCompose { get; set; }
        public Nullable<short> DL_NonLivre { get; set; }
        public string AC_RefClient { get; set; }
        public Nullable<decimal> DL_MontantHT { get; set; }
        public Nullable<decimal> DL_MontantTTC { get; set; }
        public Nullable<short> DL_FactPoids { get; set; }
        public Nullable<short> DL_Escompte { get; set; }
        public string DL_PiecePL { get; set; }
        public byte[] cbDL_PiecePL { get; set; }
        public Nullable<System.DateTime> DL_DatePL { get; set; }
        public Nullable<decimal> DL_QtePL { get; set; }
        public string DL_NoColis { get; set; }
        public Nullable<int> DL_NoLink { get; set; }
        public Nullable<int> cbDL_NoLink { get; set; }
        public string RP_Code { get; set; }
        public byte[] cbRP_Code { get; set; }
        public Nullable<int> DL_QteRessource { get; set; }
        public Nullable<System.DateTime> DL_DateAvancement { get; set; }
        public string PF_Num { get; set; }
        public byte[] cbPF_Num { get; set; }
        public string DL_CodeTaxe1 { get; set; }
        public string DL_CodeTaxe2 { get; set; }
        public string DL_CodeTaxe3 { get; set; }
        public Nullable<int> DL_PieceOFProd { get; set; }
        public string DL_PieceDE { get; set; }
        public byte[] cbDL_PieceDE { get; set; }
        public Nullable<System.DateTime> DL_DateDE { get; set; }
        public Nullable<decimal> DL_QteDE { get; set; }
        public string DL_Operation { get; set; }
        public Nullable<int> DL_NoSousTotal { get; set; }
        public Nullable<short> cbProt { get; set; }
        public int cbMarq { get; set; }
        public string cbCreateur { get; set; }
        public Nullable<System.DateTime> cbModification { get; set; }
        public Nullable<int> cbReplication { get; set; }
        public Nullable<short> cbFlag { get; set; }
        public Nullable<decimal> PRIX_VENTE_TTC { get; set; }
        public Nullable<System.DateTime> DATE_EXPEDITION_PLANIFIEE { get; set; }
        public Nullable<System.DateTime> cbCreation { get; set; }
        public Nullable<System.Guid> cbCreationUser { get; set; }
        public Nullable<int> CA_No { get; set; }
        public Nullable<int> cbCA_No { get; set; }
        public Nullable<short> DO_DocType { get; set; }
        public byte[] cbHash { get; set; }
        public Nullable<short> cbHashVersion { get; set; }
        public Nullable<System.DateTime> cbHashDate { get; set; }
        public Nullable<int> cbHashOrder { get; set; }
        public string TYPE_MOUVEMENT { get; set; }
    }
}
