﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
})
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Category/GetAll"
        },
        "columns": [
            { "data": "name", "width": "70%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                  <div class="text-center">
                    <a class="btn btn-info" href="/Admin/Category/Upsert/${data}"><i class="fas fa-edit"></i></a>
                    <a class="btn btn-danger" onclick=Delete("/Admin/Category/Delete/${data}")><i class="fas fa-trash-alt"></i></a></div>
                   `;
                }
            }
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want To Delete The Data",
        text: "Delete information!!!",
        buttons: true,
        icon: "warning",
        dangerModel: true
    }).then((Willdelete) => {
        if (Willdelete) {
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