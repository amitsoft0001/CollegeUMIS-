
$(document).ready(function () {
    //DataTable
    debugger;
    var view = false;

    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm) {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: inline;");
        $('#Percentage').attr('readonly', true);

        $('#Perr').html('Please fill subject detail ,Aggregate Percentage will automatic Calculate');

    }
    else {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: none;");
       $('#Percentage').removeAttr("readonly");
        $('#Perr').html('');
    }
    $('.subper').change(function () {
        //$(".subper").each(function () {
        debugger;
        // alert($(this).val());
        if ($(this).val() == "0" || $(this).val() == "") {
            alert('Please Enter Subject Percentage!!!');
            $(this).val('');
            $(this).focus();
            return;
        }
        else if (parseInt($(this).val()) > 99) {
            alert('Please Enter Subject Percentage less then 99 !!!');
            $(this).val('');
            $(this).focus();
            return;
        }
        //});

    });


    $('.MarksObtain').change(function () {
        //$(".subper").each(function () {
        debugger;
        var tr = $(this).closest("tr");
        var tm = tr.find("#TotalMarks").val();
        if ($(this).val() == "0" || $(this).val() == "") {
            alert('Please Enter obtained Marks !!!');
            tr.find("#SubjectPercentage").val('');
            $(this).val('');
            $(this).focus();
            return;
        }
        else if (parseInt($(this).val()) > tm) {
            alert('Obtained Marks should be less then Total marks !!!');
            $(this).val('');
            tr.find("#SubjectPercentage").val('');
            $(this).focus();
            return;
        }
        else {
            var per = (parseInt($(this).val()) * 100) / tm;
            var percentage = per.toPrecision(4);
            tr.find("#SubjectPercentage").val(percentage);
        }
        //});
        var subper = $('input[name="SubjectPercentage"]').map(function () {
            return this.value
        }).get();
        var TotalMarks1 = $('input[name="TotalMarks"]').map(function () {
            return this.value
        }).get();
        var MarksObtain1 = $('input[name="MarksObtain"]').map(function () {
            return this.value
        }).get();
        if (subper.length == 5) {
            debugger;
            var aggper = 0.0;
            var totalm = 0.0;
            for (var i = 0; i < TotalMarks1.length; i++)
            {
                aggper = aggper + parseFloat(MarksObtain1[i]);
                totalm = totalm + parseFloat(TotalMarks1[i]);
            }
            //alert(aggper);
            var final = aggper * 100 / totalm;
            //$('#Percentage').val(final);
            $('#Percentage').val(final.toPrecision(4));
        }

    });

    $('#Qualification').change(function () {
        debugger;
        var res = $('#Qualification').val();
        if (res == "")
            res = 0;
        var EnID = $('#EnID').val();
        $.ajax({
            url: "/College/Home/SubjectTable/",
            data: { id: res, sid: EnID },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {

                console.log(result);
                if (result) {
                    //alert(result);
                    view = true;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: inline;");
                    $('#Percentage').attr('readonly', true);
                    $('#Perr').html('Please fill subject detail ,Aggregate  Percentage will automatic Calculate');
                }
                else {
                    view = false;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: none;");
                    $('#Percentage').removeAttr("readonly");
                    $('#Perr').html(' ');
                }

            }
        });

    });
    $('.Subject').change(function () {

        debugger;
        var id = $('#Qualification').val();
        var res = $(this).val();
        var tr = $(this).closest("tr");
        var str = '';
        var SubjectID = $('select[name="SubjectID"]').map(function () {

            return this.value
        }).get();
        for (var i = 0; i < SubjectID.length; i++) {
            if (SubjectID[i] != "") {
                str += SubjectID[i] + ',';
            }

        }
       
        showloader();
        $.ajax({
            url: "/College/Home/Subject_bindDanamic/",
            data: { id: id, res: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
                hideloader();
                debugger;

                var str = '';
                str += '<option value=' + "" + '>' + "--Select Subject--" + '</option>';
                for (var i = 0; i < result.length; i++) {

                    str += '<option value=' + result[i].ID + '>' + result[i].SubjectName + '</option>';
                    //tr.next('tr').find('.Subject ').html($("<option     />").val(result[i].ID).text(result[i].SubjectName));

                }
                tr.next('tr').find('.Subject ').html(str);



            }
        });

    });
    $('#PassingYear').change(function () {
        //debugger;
        var res = $('#PassingYear').val();
        if (res == "")
            return;
        var EnID = $('#EnID').val();
        $.ajax({
            url: "/College/Home/checkpassingyear/",
            data: { year: res, sid: EnID },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {

                console.log(result);
                if (result.Status) {
                    $('#PassingYear').val('');
                    alert(result.Msg);

                }
                else {

                }

            }
            ,
            error: function (errormessage) {
                hideloader();
                alert(errormessage.responseText);
            }
        });

    });
});


function percheck() {
    if (parseInt($("#Percentage").val()) > 99) {
        $("#Percentage").val("");
        alert('Percentage should be less then 100 ');
        // $("#btn").attr("style", "display: none;");
        return;
    }
    else {
        $('#btn').removeAttr("style");
        $("#btn").attr("style", "display: inline;");
    }
    if (parseInt($("#Percentage").val()) <= 0) {
        $("#Percentage").val("");
        alert('Percentage should be greater then 0 ');
        // $("#btn").attr("style", "display: none;");
        return;
    }
    else {
        //$('#btn').removeAttr("style");
        //$("#btn").attr("style", "display: inline;");
    }
   
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
    $("#file").val("");
    $("#RollNo").val("");
    $("#append-big-btn").val("");
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
    debugger;
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
        alert('Please select Qualification  !!');
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
        alert('Please Enter Board/University Name  !!');
        return;
    }
    if (RollNo == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        alert('Please Enter  Roll Number  !!');
        return;
    }
    if ($('#Qualification').val() != art || $('#Qualification').val() != sci || $('#Qualification').val() != comm) {
        debugger;
        if (Percentage == "0" || Percentage == "") {

            //$('#Perr').html("Please Enter percentage  !!");
            alert('Please Enter Percentage  !!');
            return;
        }
    }

    if (PassingYear == "") {

        // $('#Yerr').html("Please select a Passing year  !!");
        alert('Please select  Passing Year  !!');
        return;
    }


   
    //console.log(obj);
    var subper = $('input[name="SubjectPercentage"]').map(function () {
        return this.value
    }).get();
    var SubjectID = $('select[name="SubjectID"]').map(function () {

        return this.value
    }).get();
    var SubID = $('input[name="ID"]').map(function () {

        return this.value
    }).get();
    var TotalMarks = $('input[name="TotalMarks"]').map(function () {

        return this.value
    }).get();
    var MarksObtain = $('input[name="MarksObtain"]').map(function () {

        return this.value
    }).get();
    var no = $('input[name="SubjectPercentage"]');
    var listObj = [];
    var view;
    debugger;


    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm) {
        view = true;
        var str1 = '';
        for (var i = 0; i < SubjectID.length; i++) {
            var j = i + 1;
            if (j == 1)
                str1 = "First";
            else if (j == 2)
                str1 = "Second";
            else if (j == 3)
                str1 = "Third";
            else if (j == 4)
                str1 = "Fourth";
            else if (j == 5)
                str1 = "Fifth";
            if (SubjectID[i] == "") {
                alert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                return;
            }
            if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
                alert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
                return;
            }
            if (MarksObtain[i] == "" || MarksObtain[i] == "0") {
                alert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
                return;
            }

            //if (subper[i] == "0" || subper[i] == "") {
            //    alert('Please Enter Subject Percentage!!!');
            //    no[i].val('');
            //    return;
            //}
            //else if (parseInt(subper[i]) > 99) {
            //    alert('Please Enter Subject Percentage less then 99 !!!');
            //    no[i].val('');
            //    return;
            //}
        }
    }
    res = 0;


    showloader();

    var EnID = $('#EnID').val();
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

        debugger;
        fileData.append('ID', ID);
        fileData.append('Qualification', Qualification);
        fileData.append('UniversityName', UniversityName);
        fileData.append('Percentage', Percentage);
        fileData.append('PassingYear', PassingYear);
        fileData.append('RollNo', RollNo);

        fileData.append('EncriptedID', ID);
        fileData.append('sublist', SubjectID);
        fileData.append('subper', subper);
        fileData.append('TotalMarks', TotalMarks);
        fileData.append('MarksObtain', MarksObtain);
        fileData.append('SubID', SubID);
        fileData.append('EnID', EnID);
        showloader();
        $.ajax({
            url: '/College/Home/AddNewQualification',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
                //debugger;
                // console.log(result);
                if (result.Status == true) {
                    debugger;
                    hideloader();
                    alert(result.Msg);
                   // window.location.reload();
                    window.location = '/College/Home/PreviousYearQualificationManualAd/?id=' + EnID;
                    //location.replace('/College/Home/PreviousYearQualificationManualAd/');
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

