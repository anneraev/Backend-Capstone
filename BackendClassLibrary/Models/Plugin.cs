
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendClassLibrary.Models

{
    public class Plugin
    {
        [Key]
        public int PluginId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public ApplicationUser Author { get; set; }
        [Required]
        [ForeignKey("Engine")]
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        [Required]
        [ForeignKey("PluginType")]
        public int PluginTypeId { get; set; }

        //0=No, 1=PaidCommercial, 3=FreeCommercial
        public int CommercialUse { get; set; }
        //Requires payment/patreon to download? Check this as true if you have optional donations.
        public bool Free { get; set; }
        public string Webpage { get; set; }
    }
}
