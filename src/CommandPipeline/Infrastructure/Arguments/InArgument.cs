namespace CommandPipeline.Infrastructure.Arguments
{
    public class InArgument<T> : Argument
    {
        public InArgument()
        {
        }

        public InArgument(T value)
        {
            this.SetValue(value);
        }

        public T Value
        {
            get
            {
                var value = this.GetValue<T>();
                return value;
            }
        }

        public static implicit operator T(InArgument<T> value)
        {
            return value.Value;
        }
    }
}