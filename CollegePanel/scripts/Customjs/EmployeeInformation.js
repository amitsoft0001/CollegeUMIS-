checkmed = function () {
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementById("file");
    var oInput = arrInputs;
    var sFileName = oInput.value;
    var sFileNamesize = document.getElementById('file').files[0].size;;
    if (sFileName.length > 0) {
        var blnValid = false;
        for (var j = 0; j < _validFileExtensions.length; j++) {
            var sCurExtension = _validFileExtensions[j];
            if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                blnValid = true;
                // break;
            }

        }
        if (!blnValid) {
            oInput.value = "";
            document.getElementById('append-big-btn').value = "";
            alert("Sorry, file is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
            return false;
        }
        else {
            if (sFileNamesize > 51200) {
                oInput.value = "";
                document.getElementById('append-big-btn').value = "";
                alert("File is too big!");
                return false;
            }
        }
    }
    //var fileSize = document.getElementById('file').files[0].size;
    return true;
}
checkchar = function () {
    // debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementById("file1");
    var oInput = arrInputs;
    var sFileName = oInput.value;
    var sFileNamesize = document.getElementById('file1').files[0].size;;
    if (sFileName.length > 0) {
        var blnValid = false;
        for (var j = 0; j < _validFileExtensions.length; j++) {
            var sCurExtension = _validFileExtensions[j];
            if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                blnValid = true;
                // break;
            }

        }
        if (!blnValid) {
            oInput.value = "";
            document.getElementById('append-big-btn1').value = "";
            alert("Sorry, file is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
            return false;
        }
        else {
            if (sFileNamesize > 51200) {
                oInput.value = "";
                document.getElementById('append-big-btn1').value = "";
                alert("File is too big!");
                return false;
            }
        }
    }
    //var fileSize = document.getElementById('file').files[0].size;
    return true;
}
function resetapplication() {
    $("#MedicalReport").val("");
    $("#CharacterCertificate").val("");
    $("#file").val("");
    $("#append-big-btn").val("");
    $("#file1").val("");
    $("#append-big-btn1").val("");
    $("#GPFNo").val("");
    $("#GPFRemarks").val("");
    $("#IsConstitution").val("");
    $("#IsSecrecy").val("");
    $("#IsMarried").val("");
    $(".chk").val("");
    $(".Percentage").val("");
    
}
function submitapplication() {
    debugger;
    var ID = $("#hid").val();
    //alert(ID);
    var MedicalReport = $("#MedicalReport").val();
    var CharacterCertificate = $("#CharacterCertificate").val();
    var GPFNo = $("#GPFNo").val();
    var GPFRemarks = $("#GPFRemarks").val();
    var IsConstitution = $("input[name='IsConstitution']:checked").val();
    var IsSecrecy = $("input[name='IsSecrecy']:checked").val();
    var IsMarried = $("input[name='IsMarried']:checked").val(); 
    var chekboxklist = "";
    var PercentageList = "";
    var valueschk = $('input[name="chk"]').map(function () {
        return this.value
    }).get();
    if (valueschk.length == 0)
    {
        alert('Please Enter Your Nominee Details First !!!!');
        return;
    }
    $.each($("input[name='chk']:checked"), function () {
         chekboxklist += $(this).val() + ",";
       // chekboxklist += "," + $(this).val();
    });
    $.each($("input[name='Percentage']"), function () {
        if ($(this).val() != "") {
            PercentageList += $(this).val() + ",";
        }      
       // PercentageList += "," + $(this).val();
    });
    var finalpercentage = PercentageList.split(',');
    var finalchekboxklist = chekboxklist.split(',');
    if (chekboxklist=="") {
            alert('Please select atleast one Nominee!!!!');
    return;
}
       
    //if (MedicalReport == "") {
    //    alert('Please Enter Medical Detail !!');
    //    $("#MedicalReport").focus();
    //    return;
    //}
    //if (ID == "0") {
    //    if ($("#file").val() == "") {
    //        alert("Please Upload Medical Report Document");
    //        $("#file").focus();
    //        return;
    //    }
    //}
    //if (CharacterCertificate == "") {
    //    alert('Please Enter Character Certificate Detail !!');
    //    $("#CharacterCertificate").focus();
    //    return;
    //}
    //if (ID == "0") {
    //    if ($("#file1").val() == "") {
    //        alert("Please Upload Character Certificate Document");
    //        $("#file1").focus();
    //        return;
    //    }
    //}
    //if (GPFNo == "") {
    //    alert('Please Enter GPF No !!');
    //    $("#GPFNo").focus();
    //    return;
    //}
    //if (GPFRemarks == "") {
    //    alert('Please Enter GPF Remarks !!');
    //    $("#GPFRemarks").focus();
    //    return;
    //}
    if (IsConstitution == "") {
        alert('Please Select Allegiance To The Constitution !!');
        $("#IsConstitution").focus();
        return;
    } 
    if (IsSecrecy == "") {
        alert('Please Select Oath of Secrecy !!');
        $("#IsSecrecy").focus();
        return;
    }
    if (IsMarried == "") {
        alert('Please Select Marital Status !!');
        $("#IsMarried").focus();
        return;
    }
    var calpercenteage = 0;
    if (finalchekboxklist.length > 0) {
        for (var i = 0; i < finalchekboxklist.length - 1; i++) {

            if (finalchekboxklist[i] == "") {
                alert('Please select Nominee!!!!');
                return;
            }
            if (finalpercentage.length > 0) {

                if (finalpercentage[i] == "" || finalpercentage[i] == "0") {
                    alert('Please Enter Nominee Percentage For Selected Nominee!!!!');
                    return;
                }
                else
                {
                    calpercenteage += parseFloat(finalpercentage[i]);
                }

            }
            else {
                alert('Please Enter atleast one Nominee percentage!!!!');
                return;
            }
        }
        }
       

        
    
    //console.log(calpercenteage);
    if (calpercenteage > 100.00)
    {
        alert("Please Enter less Then 100 Nominee Percentage(Total)!!");
        $(".Percentage").val('');
        return;
    }
    if (calpercenteage < 100.00) {
        alert("Nominee Percentage(Total) Should Not Less than 100% !!");
        $(".Percentage").val('');
        return;
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
            fileData.append("hfile", $('#hfile').val());
        }
        var fileUpload1 = $("#file1").get(0);
        var files1 = fileUpload1.files;      
        if (files1.length > 0) {
            for (var i = 0; i < files1.length; i++) {
                fileData.append("file1", files1[i]);
            }
        }
        else {
            fileData.append("chfile", $('#chfile').val());
        }
        // debugger;
        fileData.append('ID', ID);
        fileData.append('MedicalReport', MedicalReport);
        fileData.append('CharacterCertificate', CharacterCertificate);
        fileData.append('GPFNo', GPFNo);
        fileData.append('GPFRemarks', GPFRemarks);
        fileData.append('IsConstitution', IsConstitution);
        fileData.append('IsSecrecy', IsSecrecy);
        fileData.append('IsMarried', IsMarried);
        fileData.append('Nominnee', chekboxklist);
        fileData.append('NominneePercentage', PercentageList);
        
     
        showloader();
        $.ajax({
            url: '/College/Home/AddEmployeeInformation',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
                // console.log(result);
                if (result.Status == true) {

                    hideloader();
                    alert(result.Msg);
                    //window.location.reload();
                    //window.location = '/College/Home/PreviousYearQualificationManualAd/?id=' + EnID;
                    location.replace('/College/Home/EmployeeInformationDetail');
                }
                else {

                    alert(result.Msg);
                    hideloader();
                }
                //return false;
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
$(document).ready(function () {
    //showloader();   
    var valueschk = $('input[name="chk"]').map(function () {
        return this.value
    }).get();
    var pertxt = $("input[name='Percentage']");
    st = valueschk;
    var nomineeID = $('#nomineeID').val();
    var nomineeper = $('#nomineeper').val();
    var nomineeIDlist = "";
    var nomineeperList = "";
    if (nomineeID != "") {
        nomineeIDlist = nomineeID.split(',');
        nomineeperList = nomineeper.split(',');

        $.each(nomineeIDlist, function (index, value) {  
            if ($.inArray(value.toString(), st) !== -1) {
                ch = true;
                $('#' + value + '').prop('checked', true);
            }
            else {
                ch = false;
            }

        });
        //$.each(nomineeperList, function (index, value) {
        //    pertxt[index].val(Value);

        //});Percentage_0
        var pid = "";
        for (var i = 0; i < nomineeperList.length; i++) {          
            pid = "Percentage_" + i;
            $('#'+pid+'').val(nomineeperList[i]);
        }
    }
  
  
});
