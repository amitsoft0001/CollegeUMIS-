
﻿function percheck() {
    if (parseInt($("#Percentage").val()) > 100) {
        $("#Percentage").val("");
        alert('Percentage should be less then 100 ');
        $("#btn").attr("style", "display: none;");
        return;
    }
    if (parseInt($("#Percentage").val()) <= 0) {
        $("#Percentage").val("");
        alert('Percentage should be greater then 0 ');
        $("#btn").attr("style", "display: none;");
        return;
     }
     debugger;
     var Qualification = $("#Qualification").val();
     var per = parseFloat($("#Percentage").val());
    // alert(per);
     //$('#btn').attr('display', 'none');
    
     $.ajax({
         type: "POST",
         url: "/Student/Home/Checkfee",
         data: { qual: Qualification },
       
         success: function (response) {
            
             console.log(response);

             if (per < parseFloat(response.Percentage)) {
                
                 $("#Percentage").val('');
                 $("#btn").attr("style", "display: none;");
                 alert('you are not eligiable for this course ,you percentage should be more than ' + response.Percentage);
                 
                // window.location.href = "/Student/Home/FeesSubmit";
             }
             else {
                 //$('#btn').removeAttr("style");
                 $("#btn").attr("style", "display: inline;");
                 //window.location.href = "/Student/Home/FeesSubmit";
             }
         },
         Error: function (response) {

             $('#btn').attr('display', 'none');
             alert("coming in error");
         }
     });
}
function Onlynumericvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9.]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}
function OnlyIntvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}


function resetapplication() {
    $("#Qualification").val("");
    $("#UniversityName").val("");
    $("#Percentage").val("");
    $("#PassingYear").val("");
}
function encodeImagetoBase64(element) {
    //debugger;
    var file = element.files[0];

    var reader = new FileReader();

    reader.onloadend = function () {

        $(".link").attr("href", reader.result);

        $(".link").text(reader.result);

    }

    reader.readAsDataURL(file);

}
check = function () {
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementsByTagName("input");
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
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
    var ID = $("#hid").val();
    //alert(ID);
    var Qualification = $("#Qualification").val();
    var UniversityName = $("#UniversityName").val();
    var Percentage = $("#Percentage").val();
    var PassingYear = $("#PassingYear").val();
    var hfile = $("#hfile").val();
    var file1 = $("#file").val();
    var RollNo = $("#RollNo").val();
    var FileURl;
    var file;
    if ($("#file").val() == null) {
     
         FileURl = $("#hfile").val();

         file = $(".link").attr("href");
    }
    else {
        var str = $("#file").val().split('\\');
         FileURl = str.pop();
         file = $(".link").attr("href");
    }


    //alert(file);
    if (Qualification == "") {

        //$('#Berr').html("Please select a Qualification  !!");
        alert('Please select a Qualification  !!');
        return;
    }
    if (ID == "0") {
        if (file1 == "") {
            alert("Please Upload Document");
            return;
        }
    }
    if (UniversityName == "") {

        //$('#Uerr').html("Please Enter University Name  !!");
        alert('Please Enter a University Name  !!');
        return;
    }
    if (Percentage == "0") {

        //$('#Perr').html("Please Enter percentage  !!");
        alert('Please Enter a Percentage  !!');
        return;
    }
    if (PassingYear == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        alert('Please select a PassingYear  !!');
        return;
    }
    if (RollNo == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        alert('Please Enter a RollNo  !!');
        return;
    }
    
    var obj = {
        ID: ID,
        QualicationType: Qualification,
        Board_UniversityName: UniversityName,
        Percentage: Percentage,
        PassingYear: PassingYear,
        FileURl: FileURl,
        file: file,
        hfile: hfile,
        RollNo: RollNo
    };
    //console.log(obj);
    $.ajax({
        url: "/Student/Home/QualificationSave",
        data: JSON.stringify(obj),      
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(result);
          
            alert(result);
            location.replace('/Student/Home/StudentQualification');
            // window.location.reload();

        },
        error: function (errormessage) {
           alert(result);
            debugger;
            alert(errormessage.responseText);
        }


    });
}
