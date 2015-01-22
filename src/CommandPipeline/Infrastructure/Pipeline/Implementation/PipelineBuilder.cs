namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    using CommandPipeline.Infrastructure.ParametersContext;
    using CommandPipeline.Infrastructure.ParametersContext.Implementation;

    public class PipelineBuilder : IPipelineBuilder
    {
        public IMessageHandlerPipeline<T> Create<T>(ICommandContainer commandContainer) where T : class
        {
            return new MessageHandlerPipeline<T>(commandContainer, new ParametersContext<ICommand>(new ParametersContextContainer()));
        }

        public IMessageHandlerPipeline Create(ICommandContainer commandContainer)
        {
            return this.Create(commandContainer, new ParametersContext<ICommand>(new ParametersContextContainer()));
        }

        public IMessageHandlerPipeline Create(ICommandContainer commandContainer, IParametersContext<ICommand> parametersContext)
        {
            return new MessageHandlerPipeline(commandContainer, parametersContext);
        }
    }
}