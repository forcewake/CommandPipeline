namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    public abstract class Command<T> : INonParameterizedCommand, IParameterizedCommand<T> where T : class
    {
        public abstract void Execute(T parameter);

        public virtual void Execute(object parameter)
        {
            this.Execute(parameter as T);
        }

        public abstract void Execute();
    }
}