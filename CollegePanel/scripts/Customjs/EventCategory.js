$(document).ready(function () {
    $("#EventName").blur(function () {
        var EventName = $("#EventName").val();
        showloader();
        $.ajax({
            url: '/College/Home/CheckEventCategory',
            type: "POST",
            data: { name: EventName },
            success: function (result) {
                hideloader();
                if (result.Status == true) {
                    $("#EventName").val("");
                    $("#Uerr").html(result.Msg);
                }
                else {
                    $("#Uerr").html("");
                }
                return false;
            },
            error: function (err) {
                hideloader();
                alert("error");
            }
        });
    });
});
function FillGrid(pageIndex, page) {
    debugger;
    showloader();
    $.ajax({
        type: "GET",
        url: "/College/Home/EventCategory",
        contentType: "application/json; charset=utf-8",
        data: { pageIndex: pageIndex, page: page },
        success: function (data) {
            $('#grid').html($(data).find("#grid"));
            hideloader();
        },
        error: function () {
            hideloader();
            alert('Something Went Wrong!!');
        }
    });
    return false;
};
function resetapplication() {
    $("#EventName").val('');
}

function submitapplication() {
    var EventName = $("#EventName").val();
    if (EventName == "") {
        alert('Please Enter Event Name !!');
        $("#EventName").focus();
        return;
    }
    var ID= $("#hid").val();
    var ob = {
        ID:ID,
        EventName: EventName,
    };
    showloader();
    $.ajax({
        url: '/College/Home/AddNewEventCategory/',
        data: JSON.stringify(ob),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            hideloader();
            if (result.Status) {
                alert(result.Msg);
                location.replace('/College/Home/EventCategory');
            }
            else {
                alert(result.Msg);
            }
            return false;
        },
        error: function (err) {
            hideloader();
            alert("Something Went Wrong!!!");
        }
    });
}
var u = window.location.protocol + "//" + window.location.host + "/";
function changeStatus(recType, val, ctrl) {
    // debugger;
    $.ajax({
        type: 'POST',
        url: u + "College/Home/ActiveDeactiveEventCategory", // we are calling json method
        dataType: 'json',
        async: false,
        data: { id: val, type: recType },
        success: function (result) {
            if (result) {
                ctrl.html("<i class='fa fa-check fa-green' title='Active'></i>");
                setTimeout(function () { swal("Success!", "Status changed successfully.", "success"); }, 200);
                //window.location.reload();
            }
            else {

                ctrl.html("<i class='fa fa-close fa-red' title='De-Active'></i>");
                setTimeout(function () { swal("Success!", "Status changed successfully.", "success"); }, 200);
                //window.location.reload();
            }

            ctrl.data('status', result);
            window.location.reload();
        },
        error: function (ex) {
            setTimeout(function () { swal("Error!", "Something went wrong.", "error"); }, 200);
        }
    });
}

$(document).on('click', '.recordStatus', function () {

    // debugger;
    var ctrl = $(this);

    var Tile = ctrl.data('msg');

    if (Tile == undefined) {
        Tile = ctrl.data('status') == true ? "Do you want to Deactive this Event Category" : "Do you want to Active this Event Category";
    }
    else {

        Tile = (ctrl.data('status') == true ? Tile.replace('statusMsg', 'De-Active') : Tile.replace('statusMsg', 'Active'));
    }
    //console.log("Title" + Tile);
    swal({
        title: Tile,
        //text: "Change status",
        type: "warning",
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    },
        function () {
            changeStatus(ctrl.data('type'), ctrl.data('val'), ctrl)
            //var i = deleteRecord(trr.data('type'), trr.data('val'), t, trr);
        });
});
