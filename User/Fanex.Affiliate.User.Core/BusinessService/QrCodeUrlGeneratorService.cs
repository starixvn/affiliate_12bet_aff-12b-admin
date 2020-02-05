using Fanex.Affiliate.User.Core.Utilities;
using QRCoder;
using System;
using System.Drawing;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class QrCodeUrlGeneratorService : IQrCodeGeneratorService
    {
        private readonly IImageHandler _imgHandler;

        public int Size { get; set; } = 7;

        public QrCodeUrlGeneratorService(IImageHandler imgHandler)
        {
            _imgHandler = imgHandler ?? new PngImageHandler();
        }

        public string GenerateBase64QrCode(string content, Image icon = null, bool includePrefix = false)
        {
            Bitmap bitmap = null;
            if (icon != null)
            {
                 bitmap = new Bitmap(icon);
            }
            return GenerateBase64QrCode(content, bitmap, includePrefix);
        }

        public string GenerateBase64QrCode(string content, string icon = null, bool includePrefix = false)
        {
            Bitmap bitmap = null;
            if (icon != null)
            {
                Byte[] bitmapData = Convert.FromBase64String(icon);
                System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
                bitmap = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
            }
            return GenerateBase64QrCode(content, bitmap, includePrefix);
        }

        private string GenerateBase64QrCode(string content, Bitmap icon = null, bool includePrefix = false)
        {
            var urlPayload = new QRCoder.PayloadGenerator.Url(content);
            var qrCoderData = QRCodeGenerator.GenerateQrCode(urlPayload, QRCodeGenerator.ECCLevel.H);
            var qrCode = new Base64QRCode(qrCoderData);
            var base64Img = icon == null ? qrCode.GetGraphic(Size) : qrCode.GetGraphic(Size, Color.Black, Color.White, icon, 30);
            if (includePrefix)
            {
                base64Img = $"{_imgHandler.Base64Prefix}{base64Img}";
            }
            return base64Img;
        }

        public void SaveImage(string base64Img, string path)
        {
            _imgHandler.Write(base64Img, path);
        }
    }
}
