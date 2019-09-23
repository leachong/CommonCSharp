using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace CommonCSharp.Helpers
{
    public class AppConfigHelper
    {
        Configuration _config;
        string _configFile;

        Dictionary<string, string> _dicSetting = new Dictionary<string, string>();
        public static AppConfigHelper Instance { get; } = new AppConfigHelper();


        public bool GetSetting(string key, bool defValue)
        {
            var str = GetSetting(key, defValue.ToString());
            var ret = false;
            if (!bool.TryParse(str, out ret))
            {
                ret = defValue;
            }
            return ret;
        }
        public int GetSetting(string key, int defValue)
        {
            var str = GetSetting(key, defValue.ToString());
            var ret = 0;
            if (!int.TryParse(str, out ret))
            {
                ret = defValue;
            }
            return ret;
        }
        public string GetSetting(string key, string defValue)
        {
            if (!_dicSetting.ContainsKey(key))
            {
                LoadSetting(key);
            }
            if (_dicSetting.ContainsKey(key))
            {
                var result = _dicSetting[key];
                return result;
            }
            else
            {
                SetSetting(key, defValue);
            }
            return defValue;
        }
        public void SetSetting(string key, bool value, bool save = true) => SetSetting(key, value.ToString(), save);
        public void SetSetting(string key, int value, bool save = true) => SetSetting(key, value.ToString(), save);
        public void SetSetting(string key, string value, bool save = true)
        {
            if (_dicSetting.ContainsKey(key))
            {
                if (_dicSetting[key].Equals(value))
                    return;
                _dicSetting[key] = value;
            }
            else
            {
                _dicSetting.Add(key, value);
            }
            if (save)
            {
                SaveSetting(key.ToString(), value.ToString());
            }
        }
        
        void LoadSetting(string key)
        {
            var config = GetConfig();
            if (config.AppSettings.Settings.AllKeys.Contains(key))
            {
                var strValue = config.AppSettings.Settings[key].Value;
                if (!string.IsNullOrEmpty(strValue))
                    SetSetting(key, strValue, false);
            }
        }
        void SaveSetting(string key, string value)
        {
            var config = GetConfig();
            if (!config.AppSettings.Settings.AllKeys.Contains(key))
            {
                config.AppSettings.Settings.Add(key, value);
                config.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                if (config.AppSettings.Settings[key].Value != value)
                {
                    config.AppSettings.Settings[key].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);
                }
            }
        }
        Configuration GetConfig()
        {
            if (_config == null)
            {
                if (string.IsNullOrEmpty(_configFile))
                {
                    _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                }
                else
                {
                    ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                    configMap.ExeConfigFilename = GetConfigFile();
                    _config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
                }
            }

            return _config;
        }
        string GetConfigFile()
        {
            try
            {
                if (!File.Exists(_configFile))
                {
                    string xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<configuration>\r\n</configuration>";
                    File.WriteAllText(_configFile, xml);
                }
                return _configFile;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }

        // 设置配置文件路径
        public void SetConfigFileFullName(string configFile)
        {
            _configFile = configFile;
        }
    }
}
