using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Stego
{
    public class Utils
    {
        public static byte[] AesOperation(byte[] data, string key, bool encrypt)
        {
            byte[] result = null;
            using (Aes aes = Aes.Create())
            {
                byte[] Key = Encoding.UTF8.GetBytes(key);
                byte[] IV = new byte[16];

                if (Key.Length != 32)
                {
                    byte[] newKey = new byte[32];

                    for (int i = 0; i < Key.Length; i++)
                    {
                        newKey[i] = Key[i];
                    }

                    for (int i = Key.Length; i < 32; i++)
                    {
                        newKey[i] = 0;
                    }

                    Key = newKey;
                }

                aes.Key = Key;
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                ICryptoTransform cryptoTransform = encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                        cryptoStream.FlushFinalBlock();
                        result = memoryStream.ToArray();
                    }
                }
            }
            return result;
        }

        public static Bitmap Embed(Bitmap bitmap, byte[] data, int useLSBBit, string aesKey)
        {
            byte[] encrypted = data;
            if (aesKey.Length > 0)
            {
                encrypted = AesOperation(data, aesKey, true);
            }

            int dataLength = encrypted.Length;
            byte[] toEmbed = BitConverter.GetBytes(dataLength).Concat(encrypted).ToArray();

            Bitmap bmp = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);
            {
                int possible = (bmp.Width * bmp.Height * (bmp.PixelFormat.ToString().Contains("24") ? 3 : 1) * useLSBBit) / 8;
                if (toEmbed.Length > possible)
                {
                    throw new Exception("Data terlalu besar untuk disisipkan dalam gambar ini.");
                }

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;

                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] rgbValues = new byte[bytes];

                Marshal.Copy(ptr, rgbValues, 0, bytes);

                int imageColorType = bmp.PixelFormat.ToString().Contains("24") ? 3 : 1;
                int byteIndex = 0;
                int bitIndex = 0;

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        int pixelIndex = (y * bmpData.Stride) + (x * imageColorType);

                        for (int i = 0; i < imageColorType; i++)
                        {
                            if (byteIndex >= toEmbed.Length) break;

                            rgbValues[pixelIndex + i] &= (byte)(0xFF << useLSBBit);
                            rgbValues[pixelIndex + i] |= (byte)((toEmbed[byteIndex] >> (8 - useLSBBit - bitIndex)) & (0xFF >> (8 - useLSBBit)));

                            bitIndex += useLSBBit;

                            if (bitIndex >= 8)
                            {
                                bitIndex = 0;
                                byteIndex++;
                            }
                        }

                        if (byteIndex >= toEmbed.Length) break;
                    }

                    if (byteIndex >= toEmbed.Length) break;
                }

                Marshal.Copy(rgbValues, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);
            }

            return bmp;
        }

        public static byte[] Extract(Bitmap bitmap, int useLSBBit, string aesKey)
        {
            Bitmap bmp = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);

            byte[] length = new byte[4];
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int imageColorType = bmp.PixelFormat.ToString().Contains("24") ? 3 : 1;
            int byteIndex = 0;
            int bitIndex = 0;

            int dataLength = 0;
            byte[] lengthBytes = new byte[4];
            for (int y = 0; y < bmp.Height && byteIndex < lengthBytes.Length; y++)
            {
                for (int x = 0; x < bmp.Width && byteIndex < lengthBytes.Length; x++)
                {
                    int pixelIndex = (y * bmpData.Stride) + (x * imageColorType);

                    for (int i = 0; i < imageColorType && byteIndex < lengthBytes.Length; i++)
                    {
                        byte lsb = (byte)((rgbValues[pixelIndex + i] & (0xFF >> (8 - useLSBBit))) << (8 - useLSBBit - bitIndex));
                        lengthBytes[byteIndex] |= lsb;

                        bitIndex += useLSBBit;

                        if (bitIndex >= 8)
                        {
                            bitIndex = 0;
                            byteIndex++;
                            if (byteIndex < lengthBytes.Length)
                            {
                                lengthBytes[byteIndex] = 0; // Initialize the next byte
                            }
                        }
                    }
                }
            }

            dataLength = BitConverter.ToInt32(lengthBytes, 0) + 4;

            byte[] extractedData = new byte[dataLength];
            byteIndex = 0;
            bitIndex = 0;

            for (int y = 0; y < bmp.Height && byteIndex < dataLength; y++)
            {
                for (int x = 0; x < bmp.Width && byteIndex < dataLength; x++)
                {
                    int pixelIndex = (y * bmpData.Stride) + (x * imageColorType);

                    for (int i = 0; i < imageColorType && byteIndex < dataLength; i++)
                    {
                        byte lsb = (byte)((rgbValues[pixelIndex + i] & (0xFF >> (8 - useLSBBit))) << (8 - useLSBBit - bitIndex));
                        extractedData[byteIndex] |= lsb;

                        bitIndex += useLSBBit;

                        if (bitIndex >= 8)
                        {
                            bitIndex = 0;
                            byteIndex++;
                            if (byteIndex < dataLength)
                            {
                                extractedData[byteIndex] = 0; // Initialize the next byte
                            }
                        }
                    }
                }
            }

            byte[] zipBytes = extractedData.Skip(4).ToArray();
            if (aesKey.Length > 0)
            {
                zipBytes = AesOperation(zipBytes, aesKey, false);
            }
            return zipBytes;
        }
    }
}
