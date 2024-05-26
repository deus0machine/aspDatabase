using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspDatabase.Models
{
    public class Room
    {
        public int ID { get; set; }
        [Display(Name = "Отель")]

        [ForeignKey("Hotel")]
        public virtual int? Idhotel { get; set; }
        public virtual Hotel? Hotel { get; set; }

        [Display(Name = "Адрес комнаты")]
        public string AdressRoom { get; set; }
        [Display(Name = "Цена")]
        public int Cost { get; set; }
        [Display(Name = "Тип")]
        public string Type { get; set; }
        [Display(Name = "Статус")]
        public string Status { get; set; }

    }
}
