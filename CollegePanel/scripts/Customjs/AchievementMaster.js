$(document).ready(function () {
    $("#AchievementName").blur(function () {
        var AchievementName = $("#AchievementName").val();
        showloader();
        $.ajax({
            url: '/College/Home/CheckAchievementValue',
            type: "POST",
            data: { name: AchievementName },
            success: function (result) {

                hideloader();
                if (result.Status == true) {

                    $("#AchievementName").val("");
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
        url: "/College/Home/StudentAchievementMaster",
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
    $("#AchievementName").val('');
}

function submitapplication() {
    var AchievementName = $("#AchievementName").val();
    if (AchievementName == "") {
        alert('Please Enter Achievement Category !!');
        $("#AchievementName").focus();
        return;
    }
    var ob = {
        AchievementName: AchievementName,
    }; 
    showloader();
    $.ajax({
        url: '/College/Home/AddStudentAchievementMaster/',
        data: JSON.stringify(ob),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            hideloader();
            if (result.Status) {
                alert(result.Msg);
                location.replace('/College/Home/StudentAchievementMaster');
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