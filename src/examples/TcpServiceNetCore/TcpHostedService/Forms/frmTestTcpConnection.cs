public class frmTestTcpConnection : BaseForm
{
    private TextControl? lblHeader;
    private TextControl? lblOperationName;
    private TextControl? lblRequestNumber;
    private TextControl? lblRequestNumberCount;
    private TextControl? lblRequestBody;
    private TextControl? lblResponseBody;

    public frmTestTcpConnection() : base()
    {
        Name = nameof(frmTestTcpConnection);
        Height = 18;
        Width = 26;
    }

    protected override void InitializeComponent()
    {
        lblHeader = new TextControl();
        lblHeader.Name = nameof(lblHeader);
        lblHeader.Top = 0;
        lblHeader.Left = 0;
        lblHeader.EntireLine = true;
        lblHeader.HorizontalAlignment = HorizontalAlignment.Center;
        lblHeader.Value = "TCP SERVICE";
        lblHeader.Inverted = true;
        Controls.Add(lblHeader);

        lblOperationName = new TextControl();
        lblOperationName.Name = nameof(lblOperationName);
        lblOperationName.Top = 1;
        lblOperationName.Left = 0;
        lblOperationName.EntireLine = true;
        lblOperationName.Value = "TEST TCP CONNECTION";
        Controls.Add(lblOperationName);

        lblRequestNumber = new TextControl();
        lblRequestNumber.Name = nameof(lblRequestNumber);
        lblRequestNumber.Top = 3;
        lblRequestNumber.Left = 0;
        lblRequestNumber.Width = 18;
        lblRequestNumber.Value = "REQUEST NUMBER:";
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
        lblRequestBody.Top = 4;
        lblRequestBody.Left = 0;
        lblRequestBody.EntireLine = true;
        lblRequestBody.Value = "REQUEST BODY:";
        Controls.Add(lblRequestBody);

        lblResponseBody = new TextControl();
        lblResponseBody.Name = nameof(lblResponseBody);
        lblResponseBody.Top = 10;
        lblResponseBody.Left = 0;
        lblResponseBody.EntireLine = true;
        lblResponseBody.Value = "RESPONSE BODY:";
        Controls.Add(lblResponseBody);
    }
}