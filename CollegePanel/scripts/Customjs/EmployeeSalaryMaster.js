$(document).ready(function () {
    if ($("#HRA").val() != '')
    {
        $('#HRA').attr('readonly', true);       
    }
    else {
        $('#HRA').attr('readonly', false);
    }
    if ($("#DA").val() != '') {       
        $('#DA').attr('readonly', true);
    }
    else {
        $('#DA').attr('readonly', false);
    }
    $("#BasicSalary").blur(function () {
        var BasicSalary = $("#BasicSalary").val();
        var PF = BasicSalary * 10 / 100;
        showloader();
        $.ajax({
            url: '/College/PayRoll/GetDAAndHRA',
            type: "POST",            
            success: function (result) {
                hideloader();
                if (result != null) {                  
                    var DAper = result.DApercentage * BasicSalary / 100;
                    var HRAper = result.HRApercentge * BasicSalary / 100;
                    $("#HRA").val(HRAper);
                    $("#DA").val(DAper);
                    $('#HRA').attr('readonly', true);
                    $('#DA').attr('readonly', true);
                    $('input[name="Deduction_MasterList[0].Amount"]').val(PF);               
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
    $("#EmployeeID").val('');
    $("#PaybandID").val('');
    $("#BasicSalary").val('');
    $("#HRA").val('');
    $("#DA").val('');
    $('input[name="Amount"]').val('');
    $('input[name="AAmountType"]').val('');
    $('input[name="AllowanceAmount"]').val('');
    $('input[name="DAmountType"]').val('');
}

function submitapplication() {
    //debugger;
    var Id = $("#aid").val();
    var EmployeeID = $("#EmployeeID").val();
    var PaybandID = $("#PaybandID").val();
    var BasicSalary = $("#BasicSalary").val();
    var HRA = $("#HRA").val();
    var DA = $("#DA").val();    
    if (EmployeeID == "") {
        alert('Please Select Employee !!');
        $("#EmployeeID").focus();
        return;
    }
    if (PaybandID == "") {
        alert('Please Select Payband !!');
        $("#PaybandID").focus();
        return;
    }
    if (BasicSalary == "") {
        alert('Please Enter Basic Salary !!');
        $("#BasicSalary").focus();
        return;
    }
    if (HRA == "") {
        alert('Please Enter HRA !!');
        $("#HRA").focus();
        return;
    }
    if (DA == "") {
        alert('Please Enter DA !!');
        $("#DA").focus();
        return;
    }
    var valuesAllowanceAmount = $('.AllowanceAmount').map(function () {
        return this.value
    }).get();
    if (valuesAllowanceAmount.length == 0) {
        alert('Please Enter Allowance Amount First !!!!');
        return;
    }
    var valuesAAmountType = $('input[class="AAmountType"]:checked').map(function () {
        return this.value
    }).get();
    if (valuesAAmountType.length == 0) {
        alert('Please Select Allowance Amount Type First !!!!');
        return;
    }
    var valueshaid = $('input[name="haid"]').map(function () {
        return this.value
    }).get();

    var valuesAmount = $('.Amount').map(function () {
        return this.value
    }).get();
    if (valuesAmount.length == 0) {
        alert('Please Enter Deduction Amount First !!!!');
        return;
    }
    var valuesDAmountType = $('input[class="DAmountType"]:checked').map(function () {
        return this.value
    }).get();
    if (valuesDAmountType.length == 0) {
        alert('Please Select Deduction Amount Type First !!!!');
        return;
    }    
    var valueshdid = $('input[name="hdid"]').map(function () {
        return this.value
    }).get();
    var DAAmount = '';
    var DAAmountType = '';
    var DAID = '';
    var AlAmount = '';
    var AlAmountType = '';
    var AlID = '';
    if (valuesAmount.length > 0) {
       // debugger;
        for (var i = 0; i < valuesAmount.length; i++) {
            if (valuesAmount[i] == "" || valuesAmount[i] == "0") {               
            }
            else {
                DAAmount += valuesAmount[i] + ',';
                if (valuesDAmountType[i] != "") {
                    DAAmountType += valuesDAmountType[i] + ',';                 
                }
                if (valueshdid[i] != "") {
                    DAID += valueshdid[i] + ',';                    
                }
            }            
        }
    }
    if (valuesAllowanceAmount.length > 0) {
        for (var i = 0; i < valuesAllowanceAmount.length; i++) {
            if (valuesAllowanceAmount[i] == "" || valuesAllowanceAmount[i] == "0") {               
            }
            else {
                AlAmount += valuesAllowanceAmount[i] + ',';
                if (valuesAAmountType[i] != "") {
                    AlAmountType += valuesAAmountType[i] + ',';                    
                }
                if (valueshaid[i] != "") {
                    AlID += valueshaid[i] + ',';                 
                }
            }            
        }
    }  
    var ob = {
        Id:Id,
        EmployeeID: EmployeeID,
        PaybandID: PaybandID,
        BasicSalary: BasicSalary,
        HRA: HRA,
        DA: DA,
        DAAmount: DAAmount,
        DAAmountType: DAAmountType,
        DAID: DAID,
        AlAmount: AlAmount,
        AlAmountType: AlAmountType,
        AlID: AlID,
    };
    showloader();
    $.ajax({
        url: '/College/PayRoll/AddNewSalaryMaster/',
        data: JSON.stringify(ob),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            hideloader();
            if (result.Status) {
                alert(result.Msg);
                location.replace('/College/PayRoll/EmployeeSalaryMasterList');
            }
            else {
                alert(result.Msg);
            }
            return false;
        },
        error: function (err) {
            hideloader();
            alert("Something Went Wrong!!!");
        }
    });
}


