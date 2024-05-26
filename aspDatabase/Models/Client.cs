using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aspDatabase.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Display (Name = "Фамилия")]
        public string SecondName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Отчество")]
        public string ThirdName { get; set; }
        [Display(Name = "Номер телефона")]
        public string NumberofPhone{ get; set; }
        [Display(Name = "Серия пасспорта")]
        public int passportSeries { get; set; }
        [Display(Name = "Номер пасспорта")]
        public int nubmerPassport { get; set; }
    }
}
