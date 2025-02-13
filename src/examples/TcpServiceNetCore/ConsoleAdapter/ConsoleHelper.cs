namespace TcpServiceNetCore.ConsoleAdapter;

public static class ConsoleHelper
{
    public static void ClearDisplayedInfo()
    {
        // 
    }

    public static void PrintDisplayedInfo(string[,] displayedInfo, bool displayBorders = false)
    {
        if (displayedInfo == null)
        {
            throw new Exception($"Parameter '{nameof(displayedInfo)}' could not be null");
        }

        string result = GetDisplayedInfoString(displayedInfo, displayBorders);
        Console.WriteLine(result);
    }

    public static string GetDisplayedInfoString(string[,] displayedInfo, bool displayBorders = false)
    {
        string result = string.Empty;

        int qtyRows = displayedInfo.GetLength(0);
        int qtyColumns = displayedInfo.GetLength(1);

        if (displayBorders)
        {
            result += new String('-', qtyColumns);
            result += "\n";
        }

        for (int i = 0; i < qtyRows; i++)
        {
            for (int j = 0; j < qtyColumns; j++)
            {
                result += displayedInfo[i, j];
            }
            if (displayBorders)
            {
                result += "|";
            }
            result += "\n";
        }

        if (displayBorders)
        {
            result += new String('-', qtyColumns);
        }

        return result;
    }

    public static string EnterLine(
        string? hint = null,
        bool allowEmptyString = false,
        string emptyStringReplacement = null)
    {
        string result = string.Empty;
        while (true)
        {
            if (!string.IsNullOrEmpty(hint))
            {
                Console.WriteLine(hint);
            }

            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(emptyStringReplacement) && string.IsNullOrEmpty(input))
            {
                input = emptyStringReplacement;
            }
            if (!string.IsNullOrEmpty(input))
            {
                result = input;
                break;
            }
            if (allowEmptyString)
            {
                result = input;
                break;
            }
        }
        return result;
    }
}