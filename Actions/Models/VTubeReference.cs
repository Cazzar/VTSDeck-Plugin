using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actions.Models;
public class VTubeReference
{
    public string Name { get; set; }
    public string Id { get; set; }

    public override string? ToString() => $"VTubeReference {{{Name}, {Id}}}";
}