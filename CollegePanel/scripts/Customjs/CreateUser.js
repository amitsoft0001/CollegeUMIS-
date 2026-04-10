$(document).ready(function () {
$("#showpassword").change(function (event) {
    if ($('#showpassword').is(":checked")) {
        $('#newpassword').removeAttr("type");
        $('#newpassword').attr("type", "text");
    }
    else {
        $('#newpassword').removeAttr("type");
        $('#newpassword').attr("type", "password");
    }
});
$("#showconpassword").change(function (event) {
    if ($('#showconpassword').is(":checked")) {
        $('#confpassword').removeAttr("type");
        $('#confpassword').attr("type", "text");
    }
    else {
        $('#confpassword').removeAttr("type");
        $('#confpassword').attr("type", "password");
    }
    });
    $("#UserName").blur(function () {
        var UserName = $("#UserName").val();
        showloader();
        $.ajax({
            url: '/College/Home/Checkuser',
            type: "POST",
            data: { name: UserName},
            success: function (result) {
              
                hideloader();               
                if (result.status == true) {
                   
                    $("#UserName").val("");
                    $("#Uerr").html(result.msg);
                    
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

function resetapplication() {
    $("#UserType").val('');
     $("#UserName").val('');
     $("#Mobile").val('');
     $("#ContactNo").val('');
    $("#Email").val('');
    $("#newpassword").val('');
    $("#confpassword").val('');
    $("#file").val('');
}
function submitapplication() {
    
    var ID = $("#hid").val();  
    var UserName = $("#UserName").val();
    var Mobile = $("#Mobile").val();    
    var Email = $("#Email").val();
    var Password = $("#newpassword").val();
    var confpassword = $("#confpassword").val();
    
    var Fullname = $("#Fullname").val();
    
    
    
   
    if (Fullname == "") {
        alert('Please Enter User Name  !!');
        return;
    }if (UserName == "") {
        alert('Please Enter User ID  !!');
        return;
    }
    if (Mobile == "") {
        alert('Please Enter Mobile Number  !!');
        return;
    }
   
    if (Email == "") {
        alert('Please Enter Email ID !!');
        $("#Email").focus();
        return false;
    }
    else {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!filter.test(Email)) {
            alert('Please Enter Valid Email ID !!');
            $("#Email").focus();
            return false;
        }
    }
    if (Password == "") {
        alert('Please Enter  Password !!');
        return;
    }
    else if (Password.length < 6) {
        alert('Please Enter Atleast 6 character in password  !!');
        return;
    }
    if (confpassword == "") {
        alert('Please Enter Confirm password   !!');
        return;
    }

    if (Password != confpassword) {
        alert('Password And Confirm Password does not match  !!');
        return;
    }
   

    var obj = {
        ID: ID,       
        UserName: UserName,
        Mobile: Mobile,        
        Email: Email,
        Password: Password,
        Fullname: Fullname,
        EncriptedID:ID
    };
    
    showloader();
        $.ajax({
            url: '/College/Home/Addnewuser',
            data: JSON.stringify(obj),
            type: "POST",     
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                
                hideloader();
                
                if (result.status) {
                    alert(result.msg);
                    location.replace('/College/Home/ManageUser');
                    //  alert(result);
                }
                else {
                    alert(result.msg);
                }
                return false;
            },
            error: function (err) {
                
                hideloader();
                alert("error");

            }
        });
   
   


}