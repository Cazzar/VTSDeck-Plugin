using Newtonsoft.Json.Linq;
using Plugin.Contracts;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class DidRecievePackageSettings : BaseMessage, IGlobalSettings
{
    public JObject Payload { get; set; }

    public T? GetSettings<T>() => Payload.ToObject<T>();
}


