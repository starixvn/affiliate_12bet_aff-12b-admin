using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public class DataSimulator : IDataSimulator
    {
        private const int DefaultBufferSize = 4096;

        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;


        public string GetLocalData(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return JsonConvert.SerializeObject(new { status = 999 });
            }

            return string.Join(string.Empty,  ReadAllLinesAsync(filePath, Encoding.UTF8));
        }

        public static  string[] ReadAllLinesAsync(string path, Encoding encoding)
        {
            var lines = new List<string>();

            // Open the FileStream with the same FileMode, FileAccess
            // and FileShare as a call to File.OpenText would've done.
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line =  reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}