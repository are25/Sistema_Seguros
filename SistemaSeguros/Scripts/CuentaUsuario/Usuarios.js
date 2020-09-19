var UrlAPI = 'https://localhost:44301/api';
var idEditar = '#idEditar';
var txtNombre = '#txtNombre';
var txtCorreo = '#txtCorreo';
var txtContrasennia = '#txtContrasennia';
var txtIdentificacion = '#txtIdentificacion';
var btnLimpiar = '#btnLimpiar';
var btnGuardar = '#btnGuardar';
var btnActualizar = '#btnActualizar';

function LimpiarCampos() {
    $(txtNombre).val('');
    $(txtCorreo).val('');
    $(txtContrasennia).val('');
    $(txtIdentificacion).val('');
    $(txtIdentificacion).attr("ReadOnly", false);
    $(btnGuardar).show()
    $(btnActualizar).hide();

}

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

$(document).ready(function () {
    $(btnActualizar).hide();
    $(btnGuardar).on('click', function () {
        //ingresar
        if ($(txtCorreo).val() != ""
            && $(txtContrasennia).val() != ""
            && $(txtIdentificacion).val() != ""
            && $(txtNombre).val() != "") {
            if (Administracion.validar_email($(txtCorreo).val())) {
                var usuario = { CorreoUsuario: $(txtCorreo).val(), Contrasennia: $(txtContrasennia).val(), Identificacion: $(txtIdentificacion).val(), NombreUsuario: $(txtNombre).val() };
                $.ajax({
                    type: "PUT",
                    data: usuario,
                    dataType: "json",

                    url: UrlAPI + "/CuentaUsuario/RegistroUsuario"
                }).done(function (data) {

                    swal({
                        title: 'Sistema de Seguros',
                        text: "Usuario agregado con éxito.",
                        type: 'success',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblUsuarios").dataTable().fnDestroy();
                    CargarTabla();


                }).fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo guardar el usuario.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });
                });
            } else {
                swal({
                    title: 'Sistema de Seguros',
                    text: "Correo incorrecto",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                })
            }
        } else {
            swal({
                title: 'Sistema de Seguros',
                text: "Debe de ingresar la información solicitada.",
                type: 'error',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            })
        }



    });
    $(btnActualizar).on('click', function () {
        //Actualizar
        if ($(txtCorreo).val() != ""
            && $(txtContrasennia).val() != ""
            && $(txtIdentificacion).val() != ""
            && $(txtNombre).val() != "") {
            if (Administracion.validar_email($(txtCorreo).val())) {
                var usuario = { CorreoUsuario: $(txtCorreo).val(), Contrasennia: $(txtContrasennia).val(), Identificacion: $(txtIdentificacion).val(), NombreUsuario: $(txtNombre).val() };
                $.ajax({
                    type: "PATCH",
                    data: usuario,
                    dataType: "json",

                    url: UrlAPI + "/CuentaUsuario/EditarUsuario"
                }).done(function (data) {

                    swal({
                        title: 'Sistema de Seguros',
                        text: "Usuario actualizado con éxito.",
                        type: 'success',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblUsuarios").dataTable().fnDestroy();
                    CargarTabla();

                }).fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo realizar la acción",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    })
                });
            } else {
                swal({
                    title: 'Sistema de Seguros',
                    text: "Correo incorrecto",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                })
            }
        } else {
            swal({
                title: 'Sistema de Seguros',
                text: "Debe ingresar la información solicitada",
                type: 'error',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            })
        }

    });

    $(btnLimpiar).on('click', function () {
        LimpiarCampos();
    });

    CargarTabla();


});
function Carga(cedula, nombreUsuario, correoUsuario, contraseniaUsuario) {
    $(txtNombre).val(nombreUsuario);
    $(txtCorreo).val(correoUsuario);
    $(txtContrasennia).val(contraseniaUsuario);
    $(txtIdentificacion).val(cedula);
    $(txtIdentificacion).attr("ReadOnly", true);

    $(btnActualizar).show()
    $(btnGuardar).hide();

}

function Eliminar(cedula) {
    swal({
        title: "¿Desea eliminar el usuario?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                //Quitar usuario
                var usuario = { CorreoUsuario: $(txtCorreo).val(), Contrasennia: $(txtContrasennia).val(), Identificacion: cedula, NombreUsuario: $(txtNombre).val() };

                $.ajax({
                    type: "DELETE",
                    data: usuario,
                    dataType: "json",

                    url: UrlAPI + "/CuentaUsuario/EliminarUsuario"
                }).done(function (data) {

                    swal({
                        title: 'Sistema de Seguros',
                        text: "Usuario eliminado con éxito.",
                        type: 'success',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblUsuarios").dataTable().fnDestroy();
                    CargarTabla();

                }).fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo eliminar el usuario.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });
                });
            }
        });
}

function CargarTabla() {
    $("#tblUsuarios").DataTable({
        ajax: {
            url: UrlAPI + "/CuentaUsuario/CargarUsuarios",
            dataSrc: ""
        },
        columns: [
            {
                data: "Identificacion"
            },
            {
                data: "NombreUsuario"
            },
            {
                data: "CorreoUsuario"
            },
            {
                data: "Contrasennia"
            }, {
                "data": null,
                "render": function (data, type, row, meta) {
                    var btn = '<a class="btn btn-warning" id="idEditar" href="#" onclick="javaScript:Carga(\'' + data.Identificacion + '\',\'' + data.NombreUsuario + '\',\'' + data.CorreoUsuario + '\',\'' + data.Contrasennia + '\')"><span title="Editar Usuario" class="fas fa-edit"></span></a>';
                    btn = btn + '           <a class="btn btn-danger" id="idEliminar" href="#" onclick="javaScript:Eliminar(\'' + data.Identificacion + '\')"><span title="Eliminar" class="fas fa-trash-alt"></span></a>';

                    return btn;
                }
            },
        ],
        language: {
            "decimal": "",
            "emptyTable": "No hay información para mostrar.",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        "bDestroy": true
    });
}
