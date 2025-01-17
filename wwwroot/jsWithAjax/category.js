
$(document).ready(function () {
    GetAll();
    clear();

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
    //var selectedOrg = localStorage.getItem("selectedOrg");
    //$("#organizationId").val(selectedOrg);
    $.ajax({
        type: "GET",
        url: "/category/GetAll",
        dataType: "json",
        success: function (res) {
            drawTable(res);
        },
        error(err) {
            console.log(err);
        }
    });
}

function Create() {
    var data = getAndSetModel();
    $.ajax({
        type: "POST",
        url: "/category/Create",
        data: {
            category: data
        },
        dataType: "json",
        success: function (res) {
            GetAll();
            $('#categoryModel').modal('hide'); //hide the modal
            clear();
        }
    });
}

function Update() {
    var data = getAndSetModel();
    $.ajax({
        type: "POST",
        url: "/category/Update",
        data: {
            category: data
        },
        dataType: "json",
        success: function (res) {
            GetAll();
            $('#categoryModel').modal('hide'); //hide the modal
            $("#btnSave").removeClass('btn-update');
            clear();
        }
    });
}

function Delete(id) {
    $.ajax({
        type: "DELETE",
        url: "/category/Delete",
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
        url: "/category/GetById",
        data: { "id": id },
        contentType: 'application/x-www-form-urlencoded',
        dataType: "json",
        success: function (res) {
            $("#id").val(res.id);
            $("#name").val(res.name);
            $("#description").val(res.description);
            $("#btnSave").addClass("btn-update");
            $('#categoryModel').modal('show');
        }
    });
}

function drawTable(response) {
    $("#tbodyid").empty();
    $.each(response, function (i, item) {
        var $tr = $('<tr id="' + item.id + '">').append(
            $('<td>').text(item.name),
            $('<td>').text(item.description),
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
    $("#description").val("");
}

