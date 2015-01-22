namespace CommandPipeline.Infrastructure.ParametersContext
{
    using System;
    using System.Linq.Expressions;

    using CommandPipeline.Infrastructure.Arguments;

    public interface IParametersContextContainer
    {
        Argument Get(string name);

        bool TryGet(string name, out Argument argument);

        void Set(string name, object @object);

        void Set<T>(Expression<Func<T>> expression);
    }
}