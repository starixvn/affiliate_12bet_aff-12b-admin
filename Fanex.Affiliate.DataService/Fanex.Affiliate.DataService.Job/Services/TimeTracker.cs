using System;
using System.IO;
using System.Linq;
using Fanex.Affiliate.DataService.Importer.Utilities;

namespace Fanex.Affiliate.DataService.Job.Services
{
    public class TimeTracker : ITimeTracker
    {
        private readonly IAppSettings _appSetting;

        public TimeTracker(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public DateTime GetLastestCallSuccess()
        {
            var folderPath = _appSetting.BetsoftTimeTrackerFolder;
            var negativeIntervalTime = _appSetting.BetsoftIntervalTime * -1;
            if (!Directory.Exists(folderPath))
            {
                return DateTime.Now.AddMinutes(negativeIntervalTime);
            }

            var fileName = GetLatestTrackingFile(folderPath);
            if (string.IsNullOrEmpty(fileName))
            {
                return DateTime.Now.AddMinutes(negativeIntervalTime);
            }

            var lastLine = File.ReadLines(fileName).Last();

            return string.IsNullOrEmpty(lastLine) ? DateTime.Now : DateTime.Parse(lastLine);
        }

        public void SaveTime(DateTime callTime)
        {
            var folderPath = _appSetting.BetsoftTimeTrackerFolder;
            var fileName = Path.Combine(folderPath, $"{DateTime.Now.ToString("yyyyMMdd")}.txt");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (var writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(callTime.ToString());
            }
        }

        private static string GetLatestTrackingFile(string folderPath)
        {
            var directory = new DirectoryInfo(folderPath);
            var latestFile = directory.GetFiles()
             .OrderByDescending(f => f.LastWriteTime)
             .FirstOrDefault(f => File.Exists(Path.Combine(folderPath, $"{f.LastWriteTime.ToString("yyyyMMdd")}.txt")));

            return latestFile.FullName;
        }
    }
}
