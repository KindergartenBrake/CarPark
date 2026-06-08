
using System;
using System.Collections.Generic;
// Модель для аутентификации пользователя
namespace CP.Server.Models
{
    // Модель для claim пользователя
    public class ApplicationClaim
    {
        // Тип claim
        public string Type { get; set; }
        // Значение claim
        public string Value { get; set; }
    }

    // Модель для аутентификации пользователя
    public partial class ApplicationAuthenticationState
    {
        // Флаг аутентификации пользователя
        public bool IsAuthenticated { get; set; }
        // Имя пользователя
        public string Name { get; set; }
        // Claims пользователя
        public IEnumerable<ApplicationClaim> Claims { get; set; }
    }
}