using AverMediaVTubeStudio.CreatorCentral;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaVTubeStudio.Requests;
internal class SetPackageSettings : ISaveGlobalSettings
{
    [JsonProperty("event")] public string Event { get => "setPackageSettings"; }
    [JsonProperty("context")] public string ContextId { get; set; }
    [JsonProperty("payload")] public SetPayload Payload { get; set; } = new();
    [JsonIgnore] public object Settings { get => Payload.Settings; set => Payload.Settings = value; }

    public SetPackageSettings(IOptions<RegistrationOptions> registrationOptions)
    {
        ContextId = registrationOptions.Value.Uuid;
    }

    public class SetPayload
    {
        [JsonProperty("settings")] public object Settings { get; set; }
    }
}
