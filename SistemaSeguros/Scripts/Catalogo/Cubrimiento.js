var UrlAPI = 'https://localhost:44301/api';
var idEditar = '#idEditar';
var txtNombre = '#txtDescripcion';

var txtId = '#txtId';
var btnLimpiar = '#btnLimpiar';
var btnGuardar = '#btnGuardar';
var btnActualizar = '#btnActualizar';

function LimpiarCampos() {
    $(txtNombre).val('');
    $(txtId).val('');
    $(txtId).val('');
    $(txtId).attr("ReadOnly", false);
    $(btnGuardar).show()
    $(btnActualizar).hide();

}

$(document).ready(function () {
    $(btnActualizar).hide();
    $(btnGuardar).on('click', function () {
        //ingresar
        if ($(txtNombre).val() != "") {
            var TipoCubrimiento = { Id: 0, Descripcion: $(txtNombre).val() };
            $.ajax({
                type: "PUT",
                data: TipoCubrimiento,
                dataType: "json",

                url: UrlAPI + "/TipoCubrimiento/RegistroTipoCubrimiento"
            }).done(function (data) {
                 
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Tipo Cubrimiento agregado con éxito.",
                        type: 'error',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblTipoCubrimiento").dataTable().fnDestroy();
                    CargarTabla();
                 
                 
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal({
                    title: 'Sistema de Seguros',
                    text: "No se pudo guardar el Tipo Cubrimiento.",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                });
            });
        } else {
            swal({
                title: 'Sistema de Seguros',
                text: "Ingrese la información solicitada",
                type: 'error',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33'
            })
        }




    });
    $(btnActualizar).on('click', function () {
        //Actualizar
        if ($(txtNombre).val() != "") {
            var TipoCubrimiento = { Id: $(txtId).val(), Descripcion: $(txtNombre).val() };
            $.ajax({
                type: "PATCH",
                data: TipoCubrimiento,
                dataType: "json",

                url: UrlAPI + "/TipoCubrimiento/EditarTipoCubrimiento"
            }).done(function (data) {
                 
                    swal({
                        title: 'Sistema de Seguros',
                        text: "Tipo Cubrimiento actualizado con éxito.",
                        type: 'success',
                        showCancelButton: true,
                        confirmButtonColor: '#3085d6',
                        cancelButtonColor: '#d33'
                    });

                    LimpiarCampos();
                    $("#tblTipoCubrimiento").dataTable().fnDestroy();
                    CargarTabla();
                
            }).fail(function (jqXHR, textStatus, errorThrown) {
                swal({
                    title: 'Sistema de Seguros',
                    text: "No se pudo actualizar el Tipo Cubrimiento.",
                    type: 'error',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33'
                });
            });
        } else {
            swal({
                title: 'Sistema de Seguros',
                text: "Ingrese la información solicitada",
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
function Carga(id, Descripcion) {
    $(txtNombre).val(Descripcion);

    $(txtId).val(id);
    $(txtId).attr("ReadOnly", true);

    $(btnActualizar).show()
    $(btnGuardar).hide();

}

function Eliminar(Id) {
    swal({
        title: "¿Desea eliminar el Tipo Cubrimiento?",
        text: "",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                //Quitar TipoCubrimiento
                var TipoCubrimiento = { Id: Id, Descripcion: $(txtNombre).val() };

                $.ajax({
                    type: "DELETE",
                    data: TipoCubrimiento,
                    dataType: "json",

                    url: UrlAPI + "/TipoCubrimiento/EliminarTipoCubrimiento"
                }).done(function (data) {
                         swal({
                            title: 'Sistema de Seguros',
                            text: "Tipo Cubrimiento eliminado con éxito.",
                            type: 'success',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33'
                        });

                        LimpiarCampos();
                        $("#tblTipoCubrimiento").dataTable().fnDestroy();
                        CargarTabla();
                    
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    swal({
                        title: 'Sistema de Seguros',
                        text: "No se pudo eliminar el Tipo Cubrimiento.",
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
    $("#tblTipoCubrimiento").DataTable({
        ajax: {
            url: UrlAPI + "/TipoCubrimiento/CargarTipoCubrimiento",
            dataSrc: ""
        },
        columns: [
            {
                data: "Id"
            },
            {
                data: "Descripcion"
            },
            {
                "data": null,
                "render": function (data, type, row, meta) {
                    var btn = '<a class="btn btn-warning" id="idEditar" href="#" onclick="javaScript:Carga(\'' + data.Id + '\',\'' + data.Descripcion + '\')"><span title="Editar Tipo Cubrimiento" class="fas fa-edit"></span></a>';
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
        "bDestroy": true
    });
}
