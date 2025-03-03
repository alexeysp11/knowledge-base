using PixelTerminalUI.ServiceEngine.Controls;
using PixelTerminalUI.ServiceEngine.Forms;

namespace PixelTerminalUI.BusinessVisuals.Forms;

public class frmMenu : BaseForm
{
    private TextControl? lblHeader;
    private TextControl? lblOperationName;
    private TextControl? lblMenu01;
    
    private TextEditControl? txtUserInput;

    public frmMenu() : base()
    {
        Name = nameof(frmMenu);
    }
    
    protected override void InitializeComponent()
    {
        lblHeader = new TextControl();
        lblHeader.Name = nameof(lblHeader);
        lblHeader.Top = 0;
        lblHeader.Left = 0;
        lblHeader.EntireLine = true;
        lblHeader.HorizontalAlignment = HorizontalAlignment.Center;
        lblHeader.Value = "PIXEL TERMINAL UI";
        lblHeader.Inverted = true;
        Controls.Add(lblHeader);
        
        lblOperationName = new TextControl();
        lblOperationName.Name = nameof(lblOperationName);
        lblOperationName.Top = 1;
        lblOperationName.Left = 0;
        lblOperationName.EntireLine = true;
        lblOperationName.Value = "MENU";
        Controls.Add(lblOperationName);
        
        lblMenu01 = new TextControl();
        lblMenu01.Name = nameof(lblMenu01);
        lblMenu01.Top = 3;
        lblMenu01.Left = 0;
        lblMenu01.EntireLine = true;
        lblMenu01.Value = "1. TEST FORM";
        Controls.Add(lblMenu01);
        
        txtUserInput = new TextEditControl();
        txtUserInput.Name = nameof(txtUserInput);
        txtUserInput.Top = 14;
        txtUserInput.Left = 0;
        txtUserInput.EntireLine = true;
        txtUserInput.Hint = "ENTER MENU";
        txtUserInput.EnterValidation = txtUserInput_EnterValidation;
        Controls.Add(txtUserInput);
    }

    private bool txtUserInput_EnterValidation()
    {
        switch (txtUserInput.Value)
        {
            case "1":
                var frmTestForm = new frmTestForm();
                SessionInfo.CurrentForm = frmTestForm;
                frmTestForm.SessionInfo = SessionInfo;
                frmTestForm.ParentForm = this;
                frmTestForm.Init();
                frmTestForm.Show();
                break;
            
            default:
                FocusedEditControl = txtUserInput;
                break;
        }
        return true;
    }
}