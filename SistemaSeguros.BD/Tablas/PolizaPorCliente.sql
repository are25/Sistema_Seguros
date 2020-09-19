CREATE TABLE [dbo].[PolizaPorCliente]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [IdPoliza] INT NOT NULL, 
    [IdCliente] VARCHAR(50) NOT NULL, 
    [IdEstado] INT NOT NULL,
	CONSTRAINT [FK_Estado] FOREIGN KEY ([IdEstado]) REFERENCES [dbo].[EstadosPoliza] ([Id])

)
