$(document).ready(function () {
    if ($('input[name="IsPaid"]:checked').val() == 'True') {
        $('#isamt').show();
    }
$(function () {
    $('input[name="IsPaid"]').on('click', function () {
        if ($(this).val() == 'True') {
            $('#isamt').show();
        }
        else {
            $('#isamt').hide();
            $('#Amount').val('');
        }
    });
});
});
check = function () {    
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


function resetapplication() {
    $("#EventName").val("");
    $("#Description").val("");
    $("#FromDate").val("");
    $("#ToDate").val("");
    $("#file").val("");
    $("#EventTypeID").val("");
    $("#EventOrganiserID").val("");
    $("#Venue").val("");
    $("#Amount").val("");  
    $("#append-big-btn").val("");
}
function submitapplication() {
   // debugger;
    var ID = $("#hid").val();
    //alert(ID);
    var EventName= $("#EventName").val();
    var Description= $("#Description").val();
    var FromDate= $("#FromDate").val();
    var ToDate = $("#ToDate").val();   
    var IsPaid = $("input[name='IsPaid']:checked").val();    
    var EventTypeID=$("#EventTypeID").val();
    var EventOrganiserID=$("#EventOrganiserID").val();
    var Venue = $("#Venue").val();
    var Amount=$("#Amount").val();
  
     //alert(file);
    if (EventName == "") {
        alert('Please Enter Event Name !!');
        $("#EventName").focus();
        return;
    }
    if (Description == "") {
        alert('Please Enter Description !!');
        $("#Description").focus();
        return;
    }
    if (FromDate == "") {
        alert('Please Enter From Date !!');
        $("#FromDate").focus();
        return;
    }
    if (ToDate == "") {
        alert('Please Enter To Date !!');
        $("#ToDate").focus();
        return;
    }
    if (IsPaid == "") {
        alert('Please Select Event Paid !!');
        $("#ToDate").focus();
        return;
    }
    if (IsPaid == "True") {
        if (Amount == "") {
            alert('Please Enter Amount !!');
            $("#Amount").focus();
            return;
        }
       
    }
    if (EventTypeID == "") {
        alert('Please Select Event Type !!');
        $("#EventTypeID").focus();
        return;
    }
    if (EventOrganiserID == "") {
        alert('Please Select Event Organiser !!');
        $("#EventOrganiserID").focus();
        return;
    }
    if (Venue == "") {
        alert('Please Enter Event Venue !!');
        $("#Venue").focus();
        return;
    }
    
    //if (ID == "0") {
    //    if ($("#file").val() == "") {
    //        alert("Please Upload Document");
    //        return;
    //    }
    //}   
    showloader();
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

        // debugger;
        fileData.append('ID', ID);
        fileData.append('EventName', EventName);
        fileData.append('Description', Description);
        fileData.append('FromDate', FromDate);
        fileData.append('ToDate', ToDate);
        fileData.append('IsPaid', IsPaid);
        fileData.append('EventTypeID', EventTypeID);
        fileData.append('EventOrganiserID', EventOrganiserID);
        fileData.append('Venue', Venue);
        fileData.append('Amount', Amount);      
        showloader();
        $.ajax({
            url: '/College/Home/AddNewEvent',
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
                    location.replace('/College/Home/EventList');
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