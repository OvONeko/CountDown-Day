
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