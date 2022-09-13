﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTubeStudioAPI.Contracts;

namespace AverMediaVTubeStudio;
internal class VTubeStudioPluginInformation : IPluginInformation
{
    public string Name => "Creator Central Integration";
    public string Developer => "Cazzar";
    public string Icon => "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";
}
