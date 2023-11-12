namespace Plugin.Intent.Services;

public interface IAppActionsService
{
    void AddAppAction(string id, string title, string subtitle, string icon);
    Task AppActionCalled(string id, string content);
}