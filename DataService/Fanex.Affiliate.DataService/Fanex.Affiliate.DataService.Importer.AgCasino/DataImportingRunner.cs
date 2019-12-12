namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using Core.DTO;
    using Core.Utilities;

    public class DataImportingRunner : IDataImportingRunner
    {
        private readonly ICasinoDataService _casinoDataService;
        private readonly IAppSettings _appSettings;

        public DataImportingRunner() : this(null, null)
        {
        }

        public DataImportingRunner(ICasinoDataService casinoDataService, IAppSettings appSetting)
        {
            _casinoDataService = casinoDataService ?? new CasinoDataService();
            _appSettings = appSetting ?? new AppSettings();
        }

        public ResultInfo Run()
        {
            var date = DateTime.Now;
            // Run Yesterday Data at the beginning and the end of today
            if (date.Hour == Convert.ToInt32(_appSettings.GetValue("ImportCheckingTime1")) ||
                date.Hour == Convert.ToInt32(_appSettings.GetValue("ImportCheckingTime2")) ||
                _appSettings.IsLocalTest)
            {
                date = date.AddDays(-1);
            }

            return _casinoDataService.ImportData(date);
        }
    }
}