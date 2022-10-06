// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoWebTemplate.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Phải nhập {0}")]
        [Display(Name = "UserName Or Email")]
        public string UserNameOrEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Display]
        public string Password { get; set; }

        [Display(Name = "Keep Me Login")]
        public bool RememberMe { get; set; }
    }
}
