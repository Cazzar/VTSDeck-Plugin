using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Contracts.Requests;
public interface ISendToPropertyView : IContextRequired
{
    object? Payload { get; set; }
}
