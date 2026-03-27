using System.IO;

namespace L4D2_CFG_Manager.Core
{
    public static class FileManager
    {
        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
                return "";

            return File.ReadAllText(path);
        }

        public static void WriteFile(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}