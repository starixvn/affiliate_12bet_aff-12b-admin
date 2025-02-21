namespace Fanex.Affiliate.Link.Services.Impl
{
    using Fanex.Affiliate.Link.Commands;
    using Fanex.Affiliate.Link.Services.Interface;
    using Fanex.Data.Repository;

    public class TrackingSystemService : ITrackingSystemService
    {
        private readonly IDynamicRepository _dynamicRepository;

        public TrackingSystemService()
            : this(null)
        {
        }

        private TrackingSystemService(IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository ?? new DynamicRepository();
        }

        public string InsertStatistic(TrackingSystemCommand command)
        {
            _dynamicRepository.Execute(command);

            return command.RedirectPage;
        }
    }
}
