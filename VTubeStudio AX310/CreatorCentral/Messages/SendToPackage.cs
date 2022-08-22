using Newtonsoft.Json.Linq;
using Plugin.Contracts;
using Plugin.Contracts.Actions;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;

internal class SendToPackage : ContextMessage, ISendToPlugin
{
    public JObject Payload { get; set; }

    public T? GetPayload<T>() => GetPayload().ToObject<T>();
    public JObject GetPayload() => Payload;
}

