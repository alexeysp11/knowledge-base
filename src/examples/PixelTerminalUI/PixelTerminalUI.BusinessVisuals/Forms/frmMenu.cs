using PixelTerminalUI.ServiceEngine.Controls;
using PixelTerminalUI.ServiceEngine.Forms;

namespace PixelTerminalUI.BusinessVisuals.Forms;

public class frmMenu : BaseForm
{
    private TextControl? lblHeader;
    private TextControl? lblOperationName;
    private TextControl? lblMenu01;
    private TextControl? lblMenu02;
    private TextControl? lblMenu03;
    private TextControl? lblMenu04;
    private TextControl? lblMenu05;
    
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
        
        lblMenu02 = new TextControl();
        lblMenu02.Name = nameof(lblMenu02);
        lblMenu02.Top = 4;
        lblMenu02.Left = 0;
        lblMenu02.EntireLine = true;
        lblMenu02.Value = "2. USERS";
        Controls.Add(lblMenu02);
        
        lblMenu03 = new TextControl();
        lblMenu03.Name = nameof(lblMenu03);
        lblMenu03.Top = 5;
        lblMenu03.Left = 0;
        lblMenu03.EntireLine = true;
        lblMenu03.Value = "3. APPLICATIONS";
        Controls.Add(lblMenu03);
        
        lblMenu04 = new TextControl();
        lblMenu04.Name = nameof(lblMenu04);
        lblMenu04.Top = 6;
        lblMenu04.Left = 0;
        lblMenu04.EntireLine = true;
        lblMenu04.Value = "4. CONFIGURATION VARIABLES";
        Controls.Add(lblMenu04);
        
        lblMenu05 = new TextControl();
        lblMenu05.Name = nameof(lblMenu05);
        lblMenu05.Top = 7;
        lblMenu05.Left = 0;
        lblMenu05.EntireLine = true;
        lblMenu05.Value = "5. TASKS";
        Controls.Add(lblMenu05);
        
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
        try
        {
            switch (txtUserInput.Value)
            {
                case "-n":
                case "-b":
                    txtUserInput.Value = "";
                    return false;

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
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
            txtUserInput.Value = "";
            return false;
        }
        finally
        {
            txtUserInput.Value = "";
        }
        return true;
    }
}