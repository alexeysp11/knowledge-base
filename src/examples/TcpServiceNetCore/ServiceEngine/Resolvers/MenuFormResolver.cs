using TcpServiceNetCore.ServiceEngine.Forms;
using TcpServiceNetCore.ServiceEngine.Models;

namespace TcpServiceNetCore.ServiceEngine.Resolvers;

public class MenuFormResolver
{
    public SessionInfo SessionInfo { get; set; }
    public BaseForm CurrentForm { get; set; }

    public SessionInfo InitSession()
    {
        int formHeight = 18;
        int formWidth = 36;

        SessionInfo = new SessionInfo();
        SessionInfo.SessionId = 1;
        SessionInfo.FormHeight = formHeight;
        SessionInfo.FormWidth = formWidth;
        SessionInfo.AssignEmptyDisplayedInfo();
        return SessionInfo;
    }

    public void ProcessUserInput(string userInput)
    {
        SessionInfo.UserInput = userInput;
        CurrentForm.Show();
    }

    public void Start(string? menuCode = null)
    {
        try
        {
            BaseForm form = CreateForm(menuCode);

            form.SessionInfo = SessionInfo;

            form.FillFormAttributes(menuCode);
            form.Init();
            form.Show();

            CurrentForm = form;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private BaseForm CreateForm(string? menuCode = null)
    {
        string typeName = "";
        switch (menuCode)
        {
            case null:
            case "":
                typeName = "TcpServiceNetCore.BusinessVisuals.Forms.frmMenu, TcpServiceNetCore.BusinessVisuals";
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