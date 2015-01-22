namespace CommandPipeline.Infrastructure.Pipeline
{
    using CommandPipeline.Infrastructure.ParametersContext;

    public interface IPipelineBuilder
    {
        IMessageHandlerPipeline<T> Create<T>(ICommandContainer commandContainer) where T : class;
        IMessageHandlerPipeline Create(ICommandContainer commandContainer);
        IMessageHandlerPipeline Create(ICommandContainer commandContainer, IParametersContext<ICommand> parametersContext);
    }
}