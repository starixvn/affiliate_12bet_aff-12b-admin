using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using DNAide.Data;

namespace DAL
{
    public class DataAccess
    {        
        /*public static int CasinoDataUpdate(string xmlData, DateTime importDate)
        {
            Hashtable hsParam = new Hashtable();
            //Auto job, Use admin that has priveledge to run this sp
            hsParam.Add("adminid", Common.AdminID);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("isreplacedata", true);
            hsParam.Add("importdate", importDate);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            //return DbHelper.Execute("Aff_CasinoImportData", hsParam, Common.DBType.Affiliate);
            DbResult rs = DbHelper.Execute("Aff_CasinoImportData", hsParam, "Affiliate", 120000);
            return CastAide.AsInt32(rs.Outputs["errcode"]);
        }*/
        public static void CasinoDataUpdate(string xmlData, DateTime importDate, ref int errCode, ref string errMsg)
        {
            switch(Common.ForSystem)
            {
                case 0: //Run casino for all system
                    Aff_CasinoDataUpdate(xmlData, importDate, ref errCode, ref errMsg);
                    IA_CasinoDataUpdate(xmlData, importDate, ref errCode, ref errMsg);
                    break;
                case 1: //Casino Aff 12Bet
                    Aff_CasinoDataUpdate(xmlData, importDate, ref errCode, ref errMsg);
                    break;
                case 2: //Casino IA
                    IA_CasinoDataUpdate(xmlData, importDate, ref errCode, ref errMsg);
                    break;
            }
        }

        public static void Aff_CasinoDataUpdate(string xmlData, DateTime importDate, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            //Auto job, Use admin that has priveledge to run this sp
            hsParam.Add("adminid", Common.AdminID);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("isreplacedata", true);
            hsParam.Add("importdate", importDate.Date);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            //return DbHelper.Execute("Aff_CasinoImportData", hsParam, Common.DBType.Affiliate);
            DbResult rs = DbHelper.Execute("Aff_CasinoImportData", hsParam, "Affiliate", 1200);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);
        }

        public static void IA_CasinoDataUpdate(string xmlData, DateTime importDate, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            //Auto job, Use admin that has priveledge to run this sp
            hsParam.Add("adminid", Common.AdminID);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("winlostdate", importDate.Date);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            //return DbHelper.Execute("Aff_CasinoImportData", hsParam, Common.DBType.Affiliate);
            DbResult rs = DbHelper.Execute("Task_Aff_MemberDailyCasino_IA", hsParam, "IA", 1200);
            errCode = Convert.ToInt32(rs.Outputs["errcode"]);
            errMsg = Convert.ToString(rs.Outputs["errmsg"]);
        }
        
    }
}
