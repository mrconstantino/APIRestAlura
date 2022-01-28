using System.ComponentModel.DataAnnotations;
using static APIRestAlura.Enumerators.Despesas;

namespace APIRestAlura
{
    public class Despesas
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
        [Required]
        public DateTime DataDespesa { get; set; }
        public EnumCategoria? Categoria { get; set; }
    }
}
