public abstract class BaseForm
{
    public string Name { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }

    public BaseForm? ParentForm { get; set; }

    public List<TextControl> Controls { get; set; }

    public Func<bool>? ShowValidation { get; set; }
    public Func<bool>? FormValidation { get; set; }

    public BaseForm()
    {
        Name = "";
        Height = 0;
        Width = 0;
        Controls = new List<TextControl>();
    }

    public virtual void Show()
    {
        try
        {
            if (!OnShowValidation())
            {
                return;
            }

            List<TextControl> sortedControls = Controls
                .OrderBy(x => x.Top)
                .ThenBy(x => x.Left)
                .ToList();

            ConfigureControls(sortedControls);

            ShowTextControls();

            TextEditControl firstEditControl = sortedControls
                .Where(x => x is TextEditControl && x.Editable)
                .Cast<TextEditControl>()
                .FirstOrDefault();
            if (firstEditControl != null)
            {
                ShowTextEditControl(firstEditControl);
            }
        }
        catch (Exception ex)
        {
            // Display error.
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
            throw new Exception($"Failed to show form: parameter {nameof(Name)} should be assigned");
        }
        if (Controls == null || !Controls.Any())
        {
            throw new Exception($"Failed to show form '{Name}': parameter {nameof(Controls)} should be assigned and contain elements");
        }
        if (Height <= 0)
        {
            throw new Exception($"Failed to show form '{Name}': parameter {nameof(Height)} should be greater than zero");
        }
        if (Width <= 0)
        {
            throw new Exception($"Failed to show form '{Name}': parameter {nameof(Width)} should be greater than zero");
        }
        return true;
    }

    protected abstract void InitializeComponent();

    private void ConfigureControls(List<TextControl> sortedControls)
    {
        if (sortedControls == null)
            throw new Exception("Sorted list of controls was not assigned");
        
        TextControl? previousControl = null;
        foreach (TextControl control in sortedControls)
        {
            previousControl.NextControl = control;
            control.PreviousControl = previousControl;
            control.Form = this;
            previousControl = control;
        }
        previousControl = null;
    }

    private void ShowTextControls()
    {
        List<TextControl> textControls = Controls
            .Where(x => !x.Editable)
            .ToList();
        
        foreach (TextControl control in textControls)
        {
            control.Show();
        }
    }

    private void ShowTextEditControl(TextControl control)
    {
        if (control == null)
        {
            throw new Exception("Control is not found");
        }

        if (control.Editable && control is TextEditControl)
        {
            TextEditControl editControl = (TextEditControl)control;
            editControl.Show();
            editControl.GetUserInput();
        }

        if (control.NextControl == null)
        {
            if (FormValidation != null)
            {
                if (FormValidation())
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
        else
        {
            control = control.NextControl;
        }

        ShowTextEditControl(control);
    }
}