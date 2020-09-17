using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ASample.NetCore.Ftps.Options;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace ASample.NetCore.Ftps
{
    public static class FtpServices
    {
        private static readonly FtpClientOptions Options = new FtpClientOptions();

        /// <summary>
        ///  List a remote directory in the console.
        /// </summary>
        /// <param name="options">ftp服务器配置信息</param>
        public static string ListFiles(FtpClientOptions options)
        {
            var fileNameStr = string.Empty;
            using var sftp = new SftpClient(options.Host, options.UserName, options.Password);
            try
            {
                sftp.Connect();

                var files = sftp.ListDirectory(options.RemoteDir);

                foreach (var file in files)
                {
                    fileNameStr += $"{file.FullName}{Environment.NewLine}";
                }

                sftp.Disconnect();
                return fileNameStr;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Downloads a file in the desktop synchronously
        /// </summary>
        /// <param name="options"></param>
        /// <param name="fileName">文件名称 file_server.txt</param>
        public static void DownloadFile(FtpClientOptions options,string fileName)
        {
            // Path to file on SFTP server
            var pathRemoteFile = Path.Combine(options.RemoteDir, fileName);
            // Path where the file should be saved once downloaded (locally)
            var pathLocalFile = Path.Combine(options.LocalDir, fileName);
            using (var sftp = new SftpClient(options.Host, options.UserName, options.Password))
            {
                try
                {
                    sftp.Connect();

                    Console.WriteLine("Downloading {0}", pathRemoteFile);

                    using (Stream fileStream = File.OpenWrite(pathLocalFile))
                    {
                        sftp.DownloadFile(pathRemoteFile, fileStream);
                    }

                    sftp.Disconnect();
                }
                catch (Exception er)
                {
                    Console.WriteLine("An exception has been caught " + er.ToString());
                }
            }
        }

        public static void DownloadDirectoryFile()
        {

            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}开始下载文件");

            var remoteDirectory = Options.RemoteDir;
            var localDirectory = Options.LocalDir;
            var localFiles = new DirectoryInfo(localDirectory).GetFiles("*.zip");

            using var sftp = new SftpClient(Options.Host, Convert.ToInt32(Options.Port), Options.UserName, Options.Password);
            try
            {
                sftp.Connect();
                var files = sftp.ListDirectory(remoteDirectory);

                var addedFile = 0;
                foreach (var file in files)
                {
                    if (file.IsDirectory)
                        continue;
                    if (localFiles.Any(c => c.Name == file.Name))
                        continue;

                    addedFile++;
                    Console.WriteLine($"新增文件【{file.FullName}】");
                    if (!file.IsDirectory && !file.IsSymbolicLink)
                    {
                        DownloadFile(sftp, file, Options.LocalDir);
                        //var fullPath = Options.LocalDir + file.FullName;
                        //FileServices.ProcessData(fullPath);
                    }
                }
                Console.WriteLine($"共新增文件{addedFile}");
                sftp.Disconnect();
            }
            catch (Exception e)
            {
                //LogsHelper.WriteLogToFile(e.ToString(), "DownloadDirectoryErr", ".txt");
                Console.WriteLine("DownloadDirectoryErr:" + e.Message);
            }
        }

        /// <summary>
        /// Downloads a remote file through the client into a local directory
        /// </summary>
        /// <param name="client"></param>
        /// <param name="file"></param>
        /// <param name="directory"></param>
        private static void DownloadFile(SftpClient client, SftpFile file, string directory)
        {
            Console.WriteLine("Downloading {0}", file.FullName);
            using Stream fileStream = File.OpenWrite(Path.Combine(directory, file.Name));
            client.DownloadFile(file.FullName, fileStream);
        }
    }
}
