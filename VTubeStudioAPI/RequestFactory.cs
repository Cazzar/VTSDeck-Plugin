using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using VTubeStudioAPI.Contracts;
using VTubeStudioAPI.Requests;

namespace VTubeStudioAPI;
public class RequestFactory : IRequestFactory
{
    private IServiceProvider _serviceProvider;

    public RequestFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T? CreateRequest<T>(Action<T>? configure = null) where T : IApiRequest
    {
        var obj = _serviceProvider.GetService<T>();

        if (obj == null) return obj;

        configure?.Invoke(obj);

        return obj;
    }
}
