$(document).ready(function () {
    $("#RoomNo").blur(function () {
        var RoomNo = $("#RoomNo").val();
        showloader();
        $.ajax({
            url: '/College/Home/CheckRoomName',
            type: "POST",
            data: { name: RoomNo },
            success: function (result) {

                hideloader();
                if (result.Status == true) {

                    $("#RoomNo").val("");
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
function resetapplication() {
    $("#RoomNo").val('');
    $("#Description").val('');

}

function submitapplication() {

    debugger;
    var RoomNo = $("#RoomNo").val();
    var EncriptedID = $("#EncriptedID").val();
    var Description = $("#Description").val();
    var ID=$("#ID").val();
    if (RoomNo == "" || RoomNo == "0") {
        alert('Please Enter Room No !!');
        $("#RoomNo").focus();
        return;
    }
    if (Description == "" ) {
        alert('Please Enter Description !!');
        $("#Description").focus();
        return;
    }
    var ob = {
        RoomNo: RoomNo,
        Description:Description,
        EncriptedID: EncriptedID,
        ID:ID
    };
    console.log(ob);
    showloader();
    $.ajax({
        url: '/College/Home/AddNewRoomName/',
        data: JSON.stringify(ob),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            hideloader();

            if (result.Status) {
                alert(result.Msg);
                location.replace('/College/Home/RoomMaster');

            }
            else {
                alert(result.Msg);
            }
            return false;
        },
        error: function (err) {

            hideloader();
            alert("error");

        }
    });
}


