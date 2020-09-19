var UrlAPI = 'https://localhost:44301/api';
var idEditar = '#idEditar';
var txtDescripcion = '#txtDescripcion';
var txtNombre = '#txtNombre';
var txtVigenciaI = '#txtVigenciaI';
var txtId = '#txtId';
var cboRiesgo = '#cboRiesgo';
var txtCobertura = '#txtCobertura';
var txtPeriodo = '#txtPeriodo';
var txtPrecio = '#txtPrecio';

var cboCubrimiento = '#cboCubrimiento';
var btnLimpiar = '#btnLimpiar';
var btnGuardar = '#btnGuardar';
var btnActualizar = '#btnActualizar';

function LimpiarCampos() {
    $(txtNombre).val('');
    $(txtDescripcion).val('');

    $(txtVigenciaI).val('');
    $(txtId).val('');
    $(cboRiesgo).val('');
    $(cboCubrimiento).val('');
    $(txtCobertura).val('');
    $(txtPeriodo).val('');
    $(txtPrecio).val('');
    $(btnGuardar).show()
    $(btnActualizar).hide();
}

$(document).ready(function () {
    $(btnActualizar).hide();
    $(btnGuardar).on('click', function () {
        //ingresar
        var fecha = moment($(txtVigenciaI).val()).format('YYYY-MM-DDTHH:mm:ss');

        var Polizas = { Id: 0, Nombre: $(txtNombre).val(), CoberturaPoliza: $(txtCobertura).val(), PeriodoCobertura: $(txtPeriodo).val(), IdTipoRiesgo: $(cboRiesgo).val(), IdTipoCubrimiento: $(cboCubrimiento).val(), InicioVigencia: fecha, Descripcion: $(txtDescripcion).val(), PrecioPoliza: $(txtPrecio).val() };
        $.ajax({
            type: "PUT",
            data: JSON.stringify(Polizas),
            url: UrlAPI + "/Polizas/RegistroPolizas"
        }).done(function (data) {
            if (data == "1") {
                swal({
                    title: 'Sistema de Seguros',
                    text: "Póliza agregado con éxito.",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                });

                LimpiarCampos();
                $("#tblPolizas").dataTable().fnDestroy();
                CargarTabla();
            } else if (data == "2") {
                swal({
                    title: 'Sistema de Seguros',
                    text: "La % de cubrimiento no puede ser mayor a 50, debido al riesgo alto.!",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                });

                LimpiarCampos();
                $("#tblPolizas").dataTable().fnDestroy();
                CargarTabla();
            }
            else {
                swal({
                    title: 'Sistema de Seguros',
                    text: "No se pudo guardar la póliza.",
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



    });
    $(btnActualizar).on('click', function () {
        //Actualizar
        var fecha = moment($(txtVigenciaI).val()).format('YYYY-MM-DDTHH:mm:ss');
        var Polizas = { Id: $(txtId).val(), Nombre: $(txtNombre).val(), CoberturaPoliza: $(txtCobertura).val(), PeriodoCobertura: $(txtPeriodo).val(), IdTipoRiesgo: $(cboRiesgo).val(), IdTipoCubrimiento: $(cboCubrimiento).val(), InicioVigencia: fecha, Descripcion: $(txtDescripcion).val(), PrecioPoliza: $(txtPrecio).val() };
        $.ajax({
            type: "PATCH",
            data: JSON.stringify(Polizas),
            url: UrlAPI + "/Polizas/EditarPolizas"
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
                $("#tblPolizas").dataTable().fnDestroy();
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
                $("#tblPolizas").dataTable().fnDestroy();
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
                text: jqXHR.responseText || textStatus,
                type: 'error',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            })
        });


    });

    $(btnLimpiar).on('click', function () {
        LimpiarCampos();
    });
    
    $(txtVigenciaI).datepicker({
        format: 'YYYY/MM/DD'
    });
    CargarTabla();
    CargarCatalogos();

});
 
function Carga(id, Nombre, Descripcion, CoberturaPoliza,InicioVigente,PeriodoCobertura,PrecioPoliza,TipoRiesgoId,TipoCubrimientoId) {
    $(txtNombre).val(Nombre);
    $(txtCobertura).val(CoberturaPoliza);
    $(txtPeriodo).val(PeriodoCobertura);
    $(txtPrecio).val(PrecioPoliza);
    $(cboRiesgo).val(TipoRiesgoId).change();
    $(cboCubrimiento).val(TipoCubrimientoId).change();
    $(txtVigenciaI).val(InicioVigente);
    $(txtDescripcion).val(Descripcion);
    $(txtId).val(id);
    $(txtId).attr("ReadOnly", true);
    $(txtVigenciaI).datepicker({
        format: 'YYYY/MM/DD'
    });
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
                var Polizas = { Id: Id, Descripcion: $(txtNombre).val() };

                $.ajax({
                    type: "DELETE",
                    data: JSON.stringify(Polizas),
                    url: UrlAPI + "/Polizas/EliminarPolizas"
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
                        $("#tblPolizas").dataTable().fnDestroy();
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

function CargarCatalogos() {
     //Riesgo
    $.ajax({
        type: "GET",
        url: UrlAPI + "/TipoRiesgo/CargarTipoRiesgo",
        success: function (data) {
            $(cboRiesgo).empty();
              $.each(JSON.parse(data), function (key, value) {
                $(cboRiesgo).append($("<option/>").val(value.Id).text(value.Descripcion));
            })
        }
    });
    //Cubrimiento
    $.ajax({
        type: "GET",
        url: UrlAPI + "/TipoCubrimiento/CargarTipoCubrimiento",
        success: function (data) {
            $(cboCubrimiento).empty();
             $.each(JSON.parse(data), function (key, value) {
                 $(cboCubrimiento).append($("<option/>").val(value.Id).text(value.Descripcion));
            })
        }
    });

    $(cboRiesgo).val(0);
    $(cboCubrimiento).val(0);
}

function CargarTabla() {
    $("#tblPolizas").DataTable({
        ajax: {
            url: UrlAPI + "/Polizas/CargarPolizas",
            dataSrc: ""
        },
        columns: [
            {
                data: "Id"
            },
            {
                data: "Nombre"
            },
             
            {
                data: "Descripcion"
            },
             
            {
                data: "CoberturaPoliza"
            },
             
            {
                data: "InicioVigencia",
                render: function (data) {
                    return moment(data).format('YYYY/MM/DD');
                }
                 
            },
            {
                data: "PeriodoCobertura",
                render: function (data) {
                    return data + " meses";
                }
            },
            {
                data: "FinVigencia", render: function (data) {
                    return moment(data).format('YYYY/MM/DD');
                }
            },
             
            {
                data: "PrecioPoliza"
            },
             
            {
                data: "TipoCubrimiento.Descripcion"
            },
             
            {
                data: "TipoRiesgo.Descripcion"
            },
             
            
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    var btn = '<a class="btn btn-warning" id="idEditar" href="#" onclick="javaScript:Carga(\'' + data.Id + '\',\'' + data.Nombre + '\',\'' + data.Descripcion + '\',\'' + data.CoberturaPoliza + '\',\'' + moment(data.InicioVigencia).format('YYYY/MM/DD') + '\',\'' + data.PeriodoCobertura + '\',\'' + data.PrecioPoliza + '\',\'' + data.TipoRiesgo.Id + '\',\'' + data.TipoCubrimiento.Id + '\')"><span title="Editar Póliza" class="fas fa-edit"></span></a>';
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
