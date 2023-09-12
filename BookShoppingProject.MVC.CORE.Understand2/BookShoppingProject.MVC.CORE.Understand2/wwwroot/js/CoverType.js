﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/CoverType/GetAll"
        },
        "columns": [
            { "data": "name", "width": "60%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="text-center">
                      <a class="btn btn-success" href="/Admin/CoverType/Upsert/${data}"><i class="fas fa-edit"></i></a>
                      <a class="btn btn-danger" onclick=Delete("/Admin/CoverType/Delete/${data}")><i class="fas fa-trash-alt"></i></a>
                     </div>

                 `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want to delete data",
        text: "Data deleted successfully",
        buttons: true,
        dangerModel: true,
        icon:"warning"
    }).then((willdelete) => {
        if (willdelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}