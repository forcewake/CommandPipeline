namespace CommandPipeline.Infrastructure.Arguments
{
    public sealed class OutArgument<TValue> : Argument
    {
        public OutArgument()
        {
            this.Set(default(TValue));
        }

        public void Set(TValue value)
        {
            this.SetValue(value);
        }

        public static implicit operator OutArgument<TValue>(TValue value)
        {
            var argument = new OutArgument<TValue>();
            argument.SetValue(value);
            return argument;
        }

        public static implicit operator TValue(OutArgument<TValue> value)
        {
            var obj = value.GetValue<TValue>();
            return obj;
        }
    }
}