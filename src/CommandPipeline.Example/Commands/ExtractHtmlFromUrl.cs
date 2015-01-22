namespace CommandPipeline.Example.Commands
{
    using CommandPipeline.Example.Entities;
    using CommandPipeline.Example.Services;
    using CommandPipeline.Infrastructure.Arguments;
    using CommandPipeline.Infrastructure.Extensions;
    using CommandPipeline.Infrastructure.Pipeline.Implementation;

    using EnsureThat;

    public class ExtractHtmlFromUrl : NonParameterizedCommand
    {
        public OutArgument<HtmlDocument> HtmlPage { get; set; }

        public InArgument<Request> Request { get; set; }

        public IWebPageDownloader Downloader { get; private set; }

        public ExtractHtmlFromUrl(IWebPageDownloader downloader)
        {
            this.Downloader = downloader;
        }

        public override void Execute()
        {
            var request = Ensure.That(this.Request, "Request").Is(p => p.IsNotNull());

            var webPage = this.Downloader.DownloadWebPage(request.Url);

            var htmlPage = new HtmlDocument
                               {
                                   Content = webPage
                               };

            this.HtmlPage.Set(htmlPage);
        }
    }
}