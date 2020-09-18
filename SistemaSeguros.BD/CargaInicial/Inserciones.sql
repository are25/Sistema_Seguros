/**
Inserciones iniciales para el uso del sistema, conectado a Base de datos 
**/
---Inserción a usuarios

INSERT INTO Usuarios VALUES ('304670958','Arelis','arelisd.25@gmail.com','areorozco2020')
	, ('101110222','Demo','demo@gmail.com','demo2020')

---Inserción a clientes

INSERT INTO Clientes VALUES ('302150336','Carlos Orozco','c02costarica@gmail.com','88888888')
	, ('104450245','Mari Picado','mpicado02@gmail.com','88888888')


---Inserción tipo Riesgo
INSERT INTO TipoRiesgo (Descripcion) VALUES ('Bajo')
	, ('Medio')
	, ('Medio-alto')
	, ('Alto')

--Inserción tipo Cubrimiento
INSERT INTO TipoCubrimiento (Descripcion) VALUES ('Terremoto')
	, ('incendio')
	, ('Robo')
	, ('Pérdida')