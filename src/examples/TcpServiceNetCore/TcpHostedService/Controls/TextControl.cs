using System;

public class TextControl
{
    public string Name { get; set; }
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public string Value { get; set; }
    public bool Visible { get; set; }
    public bool Inverted { get; set; }
    public bool Editable { get; set; }

    public HorizontalAlignment HorizontalAlignment { get; set; }

    public Func<bool>? ShowValidation { get; set; }

    public BaseForm? Form { get; set; }

    public TextControl? PreviousControl { get; set; }
    public TextControl? NextControl { get; set; }

    public TextControl()
    {
        Name = "";
        Left = 0;
        Top = 0;
        Width = 0;
        Value = "";
        Visible = true;
        Inverted = false;
        Editable = false;
        HorizontalAlignment = HorizontalAlignment.Left;
    }

    public virtual void Show()
    {
        if (!OnShowValidation())
        {
            return;
        }

        if (HorizontalAlignment != HorizontalAlignment.Left)
        {
            // Align the control.
        }
    }

    public virtual bool OnShowValidation()
    {
        if (ShowValidation != null)
        {
            return ShowValidation();
        }

        if (string.IsNullOrEmpty(Name))
        {
            throw new Exception($"Failed to show control: parameter {nameof(Name)} should be assigned");
        }
        if (!Visible)
        {
            return false;
        }
        if (Left < 0)
        {
            throw new Exception($"Failed to show control '{Name}': parameter {nameof(Left)} should not be negative");
        }
        if (Top < 0)
        {
            throw new Exception($"Failed to show control '{Name}': parameter {nameof(Top)} should not be negative");
        }
        if (Width < 0)
        {
            throw new Exception($"Failed to show control '{Name}': parameter {nameof(Width)} should not be negative");
        }
        return true;
    }
}