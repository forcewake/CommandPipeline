namespace CommandPipeline.Example.Services
{
    using System;

    using CommandPipeline.Example.Entities;

    public interface IDataContainerService
    {
        Guid UploadDocument(Document document);
    }
}