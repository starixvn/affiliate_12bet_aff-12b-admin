namespace Fanex.Affiliate.DataService.Importer.Repository
{
    using System.Data;
    using Fanex.Affiliate.DataService.Importer.DTO;
    using Fanex.Data;

    public class CasinoDataRepository : BaseRepository, ICasinoDataRepository
    {
        public ResultInfo ImportDataFor12Bet(CasinoDataMetaModel metaModel)
        {
            const string SpName = "Aff_CasinoImportData";

            using (var objectDb = GetObjectDb(SpName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.SetParameterValue("importdate", metaModel.DateImport);
                objectDb.SetParameterValue("isreplacedata", true);

                AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo ImportDataForIncomeAccess(CasinoDataMetaModel metaModel)
        {
            const string SpName = "Task_Aff_MemberDailyCasino_IA";

            using (var objectDb = GetObjectDb(SpName))
            {
                AssignCommonParam(objectDb, metaModel);
                objectDb.SetParameterValue("winlostdate", metaModel.DateImport);

                AddResultParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignCommonParam(IObjectDb objectDb, CasinoDataMetaModel metaModel)
        {
            objectDb.SetParameterValue("adminid", metaModel.AdminId);
            objectDb.SetParameterValue("xmldata",  metaModel.XmlData);
        }
    }
}