﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI.Contracts;

public interface IAuthManager
{
    public string? Token { get; set; }
}
