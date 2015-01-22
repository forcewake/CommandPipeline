namespace CommandPipeline.Infrastructure.Arguments
{
    public class InOutArgument<T> : InArgument<T>
    {
        public InOutArgument()
        {
            this.Set(default(T));
        }

        public void Set(T value)
        {
            this.SetValue(value);
        }

        public static implicit operator InOutArgument<T>(T value)
        {
            var argument = new InOutArgument<T>();
            argument.SetValue(value);
            return argument;
        }

        public static implicit operator T(InOutArgument<T> value)
        {
            var obj = value.GetValue<T>();
            return obj;
        }
    }
}