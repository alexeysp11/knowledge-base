namespace PixelTerminalUI.ServiceEngine.Models;

public enum ValidateResultType
{
    /// <summary>
    /// Display previous control or form.
    /// </summary>
    Back = 0,

    /// <summary>
    /// Form or control validated.
    /// </summary>
    Next = 1,

    /// <summary>
    /// Display current form or control one more time.
    /// </summary>
    /// <remarks>Could be used when errors occured</remarks>
    Repeat = 2,

    /// <summary>
    /// Display current form or control.
    /// </summary>
    Show = 3
}