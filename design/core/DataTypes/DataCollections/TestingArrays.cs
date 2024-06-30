using System.Text;
using Concepts.Core.Interfaces;

namespace Concepts.Core.DataTypes.DataCollections
{
    public class TestingArrays : IConceptCore
    {
        public string Execute()
        {
            try
            {
                var sb = new StringBuilder();

                var array1 = new int[] { 1, 2, 3, 4, 5 };
                var array2 = (int[])System.Array.CreateInstance(typeof(int), 5);
                for (int i = 0; i < array2.Length; i++)
                {
                    array2.SetValue(i + 1, i);
                }

                AddArrayToStringBuilder(sb, array1);
                AddArrayToStringBuilder(sb, array2);
                sb.Append("Type equals: ").Append(array1.GetType() == array2.GetType());

                return "<service>" + sb.ToString() + "</service>";
            }
            catch (System.Exception ex)
            {
                return ex.Message;
            }
        }

        private void AddArrayToStringBuilder(StringBuilder sb, int[] array)
        {
            sb.Append("[ ");
            foreach (var element in array)
            {
                sb.Append(element).Append(" ");
            }
            sb.Append("]\n");
        }
    }
}