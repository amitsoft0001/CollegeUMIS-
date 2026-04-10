function Approve() {
   
    var res = $("#hid").val();
    showloader();
    $.ajax({
        url: "/College/Home/ApproveLeave/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (result) {
   
            hideloader();
            //debugger;
            if (result.Status==true) {
               
                alert(result.Msg);
                location.replace('/College/Home/VerifyLeave');

            }
            else {
               
                alert(result.Msg);
            }
            return false;
        },
        error: function (err) {
            //debugger;
            hideloader();
            alert("error block");

        }
    });




}
function Reject() {
   
    var res = $("#hid").val();
   
    showloader();
    $.ajax({
        url: '/College/Home/RejectLeaveEmp/',
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (result) {
            
            hideloader();
              
            if (result.Status) {
                
                alert(result.Msg);
                location.replace('/College/Home/VerifyLeave');

            }
            else {
                debugger;
                console.log(result);
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