namespace Fanex.Affiliate.User.Core.Utilities
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.IO;

    public static class CaptchaGenerator
    {
        public static byte[] GenerateImageBuffer(string captcha, int width, int height)
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

                // Render as JPEG
                bitmap.Save(memoryStream, ImageFormat.Jpeg);

                buffer = memoryStream.GetBuffer();
            }

            return buffer;
        }
    }
}