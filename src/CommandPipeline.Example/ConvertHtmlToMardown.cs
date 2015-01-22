namespace CommandPipeline.Example
{
    using System;
    using System.Collections.Generic;

    using CommandPipeline.Example.Commands;
    using CommandPipeline.Example.Entities;
    using CommandPipeline.Example.Services;
    using CommandPipeline.Example.Services.Implementation;
    using CommandPipeline.Infrastructure.ParametersContext.Implementation;
    using CommandPipeline.Infrastructure.Pipeline;
    using CommandPipeline.Infrastructure.Pipeline.Implementation;
    using CommandPipeline.Ninject;

    using FluentAssertions;

    using Xunit;

    public class DownloadPipelineTests
    {
        private readonly string[] links;

        private readonly IPipelineBuilder builder;

        public DownloadPipelineTests()
        {
            this.links = new[]
            {
                "http://habrahabr.ru/post/240219/", 
                "http://habrahabr.ru/post/240223/",
                "http://habrahabr.ru/post/240225/"
            };

            this.builder = new PipelineBuilder();
        }

        [Fact]
        public void ConvertAllLinksToMarkdownDocuments()
        {
            var container = new NinjectContainer();

            container.Bind<IWebPageDownloader>().To<WebPageDownloader>();
            container.Bind<IMarkdownConveter>().To<MarkdownConveter>();
            container.Bind<IDataContainerService>().To<DataContainerServiceFake>();

            var context = new ParametersContext<ICommand>();

            var pipeline = builder.Create(container, context)
                .Register<ExtractHtmlFromUrl>()
                .Register<ConvertHtmlToMardown>()
                .Register<UploadToDataContainer>();

            var documentsId = new List<Guid>();

            foreach (var link in links)
            {
                context.Set<ExtractHtmlFromUrl, Request>(x => x.Request, new Request { Url = link });

                pipeline.Handle();

                var documentId = context.Get<UploadToDataContainer, Guid>(x => x.UploadedDocumentId);

                documentsId.Add(documentId);
            }

            documentsId.Should().HaveCount(links.Length);
        }
    }
}
