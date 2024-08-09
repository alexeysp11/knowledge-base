using System.Threading; 
using System.Threading.Tasks; 

namespace Studying.Leetcode.TypeConversions
{
    /// <summary>
    /// 
    /// </summary>
    public class BoxingEquality : Studying.Leetcode.ILeetcodeProblem
    {
        /// <summary>
        /// 
        /// </summary>
        private class TestClass
        {
            internal long Id { get; set; }
            internal string Name { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private struct TestStruct
        {
            internal long Id { get; set; }
            internal string Name { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private enum TestEnum
        {
            First,
            Second
        }

        /// <summary>
        /// 
        /// </summary>
        public void Execute()
        {
            // 
            var class1 = new TestClass
            {
                Id = 1,
                Name = "Test name"
            };
            var class2 = new TestClass
            {
                Id = 1,
                Name = "Test name"
            };
            var struct1 = new TestStruct
            {
                Id = 1,
                Name = "Test name"
            };
            var struct2 = new TestStruct
            {
                Id = 1,
                Name = "Test name"
            };

            // 
            var intComparing = (object)1 == (object)1;
            var stringComparing = (object)"Hello" == (object)"Hello";
            var enumComparing = (object)TestEnum.First == (object)TestEnum.First;
            var class11Comparing = (object)class1 == (object)class1;
            var class12Comparing = (object)class1 == (object)class2;
            var struct11Comparing = (object)struct1 == (object)struct1;
            var struct12Comparing = (object)struct1 == (object)struct2;

            // 
            System.Console.WriteLine("intComparing: {0}", intComparing);
            System.Console.WriteLine("stringComparing: {0}", stringComparing);
            System.Console.WriteLine("enumComparing: {0}", enumComparing);
            System.Console.WriteLine("class11Comparing: {0}", class11Comparing);
            System.Console.WriteLine("class12Comparing: {0}", class12Comparing);
            System.Console.WriteLine("struct11Comparing: {0}", struct11Comparing);
            System.Console.WriteLine("struct12Comparing: {0}", struct12Comparing);
        }
    }
}