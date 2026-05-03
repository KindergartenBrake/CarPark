using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CP.Server.Models.CarPark
{
    [Table("v_trip_requests", Schema = "public")]
    public partial class VTripRequest
    {
        [Column("Идентификатор_заявки")]
        public int? ИдентификаторЗаявки { get; set; }

        [Column("Идентификатор_пользователя")]
        public int? ИдентификаторПользователя { get; set; }

        [Column("Идентификатор_водителя")]
        public int? ИдентификаторВодителя { get; set; }

        [Column("Идентификатор_ТС")]
        public int? ИдентификаторТс { get; set; }

        [Column("Дата_создания")]
        public DateTime? ДатаСоздания { get; set; }

        public string Описание { get; set; }

        [MaxLength(20)]
        public string Статус { get; set; }
    }
}