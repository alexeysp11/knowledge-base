using System.Collections.Generic;
using System.Text.Json;

namespace DB
{
    public class TerminalDB
    {
        public static void StorePrevCommands()
        {

        }

        public static void StoreUIElements(string jsonString)
        {
            System.IO.File.WriteAllText(@"..\DB\ElementBorders.json", jsonString);
        }

        /*
        public static void StoreUIElements(TerminalNetCore.MainField mfield) 
        {

        }

        public static void StoreUIElements(TerminalNetCore.CommandLine cmd) 
        {

        }
        */

        public static void DeleteJsonFile()
        {
            System.IO.File.Delete(@"..\DB\ElementBorders.json");
        }

        public static List<ElementUI> ReadJsonFile()
        {
            List<ElementUI> items;

            using (var r = new System.IO.StreamReader(@"..\DB\ElementBorders.json"))
            {
                string json = r.ReadToEnd();
                items = JsonSerializer.Deserialize<List<ElementUI>>(json);
            }

            return items;
        }
    }
}