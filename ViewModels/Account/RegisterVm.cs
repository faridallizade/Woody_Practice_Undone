﻿using System.ComponentModel.DataAnnotations;

namespace Woody.ViewModels.Account
{
    public class RegisterVm
    {
        [Required]  
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ComfirmPassword { get; set; }
    }
}
