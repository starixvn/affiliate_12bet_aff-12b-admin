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

        private readonly string[] fontNameList = new[] {
         "Comic Sans MS",
         "Arial",
         "Times New Roman",
         "Georgia",
         "Verdana",
         "Geneva"
        };

        private readonly FontStyle[] fontStyleList = new[] {
         FontStyle.Bold,
         FontStyle.Italic,
         FontStyle.Regular,
         FontStyle.Strikeout,
         FontStyle.Underline
        };

        private readonly HatchStyle[] hatchStyleList = new[] {
         HatchStyle.BackwardDiagonal, HatchStyle.Cross, HatchStyle.DashedDownwardDiagonal, HatchStyle.DashedHorizontal,
         HatchStyle.DashedUpwardDiagonal, HatchStyle.DashedVertical, HatchStyle.DiagonalBrick, HatchStyle.DiagonalCross,
         HatchStyle.Divot, HatchStyle.DottedDiamond, HatchStyle.DottedGrid, HatchStyle.ForwardDiagonal, HatchStyle.Horizontal,
         HatchStyle.HorizontalBrick, HatchStyle.LargeCheckerBoard, HatchStyle.LargeConfetti, HatchStyle.LargeGrid,
         HatchStyle.LightDownwardDiagonal, HatchStyle.LightHorizontal, HatchStyle.LightUpwardDiagonal, HatchStyle.LightVertical,
         HatchStyle.Max, HatchStyle.Min, HatchStyle.NarrowHorizontal, HatchStyle.NarrowVertical, HatchStyle.OutlinedDiamond,
         HatchStyle.Plaid, HatchStyle.Shingle, HatchStyle.SmallCheckerBoard, HatchStyle.SmallConfetti, HatchStyle.SmallGrid,
         HatchStyle.SolidDiamond, HatchStyle.Sphere, HatchStyle.Trellis, HatchStyle.Vertical, HatchStyle.Wave, HatchStyle.Weave,
         HatchStyle.WideDownwardDiagonal, HatchStyle.WideUpwardDiagonal, HatchStyle.ZigZag
        };

        private readonly int[] fontEmSizeList = new[] { 16, 18, 20 };

        public CaptchaService(IAppSetting appSetting)
        {
            _appSetting = appSetting;
        }

        public bool IsValid(string inputCaptcha, string sessionCaptcha, string userHostAddress)
        {
            return inputCaptcha == sessionCaptcha || IsValidSecretCaptcha(inputCaptcha, userHostAddress);
        }

        public byte[] GenerateImageBuffer(string captcha, int width, int height, bool isComplex = false)
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

                if (isComplex)
                {
                    DrawComplexImage(captcha, width, height, graphics, random);
                }
                else
                {
                    // Add captcha
                    graphics.DrawString(captcha, new Font("Montserrat", 14), Brushes.White, random.Next(2, 10), random.Next(0, 2));
                }

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

        private void DrawComplexImage(string captcha, int width, int height, Graphics graphics, Random random)
        {
            var rectangleF = new RectangleF(0, 0, width, height);
            var brush = new HatchBrush(
                hatchStyleList[random.Next(hatchStyleList.Length - 1)],
                Color.FromArgb((random.Next(150, 255)), (random.Next(150, 255)), (random.Next(150, 255))),
                Color.White);
            graphics.FillRectangle(brush, rectangleF);
            var matrix = new Matrix();

            for (var i = 0; i <= captcha.Length - 1; i++)
            {
                matrix.Reset();
                int charLength = captcha.Length;
                int x = (width / (charLength + 1) * i) + 5;
                int y = random.Next(0, 2);

                //Rotate text Random
                matrix.RotateAt(random.Next(-20, 15), new PointF(x, y));
                graphics.Transform = matrix;

                //Draw the letters with Randon Font Type, Size and Color
                graphics.DrawString(
                    //Text
                    captcha.Substring(i, 1),
                    //Random Font Name and Style
                    new Font(fontNameList[random.Next(fontNameList.Length - 1)], fontEmSizeList[random.Next(fontEmSizeList.Length - 1)], fontStyleList[random.Next(fontStyleList.Length - 1)]),
                    //Random Color (Darker colors RGB 0 to 100)
                    new SolidBrush(Color.FromArgb(random.Next(0, 100), random.Next(0, 100), random.Next(0, 100))),
                    x,
                    y);
                graphics.ResetTransform();
            }
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