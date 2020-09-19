CREATE TABLE [dbo].[PolizaPorCliente]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdPoliza] INT NOT NULL, 
    [IdCliente] VARCHAR(20) NOT NULL, 
    [IdEstado] INT NOT NULL,
	CONSTRAINT [FK_Estado] FOREIGN KEY ([IdEstado]) REFERENCES [dbo].[EstadosPoliza] ([Id])
,	CONSTRAINT [FK_Cliente] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Clientes] ([IdentificacionCliente])
,	CONSTRAINT [FK_Poliza] FOREIGN KEY ([IdPoliza]) REFERENCES [dbo].[Poliza] ([Id])

)
