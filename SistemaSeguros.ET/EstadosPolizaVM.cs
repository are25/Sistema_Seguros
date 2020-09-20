using System.ComponentModel.DataAnnotations;

namespace SistemaSeguros.API.Models
{
    public  class EstadosPolizaVM
    {
        [Key]
        public int Id { get; set; }

        public string Descripcion { get; set; }

    }
}
