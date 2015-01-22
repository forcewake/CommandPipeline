namespace CommandPipeline.Infrastructure.Pipeline.Implementation
{
    using System;
    using System.Collections.Generic;

    using CommandPipeline.Infrastructure.ParametersContext;

    public abstract class MessageHandlerPipelineBase
    {
        protected IParametersContext<ICommand> Context { get; private set; }

        protected ICommandContainer CommandContainer { get; private set; }

        protected readonly List<Type> CommandsType = new List<Type>();

        protected MessageHandlerPipelineBase(ICommandContainer commandContainer, IParametersContext<ICommand> context)
        {
            this.Context = context;
            this.CommandContainer = commandContainer;
        }

        protected virtual void Handle(object message)
        {
            foreach (var commandType in this.CommandsType)
            {
                var command = this.CommandContainer.Create(commandType);

                this.Context.InitializeInArguments(command);
                this.Context.InitializeOutArguments(command);

                command.Execute(message);

                this.Context.RetrieveOutArguments(command);
            }
        }
    }
}