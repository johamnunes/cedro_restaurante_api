using CedroRestaurante.DataObjects.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CedroRestaurante.DataObjects.Models
{
    [Table("Restaurantes")]
    public class Restaurante : Base
    {
        [Required(ErrorMessage = "Informe o nome do restaurante")]
        public string Nome { get; set; }
    }
}
