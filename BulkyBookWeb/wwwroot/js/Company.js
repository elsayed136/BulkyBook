﻿let dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        responsive: true,
        ajax: {
            url: "/Admin/Company/GetAll"
        },
        columns: [
            {data: "name"},
            { data: "streetAddress"},
            { data: "city"},
            { data: "state"},
            { data: "phoneNumber"},
            {
                data: "id", render: function (data) {
                    return `
                        <div class="w-75 btn-group">
                            <a href="/Admin/Company/Upsert?id=${data}" class="btn btn-info mx-2">
                                <i class="bi bi-pencil-square"></i>&nbsp;
                                Edit
                            </a>
                            <a onClick=Delete('/Admin/Company/Delete/${data}') class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>&nbsp;
                                Delete
                            </a>
                        </div>
                    `;
                }
            }
        ]
    })
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}