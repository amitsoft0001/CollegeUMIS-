$(document).ready(function () {
    $("#Fromdate").change(function (event) {
        $("#Todate").val($("#Fromdate").val());
    });

    $("#halfdaycheckbox").change(function (event) {
if ($('#halfdaycheckbox').is(":checked")) {
    //debugger;
    $("#Todate").val($("#Fromdate").val());
    $("#Todate").attr('disabled', true);
    $('#todaydiv').removeAttr("style");
    $('#todaydiv').attr("style", "display:none");
    
    $('#halfradio').removeAttr("style");
    $('#halfradio').attr("style", "display:block");
}
else {
    $("#Todate").val(''); 
    $("#Todate").attr('disabled', false);
    $('#todaydiv').removeAttr("style");
    $('#todaydiv').attr("style", "display:block");
    $('#halfradio').removeAttr("style");
    $('#halfradio').attr("style", "display:none");
}
    });
});
function resetapplication() {
    $("#Todate").val('');
    $("#Fromdate").val('');    
    $("#Subject").val('');
    $("#Reason").val('');
    $("#LeaveType").val('');
    
}
function compareDate(fromdate, todate) {
    //debugger;
    var newdate1 = '';
    var dateOne = '';
    var datearr1 = fromdate.split('/');
    var newdate2 = '';
    var dateTwo = '';
    var datearr2 = todate.split('/');
    if (datearr1.length >= 2) {
        newdate1 = datearr1[2] + '-' + datearr1[1] + '-' + datearr1[0];
        dateOne = new Date(datearr1[2], datearr1[1], datearr1[0]);
        //return newdate;
    }
    if (datearr2.length >= 2) {
        newdate2 = datearr2[2] + '-' + datearr2[1] + '-' + datearr2[0];
        dateTwo = new Date(datearr2[2], datearr2[1], datearr2[0]);
        //return newdate;
    }
    if (dateOne > dateTwo) {
        alert("To date should be greater then from date.");
        return true;
    }
    else
        return false;

}
function submitapplication() {

    var ID = $("#hid").val();
    var Fromdate = $("#Fromdate").val();
    var Todate = $("#Todate").val();
    var Subject = $("#Subject").val();
    var Reason=$("#Reason").val();
    var EmployeeID = $("#EmployeeID").val();
    var LeaveType = $("#LeaveType").val();  
    var halftime = 0;
    var Day = 0;
    if ($('#halfdaycheckbox').is(":checked")) {
        Day = 1 / 2;
        halftime = $('[name="halftime"]:radio:checked').val();
    }
    
    if (EmployeeID == "") {
        alert('Please Select Employee  !!');
        $("#EmployeeID").focus();
        return;
    }
    if (LeaveType == "") {
        alert('Please Select Leave Type  !!');
        $("#LeaveType").focus();
        return;
    }

  
    if (Fromdate == "") {
        alert('Please select From Date  !!');
        $("#Fromdate").focus();
        return;
    }
    else {
        if (Fromdate.length != 10) {
            alert('From date Should Be valid !!');
            $("#Fromdate").focus();
            return false;
        }

    }
    if (Todate == "") {
        alert('Please select To Date  !!');
        $("#Todate").focus();
        return;
    }
    else {
        if (Todate.length != 10) {
            alert('To date Should Be valid !!');
            $("#Todate").focus();
            return false;
        }

    }
    var result = compareDate(Fromdate, Todate);
    if (result) {
        return;
    }
   
    if (Subject == "") {
        alert('Please Enter Subject  !!');
        $("#Subject").focus();
        return;
    }
    if (Reason == "") {
        alert('Please Enter Reason  !!');
        $("#Reason").focus();
        return;
    }
   
   // debugger;
    var ob = {
        ID: ID,
        Fromdate: Fromdate,
        Todate: Todate,
        Subject:Subject,
        Reason: Reason,
        LeaveType: LeaveType,
        EmployeeID: EmployeeID,
        Day: Day,
        halftime: halftime
    };
   // console.log(ob);
    showloader();
    $.ajax({
        url: '/College/Home/AddNewLeave/',
        data: JSON.stringify(ob),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            hideloader();

            if (result.Status) {
                alert(result.Msg);
                location.replace('/College/Home/Applyleave');

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
var now = new Date();
var startdate11 = new Date(2018, 1, 1);
var Enddate11 = new Date(2020, 1, 1);
//(function ($) {
//    "use strict";
//    debugger;
//    $('#data_datepicker').datepicker({

//        startView: 2,
//        todayBtn: "linked",
//        keyboardNavigation: false,
//        forceParse: false,
//        calendarWeeks: true,
//        autoclose: true,
//        format: "yyyy/mm/dd",
//        defaultDate: "",
//        endDate: Enddate11,
//        startDate: startdate11
//    });
//})(jQuery);
//$('#date').datetimepicker({
//    pickTime: false,
//    icons: {
//        time: "fa fa-clock-o",
//        date: "fa fa-calendar",
//        up: "fa fa-arrow-up",
//        down: "fa fa-arrow-down"
//    },
//    minDate: moment()
//});
