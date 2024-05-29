using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aspDatabase.Models
{
    public class BookingRequest
    {
        public int Id { get; set; }
        [Display(Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string ThirdName { get; set; }
        [Display(Name = "Номер телефона")]
        public string NumberofPhone { get; set; }
        [Display(Name = "Серия пасспорта")]
        public int passportSeries { get; set; }
        [Display(Name = "Номер пасспорта")]
        public int nubmerPassport { get; set; }

        [Display(Name = "Начало брони")]
        public DateTime BookingDateStart { get; set; }
        [Display(Name = "Конец брони")]
        public DateTime BookingDateEnd { get; set; }
        [Display(Name = "Отель")]

        [ForeignKey("Hotel")]
        public virtual int? hotelId { get; set; }
        public virtual Hotel? Hotel { get; set; }

        [Display(Name = "Комната")]
        public virtual int? roomID { get; set; }
        public virtual Room? Room { get; set; }

    }
}
