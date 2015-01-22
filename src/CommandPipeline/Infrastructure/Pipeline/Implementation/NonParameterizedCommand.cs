namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    public abstract class NonParameterizedCommand : INonParameterizedCommand, ICommand
    {
        public abstract void Execute();

        public virtual void Execute(object parameter)
        {
            this.Execute();
        }
    }
}