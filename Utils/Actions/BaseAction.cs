using Newtonsoft.Json.Linq;
using Plugin.Contracts;
using Plugin.Contracts.Actions;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Actions;
public class BaseAction<T> : IHasSettings, IContextAware, IButtonReactions where T : new()
{
    public BaseAction(IConnection connection, IRequestFactory requestFactory)
    {
        Connection = connection;
        RequestFactory = requestFactory;
    }

    public T Settings { get; private set; } = new T();

    public IConnection Connection { get; set; }
    public IRequestFactory RequestFactory { get; }

    public virtual void GotSettings(JObject? settings)
    {
        if (settings is null) return;

        Settings = settings.ToObject<T>() ?? new T();
    }

    public string? ContextId { get; set; }

    public virtual void KeyUp(IKeyUp keyActionPayload)
    {
    }

    public virtual void KeyDown(IKeyDown keyActionPayload)
    {
    }

    public async void SaveSettings(T? settings = default) => await Connection.SendMessage(RequestFactory.CreateRequest<ISaveSettings>(s =>
    {
        s.ContextId = ContextId;
        s.Settings = (settings ?? Settings)!;
    }));

    public async void ShowAlert() => await Connection.SendMessage(RequestFactory.CreateRequest<IShowAlert>(s => s.ContextId = ContextId));

    public async void ShowOk() => await Connection.SendMessage(RequestFactory.CreateRequest<IShowOk>(s => s.ContextId = ContextId));

    public async void RequestSettings() => await Connection.SendMessage(RequestFactory.CreateRequest<IRequestSettings>(s => s.ContextId = ContextId));

    public async void SetTitle(string? title) => await Connection.SendMessage(RequestFactory.CreateRequest<ISetTitle>(s =>
    {
        s.ContextId = ContextId;
        s.Title = title;
    }));
}