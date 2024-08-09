using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.MemoryAllocation
{
    /// <summary>
    /// 
    /// </summary>
    public class ClassStructAllocation : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        internal class PersonClass
        {
            internal long Id { get; set; }
            internal string Name { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        internal struct PersonStruct
        {
            internal long Id { get; set; }
            internal string Name { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // 
            var personClass = new PersonClass
            {
                Id = 1,
                Name = "Person class"
            };
            var personStruct = new PersonStruct
            {
                Id = 2,
                Name = "Person Struct"
            };

            // 
            PrintPersonClass(personClass);
            PrintPersonStruct(personStruct);
        }

        private void PrintPersonClass(PersonClass person)
        {
            System.Console.WriteLine("Class #{0} {1}", person.Id, person.Name);
        }

        private void PrintPersonStruct(PersonStruct person)
        {
            System.Console.WriteLine("Struct #{0} {1}", person.Id, person.Name);
        }
    }
}