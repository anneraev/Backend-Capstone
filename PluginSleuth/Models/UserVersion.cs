using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models

{
    public class UserVersion
    {
        [Key]
        [Required]
        public int UserVersionId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser User { get; set; }
        [Required]
        [ForeignKey("Version")]
        public int VersionId { get; set; }
        public Version Version { get; set; }
        public bool Hidden { get; set; }
    }
}
