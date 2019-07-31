using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendClassLibrary.Models

{
    public class Version
    {
        [Key]
        public int VersionId { get; set; }
        [Required]
        public string Name { get; set; }
        public string ReadMe { get; set; }
        [Required]
        [ForeignKey("Plugin")]
        public int PluginId { get; set; }
        public Plugin Plugin { get; set; }
        public string DownloadLink { get; set; }
    }
}
