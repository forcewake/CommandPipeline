namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    using CommandPipeline.Infrastructure.ParametersContext;

    public class MessageHandlerPipeline<T> : MessageHandlerPipelineBase, IMessageHandlerPipeline<T> where T : class
    {
        public MessageHandlerPipeline(ICommandContainer commandContainer, IParametersContext<ICommand> context)
            : base(commandContainer, context)
        {
        }

        public IMessageHandlerPipeline<T> Register<TCommand>() where TCommand : IParameterizedCommand<T>
        {
            CommandsType.Add(typeof(TCommand));
            return this;
        }

        public virtual void Handle(T message)
        {
            Handle(message as object);
        }
    }
}