namespace CommandPipeline.Infrastructure.Arguments
{
    public abstract class Argument
    {
        protected object CurrentValue { get; set; }

        internal string Name { get; set; }

        internal T GetValue<T>()
        {
            return (T) this.CurrentValue;
        }

        internal object GetValue()
        {
            return this.CurrentValue;
        }

        public bool HasValue
        {
            get
            {
                return this.CurrentValue != null;
            }
        }

        internal void SetValue(object value)
        {
            this.CurrentValue = value;
        }
    }
}