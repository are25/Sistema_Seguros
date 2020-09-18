CREATE TABLE [dbo].[Clientes]
(
	[IdentificacionCliente] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [NombreCliente] VARCHAR(50) NOT NULL, 
    [CorreoCliente] VARCHAR(50) NULL, 
    [TelefonoContacto] VARCHAR(50) NOT NULL
)
