using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI;
internal static class Extensions
{
    public static string TrimEnd(this string source, string value) =>
        source.EndsWith(value) ?
            source.Remove(source.LastIndexOf(value, StringComparison.OrdinalIgnoreCase)) :
            source;
}
