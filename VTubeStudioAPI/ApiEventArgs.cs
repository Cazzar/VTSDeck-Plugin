using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTubeStudioAPI;

 public class ApiEventArgs<T> : EventArgs
{
    public T? Response { get; init; }

    public string RequestId { get; init; }

    public ApiEventArgs(T? response, string requestId)
    {
        Response = response;
        RequestId = requestId;
    }
}