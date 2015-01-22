namespace CommandPipeline.Infrastructure.Pipeline
{
    public interface IParameterizedCommand<in T> : ICommand where T : class
    {
        void Execute(T parameter);
    }
}