using ASample.NetCore.Extension;
using ASample.NetCore.Serialize;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;

namespace ASample.NetCore
{
    public class ConfigurationReader
    {
        public static IConfiguration Configuration {get;set; }
        public static string JsonFileNameAndExtension { get; set; }


        static ConfigurationReader()
        {
            Configuration =  GetConfigBuilder(JsonFileNameAndExtension);

            //Look(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config"));
        }


        private static IConfigurationRoot GetConfigBuilder(string jsonFileNameAndExtension = null)
        {
            if (jsonFileNameAndExtension.IsNullOrEmpty())
            {
                jsonFileNameAndExtension = "appsettings.json";
            }
            var configBuilder = new ConfigurationBuilder();
            var directory = AppContext.BaseDirectory;
            directory = directory.Replace("\\", "/");

            //在当前目录或者根目录中寻找appsettings.json文件
            var filePath = $"{directory}/{jsonFileNameAndExtension}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("/bin");
                filePath = $"{directory.Substring(0, length)}/{jsonFileNameAndExtension}";
            }

            var builder = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true);
            var configuration = configBuilder.Build();
            return configuration;
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


        public static void Look(string absluteFolderPath)
        {
            if (!Directory.Exists(absluteFolderPath))
            {
                Directory.CreateDirectory(absluteFolderPath);
            }
            _configurationDir = absluteFolderPath;
        }

        /// <summary>
        /// 这里默认使用应用程序域的目录下面的config文件夹，作为配置文件的文件夹
        /// </summary>
        private static string _configurationDir;

        private static ConcurrentDictionary<Type, object> list = new ConcurrentDictionary<Type, object>();

        public static T Read<T>(IDeserializer deserializer) where T : class
        {
            return list.GetOrAdd(typeof(T), key =>
            {
                string filePath;
                var content = ReadFileText(typeof(T).Name, out filePath);
                var result = deserializer.Deserialize<T>(content);
                //StartMonitor<T>(filePath);
                return result;
            }) as T;
        }

        public static string Read<T>()
        {
            var filePath = string.Empty;
            var content = ReadFileText(typeof(T).Name, out filePath);
            return content;
        }

        /// <summary>
        /// 在项目文件夹config 下读取指定Json文件名的内容
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string Read(string fileName)
        {
            var filePath = string.Empty;
            var content = ReadFileText(fileName, out filePath);
            return content;
        }

        private static string ReadFileText(string fileName, out string filePath)
        {
            //与扩展名无关
            var files = Directory.EnumerateFiles(_configurationDir, fileName + ".*").ToArray();
            if (files.Count() != 1)
            {
                throw new Exception(string.Format("在{0}中找到了0个或者多个配置项：" + fileName + ".*", _configurationDir));
            }
            filePath = files[0];
            return File.ReadAllText(filePath);
        }
    }
}
