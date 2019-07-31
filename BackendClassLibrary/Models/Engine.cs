using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendClassLibrary.Models

{
    public class Engine
    {   
        [Key]
        public int EngineId { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string About { get; set; }
        public string Link { get; set; }
        public string BannerPath { get; set; }
    }
}
