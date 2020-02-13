using System.ComponentModel.DataAnnotations;

namespace dropArquivo.Models
{
    public class User
    {
        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string NameUser { get; set;}

        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Password { get; set;}
    }
}