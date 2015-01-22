namespace CommandPipeline.Example.Services.Implementation
{
    using System.Net;

    public class WebPageDownloader : IWebPageDownloader
    {
        public string DownloadWebPage(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}