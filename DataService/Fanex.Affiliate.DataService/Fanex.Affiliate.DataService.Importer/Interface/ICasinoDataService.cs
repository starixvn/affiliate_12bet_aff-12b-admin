namespace Fanex.Affiliate.DataService.Importer
{
    using System;
    using DTO;

    public interface ICasinoDataService
    {
        ResultInfo ImportData(DateTime date);

        ResultInfo ImportData(DateTime date, string filePath);
    }
}