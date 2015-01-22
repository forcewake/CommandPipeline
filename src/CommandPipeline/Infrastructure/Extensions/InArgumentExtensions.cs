namespace CommandPipeline.Infrastructure.Extensions
{
    using System;
    using System.Diagnostics;

    using CommandPipeline.Infrastructure.Arguments;

    using EnsureThat;

    public static class InArgumentExtensions
    {
        [DebuggerStepThrough]
        public static TArgument TakeValue<TArgument>(this InArgument<TArgument> param)
        {
            return param.Value;
        }

        [DebuggerStepThrough]
        public static bool TryGetValue<TArgument>(this Param<InArgument<TArgument>> param, out TArgument value)
        {
            try
            {
                value = param.Value.Value;
                return true;
            }
            catch (Exception)
            {
                value = default(TArgument);
                return false;
            }
        }

        [DebuggerStepThrough]
        public static InArgument<TArgument> Then<TArgument>(this Param<InArgument<TArgument>> param)
        {
            return param.Value;
        }

        public static TArgument Is<TArgument>(this Param<InArgument<TArgument>> param, Action<Param<InArgument<TArgument>>> validation)
        {
            validation(param);
            return param.Value;
        }

        public static TArgument Is<TArgument>(this Param<InOutArgument<TArgument>> param, Action<Param<InOutArgument<TArgument>>> validation)
        {
            validation(param);
            return param.Value;
        }
    }
}
