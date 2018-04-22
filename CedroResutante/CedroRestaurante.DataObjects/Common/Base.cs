using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CedroRestaurante.DataObjects.Common
{
    public abstract class Base
    {
        [Key]
        public string Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CriadoEm { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime AtualizadoEm { get; set; }
        public bool Removido { get; set; }
    }
}
