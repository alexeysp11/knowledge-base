using System.Linq;
using System.Collections.Generic; 
using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.Linq
{
    /// <summary>
    /// 
    /// </summary>
    public class SkipInList : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            var numbers = new List<int> { 65, 56, 21, 46, 11, 11 };
            var firstResult = numbers
                .Skip(3)
                .Select(x => 
                {
                    System.Console.WriteLine(x);
                    return x;
                });
            System.Console.WriteLine("55");

            var secondResult = firstResult.OrderByDescending(x => x).Distinct().ToList();

            foreach(var number in secondResult)
                System.Console.WriteLine(number);
        }
    }
}