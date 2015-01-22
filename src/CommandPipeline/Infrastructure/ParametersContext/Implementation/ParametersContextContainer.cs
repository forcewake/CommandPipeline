namespace CommandPipeline.Infrastructure.ParametersContext.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using CommandPipeline.Infrastructure.Arguments;

    public class ParametersContextContainer : IParametersContextContainer
    {
        private readonly Dictionary<string, Argument> arguments;

        public ParametersContextContainer()
        {
            this.arguments = new Dictionary<string, Argument>();
        }

        public Argument Get(string name)
        {
            Argument argument;
            if (this.arguments.TryGetValue(name.ToLower(), out argument))
            {
                return argument;
            }

            throw new KeyNotFoundException(name);
        }

        public bool TryGet(string name, out Argument argument)
        {
            bool keyWasFound;
            try
            {
                argument = this.Get(name);
                keyWasFound = true;
            }
            catch (KeyNotFoundException)
            {
                argument = null;
                keyWasFound = false;
            }

            return keyWasFound;
        }

        public void Set(string name, object @object)
        {
            var lowerName = name.ToLower();
            var argument = new ArgumentWrapper(lowerName, @object);
            this.arguments[lowerName] = argument;
        }

        public void Set<T>(Expression<Func<T>> expression)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            this.Set(body.Member.Name, expression.Compile().Invoke());
        }
    }
}