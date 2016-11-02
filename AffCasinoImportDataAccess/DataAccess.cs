using System;
using System.Collections;
using Fanex.Data;

namespace AffCasinoImportDataAccess
{
    public class DataAccess
    {
        public static void ImportCasinoData(int system, int adminId, string xmlData, DateTime importDate, ref int errCode, ref string errMsg)
        {
            switch (system)
            {
                case 0: //Run casino for all system
                    ImportFor12Bet(adminId, xmlData, importDate, ref errCode, ref errMsg);
                    ImportForIA(adminId, xmlData, importDate, ref errCode, ref errMsg);
                    break;

                case 1: //Casino Aff 12Bet
                    ImportFor12Bet(adminId, xmlData, importDate, ref errCode, ref errMsg);
                    break;

                case 2: //Casino IA
                    ImportForIA(adminId, xmlData, importDate, ref errCode, ref errMsg);
                    break;
            }
        }

        private static void ImportFor12Bet(int adminId, string xmlData, DateTime importDate, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            //Auto job, Use admin that has priveledge to run this sp
            hsParam.Add("adminid", adminId);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("isreplacedata", true);
            hsParam.Add("importdate", importDate.Date);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            using (ObjectDb objectDb = new ObjectDb("Aff_CasinoImportData", 1200))
            {
                objectDb.ExecuteNonQuery(hsParam);
                errCode = Convert.ToInt32(objectDb.GetParameterValue("errcode"));
                errMsg = Convert.ToString(objectDb.GetParameterValue("errmsg"));
            }
        }

        private static void ImportForIA(int adminId, string xmlData, DateTime importDate, ref int errCode, ref string errMsg)
        {
            Hashtable hsParam = new Hashtable();
            //Auto job, Use admin that has priveledge to run this sp
            hsParam.Add("adminid", adminId);
            hsParam.Add("xmldata", xmlData);
            hsParam.Add("winlostdate", importDate.Date);
            hsParam.Add("errcode", 0);
            hsParam.Add("errmsg", string.Empty);
            using (ObjectDb objectDb = new ObjectDb("Task_Aff_MemberDailyCasino_IA", 1200))
            {
                objectDb.ExecuteNonQuery(hsParam);
                errCode = Convert.ToInt32(objectDb.GetParameterValue("errcode"));
                errMsg = Convert.ToString(objectDb.GetParameterValue("errmsg"));
            }
        }
    }
}