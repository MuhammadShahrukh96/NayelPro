
$(document).ready(function () {
    GetAll();
    clear();
    GetAllCategories()

    $("#anchorClose").click(function () {
        clear();
    });

    $("#btnSave").click(function () {
        if ($('#btnSave').hasClass('btn-update')) {
            Update();
        } else {
            Create();
        }

    })
});

$(document).on('click', '.btn-delete', function () {
    var id = $(this).attr('args');
    Delete(id);
});

$(document).on('click', '.btn-edit', function () {
    var id = $(this).attr('args');
    GetById(id);
});


function GetAll() {
    $.ajax({
        type: "GET",
        url: "/product/GetAll",
        dataType: "json",
        success: function (res) {
            drawTable(res);
        },
        error(err) {
            console.log(err);
        }
    });
}

function GetAllCategories() {
    $.ajax({
        type: "GET",
        url: "/category/GetAll",
        dataType: "json",
        success: function (res) {
            if (res) {
                var sel = $("#category");
                sel.empty();
                for (var i = 0; i < res.length; i++) {
                    sel.append('<option value="' + res[i].id + '">' + res[i].name + '</option>');
                }
            }
        },
        error(err) {
            console.log(err);
        }
    });
}

function Create() {
    var data = getAndSetModel();
    data.categoryId = $("#category").val();
    $.ajax({
        type: "POST",
        url: "/product/Create",
        data: {
            product: data
        },
        dataType: "json",
        success: function (res) {
            GetAll();
            $('#productModel').modal('hide'); //hide the modal
            clear();
        }
    });
}

function Update() {
    var data = getAndSetModel();
    data.categoryId = $("#category").val();
    $.ajax({
        type: "POST",
        url: "/product/Update",
        data: {
            product: data
        },
        dataType: "json",
        success: function (res) {
            GetAll();
            $('#productModel').modal('hide'); //hide the modal
            $("#btnSave").removeClass('btn-update');
            clear();
        }
    });
}

function Delete(id) {
    $.ajax({
        type: "DELETE",
        url: "/product/Delete",
        data: { "id": id },
        contentType: 'application/x-www-form-urlencoded',
        dataType: "json",
        success: function (res) {
            GetAll();
        }
    });
}

function GetById(id) {
    $.ajax({
        type: "GET",
        url: "/product/GetById",
        data: { "id": id },
        contentType: 'application/x-www-form-urlencoded',
        dataType: "json",
        success: function (res) {
            $("#id").val(res.id);
            $("#name").val(res.name);
            $("#description").val(res.description);
            $("#category").val(res.categoryId);
            $("#btnSave").addClass("btn-update");
            $('#productModel').modal('show');
        }
    });
}

function drawTable(response) {
    $("#tbodyid").empty();
    $.each(response, function (i, item) {
        var $tr = $('<tr id="' + item.id + '">').append(
            $('<td>').text(item.name),
            $('<td>').text(item.description),
            $("#categoryName").val(item.categoryName),
        $('<td>').html("<a href='#' args='" + item.id + "' class='btn btn-edit btn-sm btn-info btn-circle'><i class='fas fa-pen'></i></a> <a href='#' args='" + item.id + "' class='btn btn-delete btn-sm btn-danger btn-circle'><i class='fas fa-trash'></i></a>")
        ).appendTo('#tbodyid');
    });
}

function getAndSetModel() {
    var data = {};
    var fields = $('form').serializeArray();

    for (var i = 0; i < fields.length; i++) {
        var field = fields[i];

        if (!data.hasOwnProperty(field.name)) {
            data[field.name] = field.value;
        }
        else {
            if (!data[field.name] instanceof Array)
                data[field.name] = [data[field.name]];

            data[field.name].push(field.value);
        }
    }

    return data;
}

function clear() {
    $("#id").val("");
    $("#name").val("");
    $("#category").val();
    $("#name").val("");
}

