namespace CommandPipeline.Infrastructure.ParametersContext
{
    using System;
    using System.Linq.Expressions;

    public interface IParametersContext<in T>
    {
        void InitializeOutArguments(T obj);

        void RetrieveOutArguments(T obj);

        void InitializeInArguments(T obj);

        void Set<TType, TPropType>(Expression<Func<TType, TPropType>> expression, TPropType value);

        TPropType Get<TType, TPropType>(Expression<Func<TType, TPropType>> expression);
    }
}