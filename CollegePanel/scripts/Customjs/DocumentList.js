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
    });
  

    //$(function () {
    //    $("#session option").each(function () {
            
    //        if ($(this).val() == ((CuSession))) {
    //            $(this).attr('selected', 'selected');
    //        }
    //    });
    //});
});








