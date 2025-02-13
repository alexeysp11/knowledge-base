using TcpServiceNetCore.ConsoleAdapter;
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

        ConsoleHelper.PrintDisplayedInfo(sessionInfo.DisplayedInfo);
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