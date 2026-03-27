using L4D2_CFG_Manager.Core;

namespace L4D2_CFG_Manager.Core
{
    public class ConfigService
    {
        public string CurrentFile { get; private set; }

        public string LoadConfig(string path)
        {
            CurrentFile = path;
            return FileManager.ReadFile(path);
        }

        public void SaveConfig(string content)
        {
            if (string.IsNullOrEmpty(CurrentFile))
                return;

            FileManager.WriteFile(CurrentFile, content);
        }
    }
}