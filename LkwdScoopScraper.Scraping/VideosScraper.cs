using AngleSharp.Html.Parser;
using System.Collections.Generic;
using System.Net.Http;
using VideoScraper.Scraping;

namespace VideoScraper.Scraping
{
    public static class VideosScraper
    {
        public static List<Video> Scraper()
        {
            return ParseVideoHtml(GetVideoHtml());
        }

        private static string GetVideoHtml()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };
            using var client = new HttpClient(handler);
            var url = "https://mostlymusic.com/collections/featured-videos";
            var html = client.GetStringAsync(url).Result;
            return html;
        }

        private static List<Video> ParseVideoHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".product-wrap");
            var videos = new List<Video>();
            foreach (var div in resultDivs)
            {
                var video = new Video();

                var link = div.QuerySelector(".hidden-product-link");
                if (link != null)
                {
                    video.Link = $"https://mostlymusic.com{link.Attributes["href"].Value}";
                }               
                var imageTag = div.QuerySelector("img");   
                
                if (imageTag != null)
                {
                    video.ImageURL = imageTag.Attributes["src"].Value;
                }
               
                var title = div.QuerySelector(".title");
                if (title != null)
                {
                    video.Title = title.TextContent;
                }
                
                var isNew = div.QuerySelector(".banner_holder");
                if(isNew == null)
                {
                    continue;
                }
                if (isNew != null)
                {
                    video.IsNew = isNew.TextContent;
                }
                videos.Add(video);
            }
            return videos;
        }
    }
}
