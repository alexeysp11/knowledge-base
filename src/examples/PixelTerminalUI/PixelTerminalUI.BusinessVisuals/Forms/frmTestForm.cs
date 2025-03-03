using PixelTerminalUI.ServiceEngine.Controls;
using PixelTerminalUI.ServiceEngine.Forms;

namespace PixelTerminalUI.BusinessVisuals.Forms;

public class frmTestForm : BaseForm
{
    private TextControl? lblHeader;
    private TextControl? lblOperationName;
    private TextControl? lblRequestNumber;
    private TextControl? lblRequestNumberCount;
    private TextControl? lblRequestBody;
    private TextControl? lblResponseBody;

    private TextEditControl? txtRequestBody;
    private TextEditControl? txtResponseBody;

    public frmTestForm() : base()
    {
        Name = nameof(frmTestForm);
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
        lblOperationName.Value = "TEST FORM";
        Controls.Add(lblOperationName);

        lblRequestNumber = new TextControl();
        lblRequestNumber.Name = nameof(lblRequestNumber);
        lblRequestNumber.Top = 3;
        lblRequestNumber.Left = 0;
        lblRequestNumber.Width = 18;
        lblRequestNumber.Value = "COUNTER:";
        Controls.Add(lblRequestNumber);

        lblRequestNumberCount = new TextControl();
        lblRequestNumberCount.Name = nameof(lblRequestNumberCount);
        lblRequestNumberCount.Top = 3;
        lblRequestNumberCount.Left = 18;
        lblRequestNumberCount.Width = 4;
        lblRequestNumberCount.Value = "0";
        Controls.Add(lblRequestNumberCount);

        lblRequestBody = new TextControl();
        lblRequestBody.Name = nameof(lblRequestBody);
        lblRequestBody.Top = 5;
        lblRequestBody.Left = 0;
        lblRequestBody.EntireLine = true;
        lblRequestBody.Value = "VALUE 1:";
        Controls.Add(lblRequestBody);

        txtRequestBody = new TextEditControl();
        txtRequestBody.Name = nameof(txtRequestBody);
        txtRequestBody.Top = 6;
        txtRequestBody.Left = 0;
        txtRequestBody.EntireLine = true;
        txtRequestBody.Hint = "ENTER VALUE 1";
        txtRequestBody.EnterValidation = txtRequestBody_EnterValidation;
        Controls.Add(txtRequestBody);

        lblResponseBody = new TextControl();
        lblResponseBody.Name = nameof(lblResponseBody);
        lblResponseBody.Top = 8;
        lblResponseBody.Left = 0;
        lblResponseBody.EntireLine = true;
        lblResponseBody.Value = "VALUE 2:";
        Controls.Add(lblResponseBody);

        txtResponseBody = new TextEditControl();
        txtResponseBody.Name = nameof(txtResponseBody);
        txtResponseBody.Top = 9;
        txtResponseBody.Left = 0;
        txtResponseBody.EntireLine = true;
        txtResponseBody.Hint = "ENTER VALUE 2";
        txtResponseBody.EnterValidation = txtResponseBody_EnterValidation;
        Controls.Add(txtResponseBody);
    }

    private bool txtRequestBody_EnterValidation()
    {
        Console.WriteLine($"txtRequestBody.Value: '{txtRequestBody.Value}'");
        if (txtRequestBody.Value == "-n")
        {
            txtRequestBody.Value = "";
            return false;
        }
        FocusedEditControl = txtResponseBody;
        return true;
    }

    private bool txtResponseBody_EnterValidation()
    {
        if (txtResponseBody.Value == "-n")
        {
            txtResponseBody.Value = "";
            return false;
        }
        SessionInfo.CurrentForm = ParentForm;
        return true;
    }
}