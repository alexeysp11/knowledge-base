namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// Description: https://leetcode.com/problems/defanging-an-ip-address/
    /// </summary>
    public class DefangedIpAddress : Studying.Leetcode.ILeetcodeProblem
    {
        public void Execute()
        {
            System.Console.WriteLine("Defanging an IP-address\n".ToUpper()); 

            // Example 1:
            // Input: address = "1.1.1.1"
            // Output: "1[.]1[.]1[.]1"
            string input1 = "1.1.1.1";
            string output1 = Defang(input1);
            System.Console.WriteLine("input: " + input1); 
            System.Console.WriteLine("output: " + output1); 

            // Example 2:
            // Input: address = "255.100.50.0"
            // Output: "255[.]100[.]50[.]0"
            string input2 = "255.100.50.0";
            string output2 = Defang(input2);
            System.Console.WriteLine("input: " + input2); 
            System.Console.WriteLine("output: " + output2); 
        }

        private string Defang(string input)
        {
            // Constraints:
            // The given address is a valid IPv4 address.
            System.Net.IPAddress address;
            if (!System.Net.IPAddress.TryParse(input, out address)) 
                throw new System.Exception("Violated constraint: The given address is a valid IPv4 address"); 

            // Option 1: 
            // return input.Replace(".", "[.]"); 

            // Option 2: 
            string tmp = string.Empty; 
            for (int i = 0; i < input.Length; i++) 
                tmp += input[i] == '.' ? "[.]" : input[i].ToString(); 
            return tmp; 
        }
    }
}