using AverMediaVTubeStudio.CreatorCentral.Messages;
using Plugin;

namespace AverMediaVTubeStudio.Json;
internal class CreatorCentralEventConverter : TypeMapConverter<BaseMessage>
{
    public CreatorCentralEventConverter() : base(new Dictionary<string, Type>()
    {
        { "didReceiveWidgetSettings", typeof(DidReceiveWidgetSettings) },
        { "didReceivePackageSettings", typeof(DidRecievePackageSettings) },
        { "actionDown", typeof(ActionDown) },
        { "actionUp", typeof(ActionUp) },
        { "actionTriggered", typeof(ActionTriggered) },
        { "keyDown", typeof(KeyDown) },
        { "keyUp", typeof(KeyUp) },
        { "widgetTriggered", typeof(KeyDown) },
        { "widgetWillAppear", typeof(WidgetWillAppear) },
        { "widgetWillDisappear", typeof(WidgetWillDisappear) },
        { "propertyViewDidAppear", typeof(PropertyViewDidAppear) },
        { "propertyViewDidDisappear", typeof(PropertyViewDidDisppear) },
        { "sendToPackage", typeof(SendToPackage) },
        { "didReceiveWidgetTitle", typeof(DidReceiveWidgetTitle) },
        { "didReceiveWidgetIcon", typeof(DidReceiveWidgetIcon) },
        { "didReceiveFontAttribute", typeof(DidReceiveFontAttribute) },
    }, "event")
    { }
}
