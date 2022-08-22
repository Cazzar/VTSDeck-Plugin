using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.Contracts.Requests;
public interface IRequestFactory
{
    T? CreateRequest<T>(Action<T>? configure = null) where T : IMessage;
}
