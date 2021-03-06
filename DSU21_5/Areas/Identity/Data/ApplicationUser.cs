﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DSU21_5.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Firstname { get; set; }
       
        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Lastname { get; set; }
        



    }
}
