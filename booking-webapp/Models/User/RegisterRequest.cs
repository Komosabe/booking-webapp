﻿using System.ComponentModel.DataAnnotations;

namespace BackendBooking.Models.User
{
    public class RegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
