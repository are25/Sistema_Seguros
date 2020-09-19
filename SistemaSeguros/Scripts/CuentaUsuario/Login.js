//sección variables
var credencial = '#txtCredencial';
var correo = '#txtCorreo';
var btnIniciar = '#btnIniciar';
var UrlAPI = 'https://localhost:44301/api';
var Administracion = {
    validar_email: function (email) {
        re = /^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,3})$/
        if (!re.exec(email)) {
            return false;
        } else {
            return true;
        }
    }

}
function IniciarSesion() {
    if ($(credencial).val() != "" && $(correo).val() != "") {
        if (Administracion.validar_email($(correo).val())) {
            //Envío al API 
            var usuario = { correo: $(correo).val(), credencial: $(credencial).val() };

            $.ajax({
                type: "POST",
                data: JSON.stringify(usuario),
                url: UrlAPI + "/CuentaUsuario/InicioSesion"
            }).done(function (data) {
                if (data != "") {
                    //Usuario existe

                    $.ajax({
                        url: "/Home/CrearCookie",
                        type: "POST",
                        data: {
                            'Usuario': data
                        },
                        datatype: "json",
                        success: function (data) {
                            if (data.datosCliente) {
                                window.location.href = "/Home/Index";
                            }
                        }
                    });
                } else {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Los datos ingresados, no están registrados en el Sistema.!",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    })
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal({
                    title: 'Sistema de Seguros',
                    text: "No se pudo iniciar sesión",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                })
            });

        } else {
            swal({
                title: 'Sistema de Seguros',
                text: "El formato del correo no es el correcto!",
                type: 'error',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            })
        }
    } else {
        //faltan datos
        swal({
            title: 'Sistema de Seguros',
            text: "Debe ingresar un correo y una credencial, para poder iniciar",
            type: 'error',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33'
        })
    }
}
$(document).ready(function () {
    $(btnIniciar).on('click', function () {
        IniciarSesion();
    });
});

$(document).on("keypress", "input", function (e) {

    if (e.which == 13) {

        IniciarSesion();

    }

});