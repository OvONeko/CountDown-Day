
namespace System.IO
{
    public static class FileExtend
    {
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