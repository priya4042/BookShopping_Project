var dataTable;
$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Company/GetAll"
        },
        "columns": [
            { "data": "name", "width": "10%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "10%" },
            { "data": "postalCode", "width": "10%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`;
                    }
                    else {
                        return `<input type= "checkbox" disabled />`;
                    }
                }
            },

            
            {
                "data": "id",
                "render": function (data) {
                    return `
                     <div class="text-center">
                     <a class="btn btn-info" href="/Admin/Company/Upsert/${data}"><i class="fas fa-edit"></i></a>
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
        title: "Will you Want to Delete data!!!",
        text: "Data Deleted successfully^-^",
        buttons: true,
        dangerModel: true,
        icon:"warning"

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