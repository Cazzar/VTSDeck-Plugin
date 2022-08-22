using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin;

//From https://stackoverflow.com/a/45775657
internal class Lazier<T> : Lazy<T> where T : class
{
    public Lazier(IServiceProvider provider)
        : base(() => provider.GetRequiredService<T>())
    {
    }
}