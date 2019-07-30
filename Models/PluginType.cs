using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backendcapstone.Models
{
    public class PluginType
    {
        [Key]
        public int PluginTypeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
