using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models.PluginViews
{
    public class PluginVersionModelView
    {
        public List<Version> Versions { get; set; }
        public Plugin Plugin { get; set; }
    }
}
