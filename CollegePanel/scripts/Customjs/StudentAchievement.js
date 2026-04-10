$(document).ready(function () {
    $("#EducationTypeID").change(function (event) {
        $('#Course').find("option").remove();
        $("#Course").append($("<option></option>").val("").html("--Select Course--"));
        $('#SessionID').find("option").remove();
        $("#SessionID").append($("<option></option>").val("").html("--Select Session--"));
        var res = $(this).val();
        if (res == "") {
            res = 0;
            return;
        }
        $.ajax({
            url: "/College/Home/getcourse/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {

                //console.log(data);
                $('#Course').find("option").remove();
                $("#Course").append($("<option></option>").val("").html("--Select Course--"));
                $.each(data.data, function (key, value) {
                    $("#Course").append($("<option></option>").val(value.CourseCategoryID).html(value.CourseCategory));
                });
            }
        });
    });
    $("#Course").change(function (event) {
//debugger;
        var EducationTypeID = $("#EducationTypeID").val();
        var CourseCategoryID = $("#Course").val();
        $('#SessionID').find("option").remove();
        $("#SessionID").append($("<option></option>").val("").html("--Select Session--"));
        $('#SID').find("option").remove();
        $("#SID").append($("<option></option>").val("").html("--Select Student--"));
        if (EducationTypeID == "") {
            alert('Please Select Programme  !!');
            $('#EducationTypeID').focus();
            return;
        }
        if (CourseCategoryID == "") {
            //alert('Please Select Course  !!');
            $('#CourseCategoryID').focus();
            return;
        }
        $.ajax({
            url: "/College/Home/GetSessionList/",
            data: { EducationTypeID: EducationTypeID, CourseCategoryID: CourseCategoryID },
            //, collegeid: $('#collegeid').val()
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {
               // console.log(data);
                $('#SessionID').find("option").remove();
                $("#SessionID").append($("<option></option>").val("").html("--Select Session--"));
                $.each(data.data, function (key, value) {
                    $("#SessionID").append($("<option></option>").val(value.sessionid).html(value.sessionname));
                });
            }
        });
    });
    $("#SessionID").change(function (event) {
       // debugger;
        var SessionID = $("#SessionID").val();
        var EducationTypeID = $("#EducationTypeID").val();
        var CourseCategoryID = $("#Course").val();
        $('#SID').find("option").remove();
        $("#SID").append($("<option></option>").val("").html("--Select Student--"));
        if (SessionID == "") {
           // alert('Please Select Session  !!');
            $('#SessionID').focus();
            return;
        }
        if (EducationTypeID == "") {
            alert('Please Select Programme  !!');
            $('#EducationTypeID').focus();
            return;
        }
        if (CourseCategoryID == "") {
            alert('Please Select Course  !!');
            $('#CourseCategoryID').focus();
            return;
        }

        $.ajax({

            url: "/College/Home/GetStudentList/",
            data: { SessionID: SessionID, EducationTypeID: EducationTypeID, CourseCategoryID: CourseCategoryID },
            //, collegeid: $('#collegeid').val()
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {
                // console.log(data);
                $('#SID').find("option").remove();
                $("#SID").append($("<option></option>").val("").html("--Select Student--"));
                $.each(data.data, function (key, value) {
                    $("#SID").append($("<option></option>").val(value.ID).html(value.Name));
                });
            }
        });
    });
    $("#SID").change(function (event) {
        var SID = $("#SID").val();
        $('#AchievementMasterID').find("option").remove();
        $("#AchievementMasterID").append($("<option></option>").val("").html("--Select Achievement Category--"));
        if (SID == "") {
           // alert('Please Select Session  !!');
            $('#SID').focus();
            return;
        }       
        $.ajax({

            url: "/College/Home/GetAchievementList/",
            data: { SID: SID },           
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {
                // console.log(data);
                $('#AchievementMasterID').find("option").remove();
                $("#AchievementMasterID").append($("<option></option>").val("").html("--Select  Achievement Category--"));
                $.each(data.data, function (key, value) {
                    $("#AchievementMasterID").append($("<option></option>").val(value.ID).html(value.Name));
                });
            }
        });
    });
});

function resetapplication() {
   $("#SessionID").val("");
    $("#EducationTypeID").val("");
     $("#Course").val("");
    $("#AchievementMasterID").val("");
    $("#SID").val("");
    $("#Description").val("");
    $("#file").val("");   
    $("#append-big-btn").val("");
}

check = function () {
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".pdf"];
    var arrInputs = document.getElementsByTagName("input");
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            var sFileNamesize = document.getElementById('file').files[0].size;
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;
                    }
                }

                if (!blnValid) {
                    oInput.value = "";
                    document.getElementById("append-big-btn").value = "";
                    alert("Sorry, file is invalid ");
                    return false;
                }
                else {
                    if (sFileNamesize > 500000) {
                        oInput.value = "";
                        document.getElementById("append-big-btn").value = "";
                        alert("File is too big!");
                        break;
                    }
                }
            }
            var fileSize = document.getElementById('file').files[0].size;
        }
    }

    return true;
}
function submitapplication() {
    //debugger;
    var SessionID = $("#SessionID").val();
    var EducationTypeID = $("#EducationTypeID").val();
    var CourseCategoryID = $("#Course").val();
    var ID = $("#hid").val();
    var AchievementMasterID = $("#AchievementMasterID").val();
    var SID = $("#SID").val();
    var Description = $("#Description").val();
    var hfile = $("#hfile").val();
    var file1 = $("#file").val();    
    var FileURl;
    var file;
    if ($("#file").val() == null)
    {
        FileURl = $("#hfile").val();
        file = $(".link").attr("href");
    }
    else {
        var str = $("#file").val().split('\\');
        FileURl = str.pop();
        file = $(".link").attr("href");
    }
    if (SessionID == "") {
        alert('Please Select Session  !!');
        $('#SessionID').focus();
        return;
    }
    if (EducationTypeID == "") {
        alert('Please Select Programme  !!');
        $('#EducationTypeID').focus();
        return;
    }
    if (CourseCategoryID == "") {
        alert('Please Select Course  !!');
        $('#CourseCategoryID').focus();
        return;
    }

    //alert(file);
    if (SID == "")
    {      
        alert('Please Select Student  !!');
        $('#SID').focus();
        return;
    }
    if (AchievementMasterID == "") {
        alert('Please Select Achievement Category  !!');
        $('#AchievementMasterID').focus();
        return;
    }
    if (Description == "") {
        alert('Please Enter Description !!');
        $('#Discription').focus();
        return;
    }
    if (ID == "0") {
        if (file1 == "") {
            alert("Please Upload Document!!");
            return;
        }
    }     
    if (window.FormData !== undefined) {

        var fileUpload = $("#file").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();

        if (files.length > 0) {
            for (var i = 0; i < files.length; i++) {
                fileData.append("file", files[i]);
            }
        }
        else {
            //alert($('#hfile').val());
            fileData.append("hfile", $('#hfile').val());

        }
        // debugger;
        fileData.append('ID', ID);
        fileData.append('SessionID', SessionID);
        fileData.append('SID', SID);
        fileData.append('AchievementMasterID', AchievementMasterID);
        fileData.append('Description', Description);
      //  fileData.append('hfile', hfile);
        
        showloader();
        $.ajax({
            url: '/College/Home/AddStudentAchievement',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
                // console.log(result);
                if (result.Status == true) {
                    //debugger;
                    hideloader();
                    alert(result.Msg);
                    if (display == "1")
                    {
                        window.location = '/College/Home/StudentAchievementList';
                    }
                    else if(display == "2")
                    {
                        window.location = '/College/Home/StudentAchievementList';
                    }
                    else {
                        window.location = '/College/Home/StudentAchievement';
                    }
                    //window.location.reload();                 
                    // location.replace('/College/Home/StudentQualification/?id=' + EnID);
                }
                else {
                    alert(result.Msg);
                    hideloader();
                }
               
            },
            error: function (err) {
                // debugger;
                alert(err.statusText);
                hideloader();
                return false;
            }
        });
    }
    else {
        alert("FormData is not supported.");

        return false;
    }


}