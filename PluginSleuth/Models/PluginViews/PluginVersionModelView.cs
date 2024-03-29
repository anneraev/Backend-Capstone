﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models.PluginViews
{
    public class PluginVersionModelView
    {
        public List<Version> Versions { get; set; }
        public Plugin Plugin { get; set; }
        public Version CurrentVersion { get; set; }
        //only used to check if the user has one associated with them.
        public UserVersion UserVersion { get; set; }
        //for user-based context inside the details page.
        public string currentUserId { get; set; }
    }
}
