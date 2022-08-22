using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Plugin.Contracts;
using Plugin.Contracts.Actions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Plugin;
public class ActionRepository : IActionRepository
{
    private readonly ILogger<ActionRepository> _logger;
    private readonly IServiceProvider _serviceProvider;

    private readonly ConcurrentDictionary<string, Type> _actions = new();
    private readonly ConcurrentDictionary<string, object> _instances = new();

    public ActionRepository(ILogger<ActionRepository> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void FindActions()
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            _logger.LogTrace("Loading actions in {Assembly}", assembly.FullName);
            FindActionsInAssembly(assembly);
        }
    }

    private void FindActionsInAssembly(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            var attribute = type.GetCustomAttribute<CustomActionAttribute>();

            if (attribute is null) continue;

            _logger.LogInformation("Found action of type {Type} with id {ActionId}", type, attribute.ActionId);

            if (_actions.TryAdd(attribute.ActionId, type)) continue;

            _logger.LogError("Already found an action of type: {ActionId} registered with type {AlreadyRegistered}, not registering {Type}", attribute.ActionId, _actions[attribute.ActionId], type);
        }
    }

    public void Appeared(IWidgetAppeared appear)
    {
        if (!_actions.TryGetValue(appear.Action, out var actionType))
        {
            _logger.LogCritical("Unable to find action of type {Type}", appear.Action);
            return;
        }

        var instance = ActivatorUtilities.CreateInstance(_serviceProvider, actionType);

        if (instance is IHasSettings settings)
            settings.GotSettings(appear.GetSettings());

        if (instance is IContextAware contextAware)
            contextAware.ContextId = appear.ContextId;


        _instances[appear.ContextId] = instance;
    }

    public void Disappeared(IWidgetDisppeared disappear)
    {
        if (!_instances.ContainsKey(disappear.ContextId))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", disappear.ContextId);
            return;
        }

        _instances.Remove(disappear.ContextId, out var button);

        if (button is IDisposable d) d.Dispose();
    }

    public void GotSettings(IWidgetSettings settings)
    {
        if (!_instances.TryGetValue(settings.ContextId, out var instance))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", settings.ContextId);
            return;
        }

        if (instance is not IHasSettings hasSettings)
            return;

        hasSettings.GotSettings(settings.GetSettings());
    }

    public void ButtonDown(IKeyDown keyDown)
    {
        if (!_instances.TryGetValue(keyDown.ContextId, out var instance))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", keyDown.ContextId);
            return;
        }

        if (instance is not IButtonReactions reactions)
            return;

        reactions.KeyDown(keyDown);
    }

    public void ButtonUp(IKeyUp keyUp)
    {
        if (!_instances.TryGetValue(keyUp.ContextId, out var instance))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", keyUp.ContextId);
            return;
        }

        if (instance is not IButtonReactions reactions)
            return;

        reactions.KeyUp(keyUp);
    }

    public void PropertyInspectorAppeared(IPropertyViewAppear didAppear)
    {
        if (!_instances.TryGetValue(didAppear.ContextId, out var instance))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", didAppear.ContextId);
            return;
        }

        if (instance is not IPropertyInspector propertyInspector)
            return;

        propertyInspector.Appeared(didAppear);
    }

    public void PropertyInspectorDisappeared(IPropertyViewDisappear didDisappear)
    {
        if (!_instances.TryGetValue(didDisappear.ContextId, out var instance))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", didDisappear.ContextId);
            return;
        }

        if (instance is not IPropertyInspector propertyInspector)
            return;

        propertyInspector.Disappeared(didDisappear);
    }

    public void SendToPlugin(ISendToPlugin sendToPlugin)
    {
        if (!_instances.TryGetValue(sendToPlugin.ContextId, out var instance))
        {
            _logger.LogCritical("Instance ID {Instance} was not found in the context, this is a major internal error", sendToPlugin.ContextId);
            return;
        }

        if (instance is not IPropertyInspector propertyInspector)
            return;

        propertyInspector.OnSendToPlugin(sendToPlugin);
    }

    /*    public void TitleParamsChange(TitleParametersDidChange titleParameters)
        {
            if (!_instances.TryGetValue(titleParameters.Context, out var instance))
            {
                _logger.LogCritical("SendToPlugin: Instance ID {Instance} was not found in the context, this is a major internal error", titleParameters.Context);
                return;
            }

            if (instance is not ITitleParams titleParams)
                return;

            titleParams.GotTitleParams(titleParameters);
        }*/

    public void Tick()
    {
        foreach (var (_, action) in _instances)
        {
            if (action is not ITickHandler handler) return;

            handler.Tick();
        }
    }
}
