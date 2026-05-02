using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPC.Models.CarPark
{
    [Table("v_vehicles", Schema = "public")]
    public partial class VVehicle
    {
        [Column("Идентификатор_ТС")]
        public int? ИдентификаторТс { get; set; }

        [Column("Идентификатор_водителя")]
        public int? ИдентификаторВодителя { get; set; }

        [MaxLength(12)]
        public string Госномер { get; set; }

        [Column("VIN_номер")]
        [MaxLength(17)]
        public string VinНомер { get; set; }

        [MaxLength(50)]
        public string Марка { get; set; }

        [MaxLength(50)]
        public string Модель { get; set; }

        [Column("Год_выпуска")]
        public int? ГодВыпуска { get; set; }

        public decimal? Пробег { get; set; }

        [Column("Тип_топлива")]
        [MaxLength(20)]
        public string ТипТоплива { get; set; }

        [Column("Тип_ТС")]
        [MaxLength(20)]
        public string ТипТс { get; set; }

        [MaxLength(20)]
        public string Состояние { get; set; }

        [MaxLength(20)]
        public string Страховка { get; set; }
    }
}