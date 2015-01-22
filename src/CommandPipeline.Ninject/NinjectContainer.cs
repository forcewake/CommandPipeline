namespace CommandPipeline.Ninject
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using CommandPipeline.Infrastructure.Pipeline;

    using global::Ninject;
    using global::Ninject.Activation;
    using global::Ninject.Activation.Blocks;
    using global::Ninject.Components;
    using global::Ninject.Modules;
    using global::Ninject.Parameters;
    using global::Ninject.Planning.Bindings;
    using global::Ninject.Syntax;

    public class NinjectContainer : ICommandContainer, IKernel
    {
        public IKernel Kernel { get; private set; }

        public NinjectContainer(INinjectModule module)
            : this(new StandardKernel(module))
        {       
        }

        public NinjectContainer()
            : this(new StandardKernel())
        {
        }

        public NinjectContainer(IKernel kernel)
        {
            this.Kernel = kernel;
        }

        public ICommand Create(Type type)
        {
            var command = this.Kernel.Get(type);
            return (ICommand)command;
        }

        /// <summary>
        /// Declares a binding for the specified service.
        /// </summary>
        /// <typeparam name="T">The service to bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T> Bind<T>()
        {
            return this.Kernel.Bind<T>();
        }

        /// <summary>
        /// Declares a binding for the specified service.
        /// </summary>
        /// <typeparam name="T1">The first service to bind.</typeparam><typeparam name="T2">The second service to bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1, T2> Bind<T1, T2>()
        {
            return this.Kernel.Bind<T1, T2>();
        }

        /// <summary>
        /// Declares a binding for the specified service.
        /// </summary>
        /// <typeparam name="T1">The first service to bind.</typeparam><typeparam name="T2">The second service to bind.</typeparam><typeparam name="T3">The third service to bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1, T2, T3> Bind<T1, T2, T3>()
        {
            return this.Kernel.Bind<T1, T2, T3>();
        }

        /// <summary>
        /// Declares a binding for the specified service.
        /// </summary>
        /// <typeparam name="T1">The first service to bind.</typeparam><typeparam name="T2">The second service to bind.</typeparam><typeparam name="T3">The third service to bind.</typeparam><typeparam name="T4">The fourth service to bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1, T2, T3, T4> Bind<T1, T2, T3, T4>()
        {
            return this.Kernel.Bind<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Declares a binding from the service to itself.
        /// </summary>
        /// <param name="services">The services to bind.</param>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<object> Bind(params Type[] services)
        {
            return this.Kernel.Bind(services);
        }

        /// <summary>
        /// Unregisters all bindings for the specified service.
        /// </summary>
        /// <typeparam name="T">The service to unbind.</typeparam>
        public void Unbind<T>()
        {
            this.Kernel.Unbind<T>();
        }

        /// <summary>
        /// Unregisters all bindings for the specified service.
        /// </summary>
        /// <param name="service">The service to unbind.</param>
        public void Unbind(Type service)
        {
            this.Kernel.Unbind(service);
        }

        /// <summary>
        /// Removes any existing bindings for the specified service, and declares a new one.
        /// </summary>
        /// <typeparam name="T1">The first service to re-bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1> Rebind<T1>()
        {
            return this.Kernel.Rebind<T1>();
        }

        /// <summary>
        /// Removes any existing bindings for the specified services, and declares a new one.
        /// </summary>
        /// <typeparam name="T1">The first service to re-bind.</typeparam><typeparam name="T2">The second service to re-bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1, T2> Rebind<T1, T2>()
        {
            return this.Kernel.Rebind<T1, T2>();
        }

        /// <summary>
        /// Removes any existing bindings for the specified services, and declares a new one.
        /// </summary>
        /// <typeparam name="T1">The first service to re-bind.</typeparam><typeparam name="T2">The second service to re-bind.</typeparam><typeparam name="T3">The third service to re-bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1, T2, T3> Rebind<T1, T2, T3>()
        {
            return this.Kernel.Rebind<T1, T2, T3>();
        }

        /// <summary>
        /// Removes any existing bindings for the specified services, and declares a new one.
        /// </summary>
        /// <typeparam name="T1">The first service to re-bind.</typeparam><typeparam name="T2">The second service to re-bind.</typeparam><typeparam name="T3">The third service to re-bind.</typeparam><typeparam name="T4">The fourth service to re-bind.</typeparam>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<T1, T2, T3, T4> Rebind<T1, T2, T3, T4>()
        {
            return this.Kernel.Rebind<T1, T2, T3, T4>();
        }

        /// <summary>
        /// Removes any existing bindings for the specified services, and declares a new one.
        /// </summary>
        /// <param name="services">The services to re-bind.</param>
        /// <returns>
        /// The fluent syntax.
        /// </returns>
        public IBindingToSyntax<object> Rebind(params Type[] services)
        {
            return this.Kernel.Rebind(services);
        }

        /// <summary>
        /// Registers the specified binding.
        /// </summary>
        /// <param name="binding">The binding to add.</param>
        public void AddBinding(IBinding binding)
        {
            this.Kernel.AddBinding(binding);
        }

        /// <summary>
        /// Unregisters the specified binding.
        /// </summary>
        /// <param name="binding">The binding to remove.</param>
        public void RemoveBinding(IBinding binding)
        {
            this.Kernel.RemoveBinding(binding);
        }

        /// <summary>
        /// Determines whether the specified request can be resolved.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// <c>True</c> if the request can be resolved; otherwise, <c>false</c>.
        /// </returns>
        public bool CanResolve(IRequest request)
        {
            return this.Kernel.CanResolve(request);
        }

        /// <summary>
        /// Determines whether the specified request can be resolved.
        /// </summary>
        /// <param name="request">The request.</param><param name="ignoreImplicitBindings">if set to <c>true</c> implicit bindings are ignored.</param>
        /// <returns>
        /// <c>True</c> if the request can be resolved; otherwise, <c>false</c>.
        /// </returns>
        public bool CanResolve(IRequest request, bool ignoreImplicitBindings)
        {
            return this.Kernel.CanResolve(request, ignoreImplicitBindings);
        }

        /// <summary>
        /// Resolves instances for the specified request. The instances are not actually resolved
        ///             until a consumer iterates over the enumerator.
        /// </summary>
        /// <param name="request">The request to resolve.</param>
        /// <returns>
        /// An enumerator of instances that match the request.
        /// </returns>
        public IEnumerable<object> Resolve(IRequest request)
        {
            return this.Kernel.Resolve(request);
        }

        /// <summary>
        /// Creates a request for the specified service.
        /// </summary>
        /// <param name="service">The service that is being requested.</param><param name="constraint">The constraint to apply to the bindings to determine if they match the request.</param><param name="parameters">The parameters to pass to the resolution.</param><param name="isOptional"><c>True</c> if the request is optional; otherwise, <c>false</c>.</param><param name="isUnique"><c>True</c> if the request should return a unique result; otherwise, <c>false</c>.</param>
        /// <returns>
        /// The created request.
        /// </returns>
        public IRequest CreateRequest(Type service, Func<IBindingMetadata, bool> constraint, IEnumerable<IParameter> parameters, bool isOptional, bool isUnique)
        {
            return this.Kernel.CreateRequest(service, constraint, parameters, isOptional, isUnique);
        }

        /// <summary>
        /// Deactivates and releases the specified instance if it is currently managed by Ninject.
        /// </summary>
        /// <param name="instance">The instance to release.</param>
        /// <returns>
        /// <see langword="True"/> if the instance was found and released; otherwise <see langword="false"/>.
        /// </returns>
        public bool Release(object instance)
        {
            return this.Kernel.Release(instance);
        }

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        /// A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.
        /// </returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param>
        public object GetService(Type serviceType)
        {
            return this.Kernel.GetService(serviceType);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Kernel.Dispose();
        }

        /// <summary>
        /// Gets a value indicating whether this instance is disposed.
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this.Kernel.IsDisposed;
            }
        }

        /// <summary>
        /// Gets the modules that have been loaded into the kernel.
        /// </summary>
        /// <returns>
        /// A series of loaded modules.
        /// </returns>
        public IEnumerable<INinjectModule> GetModules()
        {
            return this.Kernel.GetModules();
        }

        /// <summary>
        /// Determines whether a module with the specified name has been loaded in the kernel.
        /// </summary>
        /// <param name="name">The name of the module.</param>
        /// <returns>
        /// <c>True</c> if the specified module has been loaded; otherwise, <c>false</c>.
        /// </returns>
        public bool HasModule(string name)
        {
            return this.Kernel.HasModule(name);
        }

        /// <summary>
        /// Loads the module(s) into the kernel.
        /// </summary>
        /// <param name="m">The modules to load.</param>
        public void Load(IEnumerable<INinjectModule> m)
        {
            this.Kernel.Load(m);
        }

        /// <summary>
        /// Loads modules from the files that match the specified pattern(s).
        /// </summary>
        /// <param name="filePatterns">The file patterns (i.e. "*.dll", "modules/*.rb") to match.</param>
        public void Load(IEnumerable<string> filePatterns)
        {
            this.Kernel.Load(filePatterns);
        }

        /// <summary>
        /// Loads modules defined in the specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to search.</param>
        public void Load(IEnumerable<Assembly> assemblies)
        {
            this.Kernel.Load(assemblies);
        }

        /// <summary>
        /// Unloads the plugin with the specified name.
        /// </summary>
        /// <param name="name">The plugin's name.</param>
        public void Unload(string name)
        {
            this.Kernel.Unload(name);
        }

        /// <summary>
        /// Injects the specified existing instance, without managing its lifecycle.
        /// </summary>
        /// <param name="instance">The instance to inject.</param><param name="parameters">The parameters to pass to the request.</param>
        public void Inject(object instance, params IParameter[] parameters)
        {
            this.Kernel.Inject(instance, parameters);
        }

        /// <summary>
        /// Gets the bindings registered for the specified service.
        /// </summary>
        /// <param name="service">The service in question.</param>
        /// <returns>
        /// A series of bindings that are registered for the service.
        /// </returns>
        public IEnumerable<IBinding> GetBindings(Type service)
        {
            return this.Kernel.GetBindings(service);
        }

        /// <summary>
        /// Begins a new activation block, which can be used to deterministically dispose resolved instances.
        /// </summary>
        /// <returns>
        /// The new activation block.
        /// </returns>
        public IActivationBlock BeginBlock()
        {
            return this.Kernel.BeginBlock();
        }

        /// <summary>
        /// Gets the kernel settings.
        /// </summary>
        public INinjectSettings Settings
        {
            get
            {
                return this.Kernel.Settings;
            }
        }

        /// <summary>
        /// Gets the component container, which holds components that contribute to Ninject.
        /// </summary>
        public IComponentContainer Components
        {
            get
            {
                return this.Kernel.Components;
            }
        }
    }
}
