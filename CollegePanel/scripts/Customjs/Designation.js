$(document).ready(function () {
    $("#URole").blur(function () {
        var URole = $("#URole").val();
        showloader();
        $.ajax({
            url: '/College/Home/CheckURoleValue',
            type: "POST",
            data: { name: URole },
            success: function (result) {
                hideloader();
                if (result.Status == true) {
                    $("#URole").val("");
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
        url: "/College/Home/DesignationMaster",
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
    $("#URole").val('');
}

function submitapplication() {  
    var URole = $("#URole").val();  
    if (URole == "") {
        alert('Please Enter Department Name !!');
        $("#URole").focus();
        return;
    }
    var ob = {
        URole: URole,        
    };   
    showloader();
    $.ajax({
        url: '/College/Home/AddNewDesignation/',
        data: JSON.stringify(ob),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            hideloader();
            if (result.Status) {
                alert(result.Msg);
                location.replace('/College/Home/Designation');
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


