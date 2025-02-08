using System;

namespace TcpServiceNetCore.ServiceEngine.Controls;

public class TextEditControl : TextControl
{
    public bool Required { get; set; }
    public string EmptyEnterSymbol { get; set; }
    public string? Hint { get; set; }
    public Func<bool>? EnterValidation { get; set; }

    public TextEditControl() : base()
    {
        Required = true;
        EmptyEnterSymbol = ".";
    }

    public override void Show()
    {
        if (!OnShowValidation())
        {
            return;
        }

        if (HorizontalAlignment != HorizontalAlignment.Left)
        {
            // Align the control, pass EmptyEnterSymbol.
        }

        if (!string.IsNullOrEmpty(Hint))
        {
            // Display Hint.
        }
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
}