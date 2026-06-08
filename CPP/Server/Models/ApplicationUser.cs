using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
// Модель для пользователя
namespace CP.Server.Models
{
    // Модель для пользователя
    public partial class ApplicationUser : IdentityUser
    {
        // Пароль пользователя
        [JsonIgnore, IgnoreDataMember]
        public override string PasswordHash { get; set; }

        // Пароль пользователя
        [NotMapped]
        public string Password { get; set; }

        // Подтверждение пароля пользователя
        [NotMapped]
        public string ConfirmPassword { get; set; }

        // Имя пользователя
        [JsonIgnore, IgnoreDataMember, NotMapped]
        public string Name
        {
            // Имя пользователя
            get
            {
                return UserName;
            }
            // Установка имени пользователя
            set
            {
                UserName = value;
            }
        }

        // Роли пользователя
        public ICollection<ApplicationRole> Roles { get; set; }
    }
}