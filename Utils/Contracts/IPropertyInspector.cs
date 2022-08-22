using Plugin.Contracts.Actions;

namespace Plugin.Contracts;

public interface IPropertyInspector
{
    void Appeared(IPropertyViewAppear didAppear);
    void Disappeared(IPropertyViewDisappear didDisappear);
    void OnSendToPlugin(ISendToPlugin sendToPlugin);
}