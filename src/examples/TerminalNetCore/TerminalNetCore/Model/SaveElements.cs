using System.Collections.Generic;
using System.Text.Json;

namespace TerminalNetCore
{
    class SaveElements
    {
        public static void ConvertIntoJsonString(List<Button> buttons)
        {
            int i = 0;                  // Index of current element. 
            int prev = 0;               // Index of previous element. 
            var jsonString = "[\n";     // Initial symbols of JSON file.
            
            foreach (var element in buttons)
            {
                // Create instance of DB.ElementUI for storing necessary fields. 
                var uielement = new DB.ElementUI();
                uielement.Name = element.Name;
                uielement.Top = element.BtnInitRow;
                uielement.Bottom = element.BtnInitRow + 3;
                uielement.Left = element.BtnInitCol;
                uielement.Right = element.BtnInitCol + element.Width;
                
                // Separate JSON objects with comma.
                if (i != 0 && i > prev)
                {
                    jsonString += ",\n"; 
                }

                // Add JSON object.
                jsonString += JsonSerializer.Serialize(
                    uielement, 
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                    }
                );

                // Assign optional variables again. 
                prev = i;
                i++; 
            }

            jsonString += "\n]";    // Final symbol in JSON file. 

            DB.TerminalDB.StoreUIElements(jsonString);
        }
    }
}