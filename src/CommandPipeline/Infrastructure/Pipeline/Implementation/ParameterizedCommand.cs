namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    public abstract class ParameterizedCommand<T> : IParameterizedCommand<T> where T : class
    {
        public abstract void Execute(T parameter);

        public virtual void Execute(object parameter)
        {
            this.Execute(parameter as T);
        }
    }
}