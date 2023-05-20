using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Delion
{
    /* Provides saving and loading settings and other config files.
     * Have default values for all fields.
     * default folder can be overloaded 
     * singletone */
    public class Settings
    {
        public class Parameters
        {
            public int XResolution { get; set; } = 1280;
            public int YResolution { get; set; } = 720;
            public bool IsFullscreen { get; set; } = false;
            public int UIScale { get; set; } = 1;
        }

        public Parameters Params { get; set; } = new Parameters();

        public string SystemName { get; } = "SpamGa";
        public string Name { get; }  = "Delion";
        public string SettingsFileName { get; private set; } = "UserConfig";
        public string LogFile { get; private set; }  = "Recent.log";
        public string AssetFolder { get; private set; }  = "assets/";
        public string DataPath { get; }

        public Settings(string folderPath = "")
        {
            if (folderPath != "")
                DataPath = folderPath;
            else
                DataPath = CreateDefaultDataPath();

            string settingsFilePath = Path.Combine(DataPath, SettingsFileName);

            //if file doesnt't exist then create and fill with default values
            try
            {
                Params = LoadSettingsFromFile(settingsFilePath);
            }
            catch
            {
                SaveSettingsToFile(settingsFilePath);
            }
        }

        public void Save()
        {
            string settingsFilePath = Path.Combine(DataPath, SettingsFileName);
            SaveSettingsToFile(settingsFilePath);
        }

        public void Load()
        {
            string settingsFilePath = Path.Combine(DataPath, SettingsFileName);
            Params = LoadSettingsFromFile(settingsFilePath);
        }

        private Parameters LoadSettingsFromFile(string filePath)
        {
            string settingsData = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Parameters>(settingsData);
        }

        private void SaveSettingsToFile(string filePath)
        {
            string serialized = JsonSerializer.Serialize(Params);
            File.WriteAllText(filePath, serialized);
        }

        private string CreateDefaultDataPath()
        {
           string directoryPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{SystemName}/");

            Directory.CreateDirectory(directoryPath);

            return directoryPath;
        }




    }
}
