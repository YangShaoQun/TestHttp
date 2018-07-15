using System;
using System.IO;

namespace Utils
{
    public class FileUtil
    {
        public static string RootFolder = "../../../";

        public static string ReadText(string path)
        {
            if (!path.StartsWith("/"))
                path = RootFolder + path;
            string text = System.IO.File.ReadAllText(path);
            return text;
        }
    }
}
