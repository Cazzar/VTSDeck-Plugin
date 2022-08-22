using Microsoft.Extensions.DependencyInjection;
using Plugin.Contracts;
using Plugin.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Plugin;
public class RequestFactory : IRequestFactory
{
    private IServiceProvider _serviceProvider;

    public RequestFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T? CreateRequest<T>(Action<T>? configure = null) where T : IMessage
    {
        var obj = _serviceProvider.GetService<T>();

        if (obj == null) return obj;

        configure?.Invoke(obj);

        return obj;
    }
}
