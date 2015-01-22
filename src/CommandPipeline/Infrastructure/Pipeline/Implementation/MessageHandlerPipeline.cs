namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    using CommandPipeline.Infrastructure.ParametersContext;

    public class MessageHandlerPipeline : MessageHandlerPipelineBase, IMessageHandlerPipeline
    {
        public MessageHandlerPipeline(ICommandContainer commandContainer, IParametersContext<ICommand> context)
            : base(commandContainer, context)
        {
        }

        public IMessageHandlerPipeline Register<TCommand>() where TCommand : INonParameterizedCommand
        {
            this.CommandsType.Add(typeof(TCommand));
            return this;
        }

        public void Handle()
        {
            this.Handle(null);
        }
    }
}