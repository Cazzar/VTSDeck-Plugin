using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Contracts;
public interface IGlobalSettingsHandler
{
    void GotGlobalSettings(IGlobalSettings settings);
}
