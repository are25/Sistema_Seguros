using System.Collections.Generic;

namespace SistemaSeguros.API.Models
{
    public  class PolizaPorClienteVM
    {
        public int Id { get; set; }

        public int IdPoliza { get; set; }

       
        public string IdCliente { get; set; }

        public int IdEstado { get; set; }

        public List<ClientesVM> ListadoClientes { get; set; }

        public List<EstadosPolizaVM> ListadoEstadosPoliza { get; set; }

        public List<PolizaVM> ListadoPoliza { get; set; }

        public ClientesVM Clientes { get; set; }

        public EstadosPolizaVM EstadosPoliza { get; set; }

        public PolizaVM Poliza { get; set; }
    }
}
