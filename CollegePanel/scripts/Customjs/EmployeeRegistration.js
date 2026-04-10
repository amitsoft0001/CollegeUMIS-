$(document).ready(function () {

    $("#parmanentcheckbox").change(function (event) {
        //debugger;
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
            $("#PA_Address").val($("#CurrentAddress").val());
            $("#PA_PinCode").val($("#pincode").val());
            $("#PA_City").val($("#CA_City").val());

            $('#PCountry').val($("#Country").val());

            $("#PA_City").attr('readonly', 'readonly');
            $("#PA_Address").attr('readonly', 'readonly');
            $("#PA_PinCode").attr('readonly', 'readonly');
            $("#PState").attr('readonly', 'readonly');

            $("#PState").append($("<option></option>").val("").html("--Select State--"));
            var res = $("#Country").val()
            if (res == "")
                res = 0;
            $.ajax({
                url: "/College/Home/State_Bind/",
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
            $("#PA_Address").val('');
            $("#PA_PinCode").val('');
            $("#PA_City").val('');
            // $('#PCountry').val('');
            $('#PState').val("");
            $("#PA_City").removeAttr('readonly');
            $("#PA_Address").removeAttr('readonly');
            $("#PA_PinCode").removeAttr('readonly');
            $("#PState").removeAttr('readonly');
            //$("#PState").append($("<option></option>").val("").html("--Select State--"));
        }
    });

    $("#UserName").blur(function () {
        var UserName = $("#UserName").val();
        showloader();
        $.ajax({
            url: '/College/Home/Checkuser',
            type: "POST",
            data: { name: UserName },
            success: function (result) {

                hideloader();
                if (result.status == true) {

                    $("#UserName").val("");
                    $("#UError").html(result.msg);

                }
                else {
                    $("#UError").html("");
                }
                return false;
            },
            error: function (err) {

                hideloader();
                alert("error");

            }
        });
    });

    $("#MobileNo").blur(function () {
        var MobileNo = $("#MobileNo").val();
        showloader();
        $.ajax({
            url: '/College/Home/CheckuserMobile',
            type: "POST",
            data: { mobile: MobileNo },
            success: function (result) {

                hideloader();
                if (result.status == true) {
                    $("#mid").attr("style", "display:block");
                  
                    $("#MobileNo").val("");
                    $("#MError").html(result.msg);

                }
                else {
                    $("#mid").attr("style", "display:none");
                    $("#MError").html("");
                }
                return false;
            },
            error: function (err) {

                hideloader();
                alert("error");

            }
        });
    });
    $("#Email").blur(function () {
       
        var Email = $("#Email").val();
        showloader();
        $.ajax({
            url: '/College/Home/CheckuserEmail',
            type: "POST",
            data: { email: Email },
            success: function (result) {

                hideloader();
                if (result.status == true) {
                    //debugger;
                    $("#eid").attr("style", "display:block");
                     $("#Email").val("");
                    $("#EError").html(result.msg);

                }
                else {
                    $("#eid").attr("style", "display:none");
                    $("#EError").html("");
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
                document.getElementById('append-big-btn2').value = "";
                alert("File is too big!");
                return false;
            }
        }
    }
    //var fileSize = document.getElementById('file').files[0].size;
    return true;
}