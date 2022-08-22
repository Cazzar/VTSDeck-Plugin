using AverMediaVTubeStudio.CreatorCentral;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AverMediaVTubeStudio.Requests;
internal class GetPackageSettings
{
    [JsonProperty("event")] public string Event { get; init; } = "getPackageSettings";
    [JsonProperty("context")] public string Context { get; init; }

    public GetPackageSettings(IOptions<RegistrationOptions> registrationOptions)
    {
        Context = registrationOptions.Value.Uuid;
    }
}
