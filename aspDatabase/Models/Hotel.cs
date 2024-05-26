using System.ComponentModel.DataAnnotations;

namespace aspDatabase.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Владелец")]
        public string Owner{ get; set; }

        [Display(Name = "Фото")]
        public string? Img {  get; set; }

    }
}
