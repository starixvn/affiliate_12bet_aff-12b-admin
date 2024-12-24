namespace Fanex.Affiliate.User.Core.CoreService
{
    using System.Collections.Generic;
    using System.Linq;
    using Commands;
    using Data.Repository;
    using DTO;

    public class BaseDynamicService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public BaseDynamicService(IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        public IEnumerable<T> FetchTData<T>(CriteriaBase criteria) where T : class
        {
            return _dynamicRepository.Fetch<T>(criteria);
        }

        public T GetTData<T>(CriteriaBase criteria) where T : class
        {
            return _dynamicRepository.Get<T>(criteria);
        }

        public void ExecuteCommand(NonQueryCommand command)
        {
            _dynamicRepository.Execute(command);
        }

        public ResultInfo ExecuteReturnResultInfo(BaseNonQueryCommand command)
        {
            _dynamicRepository.Execute(command);

            return new ResultInfo
            {
                IsOk = command.ErrCode == 0,
                Code = command.ErrCode,
                Message = command.ErrMsg
            };
        }

        public EntityResult<T> FetchEntityResult<T>(CriteriaBase criteria) where T : class
        {
            var(dataList, totalItem) = _dynamicRepository.FetchMultiple<T, T>(criteria);

            return new EntityResult<T>
            {
                MainDataList = dataList,
                Total = totalItem.FirstOrDefault()
            };
        }
    }
}
