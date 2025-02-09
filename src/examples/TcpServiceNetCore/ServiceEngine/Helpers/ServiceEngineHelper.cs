using TcpServiceNetCore.ServiceEngine.Resolvers;

namespace TcpServiceNetCore.ServiceEngine.Helpers;

public static class ServiceEngineHelper
{
    public static void SendMessage(string message)
    {
        // 
    }

    public static void LogForm(SessionInfo sessionInfo)
    {
        if (sessionInfo == null)
        {
            throw new ArgumentNullException($"Parameter '{nameof(sessionInfo)}' could not be null");
        }
        if (sessionInfo.DisplayedInfo == null)
        {
            throw new Exception($"Parameter '{nameof(sessionInfo.DisplayedInfo)}' could not be null");
        }

        // Print data.
        for (int i = 0; i < sessionInfo.DisplayedInfo.GetLength(0); i++)
        {
            for (int j = 0; j < sessionInfo.DisplayedInfo.GetLength(1); j++)
            {
                Console.Write(sessionInfo.DisplayedInfo[i, j]);
            }
            Console.WriteLine();
        }
    }

    public static void SendForm(SessionInfo sessionInfo)
    {
        // 
    }

    public static void SaveForm(SessionInfo sessionInfo)
    {
        // 
    }

    public static void RestoreForm(SessionInfo sessionInfo)
    {
        // 
    }
}