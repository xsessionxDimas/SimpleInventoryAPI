﻿using System.ComponentModel.DataAnnotations;

namespace SimpleInventoryAPI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email    { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
    }
}
