namespace CommandPipeline.Example.Commands
{
    using CommandPipeline.Example.Entities;
    using CommandPipeline.Example.Services;
    using CommandPipeline.Infrastructure.Arguments;
    using CommandPipeline.Infrastructure.Extensions;
    using CommandPipeline.Infrastructure.Pipeline.Implementation;

    using EnsureThat;

    public class ConvertHtmlToMardown : NonParameterizedCommand
    {
        public InArgument<HtmlDocument> HtmlPage { get; set; }

        public OutArgument<MarkdownDocument> MarkdownDocument { get; set; }

        public IMarkdownConveter Conveter { get; private set; }

        public ConvertHtmlToMardown(IMarkdownConveter conveter)
        {
            this.Conveter = conveter;
        }

        public override void Execute()
        {
            HtmlDocument htmlDocument = Ensure.That(this.HtmlPage, "HtmlDocument").Is(p => EnsureObjectExtensions.IsNotNull<InArgument<HtmlDocument>>(p));

            var markdownDocument = this.Conveter.ConvertFromHtml(htmlDocument.Content);

            var document = new MarkdownDocument { Content = markdownDocument };

            this.MarkdownDocument.Set(document);
        }
    }
}