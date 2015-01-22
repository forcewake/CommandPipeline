namespace CommandPipeline.Example.Services.Implementation
{
    using System;

    using CommandPipeline.Example.Entities;

    public class DataContainerServiceFake : IDataContainerService
    {
        public Guid UploadDocument(Document document)
        {
            return Guid.NewGuid();
        }
    }
}