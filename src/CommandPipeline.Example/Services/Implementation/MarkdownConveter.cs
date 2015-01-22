namespace CommandPipeline.Example.Services.Implementation
{
    using BFound.HtmlToMarkdown;

    public class MarkdownConveter : IMarkdownConveter
    {
        public string ConvertFromHtml(string html)
        {
            return MarkDownDocument.FromHtml(html);
        }
    }
}