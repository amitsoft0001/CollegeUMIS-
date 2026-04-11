$(document).ready(function () {
    $("#EducationTypeID").change(function (event) {
        $('#CourseCategoryID').find("option").remove();
        $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
        var res = $(this).val();
        if (res == "") {
            res = 0;
            return;
        }
        $.ajax({
            url: "/College/Home/getcourse/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {

                //console.log(data);
                $('#CourseCategoryID').find("option").remove();
                $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
                $.each(data.data, function (key, value) {
                    $("#CourseCategoryID").append($("<option></option>").val(value.CourseCategoryID).html(value.CourseCategory));
                });
            }
        });
    });
    $("#CourseCategoryID").change(function (event) {

        $('#Subject').find("option").remove();
        $("#Subject").append($("<option></option>").val("").html("--Select Subject--"));
        var res = $(this).val();
        if (res == "") {
            res = 0;
            return;
        }
        $.ajax({

            url: "/College/Home/getsubjtectbycousrescollege/",
            data: { id: res },
            //, collegeid: $('#collegeid').val()
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {
                // console.log(data);
                $('#Subject').find("option").remove();
                $("#Subject").append($("<option></option>").val("").html("--Select Subject--"));
                $.each(data.data, function (key, value) {
                    $("#Subject").append($("<option></option>").val(value.StreamCategoryID).html(value.streamCategory));
                });
            }
        });
        $.ajax({

            url: "/College/Home/CourseYear/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
            success: function (data) {
                // console.log(data);
                $('#CourseYearID').find("option").remove();
                $("#CourseYearID").append($("<option></option>").val("").html("--Select Course Year--"));
                $.each(data.data, function (key, value) {
                    $("#CourseYearID").append($("<option></option>").val(value.id).html(value.YearName));
                });
            }
        });
    });
    $(function () {
        $("#session option").each(function () {
            if ($(this).val() == ((CuSession))) {
                $(this).attr('selected', 'selected');
            }
        });
    });
  
});
$(document).on('click', '.myBtn3', function () {
    var id = $(this).data("id");
    if (id == "") {
        $("#Feedetailmodal").modal();
        return;
    }
    $.ajax({
        url: "/College/Home/getStudentFeeDetail/",
        data: { id: id },
        cache: false,
        type: "GET",
        dataType: "json",
        headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
        success: function (data) {
          // console.log(data);
            if (data != null)
            {
                $("#Feedetailmodal").modal();
                $("#ApplicationNo").html(data.ApplicationNo);
                $("#TransactionStatus").html(data.status);
                $("#TransactionID").html(data.TransactionId);
                $("#refnumber").html(data.banktrxid);
                $("#TDate").html(data.trxdate);
                $("#PaymentMode").html(data.PaymentType);
                $("#Fees").html(data.Fees);
            }
            else {
                $("#Feedetailmodal").hide();
            }
         }
    });


});
