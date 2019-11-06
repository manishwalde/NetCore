﻿using EmployeeManagement.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Remote(action: "IsEmailInUse", controller:"account")]
        [ValidEmailDomain(allowedDomain:"afourtech.com", ErrorMessage ="Email domain must be afourtech.com")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage ="Password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage ="City cannot exceed 50 characters.")]
        public string City { get; set; }
    }
}
