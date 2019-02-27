using System;
using System.Data;
using Fanex.Affiliate.DataService.Importer.Exceptions;

namespace Fanex.Affiliate.DataService.Importer.Repository
{
    public class ThirdPartyProductRepository : BaseRepository, IThirdPartyProductRepository
    {
        public void ImportData(int productId, string xmlData, byte jobType = 0)
        {
            const string SpName = "Task_APIProduct_ImportQueue";

            try
            {
                using (var objectDb = GetObjectDb(SpName))
                {
                    objectDb.AddInParameter("ProductId", DbType.Int32, productId);
                    objectDb.AddInParameter("XmlData", DbType.String, xmlData);
                    objectDb.AddInParameter("JobType", DbType.Byte, jobType);
                    objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
                    objectDb.AddOutParameter("ErrMsg", DbType.String, 4000);

                    objectDb.ExecuteNonQuery();

                    var code = objectDb.GetParameterValue("ErrCode");
                    var errorCode = code == DBNull.Value ? 0 : Convert.ToInt32(code);
                    if (errorCode != 0)
                    {
                        var errorMessage = objectDb.GetParameterValue("ErrMsg").ToString();

                        throw new ThirdPartyProductException($"ProductId: {productId}\n{errorCode} : {errorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ThirdPartyProductException(ex.Message, ex);
            }
        }
    }
}
