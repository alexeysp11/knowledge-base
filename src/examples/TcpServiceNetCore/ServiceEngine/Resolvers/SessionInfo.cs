namespace TcpServiceNetCore.ServiceEngine.Resolvers;

public class SessionInfo
{
    public long SessionId { get; set; }
    public long? MenuId { get; set; }
    public string? MenuCode { get; set; }
    public string[][] DisplayedInfo { get; set; }
}