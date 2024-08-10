class Temperature: 
    """
    Additional class for representing temparature 
    """

    def __init__(self, celsius): 
        self.celsius = celsius 
        self.kelvin = celsius + 273.15
        self.farenheit = celsius * 1.80 + 32.00

class ConvertTemperature:
    """
    Description: https://leetcode.com/problems/convert-the-temperature/
    """
    
    def execute(): 
        """
        Executes the algorithm
        """
        
        print('Convert the Temperature\n'.upper())
        
        # Example 1:
        # Input: celsius = 36.50
        # Output: [309.65000,97.70000]
        # Explanation: Temperature at 36.50 Celsius converted in Kelvin is 309.65 and converted in Fahrenheit is 97.70.
        c1 = 36.50
        t1 = Temperature(c1)
        print(f'celsius: {t1.celsius}, kelvin: {t1.kelvin}, farenheit: {t1.farenheit}')

        # Example 2:
        # Input: celsius = 122.11
        # Output: [395.26000,251.79800]
        # Explanation: Temperature at 122.11 Celsius converted in Kelvin is 395.26 and converted in Fahrenheit is 251.798.
        c2 = 122.11
        t2 = Temperature(c2)
        print(f'celsius: {t2.celsius}, kelvin: {t2.kelvin}, farenheit: {t2.farenheit}')
