namespace CommandPipeline.Infrastructure.Pipeline
{
    public interface IMessageHandlerPipeline
    {
        IMessageHandlerPipeline Register<TCommand>() where TCommand : INonParameterizedCommand;

        void Handle();
    }
}