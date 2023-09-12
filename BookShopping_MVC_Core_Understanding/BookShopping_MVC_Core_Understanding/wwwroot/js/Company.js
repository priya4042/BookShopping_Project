﻿var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
           

            {

                "data": "isAuthorized",
                "render": function (data) {
                    if (data) {
                        return `
                        <input type="checkbox" disable checked />
                      `;
                    }
                    else {
                        return `
                      <input type="checkbox" disable />
                     `;
                    }
                }
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="text-center">
                       <a class="btn btn-dark" href="/Admin/Company/Upsert/${data}"><i class="fas fa-edit"></i></a>
                      <a class="btn btn-danger" onclick=Delete("/Admin/Company/Delete/${data}")><i class="fas fa-trash-alt"></i></a>
                      </div>
                  `;
                }
            }
            
        ]
    })
}

function Delete(url) {
    swal({
        title: "Want To Delete Data", 
        text: "Data Deleted successfully",
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