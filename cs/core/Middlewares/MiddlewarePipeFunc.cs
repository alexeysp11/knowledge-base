namespace Concepts.Core.Middlewares
{
    /// <summary>
    /// Uses pointers to a function and creates a pipe of functions
    /// </summary>
    public class MiddlewarePipeFunc : IMiddlewarePipe
    {
        public string Execute()
        {
            string message = "hello"; 
            return Try(message, 
                msgw1 => Wrap(msgw1, 
                    msgt1 => Try(msgt1, 
                        msgs1 => First(msgs1)))); 
        }
        private string Try(string msg, System.Func<string, string> function)
        {
            return "<try>" + function(msg) + "</try>"; 
        }
        private string Wrap(string msg, System.Func<string, string> function)
        {
            return "<wrap>" + function(msg) + "</wrap>"; 
        }
        private string First(string msg)
        {
            return "<first>" + msg + "</first>"; 
        }
        private string Second(string msg)
        {
            return "<second>" + msg + "</second>"; 
        }
    }
}