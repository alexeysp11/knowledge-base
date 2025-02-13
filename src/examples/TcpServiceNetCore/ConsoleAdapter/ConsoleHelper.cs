namespace TcpServiceNetCore.ConsoleAdapter;

public static class ConsoleHelper
{
    public static void ClearDisplayedInfo()
    {
        // 
    }

    public static void PrintDisplayedInfo(string[,] displayedInfo)
    {
        if (displayedInfo == null)
        {
            throw new Exception($"Parameter '{nameof(displayedInfo)}' could not be null");
        }

        string result = GetDisplayedInfoString(displayedInfo);
        Console.WriteLine(result);
    }

    public static string GetDisplayedInfoString(string[,] displayedInfo)
    {
        string result = string.Empty;
        for (int i = 0; i < displayedInfo.GetLength(0); i++)
        {
            for (int j = 0; j < displayedInfo.GetLength(1); j++)
            {
                result += displayedInfo[i, j];
            }
            result += "\n";
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