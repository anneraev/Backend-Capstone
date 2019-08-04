using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models

{
    public class Engine
    {   
        [Key]
        public int EngineId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Please shorten the title to 50 characters")]
        public string Title { get; set; }
        public string Language { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Please shorten the description to 255 characters")]
        public string About { get; set; }
        public string Link { get; set; }
        public string BannerPath { get; set; }
    }
}
