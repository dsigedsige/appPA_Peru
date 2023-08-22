import { DetalleGuia } from "./detalleguia.models";

export interface GuiaSap {
  Series:string;
  DocDate:string;
  TaxDate:string;
  DocTotal:string;
  Reference2:string;
  Comments:string;
  U_EXP_CODSN:string;
  U_EXP_NOMBRESN:string;
  U_EXX_FE_SN:string;
  U_PA_AVCREADOPOR:number;
  U_EXX_TIPOOPER:string;
  U_EXX_TIPMOV:string;
  U_EXX_MOTIVTRA:string;
  U_EXX_FE_MODTRA:string;
  U_EXX_FE_DIRSN:string;
  U_EXX_CODTRANS:string;
  U_EXX_NOMTRANS:string;
  U_EXX_RUCTRANS:string;
  U_EXX_DIRTRANS:string;
  U_EXX_LICCONDU:string;
  U_EXX_NOMCONDU:string;
  U_EXX_PLACAVEH:string;
  U_EXP_OTROS:string;
  U_EXP_DEST:string;
  U_EXX_FE_GR_FInicio:string;
  U_EXX_FE_GRPESOTOTAL:string;
  U_EXX_FE_GRDESMOTOTROS:string;
  U_EXX_FE_INDVHM1L :string;
  U_EXX_FE_GRINDRETVH :string;
  U_EXX_FE_GRINDENVV:string;
  U_EXX_FE_GRINDTTDAM:string;
  U_EXX_FE_GRINDVHCT:string;
  U_EXX_FE_GRUMPESOTOTAL:string;
  DocumentLines:DetalleGuia[];
}



