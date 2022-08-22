using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI.Responses;
public class StatisticsResponse
{
    public int Uptime { get; set; }
    public int Framerate { get; set; }
    public string VTubeStudioVersion { get; set; }
    public int AllowedPlugins { get; set; }
    public int ConnectedPlugins { get; set; }
    public bool StartedWithSteam { get; set; }
    public int WindowWidth { get; set; }
    public int WindowHeight { get; set; }
    public bool WindowIsFullscreen { get; set; }
}
