using System;
using System.ComponentModel.DataAnnotations;

namespace CedroRestaurante.DataObjects.Common
{
    public abstract class Base
    {
        [Key]
        public string Id { get; set; }

        public DateTimeOffset CriadoEm { get; set; }

        public DateTimeOffset? AtualizadoEm { get; set; }
    }
}
