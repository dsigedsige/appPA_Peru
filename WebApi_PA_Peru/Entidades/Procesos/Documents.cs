using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Procesos
{
    public class Documents
    {
        public string Series { get; set; } = null;
        public string DocDate { get; set; } = null;
        public string TaxDate { get; set; } = null;
        public string DocTotal { get; set; } = null;
        public string Reference2 { get; set; } = null;
        public string Comments { get; set; } = null;
        public string U_EXP_CODSN { get; set; } = null;
        public string U_EXP_NOMBRESN { get; set; } = null;
        public string U_EXX_FE_SN { get; set; } = null;
        public int U_PA_AVCREADOPOR { get; set; }
        public string U_EXX_TIPOOPER { get; set; } = null;
        public string U_EXX_TIPMOV { get; set; } = null;
        public string U_EXX_MOTIVTRA { get; set; } = null;
        public string U_EXX_FE_MODTRA { get; set; } = null;
        public string U_EXX_FE_DIRSN { get; set; } = null;
        public string U_EXX_CODTRANS { get; set; } = null;
        public string U_EXX_NOMTRANS { get; set; } = null; 
        public string U_EXX_RUCTRANS { get; set; } = null;
        public string U_EXX_DIRTRANS { get; set; } = null;
        public string U_EXX_LICCONDU { get; set; } = null;
        public string U_EXX_NOMCONDU { get; set; } = null;
        public string U_EXX_PLACAVEH { get; set; } = null;
        public string U_EXP_OTROS { get; set; } = null;
        public string U_EXP_DEST { get; set; } = null;
        public string U_EXX_FE_GR_FInicio { get; set; } = null;
        public string U_EXX_FE_GRPESOTOTAL { get; set; } = null;
        public string U_EXX_FE_GRDESMOTOTROS { get; set; } = null;
        public string U_EXX_FE_INDVHM1L { get; set; } = null;
        public string U_EXX_FE_GRINDRETVH { get; set; } = null;
        public string U_EXX_FE_GRINDENVV { get; set; } = null;
        public string U_EXX_FE_GRINDTTDAM { get; set; } = null;
        public string U_EXX_FE_GRINDVHCT { get; set; } = null;
        public string U_EXX_FE_GRUMPESOTOTAL { get; set; } = null;
        public List<DocumentLines> DocumentLines { get; set; }
    }


    public class DocumentLines
    {
        public string ItemCode { get; set; } = null;
        public string Dscription { get; set; } = null;
        public string WarehouseCode { get; set; } = null;
        public int    Quantity { get; set; }
        public string UnitPrice { get; set; } = null;
        public string LineTotal { get; set; } = null;
        public int LineId { get; set; }
        public string CostingCode { get; set; } = null;
        public string CostingCode2 { get; set; } = null;
        public string CostingCode3 { get; set; } = null;
        public string AccountCode { get; set; } = null;

    }
}




