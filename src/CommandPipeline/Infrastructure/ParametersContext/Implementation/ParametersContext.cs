namespace CommandPipeline.Infrastructure.ParametersContext.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    using CommandPipeline.Infrastructure.Arguments;
    using CommandPipeline.Infrastructure.Extensions;

    public class ParametersContext<T> : IParametersContext<T>
    {
        public ParametersContext()
            : this(new ParametersContextContainer())
        {
        }

        public ParametersContext(IParametersContextContainer container)
        {
            this.Container = container;
        }

        protected IParametersContextContainer Container { get; set; }

        public virtual void InitializeOutArguments(T obj)
        {
            this.InitializeObjectProperties(obj, typeof(OutArgument<>));
            this.InitializeObjectProperties(obj, typeof(InOutArgument<>));
        }

        private void InitializeObjectProperties(T obj, Type genericType)
        {
            var instanceContainer = this.GetGenericPropertiesOfType(obj, genericType);
            var properties = instanceContainer.PropertyInfos;
            var instance = instanceContainer.Instance;
            foreach (var outProperty in properties)
            {
                var value = outProperty.GetValue(instance, null);

                if (value == null)
                {
                    var genericArguments = outProperty.PropertyType.GetGenericArguments();
                    var instanceForGeneric = genericType.CreateInstanceForGeneric(genericArguments);
                    outProperty.SetValue(instance, instanceForGeneric);
                }
            }
        }

        public virtual void RetrieveOutArguments(T obj)
        {
            this.RetrieveInstanceArguments(obj, typeof(OutArgument<>));
            this.RetrieveInstanceArguments(obj, typeof(InOutArgument<>));
        }

        private void RetrieveInstanceArguments(T obj, Type genericType)
        {
            var instanceContainer = this.GetGenericPropertiesOfType(obj, genericType);

            var properties = instanceContainer.PropertyInfos;
            var instance = instanceContainer.Instance;

            foreach (var outProperty in properties)
            {
                var value = outProperty.GetValue(instance, null);
                var argument = (Argument)value;
                this.Container.Set(outProperty.Name, argument.GetValue());
            }
        }

        public virtual void InitializeInArguments(T obj)
        {
            this.InitializeArguments(obj, typeof(InArgument<>));
            this.InitializeArguments(obj, typeof(InOutArgument<>));
        }

        private void InitializeArguments(T obj, Type genericType)
        {
            InstanceContainer instanceContainer = this.GetGenericPropertiesOfType(obj, genericType);

            var properties = instanceContainer.PropertyInfos;
            var instance = instanceContainer.Instance;

            foreach (var inputProperty in properties)
            {
                var genericArguments = inputProperty.PropertyType.GetGenericArguments();

                Argument argument;

                if (!this.Container.TryGet(inputProperty.Name, out argument))
                {
                    continue;
                }

                // Get value from Context
                var value = argument.GetValue();

                var instanceForGeneric = genericType.CreateInstanceForGeneric(genericArguments);

                var instanceArgument = (Argument)instanceForGeneric;

                instanceArgument.SetValue(value);

                instanceArgument.Name = inputProperty.Name;

                inputProperty.SetValue(instance, instanceForGeneric);
            }
        }

        public void Set<TType, TPropType>(Expression<Func<TType, TPropType>> expression, TPropType value)
        {
            string name = GetMemberName(expression);

            this.Container.Set(name, value);
        }

        public TPropType Get<TType, TPropType>(Expression<Func<TType, TPropType>> expression)
        {
            string name = GetMemberName(expression);

            Argument arg = this.Container.Get(name);

            return arg != null ? arg.GetValue<TPropType>() : default(TPropType);
        }

        protected virtual InstanceContainer GetGenericPropertiesOfType(T instance, Type genericType)
        {
            InstanceContainer instanceContainer = GetInstanceProperties(instance);

            var acceptableProperties = new List<PropertyInfo>();
            var properties = instanceContainer.PropertyInfos;

            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType)
                {
                    Type propertyType = property.PropertyType.GetGenericTypeDefinition();
                    if (propertyType == genericType)
                    {
                        acceptableProperties.Add(property);
                    }
                }
            }
            return new InstanceContainer(instanceContainer.Instance, acceptableProperties);
        }

        private static InstanceContainer GetInstanceProperties(T instance)
        {
            Type instanceType = instance.GetType();

            PropertyInfo[] instanceProperties = instanceType.GetProperties().ToArray();

            return new InstanceContainer(instance, instanceProperties);
        }

        protected class InstanceContainer
        {
            public InstanceContainer(object instance, IEnumerable<PropertyInfo> propertyInfos)
            {
                this.Instance = instance;
                this.PropertyInfos = propertyInfos;
            }

            public object Instance { get; private set; }

            public IEnumerable<PropertyInfo> PropertyInfos { get; private set; }
        }

        private static string GetMemberName(LambdaExpression memberSelector)
        {
            Func<Expression, string> nameSelector = null;
            nameSelector = e =>
                {
                    switch (e.NodeType)
                    {
                        case ExpressionType.Parameter:
                            return ((ParameterExpression)e).Name;
                        case ExpressionType.MemberAccess:
                            return ((MemberExpression)e).Member.Name;
                        case ExpressionType.Call:
                            return ((MethodCallExpression)e).Method.Name;
                        case ExpressionType.Convert:
                        case ExpressionType.ConvertChecked:
                            return nameSelector(((UnaryExpression)e).Operand);
                        case ExpressionType.Invoke:
                            return nameSelector(((InvocationExpression)e).Expression);
                        case ExpressionType.ArrayLength:
                            return "Length";
                        default:
                            throw new Exception("not a proper member selector");
                    }
                };

            return nameSelector(memberSelector.Body);
        }
    }
}