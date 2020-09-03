using HtmlAgilityPack;
using Squash.Data;
using Squash.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Downloader
    {
        readonly string siteUri = "https://squash.jogo.sk";

        public async Task DownloadResults()
        {
            var lastDate = GetInfo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(siteUri);
                var data = await GetSiteContent(client, siteUri);
                var dates = GetDates(data);
                foreach(var date in dates.Reverse())
                {
                    if (date <= lastDate)
                    {
                        continue;
                    }

                    data = await GetPost(client, "/index.php", $"{date.Year}-{date.Month}-{date.Day}");
                    var extractor = new DataExtractor(data, date);
                    await extractor.Process();
                }

                using (var _context = new SquashContext())
                {
                    _context.Info.Add(new Info { LastRun = DateTime.Now, LastTournamentProcessed = dates.First()});
                    await _context.SaveChangesAsync();
                }
            }
        }

        private DateTime GetInfo()
        {
            using (var _context = new SquashContext())
            {
                return _context.Info.OrderByDescending(a => a.LastRun).FirstOrDefault()?.LastTournamentProcessed ?? DateTime.MinValue;
            }
        }

        private async Task<string> GetSiteContent(HttpClient client, string uri)
        {
            return await client.GetStringAsync(uri);
        }

        private async Task<string> GetPost(HttpClient client, string uri, string date)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("datum", date)
            });
            var response = await client.PostAsync(uri, content);
            return await response.Content.ReadAsStringAsync();
        }

        private IEnumerable<DateTime> GetDates(string data)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            return doc.DocumentNode.SelectNodes("//select").FirstOrDefault().InnerText.Split("&nbsp;", StringSplitOptions.RemoveEmptyEntries).Select(a=>DateTime.Parse(a));
        }
    }
}
