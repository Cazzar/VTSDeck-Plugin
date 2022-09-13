using Newtonsoft.Json;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaVTubeStudio.Requests;

internal class ChangeWidgetTitle : ISetTitle
{
    [JsonIgnore]
    public string? Title
    {
        get => Payload.Title; 
        set => Payload.Title = value;
    }

    [JsonProperty("payload")]
    public PayloadData Payload { get; set; } = new();

    [JsonProperty("context")]
    public string ContextId { get; set; }

    [JsonProperty("event")]
    public string Event { get => "setWidgetSettings"; }

    internal class PayloadData
    {
        [JsonProperty("title")]
        public string? Title { get; set; }
        [JsonProperty("state")]
        public int State { get; set; } = 0;
    }
}
