﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "discription", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "price", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                       <div class="text-center">
                       <a class="btn btn-info" href="/Admin/Product/Upsert/${data}"><i class="fas fa-edit"></i></a>
                     <a class="btn btn-danger" onclick=Delete("/Admin/Product/Delete/${data}")><i class="fas fa-trash-alt"></i></a>
                     </div>
                  `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "want to delete data",
        text: "data deleted successfully",
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
                        dataTable.ajax.reload()
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}