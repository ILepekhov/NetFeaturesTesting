using FluentFTP;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace FluentFTPTesting
{
    class Program
    {
        #region Const

        private const string Username = "smmad539_ftpusr";
        private const string Password = "N7s4G0q6";
        private const string FtpServerAddress = "148.251.7.151";
        private const int FtpServerPort = 21;
        private const string SlidesDirectoryPath = "/www/new-blogprosmm.ru/public/images/slides/";

        private const string LocalSlidesDirectoryName = "Slides";

        #endregion

        static void Main(string[] args)
        {
            FtpClient client = new FtpClient(FtpServerAddress, FtpServerPort, Username, Password);

            Console.WriteLine("Trying to establish connection...");
            client.Connect();
            Console.WriteLine("Successfully connected");

            Console.WriteLine("Downloading slides...");

            var slidesDirectoryPath = PrepareSlidesDirectory();

            foreach (FtpListItem item in client.GetListing(SlidesDirectoryPath))
            {
                Console.WriteLine("Downloading file {0}", item.Name);

                client.DownloadFile(Path.Combine(slidesDirectoryPath, item.Name), item.FullName);
            }

            Console.WriteLine("Disconnecting");
            client.Disconnect();
        }

        private static string PrepareSlidesDirectory()
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var slidesDirectoryPath = Path.Combine(currentDirectory, LocalSlidesDirectoryName);

            if (Directory.Exists(slidesDirectoryPath))
            {
                Directory.Delete(slidesDirectoryPath, true);
            }

            Directory.CreateDirectory(slidesDirectoryPath);

            return slidesDirectoryPath;
        }
    }
}
