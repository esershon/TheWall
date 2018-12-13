using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace TheWall.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name="Email")]
        public string LoginEmail { get; set; }
        [Required]
        [Display(Name="Password")]
        public string LoginPassword { get; set; }
    }
}
