using System.Collections.Generic; 
using System.Linq; 
using Concepts.Core.Interfaces;

namespace Concepts.Core.DependencyInjection
{
    public class DIBuilderLifetime : IConceptCore
    {
        public string Execute()
        {
            try
            {
                var container = new DependencyContainer(); 
                container.AddTransient<HelloService>(); 
                container.AddTransient<ServiceConsumer>(); 
                container.AddSingleton<MessageService>(); 

                var resolver = new DependencyResolver(container);
                var service1 = resolver.GetService<ServiceConsumer>(); 
                var service2 = resolver.GetService<ServiceConsumer>(); 
                var service3 = resolver.GetService<ServiceConsumer>(); 

                return "<service1>" + service1.GetString() + "</service1>" 
                    + "<service2>" + service2.GetString() + "</service2>"
                    + "<service3>" + service3.GetString() + "</service3>";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        private class Dependency
        {
            public System.Type Type { get; private set; } 
            public DependencyLifetime Lifetime { get; private set; }
            public object Implementation { get; private set; }
            public bool Implemented { get; set; }
            public Dependency(System.Type type, DependencyLifetime lifetime)
            {
                Type = type; 
                Lifetime = lifetime; 
            }
            public void AddImplementation(object implementation)
            {
                Implementation = implementation; 
                Implemented = true; 
            }
        }

        private enum DependencyLifetime
        {
            Singleton = 0, 
            Transient = 1
        }

        private class DependencyResolver
        {
            private DependencyContainer _container; 
            public DependencyResolver(DependencyContainer container)
            {
                _container = container; 
            }
            public T GetService<T>()
            {
                return (T)GetService(typeof(T)); 
            }
            private object GetService(System.Type type)
            {
                var dependency = _container.GetDependency(type); 
                var constructor = dependency.Type.GetConstructors().Single();
                var parameters = constructor.GetParameters().ToArray(); 
                if (parameters.Length > 0)
                {
                    var parameterImplementations = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                        parameterImplementations[i] = GetService(parameters[i].ParameterType); 
                    return CreateImplementation(dependency, t => System.Activator.CreateInstance(t, parameterImplementations)); 
                }
                return CreateImplementation(dependency, t => System.Activator.CreateInstance(t)); 
            }
            private object CreateImplementation(Dependency dependency, System.Func<System.Type, object> factory)
            {
                if (dependency.Implemented)
                    return dependency.Implementation; 
                var implementation = factory(dependency.Type); 
                if (dependency.Lifetime == DependencyLifetime.Singleton)
                {
                    dependency.AddImplementation(implementation); 
                }
                return implementation; 
            }
        }

        private class DependencyContainer
        {
            private List<Dependency> _dependencies = new List<Dependency>(); 
            public void AddSingleton<T>()
            {
                _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Singleton)); 
            }
            public void AddTransient<T>()
            {
                _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Transient)); 
            }
            public Dependency GetDependency(System.Type type)
            {
                return _dependencies.First(x => x.Type.Name == type.Name); 
            }
        }

        private class ServiceConsumer
        {
            private HelloService _hello; 
            public ServiceConsumer(HelloService hello)
            {
                _hello = hello; 
            }
            public string GetString()
            {
                return "<consumer>" + _hello.GetString() + "</consumer>"; 
            }
        }

        private class HelloService
        {
            private MessageService _message; 
            int _random; 
            public HelloService(MessageService message)
            {
                _message = message; 
                _random = new System.Random().Next();
            }
            public string GetString()
            {
                return "<hello>" + _message.GetString() + "#" + _random + "</hello>"; 
            }
        }

        private class MessageService
        {
            int _random; 
            public MessageService()
            {
                _random = new System.Random().Next();
            }
            public string GetString()
            {
                return $"<message>Yo #{_random}</message>"; 
            }
        }
    }
}