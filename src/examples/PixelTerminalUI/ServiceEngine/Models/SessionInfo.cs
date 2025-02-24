using PixelTerminalUI.ServiceEngine.Forms;

namespace PixelTerminalUI.ServiceEngine.Models;

public class SessionInfo
{
    public long SessionId { get; set; }
    public int FormHeight { get; set; }
    public int FormWidth { get; set; }
    public long? MenuId { get; set; }
    public string? MenuCode { get; set; }
    public string[,] DisplayedInfo { get; set; }
    public string[,] SavedDisplayedInfo { get; set; }
    public string? UserInput { get; set; }

    public BaseForm? CurrentForm { get; set; }

    public void AssignEmptyDisplayedInfo(DisplayedInfoType displayedInfoType = DisplayedInfoType.Current)
    {
        var displayedInfo = new string[FormHeight, FormWidth];

        for (int i = 0; i < displayedInfo.GetLength(0); i++)
        {
            for (int j = 0; j < displayedInfo.GetLength(1); j++)
            {
                displayedInfo[i, j] = " ";
            }
        }

        if (displayedInfoType == DisplayedInfoType.Saved)
        {
            SavedDisplayedInfo = displayedInfo;
        }
        else
        {
            DisplayedInfo = displayedInfo;
        }
    }
}