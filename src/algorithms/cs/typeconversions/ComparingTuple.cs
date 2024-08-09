using System; 
using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.TypeConversions
{
    /// <summary>
    /// 
    /// </summary>
    public class ComparingTuple : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            var a = new Tuple<object, object, object>(1, 1, 1);
            var b = new Tuple<object, object, object>(1, 1, 1);

            System.Console.WriteLine("a.Item1 == b.Item1: " + (a.Item1 == b.Item1));
            System.Console.WriteLine("a.Equals(b): " + (a.Equals(b)));
            System.Console.WriteLine("a == b: " + (a == b));
        }
    }
}