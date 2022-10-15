// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebTemplate.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Must Import {0}")]
        [EmailAddress(ErrorMessage = "Fail Format Email")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Must Import {0}")]
        [StringLength(100, ErrorMessage = "{0} length {2} to {1} char.", MinimumLength = 2)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Not Correct.")]
        public string? ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Must Import {0}")]
        [StringLength(100, ErrorMessage = "{0} Length {2} To {1} Char.", MinimumLength = 3)]
        public string? UserName { get; set; }

    }
}
