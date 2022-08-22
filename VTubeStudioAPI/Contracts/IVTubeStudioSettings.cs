using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI.Contracts;

public interface IVTubeStudioSettings
{
    string Host { get; set; }
    ushort? Port { get; set; }

    bool SetConnectionParams(string host, ushort port)
    {
        if ((host, port) == (Host, Port)) return false;

        (Host, Port) = (host, port);
        return true;
    }
}
