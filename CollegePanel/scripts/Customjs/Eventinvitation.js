$(document).ready(function () {
});


function resetapplication() {
    $("#Name").val("");
    $("#EventID").val("");
    $("#MobileNo").val("");
    $("#Email").val("");
    $("#MobileNo").val("");
    $("#Designation").val("");
    
}
function submitapplication() {
  //  debugger;
    var ID = $("#hid").val();  
    var Name = $("#Name").val();
    var EventID = $("#EventID").val();
    var MobileNo = $("#MobileNo").val();
    var Email = $("#Email").val();
    var Designation = $("#Designation").val();
    if (EventID == "") {
        alert('Please Select Event !!');
        $("#OrganiserName").focus();
        return;
    }
    if (Name == "") {
        alert('Please Enter Guest Name !!');
        $("#Name").focus();
        return;
    }
    if (Designation == "") {
        alert('Please Enter Guest Designation !!');
        $("#Designation").focus();
        return;
    }    
    if ($("#Email").val() == "") {
        alert('Please Enter Organiser Email !!');
        $("#Email").focus();
        return;
    }
    else {
        var Email = $("#Email").val();
        var filter = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        if (!filter.test(Email)) {
            alert('Please Enter Valid Email ID !!');
            $("#Email").focus();
            return false;
        }
    }
    if (MobileNo == "") {
        alert('Please Enter Guest MobileNo !!');
        $("#MobileNo").focus();
        return;
    }
    var obj = {
        ID: ID,
        EventID: EventID,
        MobileNo: MobileNo,
        Name: Name,
        Designation: Designation,
        Email: Email

    };
    showloader();
    $.ajax({
        url: "/College/Home/AddNewGuest",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //console.log(result);
            hideloader();
            if (result.Status) {
                showloader();
                alert(result.Msg);
                location.replace('/College/Home/GuestList');
            }
            else {
                hideloader();
                alert(result.Msg);
            }

        },
        error: function (errormessage) {

            hideloader();
            alert(errormessage.responseText);
        }


    });
}