using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CP.Server.Models.CarPark
{
    [Table("v_trips", Schema = "public")]
    public partial class VTrip
    {
        [Column("Идентификатор_поездки")]
        public int? ИдентификаторПоездки { get; set; }

        [Column("Идентификатор_заявки")]
        public int? ИдентификаторЗаявки { get; set; }

        [Column("Идентификатор_ТС")]
        public int? ИдентификаторТс { get; set; }

        [Column("Дата_поездки")]
        public DateTime? ДатаПоездки { get; set; }

        [Column("Время_начала")]
        public DateTime? ВремяНачала { get; set; }

        [Column("Время_окончания")]
        public DateTime? ВремяОкончания { get; set; }

        [Column("Пробег_в_начале")]
        public decimal? ПробегВНачале { get; set; }

        [Column("Пробег_в_конце")]
        public decimal? ПробегВКонце { get; set; }

        [MaxLength(20)]
        public string Статус { get; set; }
    }
}