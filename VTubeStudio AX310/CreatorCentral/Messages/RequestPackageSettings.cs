using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AverMediaVTubeStudio.CreatorCentral.Messages;
internal class RequestPackageSettings : IRequestGlobalSettings
{
    [JsonProperty("context")] public string Context { get; set; }
    [JsonProperty("event")] public string Event { get => "getPackageSettings"; }

    public RequestPackageSettings(IOptions<RegistrationOptions> options)
    {
        Context = options.Value.Uuid;
    }
}
