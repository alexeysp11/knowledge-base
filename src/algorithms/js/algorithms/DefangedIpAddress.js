// Description: https://leetcode.com/problems/defanging-an-ip-address/

class DefangedIpAddress {
    execute() {
        // Example 1:
        // Input: address = "1.1.1.1"
        // Output: "1[.]1[.]1[.]1"
        var input1 = '1.1.1.1'; 
        var result = '{ { input: ' + input1 + ', output: ' + this.defang(input1) + ' },'; 
        
        // Example 2:
        // Input: address = "255.100.50.0"
        // Output: "255[.]100[.]50[.]0"
        var input2 = '255.100.50.0'; 
        result += '{ input: ' + input2 + ', output: ' + this.defang(input2) + ' } }'; 
        
        return result; 
    }

    defang(aInputIp) {
        var outputIp = ''; 
        for (var i = 0; i < aInputIp.length; i++) {
            if (aInputIp[i] == '.') {
                outputIp += '[.]';
            } else {
                outputIp += aInputIp[i];
            }
        }
        return outputIp; 
    }

    static getLeetcodeProblemName() {
        return "Defanging an IP-address".toUpperCase(); 
    }
}
