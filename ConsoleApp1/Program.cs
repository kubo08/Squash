using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var downloader = new Downloader();
            await downloader.DownloadResults();
        }
    }
}
