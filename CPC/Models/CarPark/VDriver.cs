using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("v_drivers", Schema = "public")]
    public partial class VDriver
    {
        [Column("Идентификатор_водителя")]
        public int? ИдентификаторВодителя { get; set; }

        [MaxLength(50)]
        public string Имя { get; set; }

        [MaxLength(50)]
        public string Фамилия { get; set; }

        [MaxLength(50)]
        public string Отчество { get; set; }

        [Column("Номер_прав")]
        [MaxLength(20)]
        public string НомерПрав { get; set; }

        [Column("Дата_выдачи_прав")]
        public DateTime? ДатаВыдачиПрав { get; set; }

        [Column("Дата_окончания_прав")]
        public DateTime? ДатаОкончанияПрав { get; set; }

        [Column("Дата_рождения")]
        public DateTime? ДатаРождения { get; set; }

        [MaxLength(20)]
        public string Телефон { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public bool? Состояние { get; set; }
    }
}