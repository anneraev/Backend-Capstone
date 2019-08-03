using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models.HomeViews
{
    public class HomeViewModel
    {
        public Plugin Plugin { get; set; }
        public List<Engine> Engines { get; set; }
    }
}
