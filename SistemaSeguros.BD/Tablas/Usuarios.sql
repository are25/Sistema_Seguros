CREATE TABLE [dbo].[Usuarios]
(
	[Identificacion] VARCHAR(20) NOT NULL PRIMARY KEY, 
    [NombreUsuario] VARCHAR(50) NOT NULL, 
    [CorreoUsuario] VARCHAR(50) NOT NULL, 
    [Contrasennia] VARCHAR(50) NOT NULL
)
