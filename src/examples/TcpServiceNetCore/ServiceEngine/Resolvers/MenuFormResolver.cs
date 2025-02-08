using TcpServiceNetCore.ServiceEngine.Forms;

namespace TcpServiceNetCore.ServiceEngine.Resolvers;

public class MenuFormResolver
{
    public void Start(string menuCode)
    {
        BaseForm form = CreateForm(menuCode);
        form.FillFormAttributes(menuCode);
        form.Init();
        form.Show();
    }

    private BaseForm CreateForm(string menuCode)
    {
        string typeName = "";
        switch (menuCode)
        {
            case "0-0-1":
                typeName = "";
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