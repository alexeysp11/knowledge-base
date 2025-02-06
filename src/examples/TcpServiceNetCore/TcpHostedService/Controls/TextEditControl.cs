using System;

public class TextEditControl : TextControl
{
    public bool Required { get; set; }
    public string? Hint { get; set; }
    public Action? RequireValidation { get; set; }
    public Action? EnterValidation { get; set; }

    public TextEditControl() : base()
    {
        Required = true;
    }

    public override void OnShowValidation()
    {
        // Show hint.
        
        base.OnShowValidation();
    }

    public virtual void OnRequireValidation()
    {
        // Check if required.

        if (RequireValidation != null)
        {
            RequireValidation();
        }
    }

    public virtual void OnEnterValidation()
    {
        if (EnterValidation != null)
        {
            EnterValidation();
        }
    }
}