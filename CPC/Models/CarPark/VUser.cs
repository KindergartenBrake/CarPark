using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("v_users", Schema = "public")]
    public partial class VUser
    {
        [Column("Идентификатор пользователя")]
        public int? ИдентификаторПользователя { get; set; }

        [Column("Имя_пользователя")]
        [MaxLength(50)]
        public string ИмяПользователя { get; set; }

        [Column("Хэш_пароля")]
        [MaxLength(255)]
        public string ХэшПароля { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public bool? Состояние { get; set; }

        [MaxLength(20)]
        public string Роль { get; set; }
    }
}