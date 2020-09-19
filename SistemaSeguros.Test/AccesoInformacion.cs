using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaSeguros.API.Models;
using SistemaSeguros.API.Controllers;
using System.Web.Http.Results;
using System.Net.Http;
using System.Web.Http;

namespace SistemaSeguros.Test
{
    [TestClass]
    public class AccesoInformacion
    {
        [TestMethod]
        public void AgregarCliente()
        {
            // Arrange
            var controller = new ClientesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            Clientes cli = new Clientes()
            {
                IdentificacionCliente = "304670958",
                NombreCliente = "Arelis Orozco",
                CorreoCliente = "arelisd.25@gmail.com",
                TelefonoContacto = "71510015"
            };

            // Act
            var response = controller.RegistroCliente();

            // Assert
            Clientes product;
            Assert.IsTrue(response.TryGetContentValue<Clientes>(out product));
            Assert.AreEqual(10, product.IdentificacionCliente);
        }
    }
}
