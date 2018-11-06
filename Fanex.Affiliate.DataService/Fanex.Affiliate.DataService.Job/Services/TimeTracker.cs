using System;
using System.IO;
using System.Linq;

namespace Fanex.Affiliate.DataService.Job.Services
{
    public class TimeTracker : ITimeTracker
    {
        public DateTime GetLastestCallSuccess(string folderPath, DateTime defaultValue)
        {
            if (!Directory.Exists(folderPath))
            {
                return defaultValue;
            }

            var fileName = GetLatestTrackingFile(folderPath);
            if (string.IsNullOrEmpty(fileName))
            {
                return defaultValue;
            }

            var lastLine = File.ReadLines(fileName).Last();

            return string.IsNullOrEmpty(lastLine) ? defaultValue : DateTime.Parse(lastLine);
        }

        public void SaveTime(string folderPath, DateTime callTime)
        {
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

            return latestFile?.FullName;
        }
    }
}
