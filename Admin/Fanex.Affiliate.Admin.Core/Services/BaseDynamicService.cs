using Fanex.Affiliate.Admin.Core.Commands;
using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class BaseDynamicService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public BaseDynamicService(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        protected IDynamicRepository DynamicRepository => _dynamicRepository;

        public IEnumerable<T> FetchTData<T>(string spKey) where T : class
        {
            return _dynamicRepository.Fetch<T>(new BaseCriteria(spKey));
        }

        public IEnumerable<T> FetchTData<T>(CriteriaBase criteria) where T : class
        {
            return _dynamicRepository.Fetch<T>(criteria);
        }

        public T GetTData<T>(string spKey) where T : class
        {
            return _dynamicRepository.Get<T>(new BaseCriteria(spKey));
        }

        public T GetTData<T>(CriteriaBase criteria) where T : class
        {
            return _dynamicRepository.Get<T>(criteria);
        }

        public ResultInfo ExecuteReturnResultInfo(BaseNonQueryCommand command)
        {
            _dynamicRepository.Execute(command);

            return new ResultInfo(command.ErrCode, command.ErrMsg);
        }

        public void ExecuteCommand(NonQueryCommand command)
        {
            _dynamicRepository.Execute(command);
        }

        public EntityResult<T> FetchEntityResult<T>(PagingCriteria criteria) where T : class
        {
            var dataList = _dynamicRepository.Fetch<T>(criteria);

            return new EntityResult<T>
            {
                MainData = dataList,
                TotalItem = criteria.TotalItem,
            };
        }

        public EntityResult<T> FetchEntityResultHasTotalItem<T>(PagingCriteria criteria) where T : class
        {
            var (dataList, totalItem) = _dynamicRepository.FetchMultiple<T, T>(criteria);

            return new EntityResult<T>
            {
                MainData = dataList,
                TotalItem = criteria.TotalItem,
                Total = totalItem.FirstOrDefault()
            };
        }
    }
}
