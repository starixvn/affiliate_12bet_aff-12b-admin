using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Exceptions;
using Fanex.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Importer.Core.Repository
{
    public class ThirdPartyProductRepository : BaseRepository, IThirdPartyProductRepository
    {
        private const string SpName = "Task_APIProduct_ImportQueue";
        public void ImportData(int productId, string xmlData, byte jobType = 0)
        {
            try
            {
                var paramValues = BuildParam(productId, xmlData, jobType);
                ImportData(paramValues);
            }
            catch (Exception ex)
            {
                throw new ThirdPartyProductException(ex.Message, ex);
            }
        }

        public void ImportData(int productId, IEnumerable<string> xmlDatas, byte jobType = 0)
        {
            using (var trans = GetObjectDb(SpName).CreateDbConnection().BeginTransaction())
            {
                try
                {
                    foreach (var xmlData in xmlDatas)
                    {
                        var paramValues = BuildParam(productId, xmlData, jobType);
                        ImportData(paramValues, trans);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new ThirdPartyProductException(ex.Message, ex);
                }
            }
        }

        public async Task ImportDataAsync(int productId, string xmlData, byte jobType = 0)
        {
            try
            {
                var paramValues = BuildParam(productId, xmlData, jobType);
                await ImportDataAsync(paramValues).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new ThirdPartyProductException(ex.Message, ex);
            }
        }

        public async Task ImportDataAsync(int productId, IEnumerable<string> xmlDatas, byte jobType = 0)
        {
            try
            {
                foreach (var xmlData in xmlDatas)
                {
                    var paramValues = BuildParam(productId, xmlData, jobType);
                    await ImportDataAsync(paramValues);
                }
            }
            catch (Exception ex)
            {
                throw new ThirdPartyProductException(ex.Message, ex);
            }
        }

        private void ImportData(IDictionary paramValues, DbTransaction trans = null)
        {
            using (var objectDb = GetObjectDb(SpName))
            {
                objectDb.ExecuteNonQuery(paramValues, trans);
                EnsureUpdateSuccess(objectDb);
            }
        }

        private async Task ImportDataAsync(IDictionary paramValues, DbTransaction trans = null)
        {
            using (var objectDb = GetObjectDb(SpName))
            {
                await objectDb.ExecuteNonQueryAsync(paramValues, trans).ConfigureAwait(false);

                EnsureUpdateSuccess(objectDb);
            }
        }

        private static IDictionary BuildParam(int productId, string xmlData, byte jobType)
        {
            var dic = new Dictionary<string, object>(8);
            dic.Add("ProductId", productId);
            dic.Add("XmlData", xmlData);
            dic.Add("JobType", jobType);
            dic.Add("ErrCode", RespositoryResponse.Success);
            dic.Add("ErrMsg", RespositoryResponse.SuccessMessage);
            return dic;
        }

        private static void EnsureUpdateSuccess(IObjectDb objectDb)
        {
            var code = objectDb.GetParameterValue("ErrCode");
            var productId = objectDb.GetParameterValue("ProductId");
            var errorCode = code == DBNull.Value ? 0 : Convert.ToInt32(code);
            if (errorCode != 0)
            {
                var errorMessage = objectDb.GetParameterValue("ErrMsg").ToString();

                throw new ThirdPartyProductException($"ProductId: {productId}\n{errorCode} : {errorMessage}");
            }
        }

    }
}