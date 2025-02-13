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

        for (int i = 0; i < displayedInfo.GetLength(0); i++)
        {
            for (int j = 0; j < displayedInfo.GetLength(1); j++)
            {
                Console.Write(displayedInfo[i, j]);
            }
            Console.WriteLine();
        }
    }
}