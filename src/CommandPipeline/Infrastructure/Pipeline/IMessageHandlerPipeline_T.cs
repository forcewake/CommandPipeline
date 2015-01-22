namespace CommandPipeline.Infrastructure.Pipeline
{
    public interface IMessageHandlerPipeline<T> where T : class
    {
        IMessageHandlerPipeline<T> Register<TCommand>() where TCommand : IParameterizedCommand<T>;
        void Handle(T message);
    }
}
