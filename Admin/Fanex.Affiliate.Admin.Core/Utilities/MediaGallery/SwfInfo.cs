using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.Utilities.MediaGallery
{
    public class SwfInfo
    {
        #region Properties
        private string Filename { get; set; }

        private string MagicBytes { get; set; }

        private bool IsCompressed { get; set; }

        private short Version { get; set; }

        private int Size { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        private bool IsValid { get; set; }

        private double FrameRate { get; set; }

        private int FrameCount { get; set; }

        #endregion

        public SwfInfo(string filename)
        {
            LoadSwf(filename);
        }

        public void LoadSwf(string filename)
        {
            this.Filename = filename;

            using (BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                // Read MAGIC FIELD
                this.MagicBytes = new String(reader.ReadChars(3));

                if (this.MagicBytes != "FWS" && this.MagicBytes != "CWS")
                {
                    this.IsValid = false;
                    throw new Exception(filename + " is not a valid/supported SWF file.");
                }

                this.IsCompressed = this.MagicBytes.StartsWith("C", StringComparison.CurrentCulture);
                this.Version = Convert.ToInt16(reader.ReadByte());

                // 4 LSB-MSB
                for (int i = 0; i < 4; i++)
                {
                    byte t = reader.ReadByte();
                    this.Size += t << (8 * i);
                }

                // RECT... we will "simulate" a stream from now on... read remaining file
                byte[] buffer = reader.ReadBytes(Size);

                // First decompress GZ stream
                if (this.IsCompressed)
                {
                    // Let's set GZip magic bytes which GZipStream can process
                    Array.Resize(ref buffer, buffer.Length + 8);
                    for (int i = buffer.Length - 1; i > 9; i--)
                    {
                        buffer[i] = buffer[i - 8];
                    }
                    ((Array)(new byte[] { 31, 139, 8, 0, 0, 0, 0, 0, 4, 0 })).
                        CopyTo(buffer, 0);

                    MemoryStream ms = new MemoryStream(buffer);

                    GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);

                    byte[] decompressedBuffer = new byte[buffer.Length + 1000000];

                    int gzipLength = ReadAllBytesFromStream(gzip, decompressedBuffer);

                    gzip.Close();
                    ms.Close();

                    Array.Resize(ref buffer, gzipLength);
                    Array.Resize(ref decompressedBuffer, gzipLength);
                    decompressedBuffer.CopyTo(buffer, 0);

                    Array.Clear(decompressedBuffer, 0, decompressedBuffer.Length);
                }

                byte cbyte = buffer[0];
                int bits = (int)cbyte >> 3;

                Array.Reverse(buffer);
                Array.Resize(ref buffer, buffer.Length - 1);
                Array.Reverse(buffer);

                BitArray cval = new BitArray(bits, false);

                // Current byte
                cbyte &= 7;
                cbyte <<= 5;

                // Current bit (first byte starts off already shifted)
                int cbit = 2;

                // Must get all 4 values in the RECT
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < cval.Count; j++)
                    {
                        if ((cbyte & 128) > 0)
                        {
                            cval[j] = true;
                        }

                        cbyte <<= 1;
                        cbyte &= 255;
                        cbit--;

                        // We will be needing a new byte if we run out of bits
                        if (cbit < 0)
                        {
                            cbyte = buffer[0];

                            Array.Reverse(buffer);
                            Array.Resize(ref buffer, buffer.Length - 1);
                            Array.Reverse(buffer);

                            cbit = 7;
                        }
                    }

                    // O.k. full value stored... calculate
                    int c = 1;
                    int val = 0;

                    for (int j = cval.Count - 1; j >= 0; j--)
                    {
                        if (cval[j])
                        {
                            val += c;
                        }
                        c *= 2;
                    }

                    val /= 20;

                    switch (i)
                    {
                        case 0:
                            // tmp value
                            this.Width = val;
                            break;
                        case 1:
                            this.Width = val - this.Width;
                            break;
                        case 2:
                            // tmp value
                            this.Height = val;
                            break;
                        case 3:
                            this.Height = val - this.Height;
                            break;
                    }

                    cval.SetAll(false);
                }

                this.FrameRate += buffer[1];
                this.FrameRate += Convert.ToSingle(buffer[0] / 100);

                this.FrameCount += BitConverter.ToInt16(buffer, 2);
            }
        }

        public static int ReadAllBytesFromStream(Stream stream, byte[] buffer)
        {
            int offset = 0;
            int totalCount = 0;
            while (true)
            {
                int bytesRead = stream.Read(buffer, offset, 100);
                if (bytesRead == 0)
                {
                    return totalCount;
                }
                offset += bytesRead;
                totalCount += bytesRead;
            }
        }
    }
}
