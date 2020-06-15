using System.Drawing;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IQrCodeGeneratorService
    {
        string GenerateBase64QrCode(string content, string icon = null, bool includePrefix = false);

        string GenerateBase64QrCode(string content, Image icon = null, bool includePrefix = false);

        Image GetImage(string path);

        int Size { get; set; }
    }
}
