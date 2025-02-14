namespace TcpServiceNetCore.ServiceEngine.Models;

public class SessionInfo
{
    public long SessionId { get; set; }
    public int FormHeight { get; set; }
    public int FormWidth { get; set; }
    public long? MenuId { get; set; }
    public string? MenuCode { get; set; }
    public string[,] DisplayedInfo { get; set; }

    public void AssignEmptyDisplayedInfo()
    {
        DisplayedInfo = new string[FormHeight, FormWidth];

        for (int i = 0; i < DisplayedInfo.GetLength(0); i++)
        {
            for (int j = 0; j < DisplayedInfo.GetLength(1); j++)
            {
                DisplayedInfo[i, j] = " ";
            }
        }
    }
}