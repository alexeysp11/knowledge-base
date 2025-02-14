using System;

namespace TcpServiceNetCore.ServiceEngine.Controls;

public class TextEditControl : TextControl
{
    public bool Required { get; set; }
    public string EmptyEnterSymbol { get; set; }
    public string? Hint { get; set; }
    public Func<bool>? EnterValidation { get; set; }

    public TextEditControl? NextEditControl { get; set; }
    public TextEditControl? PreviousEditControl { get; set; }

    public TextEditControl() : base()
    {
        Editable = true;
        Required = true;
        EmptyEnterSymbol = ".";
    }

    public override void Show()
    {
        if (!OnShowValidation())
        {
            return;
        }

        AddControlToForm();
    }

    public override bool OnShowValidation()
    {
        if (!base.OnShowValidation())
        {
            return false;
        }
        return Required;
    }

    public virtual bool OnEnterValidation()
    {
        if (EnterValidation != null)
        {
            return EnterValidation();
        }
        return true;
    }

    public void GetUserInput()
    {
        // Get value from user.

        // Validate user input.
        if (OnEnterValidation())
        {
            return;
        }
        
        Value = "";

        GetUserInput();
    }

    public void AddControlToForm()
    {
        int width = 0;
        int left = Left;
        int top = Top;

        if (EntireLine)
        {
            Width = SessionInfo.FormWidth;
        }

        // Display empty enter symbol.
        for (int i = left; i < Width; i++)
        {
            if (top >= SessionInfo.DisplayedInfo.GetLength(0))
            {
                break;
            }
            SessionInfo.DisplayedInfo[top, i] = EmptyEnterSymbol;
        }

        // Display value.
        foreach (char ch in Value)
        {
            if (width >= Width)
            {
                break;
            }
            if (top >= SessionInfo.DisplayedInfo.GetLength(0) || left >= SessionInfo.DisplayedInfo.GetLength(1))
            {
                break;
            }

            SessionInfo.DisplayedInfo[top, left] = $"{ch}";

            left += 1;
            width += 1;
        }

        // Display hint.
        int lastRowIndex = SessionInfo.DisplayedInfo.GetLength(0) - 1;
        for (int i = 0; i < Width; i++)
        {
            SessionInfo.DisplayedInfo[lastRowIndex, i] = " ";
        }
        if (!string.IsNullOrEmpty(Hint))
        {
            int i = 0;
            foreach (var ch in Hint)
            {
                if (i >= Width)
                {
                    break;
                }
                SessionInfo.DisplayedInfo[lastRowIndex, i] = $"{ch}";
                i += 1;
            }
        }
    }
}