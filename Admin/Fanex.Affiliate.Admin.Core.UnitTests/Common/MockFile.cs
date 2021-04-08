using System.IO;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Common
{
    public class MockFile : HttpPostedFileBase
    {
        public Stream stream;
        public string contentType;
        public string fileName;
        public int contentLength;

        public MockFile()
        {
        }

        public MockFile(MemoryStream stream, string contentType = "", string fileName = "", int contentLength = 0)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
            this.contentLength = contentLength;
        }

        public override int ContentLength
        {
            get { return contentLength; }
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override string FileName
        {
            get { return fileName; }
        }

        public override Stream InputStream
        {
            get { return stream; }
        }
    }
}
