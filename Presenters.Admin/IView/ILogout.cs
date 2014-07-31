namespace Presenters.Admin.IView
{
    public interface ILogoutView : Application.Core.IView
    {
       string User { set; }
        string Role { set; }

    }
}