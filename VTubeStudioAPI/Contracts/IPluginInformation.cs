using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI.Contracts;
public interface IPluginInformation
{
    public string Name { get; }
    public string Developer { get; }
    public string Icon { get; }
}
