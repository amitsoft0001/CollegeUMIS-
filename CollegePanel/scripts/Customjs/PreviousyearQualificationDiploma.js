
$(document).ready(function () {
    //DataTable
    //debugger;
    var view = false;
   
    if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm)
    {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: inline;");
        //$('#Percentage').attr('readonly', true);
        
        $('#Perr').html('Please fill subject detail ,Aggregate Percentage will automatic Calculate');

    }
    else {
        $('#SubjectTable').removeAttr("style");
        $("#SubjectTable").attr("style", "display: none;");
        //$('#Percentage').removeAttr("readonly");
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
       var tm= tr.find("#TotalMarks").val();
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
        var subper = $('input[name="MarksObtain"]').map(function () {
            return this.value
        }).get();
        var TotalMarks = $('input[name="TotalMarks"]').map(function () {
            return this.value
        }).get();
        var count = 0;
        var aggper = 0.0;
        var totalm = 0;
        var Subject = "";
        var SubjectID = $('select[name="SubjectID"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < SubjectID.length; i++) {
            if (SubjectID[i] != "") {
                Subject += SubjectID[i] + ',';
            }

        }
        if ($('#SubjectLLID').val() != "") {
            Subject = Subject  + $('#SubjectLLID').val();

        }
        if ($('#SubjectNRBID').val() != "") {
            Subject = Subject + ',' + $('#SubjectNRBID').val();

        }
        if ($('#SubjectNBID').val() != "") {
            Subject = Subject + ',' + $('#SubjectNBID').val();

        }
        if ($('#SubjectExtraID').val() != "") {
            Subject = Subject + ',' + $('#SubjectExtraID').val();

        }
        var sublist = Subject.split(',');
        var Bper = 0.0;
        var Mper = 0.0;
        for (var k = 0; k < sublist.length; k++)
        {
            if (sublist[k] == 12)
            {
                 Mper = subper[k];
            }
            if (sublist[k] == 33) {
                 Bper = subper[k];
            }
        }
        var Cper = 0;
        if (Bper > Mper)
        {
            Cper = 12;
        }
        else {
            Cper = 33;
        }

   
        if ($('#SubjectExtraID').val() != "") {
            var subj = $('#SubjectExtraID').val();

        }
        if ($('#Qualification').val() == sci)
        {

            if ($('#SubjectExtraID').val() != undefined) {
                if ($('#SubjectExtraID').val() != "") {

                    for (var i = 0; i < subper.length; i++) {

                        if (subper[i] != "") {
                            if (sublist[i] != Cper) {
                                aggper = aggper + parseFloat(subper[i]);
                                totalm = totalm + parseInt(TotalMarks[i]);
                                count++;
                            }
                            //debugger;


                            //alert(aggper);

                        }
                    }
                }
                else {

                    for (var i = 0; i < 6; i++) {
                        if (subper[i] != "") {
                            //debugger;
                            count++;
                            aggper = aggper + parseFloat(subper[i]);
                            totalm = totalm + parseInt(TotalMarks[i]);
                            //alert(aggper);

                        }
                    }
                }
            }
            else {

                for (var i = 0; i < 6; i++) {
                    if (subper[i] != "") {
                        //debugger;
                        count++;
                        aggper = aggper + parseFloat(subper[i]);
                        totalm = totalm + parseInt(TotalMarks[i]);
                        //alert(aggper);

                    }
                }
            }

           



        }
        else
        {
            for (var i = 0; i < 6; i++) {
                if (subper[i] != "") {
                    //debugger;
                    count++;
                    aggper = aggper + parseFloat(subper[i]);
                    totalm = totalm + parseInt(TotalMarks[i]);
                    //alert(aggper);

                }
            }
        }
       
        var final = aggper*100 / totalm;
        $('#Percentage').val(final.toPrecision(4));
       
      

    });
    
   $('#Qualification').change(function () {        
        //debugger;
        var res = $('#Qualification').val();
        if (res == "")
            res = 0;
        $.ajax({
            url: "/Student/Home/SubjectTable/",
            data: { id: res },
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
                  //  $('#Percentage').attr('readonly', true);
                    $('#Perr').html('Please fill subject detail ,Aggregate  Percentage will automatic Calculate');
                }
                else {
                    view = false;
                    $('#SubjectTable').removeAttr("style");
                    $("#SubjectTable").attr("style", "display: none;");
                   // $('#Percentage').removeAttr("readonly");
                    $('#Perr').html(' ');
                }
               
            }
        });

    }); 
    $('.Subject').change(function () {
       
       
        var id = $('#Qualification').val();
        var res = $(this).val();
        //alert(res);
        var alltr = $(this).closest('tr').nextAll('tr').find('.Subject').html('<option value=' + "" + '>' + "--Select Subject--" + '</option>');
      
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
        //alert(str);
        //tr.find('.Subject').find("option").remove();
        //tr.find('.Subject').append($("<option></option>").val("").html("Select Subject"));
        //tr.find('#compulsory1 ').find("option").remove();
        //tr.find('#compulsory1 ').append($("<option></option>").val("").html("--Select compulsory1--"));
        showloader();
      
        $.ajax({
            url: "/Student/Home/Subject_bindDanamic/",
            data: { id: id, res: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
               // hideloader();
                //debugger;
           
                    var str = '';
                    str += '<option value=' + "" + '>' + "--Select Subject--" + '</option>';
                for (var i = 0; i < result.length; i++) {

                    str += '<option value=' + result[i].ID + '>' + result[i].SubjectName + '</option>';
                    //tr.next('tr').find('.Subject ').html($("<option     />").val(result[i].ID).text(result[i].SubjectName));
                   
                    }
                tr.next('tr').find('.Subject ').html(str);
                   
                hideloader();

            },
            error: function (errormessage) {
                hideloader();
                alert(errormessage.responseText);
            }
            
        });
        
    });
    $('.Subject').change(function () {

        //debugger;
        var id = $('#Qualification').val();
        var res = $(this).val();
        
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
            url: "/Student/Home/Subject_bindDanamic/",
            data: { id: id, res: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
               
                //debugger;

                var str = '';
                str += '<option value=' + "" + '>' + "--Select Extra Subject--" + '</option>';
                for (var i = 0; i < result.length; i++) {
                    str += '<option value=' + result[i].ID + '>' + result[i].SubjectName + '</option>';                   

                }
                $('#SubjectExtraID ').html(str);
                hideloader();

            },
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
      
        return;
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
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png",".pdf"];
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
   debugger;
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
        //debugger;
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
    
    
    var obj = {
        ID: ID,
        QualicationType: Qualification,
        Board_UniversityName: UniversityName,
        Percentage: Percentage,
        PassingYear: PassingYear,
        FileURl: FileURl,
        file: file,
        hfile: hfile,
        RollNo: RollNo,
        EncriptedID:ID
    };
    //console.log(obj);
    if ($('#Qualification').val() == 2 || $('#Qualification').val() == 3 || $('#Qualification').val() == 4)
    {
        var subper = $('input[name="SubjectPercentage"]').map(function () {
            return this.value
        }).get();
        var SubjectID = "";
        var SubjectID11 = $('select[name="SubjectID"]').map(function () {

            return this.value
        }).get();
        for (var i = 0; i < SubjectID11.length; i++)
        {
            var j = i + 1;
            if (j == 1)
                str1 = "First";
            else if (j == 2)
                str1 = "Second";
            else if (j == 3)
                str1 = "Third";
           
            if (SubjectID11[i] != "")
            {
                SubjectID += SubjectID11[i] + ',';
            }
            else
            {
                
                    alert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                    return;
                
            }


        }
        if ($('#SubjectLLID').val() != "")
        {
            SubjectID = SubjectID + $('#SubjectLLID').val()+",";

        }
        else {
            alert('Please select  LL Subject !!');
            return;
        }
        if ($('#SubjectNRBID').val() != "") {
            SubjectID = SubjectID  + $('#SubjectNRBID').val()+ ',';

        }
        else {
            alert('Please select  NRB Subject !!');
            return;
        }
        if ($('#SubjectNBID').val() != "") {
            SubjectID = SubjectID  + $('#SubjectNBID').val();

        }
        else
        {
            alert('Please select  MB Subject !!');
            return;
        }
        if ($('#SubjectExtraID').val() != undefined)
        {
            if ($('#SubjectExtraID').val() != "")
            {
                SubjectID = SubjectID+',' + $('#SubjectExtraID').val() ;
            }
        }



        //var SubID = $('input[name="ID"]').map(function () {

        //    return this.value
        //}).get();
        //var TotalMarks = $('input[name="TotalMarks"]').map(function () {

        //    return this.value
        //}).get();
        //var MarksObtain = $('input[name="MarksObtain"]').map(function () {

        //    return this.value
        //}).get();
        //var no = $('input[name="SubjectPercentage"]');
        var listObj = [];
        var view;
        debugger;

        var sublist = SubjectID.split(',');
        if ($('#Qualification').val() == art || $('#Qualification').val() == sci || $('#Qualification').val() == comm)
        {
            view = true;
            var str1 = '';
          
            if ($('#SubjectExtraID').val() != undefined )
            {
                if ($('#SubjectExtraID').val() != "")
                {
                    for (var i = 0; i < sublist.length ; i++)
                    {
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
                        else if (j == 6)
                            str1 = "Sixth";
                        else if (j == 7)
                            str1 = "Seventh";
                        if (SubjectID11[i] == "")
                        {
                            alert('Please Choose ' + str1 + ' Subject For Intermediate   !!!');
                            return;
                        }
                        if (TotalMarks[i] == "" || TotalMarks[i] == "0") {
                            alert('Please Enter Total marks for ' + str1 + ' Subject   !!!');
                            return;
                        }
                        if (MarksObtain[i] == "" || MarksObtain[i] == "0")
                        {
                            alert('Please Enter marks obtain for ' + str1 + ' Subject    !!!');
                            return;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < sublist.length ; i++)
                    {
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
                        else if (j == 6)
                            str1 = "Sixth";

                        if (SubjectID11[i] == "") {
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
                    }
                }
            }
            else
            {
                for (var i = 0; i < sublist.length ; i++) {
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
                    else if (j == 6)
                        str1 = "Sixth";
                    
                    if (SubjectID11[i] == "") {
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
                }
            }


        }
    }
        res = 0;
  
   
    showloader();

    $.ajax({
        url: "/Student/Home/AddNewQualification",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //console.log(result);
            hideloader();
            if (result.Status) {
                if (view)
                {
                    
                        showloader();
                        location.replace('/Student/Home/PreviousyearQualification');
                    debugger;
                   
                }
                else {
                    alert(result.Msg);
                    hideloader();
                    location.replace('/Student/Home/PreviousyearQualification');
                }
            }
            else {
                hideloader();
                alert(result.Msg);
               // location.replace('/Student/Home/StudentQualification');
            }
            //location.replace('/Student/Home/PreviousyearQualification');
            // window.location.reload();

        },
        error: function (errormessage) {
            
            hideloader();
            alert(errormessage.responseText);
        }


    });
    
   
}

