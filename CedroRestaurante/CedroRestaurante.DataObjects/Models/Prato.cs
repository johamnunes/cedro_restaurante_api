using CedroRestaurante.DataObjects.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CedroRestaurante.DataObjects.Models
{
    [Table("Pratos")]
    public class Prato : Base
    {
        [Required(ErrorMessage = "Informe a descrição do prato")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe um valor")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor não pode ser menor ou igual a zero")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "Informe um restaurante")]
        [ForeignKey("Restaurante")]
        public string RestauranteId { get; set; }

        public virtual Restaurante Restaurante { get; set; }
    }
}
