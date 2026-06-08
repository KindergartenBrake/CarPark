using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
// Модель для роли пользователя
namespace CP.Server.Models
{
    // Модель для роли пользователя
    public partial class ApplicationRole : IdentityRole
    {
        // Пользователи роли
        [JsonIgnore]
        public ICollection<ApplicationUser> Users { get; set; }

    }
}