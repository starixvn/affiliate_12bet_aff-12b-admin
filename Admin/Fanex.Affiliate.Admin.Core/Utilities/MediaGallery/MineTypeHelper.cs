using System.IO;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Utilities.MediaGallery
{
    public static class MimeTypeHelper
    {
        private static readonly byte[][] HeaderBytes = {
            new byte[] { 71, 73, 70, 56 }, // GIF
            new byte[] { 255, 216, 255 }, // JPG
            new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 } // PNG
        };

        public static bool IsValidMimeType(Stream stream)
        {
            var file = ReadBytes(stream);
            foreach (var bytes in HeaderBytes)
            {
                if (file.Take(bytes.Length).SequenceEqual(bytes))
                {
                    return true;
                }
            }

            return false;
        }

        private static byte[] ReadBytes(Stream input)
        {
            byte[] buffer = new byte[16];
            using (MemoryStream memoryStream = new MemoryStream())
            {
                var read = input.Read(buffer, 0, buffer.Length);
                if (read > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }

                return memoryStream.ToArray();
            }
        }
    }
}
