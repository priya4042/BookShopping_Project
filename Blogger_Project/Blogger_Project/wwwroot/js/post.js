﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Post/GetAll"
        },
        "columns": [
            { "data": "title", "width": "20%" },
            { "data": "body", "width": "40%" },
            
            {
                "data": "id",
                "render": function (data) {
                    return `
                      <div class="text-center">
                     <a class="btn btn-info" href="/Admin/Post/Upsert/${data}"><i class="fas fa-edit"></i></a>
                      <a class="btn btn-danger" onclick=Delete("/Admin/Post/Delete/${data}")><i class="fas fa-trash-alt"></i></a></div>
                  `;
                }
            }

        ]
    })
}

function Delete(url) {
    swal({
        title: "Want To Delete Data",
        text: "Data Deleted Successfully",
        buttons: true,
        icon: "warning",
        dangerModel: true
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