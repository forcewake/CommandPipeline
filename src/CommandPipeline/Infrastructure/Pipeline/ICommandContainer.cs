namespace CommandPipeline.Infrastructure.Pipeline
{
    using System;

    public interface ICommandContainer
    {
        ICommand Create(Type type);
    }
}