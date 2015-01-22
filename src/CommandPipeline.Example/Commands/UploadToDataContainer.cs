namespace CommandPipeline.Example.Commands
{
    using System;

    using CommandPipeline.Example.Entities;
    using CommandPipeline.Example.Services;
    using CommandPipeline.Infrastructure.Arguments;
    using CommandPipeline.Infrastructure.Extensions;
    using CommandPipeline.Infrastructure.Pipeline.Implementation;

    using EnsureThat;

    public class UploadToDataContainer : NonParameterizedCommand
    {
        public UploadToDataContainer(IDataContainerService dataContainerService)
        {
            this.DataContainerService = dataContainerService;
        }

        public InArgument<MarkdownDocument> MarkdownDocument { get; set; }

        public OutArgument<Guid> UploadedDocumentId { get; set; }

        public IDataContainerService DataContainerService { get; private set; }

        public override void Execute()
        {
            var document = Ensure.That(this.MarkdownDocument, "MarkdownDocument").Is(p => p.IsNotNull());

            var documentId = this.DataContainerService.UploadDocument(document);

            this.UploadedDocumentId.Set(documentId);
        }
    }
}