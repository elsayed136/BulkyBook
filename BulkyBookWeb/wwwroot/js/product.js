let dataTable;

$(document).ready(function () {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#tblData").DataTable({
        responsive: true,
        ajax: {
            "url": "/Admin/Product/GetAll"
        },
        columns: [
            { "data": "title" },
            { "data": "isbn" },
            { "data": "price" },
            { "data": "author" },
            { "data": "category.name" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group">
                            <a href="/Admin/Product/Upsert?id=${data}" class="btn btn-info mx-2">
                                <i class="bi bi-pencil-square"></i>&nbsp;
                                Edit
                            </a>
                            <a href="/Admin/Product/Delete?id=${data}" class="btn btn-danger mx-2">
                                <i class="bi bi-trash-fill"></i>&nbsp;
                                Delete
                            </a>
                        </div>
                        `
                }
            },
        ],
    });
}