using Fanex.Affiliate.Admin.Core.DTO.Home;
using Fanex.Affiliate.Admin.Core.DTO;
using System;
using Fanex.Affiliate.Admin.Core.Repository;
using System.Collections.Generic;
using System.Data;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Services.Home
{
    public class HomeService: BaseRepository, IHomeService
    {
        public EntityResult<OnlineListInfo> GetOnlineList(int pageIndex, int pageSize)
        {
            const string SpKey = "Aff_UserOnlineList";

            var result = new EntityResult<OnlineListInfo>();
            var dataList = new List<OnlineListInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("PageIndex", DbType.Int32, pageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, pageSize);

                objectDb.AddOutParameter("TotalPages", DbType.Int32, Size.SizeOfInt);
                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new OnlineListInfo
                        {
                            UserName = Convert.ToString(reader["UserName"]),
                            LoggedInDate = reader["LoggedIn"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LoggedIn"]),
                            LastRequestDate = reader["LastRequest"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["LastRequest"]),
                            Ip = Convert.ToString(reader["IP"]),
                            Role = Convert.ToString(reader["RoleName"])
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public decimal GetPSPToday()
        {
            const string SpKey = "DEP_AffiliateGetPSP";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddOutParameter("PSP", DbType.Int32, Size.SizeOfCurrency);

                objectDb.ExecuteNonQuery();

                var psp = objectDb.GetParameterValue("PSP");
                return Convert.ToDecimal(psp);
            }
        }

        public DepositInfo GetDepositInfo()
        {
            const string SpKey = "DEP_AffiliateGeneralInfo";
            DepositInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = new DepositInfo
                        {
                            AllMemberDepositUpToNow = reader.GetVal<int>("MemInProAllNum"),
                            AllMemberDepositThisMonth = reader.GetVal<int>("MemInProMonthNum")
                        };
                    }
                }
            }

            return result;
        }

        public GeneralInfo GetGeneralInfo()
        {
            const string SpKey = "Aff_Admin_GeneralInfo_Sel";
            GeneralInfo result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildItem(reader);
                    }
                }
            }

            return result;
        }

        public GeneralInfo BuildItem(IDataReader reader)
        {
            return new GeneralInfo
            {
                PaymentRequestNumber = reader.GetVal<int>("PaymentReqNum"),
                BannerNumber = reader.GetVal<int>("BannerNum"),
                ImageBannerNumber = reader.GetVal<int>("ImageBannerNum"),
                FlashBannerNumber = reader.GetVal<int>("FlashBannerNum"),
                TextBannerNumber = reader.GetVal<int>("TextBannerNum"),
                AllAffiliateNumber = reader.GetVal<int>("AffInProAllNum"),
                PendingAffiliateNumber = reader.GetVal<int>("PendingAllNum"),
                ApprovedAffiliateNumber = reader.GetVal<int>("ApprovedAllNum"),
                DisapprovedAffiliateNumber = reader.GetVal<int>("DisapprovedAllNum"),
                ClosedAffiliateNumber = reader.GetVal<int>("ClosedAllNum"),
                AllMemberDepositNumber = reader.GetVal<int>("DepositMemAllNum"),
                AllDepositNumber = reader.GetVal<decimal>("DepositAll"),
                AllWinlostNumber = reader.GetVal<decimal>("WinlostAll"),
                AllBonusNumber = reader.GetVal<decimal>("BonusAll"),
                AllPSPFeeNumber = reader.GetVal<decimal>("PSPFeeAll"),
                AllNetWinlostNumber = reader.GetVal<decimal>("NetWinlostAll"),
                AllAffiliateMonthNumber = reader.GetVal<int>("AffInProMonthNum"),
                PendingAffiliateMonthNumber = reader.GetVal<int>("PendingMonthNum"),
                ApprovedAffiliateMonthNumber = reader.GetVal<int>("ApprovedMonthNum"),
                DisapprovedAffiliateMonthNumber = reader.GetVal<int>("DisapprovedMonthNum"),
                ClosedAffiliateMonthNumber = reader.GetVal<int>("ClosedMonthNum"),
                AllMemberDepositMonthNumber = reader.GetVal<int>("DepositMemMonthNum"),
                AllDepositMonthNumber = reader.GetVal<decimal>("DepositMonth"),
                AllWinlostMonthNumber = reader.GetVal<decimal>("WinlostMonth"),
                AllBonusMonthNumber = reader.GetVal<decimal>("BonusMonth"),
                AllPSPFeeMonthNumber = reader.GetVal<decimal>("PSPFeeMonth"),
                AllNetWinlostMonthNumber = reader.GetVal<decimal>("NetWinlostMonth"),
            };
        }
    }
}
