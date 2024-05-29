using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspDatabase.Models
{
    public class Agreement
    {
        public int Id { get; set; }
        [Display(Name = "Дата составления")]
        public DateTime dateDoc { get; set; }

        [Display(Name = "Клиент")]
        [ForeignKey("Client")]
        public int? clientID { get; set; }
        public virtual Client? Client{ get; set; }

        [Display(Name = "Отель")]
        [ForeignKey("Hotel")]
        public int? hotelId { get; set; }
        public virtual Hotel? Hotel { get; set; }

        [Display(Name = "Номер")]
        [ForeignKey("Room")]
        public int roomID { get; set; }
        public virtual Room? Room { get; set; }

        [Display(Name = "Начало бронирования")]
        public DateTime reservStart { get; set; }

        [Display(Name = "Конец бронирования")]
        public DateTime reservEnd { get; set; }

        [Display(Name = "Стоимость")]
        public int Cost { get; set; }
    }
}
