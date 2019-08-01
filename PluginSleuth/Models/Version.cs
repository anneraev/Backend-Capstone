using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models

{
    public class Version
    {
        [Key]
        public int VersionId { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Please shorten the name to 50 characters")]
        public string Name { get; set; }
        public string ReadMe { get; set; }
        [Required]
        [ForeignKey("Plugin")]
        public int PluginId { get; set; }
        public Plugin Plugin { get; set; }
        public string DownloadLink { get; set; }
        public List<UserVersion> UserVersions { get; set; }
        [Required]
        public int Iteration { get; set; }
    }
}
