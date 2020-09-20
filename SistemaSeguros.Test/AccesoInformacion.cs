using Microsoft.VisualStudio.TestTools.UnitTesting;
using SistemaSeguros.API.Models;
using SistemaSeguros.API.Controllers;
using System.Web.Http.Results;
using System.Web.Http;
using System;

namespace SistemaSeguros.Test
{
    [TestClass]
    public class AccesoInformacion
    {
        [TestMethod]
        public void AgregarCliente()
        {
            var controller = new ClientesController();

            Clientes clientes = new Clientes()
            {
                IdentificacionCliente = "304670958",
                NombreCliente = "Arelis Orozco",
                CorreoCliente = "arelisd.25@gmail.com",
                TelefonoContacto = "71510015"
            };
            // Act
            IHttpActionResult actionResult = controller.RegistroCliente(clientes);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Clientes>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

        [TestMethod]
        public void AgregarUsuario()
        {
            var controller = new CuentaUsuarioController();

            Usuarios usuario = new Usuarios()
            {
                Identificacion = "123654789",
                NombreUsuario = "Usuario Prueba2",
                Contrasennia = "demo2020",
                CorreoUsuario = "dco2020@gmail.com"
            };
            // Act
            IHttpActionResult actionResult = controller.RegistroUsuario(usuario);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Usuarios>;
            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);

        }

        [TestMethod]
        public void AgregarRiesgo()
        {
            var controller = new TipoRiesgoController();

            TipoRiesgo riesgo = new TipoRiesgo()
            {
                Id = 0,
                Descripcion = "Prueba Riesg44o"
            };
            // Act
            IHttpActionResult actionResult = controller.RegistroTipoRiesgo(riesgo);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<TipoRiesgo>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);

        }

        [TestMethod]
        public void AgregarCubrimiento()
        {
            var controller = new TipoCubrimientoController();

            TipoCubrimiento riesgo = new TipoCubrimiento()
            {
                Id = 0,
                Descripcion = "Prueba cubrimiento"
            };
            // Act
            IHttpActionResult actionResult = controller.RegistroTipoCubrimiento(riesgo);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<TipoCubrimiento>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);

        }

        [TestMethod]
        public void AgregarPolizaCorrecta()
        {
            var controller = new PolizasController();

            Poliza poliza = new Poliza()
            {
                Id = 0,
                CoberturaPoliza = 20,
                Descripcion = "Prueba póliza",
                IdTipoCubrimiento = 1,
                IdTipoRiesgo = 4,
                PrecioPoliza = 34220,
                Nombre = "POLIZA DEMO",
                PeriodoCobertura = 3,
                InicioVigencia = Convert.ToDateTime("2020-06-22 15:00:00"),
                FinVigencia = Convert.ToDateTime("2020-09-22 15:00:00")
            };
            // Act
            IHttpActionResult actionResult = controller.RegistroPolizas(poliza);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Poliza>;
            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }


        [TestMethod]
        public void AgregarPolizaRiesgoAlto()
        {
            var controller = new PolizasController();

            Poliza poliza = new Poliza()
            {
                Id = 0,
                CoberturaPoliza = 55,
                Descripcion = "Prueba póliza",
                IdTipoCubrimiento = 1,
                IdTipoRiesgo = 4,
                PrecioPoliza = 34220,
                Nombre = "POLIZA DEMO #2",
                PeriodoCobertura = 3,
                InicioVigencia = Convert.ToDateTime("2020-06-22 15:00:00"),
                FinVigencia = Convert.ToDateTime("2020-09-22 15:00:00")
            };
            // Act
            IHttpActionResult actionResult = controller.RegistroPolizas(poliza);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Poliza>;
            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual(2, createdResult.RouteValues["id"]);//El 2 es la bandera para evitar registrar la póliza por riesgo alto.
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

    }
}
