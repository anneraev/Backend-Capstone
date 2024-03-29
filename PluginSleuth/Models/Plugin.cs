﻿
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models

{
    public class Plugin
    {
        [Key]
        public int PluginId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Please shorten the title to 50 characters")]
        public string Title { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Author")]
        public ApplicationUser User { get; set; }
        [Required]
        [ForeignKey("Engine")]
        public int EngineId { get; set; }
        public Engine Engine { get; set; }
        [Required]
        [ForeignKey("PluginType")]
        public int PluginTypeId { get; set; }
        public PluginType PluginType { get; set;  }

        //0=NonCommercial, 1=PaidCommercial, 2=ContactAuthor, 3=FreeCommercial
        [Display(Name = "Commercial Use")]
        public int CommercialUse { get; set; }
        //Requires payment/patreon to download? Check this as true if you have optional donations.
        [Display(Name ="Free download?")]
        public bool Free { get; set; }
        [Url(ErrorMessage = "Please enter a valid url")]
        public string Webpage { get; set; }
        public bool IsListed { get; set; }
        [StringLength(255, ErrorMessage = "Please shorten the description to 255 characters")]
        public string About { get; set; }
        public string Keywords { get; set; }
    }
}
