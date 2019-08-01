using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PluginSleuth.Models
{
    /*
        Add profile data for application users by adding
        properties to the ApplicationUser class
    */
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() { }

        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; }
        public string AvatarPath { get; set; }
        public string WebSite { get; set; }
        public string Github { get; set; }
        public bool IsAdmin { get; set; }
        public List<UserVersion> UserVersions { get; set; }
        /*
            Which resources are related to the User? The code
            below handles a case where a user can create many
            products.
        */
        //public virtual ICollection<Product> Products { get; set; }
    }
}
