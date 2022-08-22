using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Contracts.Actions;

public interface IGlobalSettings : IActionReference
{
    T? GetSettings<T>();
    JObject? GetSettings();
}
