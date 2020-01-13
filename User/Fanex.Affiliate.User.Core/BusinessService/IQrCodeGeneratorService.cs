using System.Drawing;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IQrCodeGeneratorService
    {
        string GenerateBase64QrCode(string content, string icon = null);

        string GenerateBase64QrCode(string content, Image icon = null);

        void SaveImage(string base64Img, string path); 

        int Size { get; set; }

    }
}
