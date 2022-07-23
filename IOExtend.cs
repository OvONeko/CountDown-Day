using System;
using System.Text;
using XSystem.Security.Cryptography;

namespace System.IO
{
    public static class FileExtend
    {
        /// <summary>
        /// 檢測文件是否爲空
        /// </summary>
        /// <param name="filename">文件名(需包含路徑)</param>
        public static bool IsEmpty(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            if (fi.Length == 0)
            {
                return true;
            }
            return false;
        }
    }
}

namespace CountDown_Day
{
    public static class HE
    {
        public static string Hash(string stringToHash)
        {
            using (var sha1 = new SHA1Managed())
            {
                return BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(stringToHash)));
            }
        }
    }
}