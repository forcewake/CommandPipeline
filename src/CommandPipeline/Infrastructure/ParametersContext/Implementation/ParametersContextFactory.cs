namespace CommandPipeline.Infrastructure.ParametersContext.Implementation
{
    public class ParametersContextFactory : IParametersContextFactory
    {
        public IParametersContext<T> Get<T>()
        {
            return new ParametersContext<T>(new ParametersContextContainer());
        }
    }
}