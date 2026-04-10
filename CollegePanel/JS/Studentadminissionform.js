function continuephase() {
      $("#Instructionview").attr("style", "display:none");
      $("#adminissiontype").attr("style", "display:block");
   
}
function Prevoius1() {
    $("#Instructionview").attr("style", "display:block");
    $("#adminissiontype").attr("style", "display:none");

}
function nextphase() {
    var AdminissionType = $("#AdminissionType").val();
    var Educationtype = $("#Educationtype").val();
    var Coursetype = $("#Coursetype").val();
    var Streamtype = $("#Streamtype").val();
    var previousStreamtype = $("#iastream").val();
    if (AdminissionType == '') {
        alert("Please Select  Adminission Type !!");
        return;
    }
    if (Educationtype == '') {
        alert("Please Select  Programme   !!");
        return;
    }
    if ($("#iastream").val() == '') {
        alert("Please Select Previous Stream !!");
        return;
    }
    if (Coursetype == '') {
        alert("Please Select Course !!");
        return;
    }
    //if (Streamtype == '') {
    //    alert("Please Select  Stream !!");
    //    return;
    //}
    $("#adminissiontype").attr("style", "display:none");
    $("#basicinfo").attr("style", "display:block");
    $("#administype1").val(AdminissionType);
    $("#educationtype1").val(Educationtype);
    $("#coursetype1").val(Coursetype);
    $("#stream1").val(Streamtype);
    $("#prevoiustreamid").val(previousStreamtype);
    
}
$("#Educationtype").change(function (event) {
    debugger;
    $('#iastream').find("option").remove();
    $("#iastream").append($("<option></option>").val("").html("--Select Previous Stream--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/Home/getprevioustream/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            hideloader();
           //console.log(data);
            $('#iastream').find("option").remove();
            $("#iastream").append($("<option></option>").val("").html("--Select Previous Stream--"));
            $.each(data.data, function (key, value) {
                $("#iastream").append($("<option></option>").val(value.ID).html(value.QualificationType));
            });
        }
        ,
        error: function (err) {
            debugger;
           
            hideloader();
            return false;
        }
    });
});
$("#iastream").change(function (event) {
    debugger;
    $('#Coursetype').find("option").remove();
    $("#Coursetype").append($("<option></option>").val("").html("--Select Course--"));
    var res = $("#Educationtype").val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/Home/getcousrequlification/",
        data: { id: res,quaid:  $(this).val()},
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data)
        {
            hideloader();
            $('#Coursetype').find("option").remove();
            $("#Coursetype").append($("<option></option>").val("").html("--Select Course--"));
            $.each(data.data, function (key, value) {
                $("#Coursetype").append($("<option></option>").val(value.CourseCategoryID).html(value.CourseCategory));
            });
        },
        error: function (err) {
            debugger;

            hideloader();
            return false;
        }
    });
});
$("#Coursetype").change(function (event) {

    alert('Test');
    $('#Streamtype').find("option").remove();
    $("#Streamtype").append($("<option></option>").val("").html("--Select Stream--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    showloader();
    $.ajax({
        url: "/Home/getstream/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            hideloader();
           // console.log(data);
            $('#Streamtype').find("option").remove();
            $("#Streamtype").append($("<option></option>").val("").html("--Select Stream--"));
            $.each(data.data, function (key, value) {
                $("#Streamtype").append($("<option></option>").val(value.StreamCategoryID).html(value.streamCategory));
            });

        },
        error: function (err) {
            debugger;

            hideloader();
            return false;
        }
    });
});


$("#Gender").change(function (event) {
    debugger;
    return;
    $('#Cast').find("option").remove();
    $("#Cast").append($("<option></option>").val("").html("--Select Category--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/Home/Bind_caste/",
        data: { gender: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#Cast').find("option").remove();
            $("#Cast").append($("<option></option>").val("").html("--Select Category--"));
            $.each(data, function (key, value) {
                $("#Cast").append($("<option></option>").val(value.CommonId).html(value.Title));
            });
        }
    });
});
$("#Country").change(function (event) {
    debugger;
    $('#State').find("option").remove();
    $("#State").append($("<option></option>").val("").html("--Select State--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/Home/State_Bind/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#State').find("option").remove();
            $("#State").append($("<option></option>").val("").html("--Select State--"));
            $.each(data, function (key, value) {
                $("#State").append($("<option></option>").val(value.Value).html(value.Text));
            });
        }
    });
});
$("#PCountry").change(function (event) {
    debugger;
    $('#PState').find("option").remove();
    $("#PState").append($("<option></option>").val("").html("--Select State--"));
    var res = $(this).val();
    if (res == "")
        res = 0;
    $.ajax({
        url: "/Home/State_Bind/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            //console.log(data);
            $('#PState').find("option").remove();
            $("#PState").append($("<option></option>").val("").html("--Select State--"));
            $.each(data, function (key, value) {
                $("#PState").append($("<option></option>").val(value.Value).html(value.Text));
            });
        }
    });
});
$("#firstnameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#firstnameHindi").val(data.data);
        }
    });
});
$("#middlenameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#middlenameHindi").val(data.data);
        }
    });
});
$("#lastnameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#lastnameHindi").val(data.data);
        }
    });
});
$("#fathernameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#fathernameHindi").val(data.data);
        }
    });
});
$("#mothernameHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#mothernameHindi").val(data.data);
        }
    });
});
$("#FirstNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#FirstNameInHindi").val(data.data);
        }
    });
});
$("#MiddleNameInHindi").change(function (event) {
    debugger;
    var res = $(this).val();
    $.ajax({
        url: "/college/Home/googletranslate/",
        data: { text: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {

            console.log(data);
            console.log(data.data);
            //$(this).val(data.data);
            //$("#firstnameHindi").html(data.data);
            $("#MiddleNameInHindi").val(data.data);
        }
    });
});
$(document).ready(function () {
    $("#parmanentcheckbox").change(function (event) {
        debugger;
        //var res11 = $("#parmanentcheckbox").check();
        if ($('#parmanentcheckbox').is(":checked")) {


            if ($("#Address").val() == "") {
                alert("Please Enter Address !!");
                $(this).val();
                $(this).prop('checked', false);
                return;
            }
            if ($("#city").val() == "") {
                alert("Please Enter City !!");
                $(this).prop('checked', false);
                return;
            }
            if ($("#pincode").val() == "") {
                alert("Please Enter PinCode !!");
                $(this).prop('checked', false);
                return;
            }
            if ($("#Country").val() == "") {
                alert("Please Select Country !!");
                $(this).prop('checked', false);
                return;
            }
            if ($("#State").val() == "") {
                alert("Please Select State !!");
                $(this).prop('checked', false);
                return;
            }
            $("#PAddress").val($("#Address").val());
            $("#PPinCode").val($("#pincode").val());
            $("#Pcity").val($("#city").val());

            $('#PCountry').val($("#Country").val());


            $("#Pcity").attr('readonly', 'readonly');
            $("#PAddress").attr('readonly', 'readonly');
            $("#PPinCode").attr('readonly', 'readonly');
            $("#PState").attr('readonly', 'readonly');
            $("#PState").append($("<option></option>").val("").html("--Select State--"));
            var res = $("#Country").val()
            if (res == "")
                res = 0;
            $.ajax({
                url: "/Home/State_Bind/",
                data: { id: res },
                cache: false,
                type: "POST",
                dataType: "json",
                success: function (data) {

                    // console.log(data);
                    $('#PState').find("option").remove();
                    $("#PState").append($("<option></option>").val("").html("--Select State--"));
                    $.each(data, function (key, value) {
                        $("#PState").append($("<option></option>").val(value.Value).html(value.Text));
                    });
                    $('#PState').val($("#State").val());
                }
            });

        }
        else {
            $("#PAddress").val('');
            $("#PPinCode").val('');
            $("#Pcity").val('');
            // $('#PCountry').val('');
            $('#PState').find("option").remove();
            $("#PState").append($("<option></option>").val("").html("--Select State--"));
            $("#Pcity").removeAttr('readonly');
            $("#PAddress").removeAttr('readonly');
            $("#PPinCode").removeAttr('readonly');
            $("#PState").removeAttr('readonly');
        }
    });
});
checkphoto = function () {
    //debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementById("photo");
    var oInput = arrInputs;
    var sFileName = oInput.value;
    var sFileNamesize = document.getElementById('photo').files[0].size;;
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
checksigh = function () {
   // debugger;
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementById("sign");
    var oInput = arrInputs;
    var sFileName = oInput.value;
    var sFileNamesize = document.getElementById('sign').files[0].size;;
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
            document.getElementById('append-big-btn2').value = "";
            alert("Sorry, file is invalid, allowed extensions are: " + _validFileExtensions.join(", "));
            return false;
        }
        else {
            if (sFileNamesize > 51200) {
                oInput.value = "";
               // document.getElementById('append-big-btn2').value = "";
                alert("File is too big!");
                return false;
            }
        }
    }
    //var fileSize = document.getElementById('file').files[0].size;
    return true;
}

function Prevoius() {
    $("#adminissiontype").attr("style", "display:block");
    $("#basicinfo").attr("style", "display:none");
}
function onsave() {

    alert('Test');
    if ($("#checkAgree").prop("checked") == false) {
        alert("Please agree to terms and conditions !!");
        $("#checkAgree").focus();
        return false;

    }

    if ($("#Title").val() == "") {
        alert('Please Select Title !!');
        $("#Title").focus();
        return false;

    }
    if ($("#firstname").val() == "")
    {
        alert('Please Enter Firstname !!');
        $("#firstname").focus();
        return false;
        
    }
    //if ($("#lastname").val() == "") {

    //    alert('Please Enter Last Name !!');
    //    $("#lastname").focus();
    //    return;
    //}
    if ($("#firstnameHindi").val() == "") {
        alert('Please Enter Firstname in Hindi!!');
        $("#firstnameHindi").focus();
        return false;
    }
    if ($("#Gender").val() == "") {
        alert('Please Select Gender !!');
        $("#Gender").focus();
        return false;
    }
    if ($("#dob").val() == "") {
        alert('Please Enter DOB !!');
        $("#dob").focus();
        return false;
    }
    if ($("#Cast").val() == "") {
        alert('Please Select Cast !!');
        $("#Cast").focus();
        return false;
    }
    //if ($("#Blood_Group").val() == "") {
    //    return;
    //}
    if ($("#mobileno").val() == "") {
        alert('Please Enter Mobile Number !!');
        $("#mobileno").focus();
        return false;
    }
    if ($("#email").val() == "") {
        alert('Please Enter Email ID !!');
        $("#email").focus();
        return false;
    }
    else
    {
        var email = $("#email").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email))
        {
            alert('Please Enter Valid Email ID !!');
            $("#email").focus();
            return false;   
        }
    }
    if ($("#Nationality").val() == "") {
        alert('Please Select Nationality !!');
        $("#Nationality").focus();
        return false;
    }
    if ($("#Religion").val() == "") {
        alert('Please Select Religion !!');
        $("#Religion").focus();
        return false;
    }
    if ($("#Religion").val() == "28")
    {
        if ($("#ReligonOther").val() == "") {
            alert('Please Enter Religion !!');
            $("#ReligonOther").focus();
            return false;
        }
    }
   
    if ($("#Address").val() == "") {
        alert('Please Enter Address !!');
        $("#Address").focus();
        return false;
    }
    if ($("#city").val() == "") {
        alert('Please Enter  City !!');
        $("#city").focus();
        return;
    }
    if ($("#pincode").val() == "")
    {
        alert('Please Enter pincode !!');
        $("#pincode").focus();
        return false;
    }
    if ($("#Country").val() == "") {
        alert('Please Select Country !!');
        $("#Country").focus();
        return false;
    }
   
    if ($("#State").val() == "") {
        alert('Please Select State !!');
        $("#State").focus();
        return false;
    }
    if ($("#PAddress").val() == "") {
        alert('Please Enter Permanent  Address !!');
        $("#PAddress").focus();
       
        return false;
    }
    if ($("#Pcity").val() == "") {
        alert('Please Enter Permanent  City !!');
        $("#Pcity").focus();
        return;
    }
    if ($("#PPinCode").val() == "") {
        alert('Please Enter Permanent  Pincode !!');
        $("#PPinCode").focus();
        return false;
    }
    if ($("#PCountry").val() == "") {
        alert('Please Select Permanent  Country !!');
        $("#PCountry").focus();
        return false;
    }
    if ($("#PState").val() == "") {
        alert('Please Select Permanent  State !!');
        $("#PState").focus();
        return false;
    }

    
    if ($("#typeTitle").val() == "") {
        alert('Please Select  Father title');
        $("#typeTitle").focus();
        return false;
    }
    if ($("#fathername").val() == "")
    {
        alert('Please Enter Father Name !!');
        $("#fathername").focus();
        return false;
    }
    if ($("#fathernameHindi").val() == "") {
        alert('Please Enter Father Name in Hindi!!');
        $("#fathernameHindi").focus();
        return false;
    }
    //if ($("#fathermobile").val() == "") {
    //    alert('Please Enter Father Mobile !!');
    //    $("#fathermobile").focus();
    //    return false;
    //}
    if ($("#fatheremail").val() != "")
    {
        var email = $("#fatheremail").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            alert('Please Enter Valid Email ID for Father !!');
            $("#fatheremail").focus();
            return false;
        }
        
    }
   
    
    //if ($("#fatherqulification").val() == "") {
    //    return;
    //}
    //if ($("#fatheroccupation").val() == "") {
    //    return;
    //}


    if ($("#mothername").val() == "") {
        alert('Please Enter Mother Name !!');
        $("#mothername").focus();
        return false;
    }
    if ($("#mothernameHindi").val() == "") {
        alert('Please Enter Mother Name in Hindi!!');
        $("#mothernameHindi").focus();
        return false;
    }
    //if ($("#mothermobile").val() == "") {
    //    alert('Please Enter Mother Mobile !!');
    //    $("#mothermobile").focus();
    //    return false;
    //}
    //if ($("#motheremail").val() == "") {
    //    return;
    //}
    if ($("#motheremail").val() != "") {
        var email = $("#motheremail").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            alert('Please Enter Valid Email ID for Mother !!');
            $("#motheremail").focus();
            return false;
        }
        
    }
    //if ($("#motherqulicafication").val() == "") {
    //    return;
    //}
    //if ($("#motheroccupation").val() == "") {
    //    return;
    //}
    if ($("#photo").val() == "")
    {
        alert('Please Select photo !!');
        $("#photo").focus();
        return false;
    }
    if ($("#sign").val() == "") {
        alert('Please Select Signature !!');
        $("#sign").focus();
        return false;
    }
    if (window.FormData !== undefined)
    {

        var fileUpload = $("#photo").get(0);
        var files = fileUpload.files;
        var fileData = new FormData();
        for (var i = 0; i < files.length; i++) {
            fileData.append("photo", files[i]);
        }
        var fileUpload2 = $("#sign").get(0);
        var files2 = fileUpload2.files;
        for (var i = 0; i < files2.length; i++) {
            fileData.append("sign", files2[i]);
        }
        fileData.append('firstname', $("#firstname").val());
        fileData.append('middlename', $("#middlename").val());
        fileData.append('lastname', $("#lastname").val());
        fileData.append('Gender', $("#Gender").val());
        fileData.append('dob', $("#dob").val());
        fileData.append('Cast', $("#Cast").val());
        fileData.append('Blood_Group', $("#Blood_Group").val());
        fileData.append('mobileno', $("#mobileno").val());
        fileData.append('email', $("#email").val());
        fileData.append('Address', $("#Address").val());
        fileData.append('pincode', $("#pincode").val());
        fileData.append('Country', $("#Country").val());
        fileData.append('State', $("#State").val());
        fileData.append('PAddress', $("#PAddress").val());
        fileData.append('Pcity', $("#Pcity").val());
        fileData.append('city', $("#city").val());
        fileData.append('PPinCode', $("#PPinCode").val());
        fileData.append('PCountry', $("#PCountry").val());
        fileData.append('PState', $("#PState").val());
        fileData.append('fathermobile', $("#fathermobile").val());
        fileData.append('fatheremail', $("#fatheremail").val());
        fileData.append('fathername', $("#fathername").val());
        fileData.append('fatherqulification', $("#fatherqulification").val());
        fileData.append('fatheroccupation', $("#fatheroccupation").val());
        fileData.append('mothername', $("#mothername").val());
        fileData.append('mothermobile', $("#mothermobile").val());
        fileData.append('motheremail', $("#motheremail").val());
        fileData.append('motherqulicafication', $("#motherqulicafication").val());
        fileData.append('motheroccupation', $("#motheroccupation").val());
        fileData.append('administype1', $("#administype1").val());
        fileData.append('educationtype1', $("#educationtype1").val());
        fileData.append('coursetype1', $("#coursetype1").val());
        fileData.append('stream1', $("#stream1").val());
        fileData.append('Guardianname', $("#Guardianname").val());
        fileData.append('Guardianmobile', $("#Guardianmobile").val());
        fileData.append('Guardianrelation', $("#Guardianrelation").val());
        fileData.append('title', $("#Title").val());
        fileData.append('ftitle', $("#typeTitle").val());
        fileData.append('cseesionid', $("#cseesionid").val());
        fileData.append('Nationality', $("#Nationality").val());
        fileData.append('Religion', $("#Religion").val());
        fileData.append('MotherTongue', $("#MotherTongue").val());
        fileData.append('ishandicapped', $("input[name='ishandicapped']:checked").val());
        fileData.append('isex_service_man',$("input[name='isex_service_man']:checked").val() );
        fileData.append('is_ncc_candidate', $("input[name='is_ncc_candidate']:checked").val() );
        fileData.append('aadharno', '');
        fileData.append('prevoiustreamid', $("#prevoiustreamid").val());
        debugger;
        var tttt = document.getElementById("firstnameHindi").innerHTML;

        fileData.append('FirstNameInHindi', $("#firstnameHindi").val());
        fileData.append('MiddleNameInHindi', $("#middlenameHindi").val());
        fileData.append('LastNameInHindi', $("#lastnameHindi").val());
        fileData.append('ReligonOther', $("#ReligonOther").val());
        fileData.append('IsSports',$("input[name='IsSports']:checked").val() );
        fileData.append('IsStaff', $("input[name='IsStaff']:checked").val());
        fileData.append('FatherNameInHindi', $("#fathernameHindi").val());
        fileData.append('MotherNameInHindi', $("#mothernameHindi").val());


        $("#privious").attr("style", "display:none");
        $("#btnUpload").attr("style", "display:none");
        showloader();
        $.ajax({
            url: '/Home/UploadSaveFiles',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result)
            {
                debugger;
                console.log(result);
                if (result.status == true)
                {
                    $("#adminissiontype").attr("style", "display:none");
                    $("#basicinfo").attr("style", "display:none");
                    $("#privious").attr("style", "display:none");
                    $("#btnUpload").attr("style", "display:none");
                    $("#welcome").attr("style", "display:block");
                    $("#welnamename").html(result.FirstName);
                    $("#applicationno").html(result.ApplicationNo);
                    $("#welMobileno").html(result.MobileNo);
                    $("#showpassword").html(result.Password);
                    hideloader();
                  //  alert(result);
                }
                else
                {
                    $("#adminissiontype").attr("style", "display:none");
                    $("#basicinfo").attr("style", "display:block");
                    $("#welcome").attr("style", "display:none");
                    $("#privious").attr("style", "display:block");
                    $("#btnUpload").attr("style", "display:block");
                    alert(result.Message);
                    hideloader();
                }
                return false;
            },
            error: function (err)
            {
                debugger;
                alert(err.statusText);
                $("#adminissiontype").attr("style", "display:none");
                $("#basicinfo").attr("style", "display:block");
                $("#welcome").attr("style", "display:none");
                $("#privious").attr("style", "display:block");
                $("#btnUpload").attr("style", "display:block");
                hideloader();
                return false;
            }
        });
    }
    else
    {
        alert("FormData is not supported.");
        $("#adminissiontype").attr("style", "display:none");
        $("#basicinfo").attr("style", "display:block");
        $("#welcome").attr("style", "display:none");
        $("#privious").attr("style", "display:block");
        $("#btnUpload").attr("style", "display:block");; hideloader();
        return false;
    }
}