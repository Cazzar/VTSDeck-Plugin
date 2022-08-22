using Newtonsoft.Json;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaVTubeStudio.Requests;
internal class SaveSettings : ISaveSettings
{
    [JsonProperty("context")]
    public string ContextId { get; set; }

    [JsonProperty("event")]
    public string Event { get => "setWidgetSettings"; }

    [JsonProperty("payload")]
    public object Settings { get; set; }
}
