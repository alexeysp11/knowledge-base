using System.Collections.Generic; 
using System.Linq; 
using Concepts.Core.Interfaces;

namespace Concepts.Core.DependencyInjection
{
    public class DIBuilder : IConceptCore
    {
        public string Execute()
        {
            try
            {
                // var service = new HelloService(); 
                // var consumer = new ServiceConsumer(service); 

                var container = new DependencyContainer(); 
                container.AddDependency(typeof(HelloService)); 
                container.AddDependency<ServiceConsumer>(); 
                container.AddDependency<MessageService>(); 

                var resolver = new DependencyResolver(container);
                var service = resolver.GetService<ServiceConsumer>(); 

                return "<service>" + service.GetString() + "</service>";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
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
            public object GetService(System.Type type)
            {
                var dependency = _container.GetDependency(type); 
                var constructor = dependency.GetConstructors().Single();
                var parameters = constructor.GetParameters().ToArray(); 
                if (parameters.Length > 0)
                {
                    var parameterImplementations = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; i++)
                        parameterImplementations[i] = GetService(parameters[i].ParameterType); 
                    return System.Activator.CreateInstance(dependency, parameterImplementations); 
                }
                return System.Activator.CreateInstance(dependency); 
            }
        }

        private class DependencyContainer
        {
            private List<System.Type> _dependencies = new List<System.Type>(); 
            public void AddDependency(System.Type type)
            {
                _dependencies.Add(type); 
            }
            public void AddDependency<T>()
            {
                AddDependency(typeof(T)); 
            }
            public System.Type GetDependency(System.Type type)
            {
                return _dependencies.First(x => x.Name == type.Name); 
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
            public HelloService(MessageService message)
            {
                _message = message; 
            }
            public string GetString()
            {
                return "<hello>" + "Hello world" + _message.GetString() + "</hello>"; 
            }
        }

        private class MessageService
        {
            public string GetString()
            {
                return "<message>Yo</message>"; 
            }
        }
    }
}