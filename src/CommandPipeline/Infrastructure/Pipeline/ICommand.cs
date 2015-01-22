namespace CommandPipeline.Infrastructure.Pipeline
{
    public interface ICommand
    {
        void Execute(object parameter);
    }
}