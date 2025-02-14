using System.Net.Sockets;
using System.Text;
using TcpServiceNetCore.ConsoleAdapter.Helpers;
using TcpServiceNetCore.ServiceEngine.Models;

namespace TcpServiceNetCore.ServiceEngine.Helpers;

public static class ServiceEngineHelper
{
    public static async Task<string> ReadMessageAsync(NetworkStream stream)
    {
        byte[] messageData = new byte[256];
        int bytesRead = await stream.ReadAsync(messageData, 0, messageData.Length);
        return Encoding.UTF8.GetString(messageData, 0, bytesRead);
    }

    public static async Task SendMessageAsync(NetworkStream stream, string message)
    {
        byte[] resultBytes = Encoding.UTF8.GetBytes(message);
        await stream.WriteAsync(resultBytes, 0, resultBytes.Length);
    }

    public static void LogForm(SessionInfo sessionInfo, bool displayBorders = false)
    {
        if (sessionInfo == null)
        {
            throw new ArgumentNullException($"Parameter '{nameof(sessionInfo)}' could not be null");
        }

        ConsoleHelper.PrintDisplayedInfo(sessionInfo.DisplayedInfo, displayBorders);
    }

    public static async Task SendFormAsync(NetworkStream stream, SessionInfo sessionInfo, bool displayBorders = false)
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

        string message = ConsoleHelper.GetDisplayedInfoString(sessionInfo.DisplayedInfo, displayBorders);
        await SendMessageAsync(stream, message);
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