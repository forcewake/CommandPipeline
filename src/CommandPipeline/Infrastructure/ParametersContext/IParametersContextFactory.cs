namespace CommandPipeline.Infrastructure.ParametersContext
{
    public interface IParametersContextFactory
    {
        IParametersContext<T> Get<T>();
    }
}