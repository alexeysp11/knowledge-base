using System.Collections.Generic; 
using KnowledgeBase.Patterns.Core.Interfaces;

namespace KnowledgeBase.Patterns.Core.Middlewares
{
    /// <summary>
    /// Uses ASP.NET Core-like approach 
    /// </summary>
    public class MiddlewarePipeDI : IConceptCore
    {
        public string Execute()
        {
            string message = "hello"; 
            var pipe = new PipeBuilder(First)
                .AddPipe(typeof(TryPipe))
                .AddPipe(typeof(WrapPipe))
                .AddPipe(typeof(TryPipe))
                .Build(); 
            return pipe(message);
        }
        private string First(string msg)
        {
            return "<first>" + msg + "</first>"; 
        }
        private string Second(string msg)
        {
            return "<second>" + msg + "</second>"; 
        }

        private class PipeBuilder
        {
            private System.Func<string, string> _mainFunction; 
            private List<System.Type> _pipeTypes; 

            public PipeBuilder(System.Func<string, string> mainFunction)
            {
                _mainFunction = mainFunction; 
                _pipeTypes = new List<System.Type>(); 
            }

            public PipeBuilder AddPipe(System.Type pipeType)
            {
                // if (!pipeType.IsInstanceOfType(typeof(AbstractPipe))) 
                //     throw new System.Exception("Incorrect pipe type"); 
                _pipeTypes.Add(pipeType); 
                return this; 
            }

            public System.Func<string, string> Build()
            {
                return CreatePipe(0); 
            }

            private System.Func<string, string> CreatePipe(int index)
            {
                if (index < _pipeTypes.Count - 1)
                {
                    var childPipeHandle = CreatePipe(index + 1); 
                    var pipe = (AbstractPipe) System.Activator.CreateInstance(_pipeTypes[index], childPipeHandle); 
                    return pipe.Handle; 
                }
                else 
                {
                    var finalPipe = (AbstractPipe) System.Activator.CreateInstance(_pipeTypes[index], _mainFunction); 
                    return finalPipe.Handle; 
                }
            }
        }

        private abstract class AbstractPipe
        {
            protected System.Func<string, string> _function; 

            public AbstractPipe(System.Func<string, string> function)
            {
                _function = function; 
            }

            public abstract string Handle(string msg); 
        }

        private class WrapPipe : AbstractPipe
        {
            public WrapPipe(System.Func<string, string> function) : base(function) {}

            public override string Handle(string msg)
            {
                return "<wrap>" + _function(msg) + "</wrap>"; 
            }
        }

        private class TryPipe : AbstractPipe
        {
            public TryPipe(System.Func<string, string> function) : base(function) {}

            public override string Handle(string msg)
            {
                return "<try>" + _function(msg) + "</try>"; 
            }
        }
    }
}