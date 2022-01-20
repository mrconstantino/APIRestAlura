using System.ComponentModel.DataAnnotations;

namespace APIRestAlura
{
    public class Receitas
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime DataReceita { get; set; }
    }
}