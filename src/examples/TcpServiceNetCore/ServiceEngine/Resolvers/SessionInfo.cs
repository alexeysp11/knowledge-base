namespace TcpServiceNetCore.ServiceEngine.Resolvers;

public class SessionInfo
{
    public long SessionId { get; set; }
    public int FormHeight { get; set; }
    public int FormWidth { get; set; }
    public long? MenuId { get; set; }
    public string? MenuCode { get; set; }
    public string[,] DisplayedInfo { get; set; }
}