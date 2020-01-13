using Fanex.Affiliate.User.Core.Utilities;
using QRCoder;
using System;
using System.Drawing;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class QrCodeUrlGeneratorService : IQrCodeGeneratorService
    {
        private readonly IImageFileWriter _fileWriter;

        public int Size { get; set; } = 10;

        public QrCodeUrlGeneratorService(IImageFileWriter imgWriter)
        {
            _fileWriter = imgWriter ?? new PngImageFileWriter();
        }

        public string GenerateBase64QrCode(string content, Image icon = null)
        {
            Bitmap bitmap = null;
            if (icon != null)
            {
                 bitmap = new Bitmap(icon);
            }
            return GenerateBase64QrCode(content, bitmap);
        }

        public string GenerateBase64QrCode(string content, string icon = null)
        {
            Bitmap bitmap = null;
            if (icon != null)
            {
                Byte[] bitmapData = Convert.FromBase64String(icon);
                System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
                bitmap = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
            }
            return GenerateBase64QrCode(content, bitmap);
        }

        private string GenerateBase64QrCode(string content, Bitmap icon = null)
        {
            var urlPayload = new QRCoder.PayloadGenerator.Url(content);
            var qrCoderData = QRCodeGenerator.GenerateQrCode(urlPayload, QRCodeGenerator.ECCLevel.H);
            var qrCode = new Base64QRCode(qrCoderData);
            var base64Img = icon == null ? qrCode.GetGraphic(Size) : qrCode.GetGraphic(Size, Color.Black, Color.White, icon);
            return base64Img;
        }

        public void SaveImage(string base64Img, string path)
        {
            _fileWriter.Write(base64Img, path);
        }
    }
}
