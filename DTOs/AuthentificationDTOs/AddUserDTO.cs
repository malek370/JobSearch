﻿using System.ComponentModel.DataAnnotations;

namespace JobSearch.DTOs.AuthentificationDTOs
{
    public class AddUserDTO
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string UserName { get; set; } = "";
    }
}
