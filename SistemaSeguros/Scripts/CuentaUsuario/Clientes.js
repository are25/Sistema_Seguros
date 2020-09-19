var UrlAPI = 'https://localhost:44301/api';
var idEditar = '#idEditar';
var txtNombre = '#txtNombre';
var txtCorreo = '#txtCorreo';
var txtTelefono = '#txtTelefono';
var txtIdentificacion = '#txtIdentificacion';
var btnLimpiar = '#btnLimpiar';
var btnGuardar = '#btnGuardar';
var btnActualizar = '#btnActualizar';

function LimpiarCampos() {
    $(txtNombre).val('');
    $(txtCorreo).val('');
    $(txtTelefono).val('');
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
        if (Administracion.validar_email($(txtCorreo).val())) {
            var Cliente = { CorreoCliente: $(txtCorreo).val(), TelefonoContacto: $(txtTelefono).val(), IdentificacionCliente: $(txtIdentificacion).val(), NombreCliente: $(txtNombre).val() };
            $.ajax({
                type: "PUT",
                data: JSON.stringify(Cliente),
                url: UrlAPI + "/Clientes/RegistroCliente"
            }).done(function (data) {
                if (data == "1") {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Cliente agregado con éxito.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblClientes").dataTable().fnDestroy();
                    CargarTabla();
                } else if (data == "2") {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Cliente existente.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblClientes").dataTable().fnDestroy();
                    CargarTabla();
                }
                else {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo guardar el Cliente.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal({
                    title: 'Sistema de Seguros',
                    text: jqXHR.responseText || textStatus,
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


    });
    $(btnActualizar).on('click', function () {
        //Actualizar
        if (Administracion.validar_email($(txtCorreo).val())) {
            var Cliente = { CorreoCliente: $(txtCorreo).val(), TelefonoContacto: $(txtTelefono).val(), IdentificacionCliente: $(txtIdentificacion).val(), NombreCliente: $(txtNombre).val() };
            $.ajax({
                type: "PATCH",
                data: JSON.stringify(Cliente),
                url: UrlAPI + "/Clientes/EditarCliente"
            }).done(function (data) {
                if (data == "1") {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Cliente actualizado con éxito.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblClientes").dataTable().fnDestroy();
                    CargarTabla();
                }
                else {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo actualizar el Cliente.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal({
                    title: 'Sistema de Seguros',
                    text: jqXHR.responseText || textStatus,
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

    });

    $(btnLimpiar).on('click', function () {
        LimpiarCampos();
    });

    CargarTabla();


});
function Carga(cedula, nombreCliente, correoCliente, telefonoCliente) {
    $(txtNombre).val(nombreCliente);
    $(txtCorreo).val(correoCliente);
    $(txtTelefono).val(telefonoCliente);
    $(txtIdentificacion).val(cedula);
    $(txtIdentificacion).attr("ReadOnly", true);

    $(btnActualizar).show()
    $(btnGuardar).hide();

}

function Eliminar(cedula) {
    swal({
        title: "¿Desea eliminar el cliente?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                //Quitar Cliente
                var Cliente = { CorreoCliente: $(txtCorreo).val(), TelefonoContacto: $(txtTelefono).val(), IdentificacionCliente: cedula, NombreCliente: $(txtNombre).val() };

                $.ajax({
                    type: "DELETE",
                    data: JSON.stringify(Cliente),
                    url: UrlAPI + "/Clientes/EliminarCliente"
                }).done(function (data) {
                    if (data == "1") {
                        swal({
                            title: 'Sistema de Seguros',
                            text: "Cliente eliminado con éxito.",
                            type: 'error',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33'
                        });

                        LimpiarCampos();
                        $("#tblClientes").dataTable().fnDestroy();
                        CargarTabla();
                    }
                    else {
                        swal({
                            title: 'Sistema de Seguros',
                            text: "No se pudo eliminar el Cliente.",
                            type: 'error',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33'
                        });
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: 'Sistema de Seguros',
                        text: jqXHR.responseText || textStatus,
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    })
                });
            }
        });
}

function CargarTabla() {
    $("#tblClientes").DataTable({
        ajax: {
            url: UrlAPI + "/Clientes/CargarClientes",
            dataSrc: ""
        },
        columns: [
            {
                data: "IdentificacionCliente"
            },
            {
                data: "NombreCliente"
            },
            {
                data: "CorreoCliente"
            },
            {
                data: "TelefonoContacto"
            }, {
                "data": null,
                "render": function (data, type, row, meta) {
                    var btn = '<a class="btn btn-warning" id="idEditar" href="#" onclick="javaScript:Carga(\'' + data.IdentificacionCliente + '\',\'' + data.NombreCliente + '\',\'' + data.CorreoCliente + '\',\'' + data.TelefonoContacto + '\')"><span title="Editar Cliente" class="fas fa-edit"></span></a>';
                    btn = btn + '           <a class="btn btn-danger" id="idEliminar" href="#" onclick="javaScript:Eliminar(\'' + data.IdentificacionCliente + '\')"><span title="Eliminar" class="fas fa-trash-alt"></span></a>';

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
