using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.Attributes;
using Fanex.Affiliate.ThirdPartyProduct.Constants;
using Fanex.Affiliate.ThirdPartyProduct.MetaModels;
using Fanex.Affiliate.ThirdPartyProduct.Utilities;
using Fanex.Affiliate.ThirdPartyProduct.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    [Authorize]
    [FunctionAuthorize]
    public class TopazController : Controller
    {
        private const int TIME_RANGE = 60;
        private const string SubFolder = "Topaz";
        private const string DateTimeFormat = "yyyyMMddTHH_mm_ss";

        private readonly TopazService _service;
        public TopazController()
        {
            _service = new TopazService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Topaz/Index.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> Index(ImporterBaseParam param)
        {
            var logBuilder = new StringBuilder();
            var rawDataBuilder = new StringBuilder();
            var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            var fileFolder = $"{startTime.ToString(DateTimeFormat)}-{endTime.ToString(DateTimeFormat)}";
            var folder = Path.Combine(Server.MapPath("~"), SubFolder, fileFolder);
            var zipFile = Path.Combine(Server.MapPath("~"), SubFolder, $"{fileFolder}.zip");
            try
            {
                var numberOfThread = Environment.ProcessorCount;
                var times = DateTimeHelper.GetProportionTimeRange(startTime, endTime, new TimeSpan(0, TIME_RANGE, 0));
                var funcSaveDataToFile = GetSaveDataToFileDelegate(_service, folder);
                var files = await ParallelForeachAsync.ForEachSemaphoreAsync(times, numberOfThread, funcSaveDataToFile);

                ZipFile.CreateFromDirectory(folder, zipFile);
                Directory.Delete(folder, true);
                //var rawDataList = await _service.GetRawDataListAsync(startTime, endTime).ConfigureAwait(false);
                

                if (files.Any())
                {
                    foreach (var item in files)
                    {
                        rawDataBuilder.AppendLine(item);
                        rawDataBuilder.AppendLine("-----------------------------------------");
                        rawDataBuilder.AppendLine();
                    }
                }
            }
            catch (Exception ex)
            {
                logBuilder.AppendLine(ex.Message);
                logBuilder.AppendLine(ex.StackTrace);
                logBuilder.AppendLine(ex.Source);
                return Content(logBuilder.ToString());
            }

            return File(zipFile, "application/zip");
        }

        [HttpGet]
        public ActionResult RawData()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Topaz/RawData.cshtml");
        }

        [HttpPost]
        public async Task<ActionResult> GetRawData(TopazRequestParam param)
        {
            var logBuilder = new StringBuilder();
            var startTime = DateTime.ParseExact(param.StartTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(param.EndTime, Format.RequestDateTime, CultureInfo.InvariantCulture);
            try
            {
                var rawData = await _service.GetRawDataAsync(startTime, endTime, param.PageNumber).ConfigureAwait(false);
                return Content(rawData);
            }
            catch (Exception ex)
            {
                logBuilder.AppendLine(ex.Message);
                logBuilder.AppendLine(ex.StackTrace);
                logBuilder.AppendLine(ex.Source);
                return Content(logBuilder.ToString());
            }
        }

        private Func<Tuple<DateTime, DateTime>, Task<string>> GetSaveDataToFileDelegate(TopazService service, string folder)
        {
            Func<Tuple<DateTime, DateTime>, Task<string>> SaveDataToFileAsync = async timeRange =>
            {
                var startTime = timeRange.Item1;
                var endTime = timeRange.Item2;
                var datas = await service.GetRawDataListAsync(startTime, endTime);
                var fileName = $"{startTime.ToString(DateTimeFormat)}-{endTime.ToString(DateTimeFormat)}.txt";
                var filePath = Path.Combine(folder, fileName);
                CreateDirectoryIfNotExist(filePath);
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (var data in datas)
                    {
                        await writer.WriteLineAsync(data);
                        await writer.WriteLineAsync("-----------------------------------------");
                    }
                }
                return filePath;
            };
            return SaveDataToFileAsync;
        }

        private void CreateDirectoryIfNotExist(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
