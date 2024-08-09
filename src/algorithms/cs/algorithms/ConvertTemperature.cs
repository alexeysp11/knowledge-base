namespace Studying.Leetcode.Algorithms
{
    /// <summary>
    /// Description: https://leetcode.com/problems/convert-the-temperature/
    /// </summary>
    public class ConvertTemperature : Studying.Leetcode.ILeetcodeProblem
    {
        private class Temperature
        {
            public float Celsius; 
            public float Kelvin; 
            public float Farenheit; 

            public Temperature(float celsius)
            {
                // Kelvin = Celsius + 273.15
                // Fahrenheit = Celsius * 1.80 + 32.00

                Celsius = celsius; 
                Kelvin = celsius + 273.15f; 
                Farenheit = celsius * 1.80f + 32.00f; 
            }

            public string GetString()
            {
                return "Celsius: " + Celsius.ToString() + ", Kelvin: " + Kelvin.ToString() + ", Farenheit: " + Farenheit.ToString(); 
            }
        }

        public void Execute()
        {
            System.Console.WriteLine("Convert the Temperature\n".ToUpper()); 
            
            // Example 1:
            // Input: celsius = 36.50
            // Output: [309.65000,97.70000]
            // Explanation: Temperature at 36.50 Celsius converted in Kelvin is 309.65 and converted in Fahrenheit is 97.70.
            float celsius1 = 36.50f; 
            Temperature temperature1 = Convert(celsius1); 
            System.Console.WriteLine(temperature1.GetString()); 

            // Example 2:
            // Input: celsius = 122.11
            // Output: [395.26000,251.79800]
            // Explanation: Temperature at 122.11 Celsius converted in Kelvin is 395.26 and converted in Fahrenheit is 251.798.
            float celsius2 = 122.11f; 
            Temperature temperature2 = Convert(celsius2); 
            System.Console.WriteLine(temperature2.GetString()); 
        }

        private Temperature Convert(float celsius) 
        {
            // Constraints:
            // 0 <= celsius <= 1000
            
            if (celsius < 0 || celsius > 1000) 
                throw new System.Exception("Violated constraint: 0 <= celsius <= 1000"); 

            Temperature temperature = new Temperature(celsius); 
            if (System.Math.Abs(temperature.Celsius - celsius) > .001f) 
                throw new System.Exception("Temperature has been initialized incorrectly: celsius is different"); 
            return temperature; 
        } 
    }
}
