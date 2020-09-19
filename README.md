# Sistema_Seguros
Sistema para póliza de seguros.

Se debe crear en SQL Server , una Base de datos con el nombre "Seguros".
Seguidamente, en el proyecto de BD Sistema seguros, hacer clic en publicar, para poder cargar la información/tablas a la BD. Para esto se debe ingresar
el string de conexión de cada computador.

Luego de tener identificado el string de conexión y tener lista la BD, se procede a actualizar los web.config, y app.config con el "Data Source" que corresponde.

El proyecto está para cargar el API y el WEB Al mismo tiempo.
En caso de modificar los puertos de inicio para el API, se debe de cambiar en la carpeta "Scripts"--> Catálogo, Poliza y CuentaUsuario, la variable "UrlAPI"

**Inicio de sesión**
Se debe contar con un usuario registrado en BD. Para temas de ejemplificar dicha funcionalidad, la contraseña no se encriptó.
Accesos:
U: demo.25@gmail.com
P: demo2020

Además el sistema permite crear usuarios, y cada inicio de sesión se almacena en una cookie, que al vencerse el tiempo dentro del browser, se procede a 
solicitar el login nuevamente.

El sistema cuenta con registro de errores sobre un txt. Para esto en el Web.config del API, se debe modificar la unidad de disco, a la que el computador tenga permisos de lectura.