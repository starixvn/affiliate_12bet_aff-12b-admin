using SkiaSharp;
using System;
using System.Drawing;
using System.IO;

namespace Fanex.Affiliate.Admin.Core.Utilities.MediaGallery
{
    /// <summary>
    /// Helper class for extracting image dimensions with WebP support using SkiaSharp
    /// </summary>
    public static class ImageDimensionHelper
    {
        private const string DefaultDimensions = "0x0";

        /// <summary>
        /// Extracts image dimensions from a stream using SkiaSharp (supports WebP) with System.Drawing fallback
        /// </summary>
        /// <param name="inputStream">The image stream</param>
        /// <returns>Formatted dimensions string (e.g., "1920x1080")</returns>
        public static string ExtractImageDimensions(Stream inputStream)
        {
            try
            {
                var dimensions = GetImageDimensions(inputStream);
                return $"{dimensions.Width}x{dimensions.Height}";
            }
            catch
            {
                return DefaultDimensions;
            }
        }

        /// <summary>
        /// Gets image dimensions from a stream using SkiaSharp (supports WebP) with System.Drawing fallback
        /// </summary>
        /// <param name="inputStream">The image stream</param>
        /// <returns>Image dimensions as tuple</returns>
        public static (int Width, int Height) GetImageDimensions(Stream inputStream)
        {
            try
            {
                // Reset stream position to beginning
                inputStream.Position = 0;
                
                // Try SkiaSharp first (supports WebP and other formats)
                try
                {
                    using (var skiaStream = new SKManagedStream(inputStream))
                    {
                        using (var codec = SKCodec.Create(skiaStream))
                        {
                            if (codec != null)
                            {
                                return (codec.Info.Width, codec.Info.Height);
                            }
                        }
                    }
                }
                catch
                {
                    // SkiaSharp failed, try System.Drawing.Bitmap as fallback
                }

                // Fallback to System.Drawing.Bitmap for other formats
                inputStream.Position = 0;
                using (var bitmap = new Bitmap(inputStream))
                {
                    return (bitmap.Width, bitmap.Height);
                }
            }
            catch
            {
                return (0, 0);
            }
            finally
            {
                inputStream.Position = 0;
            }
        }

        /// <summary>
        /// Validates image dimensions against maximum limits using SkiaSharp (supports WebP) with System.Drawing fallback
        /// </summary>
        /// <param name="inputStream">The image stream</param>
        /// <param name="maxWidth">Maximum allowed width</param>
        /// <param name="maxHeight">Maximum allowed height</param>
        /// <returns>True if dimensions are within limits, false otherwise</returns>
        public static bool ValidateImageDimensions(Stream inputStream, int maxWidth, int maxHeight)
        {
            try
            {
                var dimensions = GetImageDimensions(inputStream);
                return dimensions.Width <= maxWidth && dimensions.Height <= maxHeight;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Gets detailed dimension validation result with actual dimensions
        /// </summary>
        /// <param name="inputStream">The image stream</param>
        /// <param name="maxWidth">Maximum allowed width</param>
        /// <param name="maxHeight">Maximum allowed height</param>
        /// <returns>Validation result with dimensions</returns>
        public static ImageDimensionValidationResult ValidateImageDimensionsDetailed(Stream inputStream, int maxWidth, int maxHeight)
        {
            try
            {
                var dimensions = GetImageDimensions(inputStream);
                return new ImageDimensionValidationResult
                {
                    IsValid = dimensions.Width <= maxWidth && dimensions.Height <= maxHeight,
                    Width = dimensions.Width,
                    Height = dimensions.Height,
                    MaxWidth = maxWidth,
                    MaxHeight = maxHeight
                };
            }
            catch (Exception ex)
            {
                return new ImageDimensionValidationResult
                {
                    IsValid = false,
                    Width = 0,
                    Height = 0,
                    MaxWidth = maxWidth,
                    MaxHeight = maxHeight,
                    ErrorMessage = ex.Message
                };
            }
        }
    }

    /// <summary>
    /// Result of image dimension validation
    /// </summary>
    public class ImageDimensionValidationResult
    {
        public bool IsValid { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public string ErrorMessage { get; set; }

        public string GetDimensionsString()
        {
            return $"{Width}x{Height}";
        }

        public string GetValidationMessage()
        {
            if (IsValid)
            {
                return "Image dimensions are valid";
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                return $"Invalid image file or corrupted data: {ErrorMessage}";
            }

            return $"Image dimensions ({Width}x{Height}) exceed maximum allowed size ({MaxWidth}x{MaxHeight})";
        }
    }
}
