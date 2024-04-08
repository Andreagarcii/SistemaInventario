let datatable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblDatos').DataTable({
        // seccion de ajax para el plugin
        "ajax": { "url": "/Admin/Bodega/ObtenerTodos" },
        "columns": [
            { "data": "nombre", "width": "20%" },
            { "data": "descripcion", "width": "40%" },
            {
                "data": "estado",
                "render": function (data) {
                    return data ? "Activo" : "Inactivo";
                },
                "width": "20%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                        <a href="/Admin/Bodega/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                            <i class="bi bi-pencil-square"></i>
                        </a>
                        <a onclick="Delete('/Admin/Bodega/Delete/${data}')" class="btn btn-danger text-white" style="cursor:pointer">
                            <i class="bi bi-trash3-fill"></i> 
                        </a>
                    </div>`;
                },
                "width": "20%"
            }
        ],
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.25/i18n/Spanish.json"
        }
    });
}

funtion Delete(url){
    title: "¿Estas seguro de eliminar la bodega?",
        text: "Este registro no sera recuperado",
        icon: "warning",
        buttons: true
        }).then((borrar)) => {
        if (borrar) {
        $.ajax({
            type: "Post",
            url: url,

            success: function (data)
            {
                if (data.success)
                {
                toastr.success(data.me);
                datatable.ajax.reload();
                }
                else
                {
                toastr.error(data.message)
                }
            }
        });
    }
});