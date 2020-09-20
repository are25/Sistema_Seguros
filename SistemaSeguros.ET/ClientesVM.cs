namespace SistemaSeguros.API.Models
{
  

    public  class ClientesVM
    {
         
        public string IdentificacionCliente { get; set; }

        
        public string NombreCliente { get; set; }

         
        public string CorreoCliente { get; set; }

     
        public string TelefonoContacto { get; set; }

        
        //public virtual ICollection<PolizaPorCliente> PolizaPorCliente { get; set; }
    }
}
