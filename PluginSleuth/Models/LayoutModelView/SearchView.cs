using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models.LayoutModelView
{
    public class LayoutView
    {
        public int Id = 1;
        public List<PluginType> PluginTypes { get; set; }
        public List<Engine> Engines { get; set; }
    }
}
