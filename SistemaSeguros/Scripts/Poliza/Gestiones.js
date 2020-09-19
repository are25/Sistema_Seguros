var UrlAPI = 'https://localhost:44301/api';
var idEditar = '#idEditar';
var txtId = '#txtId';
var cboCliente = '#cboCliente';
var cboEstado = '#cboEstado';
var cboPoliza = '#cboPoliza';
var btnLimpiar = '#btnLimpiar';
var btnGuardar = '#btnGuardar';
var btnActualizar = '#btnActualizar';

function LimpiarCampos() {

    $(txtId).val('');
    $(cboCliente).val(0).change()
    $(cboEstado).val(1).change();
    $(cboPoliza).val(0).change()
    $(cboEstado).prop('disabled', true);
    $(btnGuardar).show()
    $(btnActualizar).hide();
}

$(document).ready(function () {
    $(btnActualizar).hide();
    $(btnGuardar).on('click', function () {
        //ingresar
        if ($(cboPoliza).val() != "0"
            && $(cboCliente).val() != "0"
            && $(cboEstado).val() != "0") {
            var PolizaPorCliente = { Id: 0, IdPoliza: $(cboPoliza).val(), IdCliente: $(cboCliente).val(), IdEstado: $(cboEstado).val() };
            $.ajax({
                type: "PUT",
                data: JSON.stringify(PolizaPorCliente),
                url: UrlAPI + "/Gestiones/RegistroPolizaCliente"
            }).done(function (data) {
                if (data == "1") {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Póliza Asignada con éxito.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblGestionPolizas").dataTable().fnDestroy();
                    CargarTabla();
                }
                else {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo guardar la póliza para el cliente.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });
                }
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
                text: "Debe elegir la información que se solicita.",
                type: 'error',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            })
        }
    });
    $(btnActualizar).on('click', function () {
        //Actualizar
        if ($(cboPoliza).val() != "0"
            && $(cboCliente).val() != "0"
            && $(cboEstado).val() != "0") {
            var PolizaPorCliente = { Id: $(txtId).val(), IdPoliza: $(cboPoliza).val(), IdCliente: $(cboCliente).val(), IdEstado: $(cboEstado).val() };
            $.ajax({
                type: "PATCH",
                data: JSON.stringify(PolizaPorCliente),
                url: UrlAPI + "/Gestiones/EditarPoliza"
            }).done(function (data) {
                if (data == "1") {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Póliza actualizado con éxito.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblGestionPolizas").dataTable().fnDestroy();
                    CargarTabla();
                }
                else if (data == "2") {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "La % de cubrimiento no puede ser mayor a 50, debido al riesgo alto.!",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblGestionPolizas").dataTable().fnDestroy();
                    CargarTabla();
                }
                else {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo actualizar la póliza.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });
                }
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
                text: "Debe elegir la información que se solicita.",
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
    CargarCatalogos();

});

function Carga(id, idPoliza, idCliente, idEstado) {

    $(cboEstado).val(idEstado).change();
    $(cboCliente).val(idCliente).change();
    $(cboPoliza).val(idPoliza).change();

    $(txtId).val(id);
    $(txtId).attr("ReadOnly", true);
    $(cboEstado).prop('disabled', false);
    $(btnActualizar).show()
    $(btnGuardar).hide();

}

function Eliminar(Id) {
    swal({
        title: "¿Desea eliminar el Póliza?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                //Quitar Polizas
                var Polizas = { Id: Id };

                $.ajax({
                    type: "DELETE",
                    data: JSON.stringify(Polizas),
                    url: UrlAPI + "/Gestiones/EliminarPoliza"
                }).done(function (data) {
                    if (data == "1") {
                        swal({
                            title: 'Sistema de Seguros',
                            text: "Póliza eliminado con éxito.",
                            type: 'error',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33'
                        });

                        LimpiarCampos();
                        $("#tblGestionPolizas").dataTable().fnDestroy();
                        CargarTabla();
                    }
                    else {
                        swal({
                            title: 'Sistema de Seguros',
                            text: "No se pudo eliminar la póliza.",
                            type: 'error',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33'
                        });
                    }
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
            }
        });
}

function CargarCatalogos() {
    //Estados
    $.ajax({
        type: "GET",
        url: UrlAPI + "/Estados/CargarEstados",
        success: function (data) {
            $(cboEstado).empty();

            $.each(JSON.parse(data), function (key, value) {
                $(cboEstado).append($("<option/>").val(value.Id).text(value.Descripcion));
            })
        }
    });
    //Polizas
    $.ajax({
        type: "GET",
        url: UrlAPI + "/Polizas/CargarPolizas",
        success: function (data) {
            $(cboPoliza).empty();
            $(cboPoliza).append($("<option/>").val(0).text("Seleccione"));

            $.each(data, function (key, value) {
                $(cboPoliza).append($("<option/>").val(value.Id).text(value.Nombre));
            })
        }
    });
    //Clientes
    $.ajax({
        type: "GET",
        url: UrlAPI + "/Clientes/CargarClientes",
        success: function (data) {
            $(cboCliente).empty();
            $(cboCliente).append($("<option/>").val(0).text("Seleccione"));

            $.each(data, function (key, value) {
                $(cboCliente).append($("<option/>").val(value.IdentificacionCliente).text(value.NombreCliente));
            })
        }
    });


    $(cboEstado).val(1).change();

    $(cboEstado).prop('disabled', true);

    $(cboPoliza).val(0);
    $(cboCliente).val(0);
}

function CargarTabla() {
    $("#tblGestionPolizas").DataTable({
        ajax: {
            url: UrlAPI + "/Gestiones/CargarPolizas",
            dataSrc: ""
        },
        columns: [
            {
                data: "Poliza.Descripcion"
            },
            {
                data: "IdCliente"
            },

            {
                data: "Clientes.NombreCliente"
            },

            {
                data: "EstadosPoliza.Descripcion"
            },

            {
                "data": null,
                "render": function (data, type, row, meta) {
                    var btn = '<a class="btn btn-warning" id="idEditar" href="#" onclick="javaScript:Carga(\'' + data.Id + '\',\'' + data.Poliza.Id + '\',\'' + data.Clientes.IdentificacionCliente + '\',\'' + data.EstadosPoliza.Id + '\')"><span title="Editar Póliza" class="fas fa-edit"></span></a>';
                    btn = btn + '           <a class="btn btn-danger" id="idEliminar" href="#" onclick="javaScript:Eliminar(\'' + data.Id + '\')"><span title="Eliminar" class="fas fa-trash-alt"></span></a>';

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
        "bDestroy": true,
        "autoWidth": true
    });
}
