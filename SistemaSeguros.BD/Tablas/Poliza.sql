CREATE TABLE [dbo].[Poliza]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nombre] VARCHAR(100) NOT NULL, 
    [Descripcion] VARCHAR(200) NULL, 
    [IdTipoCubrimiento] INT NULL, 
    [CoberturaPoliza] DECIMAL(18, 2) NOT NULL, 
    [InicioVigencia] DATE NOT NULL, 
    [PeriodoCobertura] INT NOT NULL, 
    [FinVigencia] DATE NOT NULL, 
    [PrecioPoliza] NUMERIC(18, 2) NOT NULL, 
    [IdTipoRiesgo] INT NULL,
	CONSTRAINT [FK_TipoCubrimiento] FOREIGN KEY ([IdTipoCubrimiento]) REFERENCES [dbo].[TipoCubrimiento] ([Id]),
	CONSTRAINT [FK_TipoRiesgo] FOREIGN KEY ([IdTipoRiesgo]) REFERENCES [dbo].[TipoRiesgo] ([Id])

)
