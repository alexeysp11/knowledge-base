using System.Net.Sockets;
using System.Text;
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

    public static async Task SendFormAsync(NetworkStream stream, SessionInfo sessionInfo)
    {
        if (stream == null)
        {
            throw new ArgumentNullException($"Parameter '{nameof(stream)}' could not be null");
        }
        if (sessionInfo == null)
        {
            throw new ArgumentNullException($"Parameter '{nameof(sessionInfo)}' could not be null");
        }
        if (sessionInfo.DisplayedInfo == null)
        {
            throw new ArgumentNullException($"Parameter '{nameof(sessionInfo.DisplayedInfo)}' could not be null");
        }

        string result = ConsoleHelper.GetDisplayedInfoString(sessionInfo.DisplayedInfo);
        byte[] resultBytes = Encoding.UTF8.GetBytes(result);
        await stream.WriteAsync(resultBytes, 0, resultBytes.Length);
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