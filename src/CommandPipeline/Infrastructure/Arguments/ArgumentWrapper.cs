namespace CommandPipeline.Infrastructure.Arguments
{
    internal class ArgumentWrapper : Argument
    {
        public ArgumentWrapper(string name, object value)
        {
            this.Name = name;
            this.CurrentValue = value;
        }
    }
}