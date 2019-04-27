using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASample.NetCore.ConfigReaders
{
    public class ConfigReader
    {
        public static string FileName;
        public static IConfiguration Configuration {
            get {
                var jsonFile = FileName ?? "appsetiong.json";
                var configBuilder = new ConfigurationBuilder();
                configBuilder.AddJsonFile(jsonFile);
                return  configBuilder.Build();
            }
        }
        
        public ConfigReader()
        {
           
        }

        /// <summary>
        /// 获取配置文件的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key) where T:class,new()
        {
            try
            {
                var cfg = new T();
                Configuration.Bind(key, cfg);
                return cfg;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IConfigurationSection GetSection(string key)
        { 
            try
            {
                var config = Configuration.GetSection(key);
                return config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetValue(string key)
        {
            try
            {
                var config = Configuration.GetSection(key);
                if (config != null)
                    return config.Value;
                else
                    return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
