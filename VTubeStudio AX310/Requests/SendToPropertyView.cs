using Newtonsoft.Json;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaVTubeStudio.Requests;
internal class SendToPropertyView : ISendToPropertyView
{
    [JsonProperty("event")] public string Event { get => "sendToPropertyView"; }
    [JsonProperty("payload")] public object? Payload { get; set; }
    [JsonProperty("context")] public string ContextId { get; set; }
}
