namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Report;
    using Fanex.Affiliate.Admin.Core.Extensions;
    using Fanex.Affiliate.Admin.Core.MetaModels.Report;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using Fanex.Affiliate.Admin.Core.Criterias.Report;

    public class PlayerCpaService : BaseDynamicService, IPlayerCpaService
    {
        private readonly ICacheService _cache;
        private readonly IDynamicRepository _dynamicRepository;
        private readonly CacheItemOptions _options = new CacheItemOptions
        {
            RelativeExpiration = TimeSpan.FromMinutes(10)
        };

        public PlayerCpaService(IDynamicRepository dynamicRepository, ICacheService cache) : base(dynamicRepository)
        {
            _cache = cache;
            _dynamicRepository = dynamicRepository;
        }

        public async Task<EntityResult<PlayerCpaReport>> GetDataAsync(PlayerCpaReportRequest request)
        {
            var cacheKey = GetCacheKey(request);
            var cachedResult = await GetFromCacheAsync(cacheKey);

            if (cachedResult != null)
            {
                return ApplyDataFilters(cachedResult, request);
            }

            var result = await FetchFromDatabaseAsync(request);
            await _cache.SetAsync(cacheKey, result, _options);

            return ApplyDataFilters(result, request);
        }

        public async Task<TurnoverDetailModel> GetTurnoverDetailAsync(int memberCpaId)
        {
            var criteria = new TurnoverCpaDetailCriteria { MemberCpaId = memberCpaId };

            var (groupByProducts, groupByProductDetails, total) = await _dynamicRepository
                .FetchMultipleAsync<TurnoverGroupByProduct, TurnoverGroupByProductDetail, TurnoverGroupByProduct>(criteria);

            groupByProducts = groupByProducts.Select(group =>
            {
                group.Products = groupByProductDetails.Where(detail =>
                    detail.MemberCPAId == group.MemberCPAId
                    && detail.MemberCPATierId == group.MemberCPATierId
                    && detail.GroupProductId == group.GroupProductId);
                return group;
            });

            return new TurnoverDetailModel
            {
                Total = total.FirstOrDefault(),
                Groups = groupByProducts
            };
        }

        private static string GetCacheKey(PlayerCpaReportRequest request)
        {
            return $"{request.UserName}.{request.CustName}.{request.DealName}.{request.StatusId}.{request.ViewBy}.{request.FromDate.ToShortDateString()}.{request.ToDate.ToShortDateString()}";
        }

        private async Task<EntityResult<PlayerCpaReport>> GetFromCacheAsync(string cacheKey)
        {
            return await _cache.ContainAsync(cacheKey)
                ? await _cache.GetAsync<EntityResult<PlayerCpaReport>>(cacheKey)
                : null;
        }

        private async Task<EntityResult<PlayerCpaReport>> FetchFromDatabaseAsync(PlayerCpaReportRequest request)
        {
            var criteria = request.CreateCriteria();
            var (reports, total) = await _dynamicRepository.FetchMultipleAsync<PlayerCpaReport, PlayerCpaReport>(criteria);

            return new EntityResult<PlayerCpaReport>
            {
                MainData = reports,
                Total = total.FirstOrDefault(),
                TotalItem = criteria.NRows
            };
        }

        private static EntityResult<PlayerCpaReport> ApplyDataFilters(EntityResult<PlayerCpaReport> result, PlayerCpaReportRequest request)
        {
            var sortColumns = request.SortColumn
                .Split(',')
                .ToDictionary(item => item, (e) => request.IsAscending);

            return new EntityResult<PlayerCpaReport>
            {
                MainData = result.MainData
                    .SortBy(sortColumns)
                    .Paging(request.PageIndex, request.PageSize),
                Total = result.Total,
                TotalItem = result.TotalItem
            };
        }

    }
}
