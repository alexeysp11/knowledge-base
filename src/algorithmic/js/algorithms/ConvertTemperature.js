// Description: https://leetcode.com/problems/convert-the-temperature/

class ConvertTemperature {
    constructor() {
        this.Temperature = class Temperature {
            constructor(celsius) {
                this.celsius = celsius; 
                this.kelvin = celsius  + 273.15; 
                this.farenheit = celsius * 1.80 + 32.00; 
            }

            getResult() {
                return "{ celsius: " + this.celsius + ", kelvin: " + this.kelvin + ", farenheit: " + this.farenheit + " }"; 
            }
        }
    }

    execute() {
        // Example 1:
        // Input: celsius = 36.50
        // Output: [309.65000,97.70000]
        // Explanation: Temperature at 36.50 Celsius converted in Kelvin is 309.65 and converted in Fahrenheit is 97.70.
        var result = ''; 
        var t1 = new this.Temperature(36.50);
        result += t1.getResult() + ", "; 
        
        // Example 2:
        // Input: celsius = 122.11
        // Output: [395.26000,251.79800]
        // Explanation: Temperature at 122.11 Celsius converted in Kelvin is 395.26 and converted in Fahrenheit is 251.798.
        var t2 = new this.Temperature(122.11);
        result += t2.getResult(); 

        return result;
    }

    static getLeetcodeProblemName() {
        return "Convert the Temperature".toUpperCase(); 
    }
}