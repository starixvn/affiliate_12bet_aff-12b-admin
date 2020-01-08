using System.Drawing;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IQrCodeGeneratorService
    {
        string GenerateBase64QrCode(string content, string imgBase64 = null);

        string GenerateBase64QrCode(string content, Image img = null);
    }
}
