
$(function () {
     
    $("#Gender option").each(function () {
        if ($(this).val() == ((gen)))
        {
            $(this).attr('selected', 'selected');
        }
    });
});
function Onlynumericvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9.]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}
function OnlyIntvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}


function resetapplication() { 
    $("#title").val("");
    $("#Name").val("");
    $("#Gender").val("");
    $("#ContactNo").val("");
    $("#email").val("");
    $("#Address").val("");
    $("#City").val("");
    $("#State").val("");
    $("#pincode").val("");
    $("#photo").val("");
    $("#NoOfRooms").val("");
    $("#NoOfSeats").val("");
    $("#NodalOfficerEmail").val("");
    $("#NodalOfficerMobile").val("");
    $("#PrincipalName").val("");
    $("#PrincipalEmail").val("");
    $("#PrincipalMobile").val("");
}

check = function () {
    
    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];
    var arrInputs = document.getElementsByTagName("input");
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            var sFileNamesize = document.getElementById('photo').files[0].size;
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
    //if ($("#code").val() == "") {
    //    alert('Please Enter Collage Code !!');
    //    $("#code").focus();
    //    return false;

    //}
    //if ($("#Cname").val() == "") {
    //    alert('Please Enter Collage Name !!');
    //    $("#Cname").focus();
    //    return false;

    //}
    if ($("#title").val() == "") {

        alert('Please selet title !!');
        $("#title").focus();
        return;
    }
    if ($("#Name").val() == "") {
        alert('Please Enter Name !!');
        $("#Name").focus();
        return false;
    }

    if ($("#Gender").val() == "") {
        alert('Please Select Gender !!');
        $("#Gender").focus();
        return false;
    }


    if ($("#ContactNo").val() == "") {
        alert('Please Enter Mobile Number !!');
        $("#ContactNo").focus();
        return false;
    }
    if ($("#email").val() == "") {
        alert('Please Enter Email ID !!');
        $("#email").focus();
        return false;
    }
    else {
        var email = $("#email").val();
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(email)) {
            alert('Please Enter Valid Email ID !!');
            $("#email").focus();
            return false;
        }
    }
    if ($("#Address").val() == "") {
        alert('Please Enter Address !!');
        $("#Address").focus();
        return false;
    }
    if ($("#City").val() == "") {
        alert('Please Enter  City !!');
        $("#City").focus();
        return;
    }
    if ($("#State").val() == "") {
        alert('Please Select State !!');
        $("#State").focus();
        return false;
    }
    if ($("#pincode").val() == "") {
        alert('Please Enter pincode !!');
        $("#pincode").focus();
        return false;
    }

    var hid = $("#hid").val();

    //if (hid == "") {
    //    if ($("#photo").val() == "") {
    //        alert('Please Select photo !!');
    //        $("#photo").focus();
    //        return false;
    //    }


    //}
    if ($("#NoOfRooms").val() == "") {
        alert('Please Enter No Of Rooms !!');
        $("#NoOfRooms").focus();
        return false;
    }
    else if (parseInt($("#NoOfRooms").val()) <= 0) {
        $("#NoOfRooms").val("");
        alert('Please Enter No Of Rooms !! ');

        return false;
    }
    if ($("#NoOfSeats").val() == "") {
        alert('Please Enter No Of Seats !!');
        $("#NoOfSeats").focus();
        return false;
    }
    else if (parseInt($("#NoOfSeats").val()) <= 0) {
        $("#NoOfSeats").val("");
        alert('Please Enter No Of Rooms !! ');

        return false;
    }
    if ($("#NodalOfficerName").val() == "") {
        alert('Please Enter Nodal Officer Name !!');
        $("#NodalOfficerName").focus();
        return false;
    }
    if ($("#NodalOfficerEmail").val() == "") {
        alert('Please Enter Nodal Officer Email !!');
        $("#NodalOfficerEmail").focus();
        return false;
    }
    else {
        var email1 = $("#NodalOfficerEmail").val();
        var filter1 = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter1.test(email1)) {
            alert('Please Enter Valid Email ID !!');
            $("#NodalOfficerEmail").focus();
            return false;
        }
    }
    if ($("#NodalOfficerMobile").val() == "") {
        alert('Please Enter Nodal Officer Mobile !!');
        $("#NodalOfficerMobile").focus();
        return false;
    }
    if ($("#PrincipalName").val() == "") {
        alert('Please Enter Principal Name !!');
        $("#PrincipalName").focus();
        return false;
    }
    if ($("#PrincipalEmail").val() == "") {
        alert('Please Enter Principal Email !!');
        $("#PrincipalEmail").focus();
        return false;
    }
    else {
        var email2 = $("#PrincipalEmail").val();
        var filter2 = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter2.test(email2)) {
            alert('Please Enter Valid Email ID !!');
            $("#PrincipalEmail").focus();
            return false;
        }
    }
    if ($("#PrincipalMobile").val() == "") {
        alert('Please Enter Principal Mobile !!');
        $("#PrincipalMobile").focus();
        return false;
    }
    //var FileURl;
    //var file;
    //if ($("#file").val() == null) {

    //    FileURl = $("#hfile").val();


    //}
    //else {
    //    var str = $("#file").val().split('\\');
    //    FileURl = str.pop();

    //}
    if (window.FormData !== undefined) {

        //var fileUpload = $("#photo").get(0);
        //var files = fileUpload.files;
        var fileData = new FormData();
        //for (var i = 0; i < files.length; i++) {
        //    fileData.append("photo", files[i]);
        //}
       
        //fileData.append('code', $("#code").val());
        //fileData.append('Cname', $("#Cname").val());
        fileData.append('title', $("#title").val());
        fileData.append('Name', $("#Name").val());
        fileData.append('Gender', $("#Gender").val());
        fileData.append('ContactNo', $("#ContactNo").val());
        fileData.append('email', $("#email").val());
        fileData.append('Address', $("#Address").val());
        fileData.append('City', $("#City").val());
        fileData.append('State', $("#State").val());
        fileData.append('pincode', $("#pincode").val());
        // fileData.append('photo', $("#photo").val());
        //fileData.append('photo', FileURl);
        fileData.append('ID', $("#hid").val());
        fileData.append('NoOfRooms', $("#NoOfRooms").val());
        fileData.append('NoOfSeats', $("#NoOfSeats").val());
        fileData.append('NodalOfficerName', $("#NodalOfficerName").val());
        fileData.append('NodalOfficerEmail', $("#NodalOfficerEmail").val());
        fileData.append('NodalOfficerMobile', $("#NodalOfficerMobile").val());
        fileData.append('PrincipalName', $("#PrincipalName").val());
        fileData.append('PrincipalEmail', $("#PrincipalEmail").val());
        fileData.append('PrincipalMobile', $("#PrincipalMobile").val());
        fileData.append('EncriptedID', $("#hid").val());
        showloader();
        $.ajax({
            url: '/College/Home/AddNewCollage',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
               
                hideloader();
                
                if (result.status == true) {
                    alert('Colleage Profile Updated Successfully');
                    location.replace('/College/Home/Index');
                    
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
    else {
        alert('');
    }
}
