using QRCoder;
using System;
using System.Drawing;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class QrCodeGeneratorService : IQrCodeGeneratorService
    {
        private const int SizeDefault = 7;
        private const int IconSizePercentDefault = 30;
        private const string Base64Prefix = "data:image/png;base64,";

        public int Size { get; set; } = SizeDefault;

        public string GenerateBase64QrCode(string content, string icon = null, bool includePrefix = false)
        {
            Image img = null;
            if (!String.IsNullOrWhiteSpace(icon))
            {
                img = ConvertBase64TImage(icon);
            }
            var base64Img = GenerateBase64QrCode(content, img, includePrefix);
            return base64Img;
        }

        public string GenerateBase64QrCode(string content, Image icon = null, bool includePrefix = false)
        {
            Bitmap bitmap = null;
            if (icon != null)
            {
                bitmap = new Bitmap(icon);
            }
            var base64Img = GenerateBase64QrCode(content, bitmap, includePrefix);
            return base64Img;
        }

        private static Image ConvertBase64TImage(string icon)
        {
            Byte[] imgBytes = Convert.FromBase64String(icon);
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream(imgBytes))
            {
                return Image.FromStream(stream);
            }
        }

        private string GenerateBase64QrCode(string content, Bitmap icon = null, bool includePrefix = false)
        {
            var urlPayload = new QRCoder.PayloadGenerator.Url(content);
            var qrCoderData = QRCodeGenerator.GenerateQrCode(urlPayload, QRCodeGenerator.ECCLevel.H);
            var qrCode = new Base64QRCode(qrCoderData);
            var base64Img = icon == null ? qrCode.GetGraphic(Size) : qrCode.GetGraphic(Size, Color.Black, Color.White, icon, IconSizePercentDefault);
            if (includePrefix)
            {
                base64Img = $"{Base64Prefix}{base64Img}";
            }
            return base64Img;
        }

        public Image GetImage(string path)
            => Image.FromFile(path);

    }
}
