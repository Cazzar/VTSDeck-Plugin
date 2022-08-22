namespace Plugin.Contracts.Actions;

public interface IActionRepository
{
    void Appeared(IWidgetAppeared appear);
    void ButtonDown(IKeyDown keyDown);
    void ButtonUp(IKeyUp keyUp);
    void Disappeared(IWidgetDisppeared disappear);
    void FindActions();
    void GotSettings(IWidgetSettings settings);
    void PropertyInspectorAppeared(IPropertyViewAppear didAppear);
    void PropertyInspectorDisappeared(IPropertyViewDisappear didDisappear);
    void SendToPlugin(ISendToPlugin sendToPlugin);
    void Tick();
}