using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class CaptchaService : ICaptchaService
    {
        private readonly IAppSetting _appSetting;

        public CaptchaService(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public bool IsValid(string inputCaptcha, string sessionCaptcha, string userHostAddress)
        {
            return inputCaptcha == sessionCaptcha || IsValidSecretCaptcha(inputCaptcha, userHostAddress);
        }

        public byte[] GenerateImageBuffer(string captcha, int width, int height)
        {
            byte[] buffer = null;

            using (var memoryStream = new MemoryStream())
            using (var bitmap = new Bitmap(width, height))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                var random = new Random();

                graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                graphics.Clear(Color.FromArgb(62, 164, 189));

                // Add captcha
                graphics.DrawString(captcha, new Font("Montserrat", 14), Brushes.White, random.Next(2, 10), random.Next(0, 2));

                for (int i = 0; i < 50; i++)
                {
                    int x = random.Next(bitmap.Width);
                    int y = random.Next(bitmap.Height);
                    bitmap.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                bitmap.Save(memoryStream, ImageFormat.Png);

                buffer = memoryStream.GetBuffer();
            }

            return buffer;
        }

        private bool IsValidSecretCaptcha(string inputCaptcha, string userHostAddress)
        {
            var isAllowAutomationCaptchaCode = _appSetting.GetValue("AllowAutomationCaptchaCode") == "1";
            var secretCaptcha = _appSetting.GetValue("SecretCaptcha");

            return isAllowAutomationCaptchaCode &&
                !string.IsNullOrWhiteSpace(secretCaptcha) &&
                inputCaptcha == secretCaptcha &&
                IsValidHostAddress(userHostAddress);
        }

        private bool IsValidHostAddress(string userHostAddress)
        {
            return _appSetting.IsLocal || Regex.IsMatch(userHostAddress, _appSetting.GetValue("CompanyIP"));
        }
    }
}