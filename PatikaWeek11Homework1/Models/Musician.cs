using System.ComponentModel.DataAnnotations;

namespace PatikaWeek11Homework1.Models
{
    public class Musician
    {
        [Required()]
        public int Id { get; set; }

        [Required(ErrorMessage ="Müzisyen adı gereklidir.")]
        [StringLength(maximumLength:100,MinimumLength =3, ErrorMessage = "İsim 100 karakteden fazla, 3 karakterden de az olamaz")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Meslek alanı boş geçilemez")]
        public string Profession { get; set; }

        [Required(ErrorMessage = "Eğlenceli özellik alanı boş geçilemez")]
        public string FunFeature { get; set; }
    }
}
