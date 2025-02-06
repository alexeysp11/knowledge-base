using System;

public class TextControl
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public string Value { get; set; }
    public bool Visible { get; set; }
    public bool Inverted { get; set; }
    public HorizontalAlignment HorizontalAlignment { get; set; }
    public Action? ShowValidation { get; set; }

    public TextControl()
    {
        Name = "";
        X = 0;
        Y = 0;
        Width = 0;
        Value = "";
        Visible = true;
        Inverted = false;
        HorizontalAlignment = HorizontalAlignment.Left;
    }

    public virtual void OnShowValidation()
    {
        // Check if visible.
        
        if (ShowValidation != null)
        {
            ShowValidation();
        }
    }
}