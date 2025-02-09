using TcpServiceNetCore.ServiceEngine.Forms;

namespace TcpServiceNetCore.ServiceEngine.Resolvers;

public class MenuFormResolver
{
    public SessionInfo SessionInfo { get; set; }

    public SessionInfo InitSession()
    {
        int formHeight = 18;
        int formWidth = 26;

        SessionInfo = new SessionInfo();
        SessionInfo.SessionId = 1;
        SessionInfo.FormHeight = formHeight;
        SessionInfo.FormWidth = formWidth;
        SessionInfo.DisplayedInfo = new string[formHeight, formWidth];
        return SessionInfo;
    }

    public void ProcessUserInput(string userInput)
    {
        // 
    }

    public void DisplayMenu(string? menuCode = null)
    {
        // 
    }
    
    public void StartMenu(string menuCode)
    {
        try
        {
            BaseForm form = CreateForm(menuCode);

            form.SessionInfo = SessionInfo;

            form.FillFormAttributes(menuCode);
            form.Init();
            form.Show();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private BaseForm CreateForm(string menuCode)
    {
        string assemblyName = "";
        string typeName = "";
        switch (menuCode)
        {
            case "1":
                assemblyName = "TcpServiceNetCore.TcpHostedService";
                typeName = "TcpServiceNetCore.TcpHostedService.Controls.frmTestTcpConnection, TcpServiceNetCore.TcpHostedService";
                break;
            
            default:
                throw new Exception("Menu could not be resolved");
        }

        Type type = Type.GetType(typeName, true);
        object instance = Activator.CreateInstance(type);
        if (instance == null)
        {
            throw new Exception($"Could not create the form '{menuCode}: type name is not found for the selected menu");
        }
        if (instance is not BaseForm)
        {
            throw new Exception($"Could not create the form '{menuCode}: not derived from {nameof(BaseForm)}");
        }

        return (BaseForm)instance;
    }
}