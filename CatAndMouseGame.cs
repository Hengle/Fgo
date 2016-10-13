using ICSharpCode.SharpZipLib.BZip2;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/* CatAndMouseGame
 * Fate/Grand Order 1.15
 * By Perfare
*/
public class CatAndMouseGame
{
    protected static byte[] baseData = new byte[0x20];
    protected static byte[] baseTop = new byte[0x20];
    public static byte[] infoData = Convert.FromBase64String("d0ZFYTlYWnFRVGNHRlI0ektrRHFqdllqekpLdHNUTG4=");
    protected static byte[] InfoTop = new byte[0x20];
    public static byte[] ownerData = Convert.FromBase64String("ZDlNeDZ2VDZLMmdVYW5Xd3h3YnZLdkpaSjRoUHk2RjQ=");
    protected static byte[] ownerTop = new byte[0x20];
    protected static byte[] stageData = new byte[0x20];
    protected static byte[] stageTop = new byte[0x20];
    protected static byte[] stageData2 = new byte[0x20];
    protected static byte[] stageTop2 = new byte[0x20];


    public static string CatGame1(string str, bool isPress = false)
    {
        byte[] buffer4;
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        byte[] rgbKey = Encoding.UTF8.GetBytes("b5nHjsMrqaeNliSs3jyOzgpD");
        byte[] rgbIV = Encoding.UTF8.GetBytes("wuD6keVr");
        TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
        using (MemoryStream stream = new MemoryStream())
        {
            using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
            {
                if (isPress)
                {
                    using (BZip2OutputStream stream3 = new BZip2OutputStream(stream2))
                    {
                        stream3.Write(bytes, 0, bytes.Length);
                        stream3.Close();
                    }
                }
                else
                {
                    stream2.Write(bytes, 0, bytes.Length);
                }
                stream2.Close();
            }
            buffer4 = stream.ToArray();
            stream.Close();
        }
        return Convert.ToBase64String(buffer4);
    }

    public static string CatGame3(string str)
    {
        var bytes = Encoding.UTF8.GetBytes(str);
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = (byte)~bytes[i];
        }
        return CatHome(bytes, stageData2, stageTop2, true);
    }

    public static string CatGame5(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);
        byte[] home = new byte[stageTop.Length];
        byte[] info = new byte[stageData.Length];
        for (int i = 0; i < home.Length; i++)
        {
            home[i] = (byte)(stageTop[i] ^ 4);
        }
        for (int j = 0; j < info.Length; j++)
        {
            info[j] = (byte)(stageData[j] ^ 8);
        }
        return CatHome(bytes, home, info, false);
    }

    public static string CatHome(byte[] data, byte[] home, byte[] info, bool isPress = false)
    {
        byte[] inArray = CatHomeMain(data, home, info, isPress);
        if (inArray != null)
        {
            return Convert.ToBase64String(inArray);
        }
        return null;
    }

    public static byte[] CatHomeMain(byte[] data, byte[] home, byte[] info, bool isPress = false)
    {
        MemoryStream stream = null;
        CryptoStream stream2 = null;
        byte[] buffer;
        try
        {
            ICryptoTransform transform = new RijndaelManaged
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 0x100,
                BlockSize = 0x100
            }.CreateEncryptor(home, info);
            stream = new MemoryStream();
            stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            if (isPress)
            {
                BZip2OutputStream stream3 = new BZip2OutputStream(stream2);
                stream3.Write(data, 0, data.Length);
                stream3.Close();
            }
            else
            {
                stream2.Write(data, 0, data.Length);
                stream2.FlushFinalBlock();
            }
            buffer = stream.ToArray();
        }
        catch (Exception)
        {
            buffer = null;
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
            if (stream2 != null)
            {
                stream2.Close();
            }
        }
        return buffer;
    }

    public static void ForthHomeBuilding(string data)
    {
        int v3 = 0;
        int v2 = 1;
        int v1 = 2;
        var dword_8004 = Encoding.UTF8.GetBytes(data);
        do
        {
            int v8 = v3 >> 1;
            if ((v3 & 1) == 1)
            {
                int v9 = 4 * v8;
                baseTop[v9] = dword_8004[4 * v3];
                baseTop[v9 + 1] = dword_8004[v2];
                baseTop[v9 + 2] = dword_8004[v1];
                baseTop[v9 + 3] = dword_8004[v2 + 2];
            }
            else
            {
                int v7 = 4 * v8;
                baseData[v7] = dword_8004[4 * v3];
                baseData[v7 + 1] = dword_8004[v2];
                baseData[v7 + 2] = dword_8004[v1];
                baseData[v7 + 3] = dword_8004[v2 + 2];
            }
            ++v3;
            v2 += 4;
            v1 += 4;
        } while (v3 != 16);
    }

    public static string MouseGame1(string str, bool isPress = false)
    {
        byte[] buffer4;
        byte[] buffer = Convert.FromBase64String(str);
        byte[] bytes = Encoding.UTF8.GetBytes("b5nHjsMrqaeNliSs3jyOzgpD");
        byte[] rgbIV = Encoding.UTF8.GetBytes("wuD6keVr");
        TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
        using (MemoryStream stream = new MemoryStream())
        {
            using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write))
            {
                stream2.Write(buffer, 0, buffer.Length);
                stream2.Close();
            }
            buffer4 = stream.ToArray();
            stream.Close();
        }
        if (isPress)
        {
            using (MemoryStream stream3 = new MemoryStream())
            {
                using (MemoryStream stream4 = new MemoryStream(buffer4))
                {
                    using (BZip2InputStream stream5 = new BZip2InputStream(stream4))
                    {
                        int num;
                        byte[] buffer5 = new byte[0x4000];
                        while ((num = stream5.Read(buffer5, 0, buffer5.Length)) > 0)
                        {
                            stream3.Write(buffer5, 0, num);
                        }
                        stream5.Close();
                    }
                    stream4.Close();
                }
                buffer4 = stream3.ToArray();
                stream3.Close();
            }
        }
        return Encoding.UTF8.GetString(buffer4);
    }

    public static string MouseGame2(string str, bool isPress = false)
    {
        byte[] sourceArray = Convert.FromBase64String(str);
        Array.Copy(sourceArray, 0, ownerTop, 0, 0x20);
        byte[] destinationArray = new byte[sourceArray.Length - 0x20];
        Array.Copy(sourceArray, 0x20, destinationArray, 0, sourceArray.Length - 0x20);
        return MouseHome(destinationArray, ownerData, ownerTop, true);
    }

    public static string MouseGame3(string str)
    {
        var bytes = MouseHomeMain(Convert.FromBase64String(str), stageData2, stageTop2, true);
        int v21 = bytes.Length;
        int v22 = 0;
        do
        {
            bytes[v22] = (byte)~bytes[v22++];
        } while (v21 != v22);
        return Encoding.UTF8.GetString(bytes).TrimEnd(new char[1]);
    }

    public static byte[] MouseGame4(byte[] data)
    {
        var bytes = MouseHomeMain(data, baseData, baseTop, false);
        int v10 = 0;
        int v14 = 1;
        int v13 = bytes.Length;
        do
        {
            var v15 = bytes[v10];
            var v16 = bytes[v14];
            int v17 = v10;
            v10 += 2;
            var v18 = v16;
            bytes[v17] = (byte)~(v18 ^ 0x2D);
            bytes[v14] = (byte)~(v15 ^ 0x31);
            if (v13 <= v10)
                break;
            v14 += 2;
        } while (v14 < v13);
        return bytes;
    }

    public static string MouseHome(byte[] data, byte[] home, byte[] info, bool isPress = false)
    {
        byte[] bytes = MouseHomeMain(data, home, info, isPress);
        return Encoding.UTF8.GetString(bytes).TrimEnd(new char[1]);
    }

    public static byte[] MouseHomeMain(byte[] data, byte[] home, byte[] info, bool isPress = false)
    {
        MemoryStream stream = null;
        CryptoStream stream2 = null;
        byte[] buffer3;
        try
        {
            ICryptoTransform transform = new RijndaelManaged
            {
                Padding = PaddingMode.PKCS7,
                Mode = CipherMode.CBC,
                KeySize = 0x100,
                BlockSize = 0x100
            }.CreateDecryptor(home, info);
            byte[] buffer = new byte[data.Length];
            stream = new MemoryStream(data);
            stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            stream2.Read(buffer, 0, buffer.Length);
            if (isPress)
            {
                int num;
                MemoryStream stream3 = new MemoryStream();
                MemoryStream stream4 = new MemoryStream(buffer);
                BZip2InputStream stream5 = new BZip2InputStream(stream4);
                byte[] buffer2 = new byte[0x4000];
                while ((num = stream5.Read(buffer2, 0, buffer2.Length)) > 0)
                {
                    stream3.Write(buffer2, 0, num);
                }
                stream5.Close();
                buffer = stream3.ToArray();
                stream4.Close();
                stream3.Close();
            }
            buffer3 = buffer;
        }
        catch (Exception)
        {
            buffer3 = null;
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
            if (stream2 != null)
            {
                stream2.Close();
            }
        }
        return buffer3;
    }

    public static string MouseInfo(string str)
    {
        byte[] sourceArray = Convert.FromBase64String(str);
        byte[] destinationArray = new byte[sourceArray.Length - 0x20];
        Array.Copy(sourceArray, 0, InfoTop, 0, 0x20);
        Array.Copy(sourceArray, 0x20, destinationArray, 0, sourceArray.Length - 0x20);
        return MouseHome(destinationArray, infoData, InfoTop, true);
    }

    public static void ThirdHomeBuilding(string data)
    {
        var bytes = Encoding.UTF8.GetBytes(data);
        for (int i = 0; i < 64; i++)
        {
            if (i > 31)
            {
                stageData[i - 32] = bytes[i];
            }
            else
            {
                stageTop[i] = bytes[i];
            }
            if (i % 2 == 0)
            {
                stageData2[i >> 1] = bytes[i];
            }
            else
            {
                stageTop2[i >> 1] = bytes[i];
            }
        }
    }
}
