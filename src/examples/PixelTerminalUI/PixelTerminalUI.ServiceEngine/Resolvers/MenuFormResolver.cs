using PixelTerminalUI.ServiceEngine.Forms;
using PixelTerminalUI.ServiceEngine.Models;

namespace PixelTerminalUI.ServiceEngine.Resolvers;

public class MenuFormResolver
{
    public SessionInfo SessionInfo { get; set; }
    public BaseForm CurrentForm { get; set; }
    
    private AppSettings _appSettings;

    public MenuFormResolver(AppSettings appSettings)
    {
        _appSettings = appSettings;
    }

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

    public void Start()
    {
        try
        {
            BaseForm form = CreateForm();

            form.SessionInfo = SessionInfo;

            form.FillFormAttributes(_appSettings?.MenuCode);
            form.Init();
            form.Show();

            CurrentForm = form;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private BaseForm CreateForm()
    {
        string typeName = _appSettings?.InitialFormTypeName;

        if (string.IsNullOrEmpty(typeName))
        {
            throw new Exception($"Could not create the form of type '{typeName}': type name is not specified in the appsettings");
        }

        Type type = Type.GetType(typeName, true);
        object instance = Activator.CreateInstance(type);
        if (instance == null)
        {
            throw new Exception($"Could not create the form of type '{typeName}': type name is not found for the selected menu");
        }
        if (instance is not BaseForm)
        {
            throw new Exception($"Could not create the form of type '{typeName}': not derived from {nameof(BaseForm)}");
        }

        return (BaseForm)instance;
    }
}