$(document).ready(function () {
    $("#Educationtype").change(function (event) {
       // debugger;
        $('#CourseCategoryID').find("option").remove();
        $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
        var res = $(this).val();
        if (res == "")
            return;
        if (res == "")
            res = 0;

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
});
    $(document).on('click', '.myBtn', function () {

        var id = $(this).data("id");
        var totalseats1 = $(this).data("seats");
        if (id == "") {
            $("#dynamicvalue").html('');
            return;
        }
        else {
            var result = "";

            // debugger;

            $.ajax({
                url: "/College/Home/getcastseatidseatscollege",
                data: { id: id },
                cache: false,
                type: "POST",
                dataType: "json",
                headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
                success: function (data) {
                    // debugger;
                    var i = 1;

                    result += '<table class="table table-bordered">';
                    result += '<tr>';
                    result += '<TH>Sr.No.</TH>';
                    result += '<TH>Cast</TH>';
                    result += '<TH>Seats</TH>';
                    result += '<TH>Seats (Handicapped)</TH>';
                    result += '<TH >Total Seats</TH>';
                    result += '</tr>';
                    $.each(data.data, function (key, value) {
                        // console.log("DueDate" + ": " + value.CasteID);
                        result += '<tr>';
                        result += '<td>' + i++ + '</td>';
                        result += '<td>' + value.CasteID + '</td>';
                        result += '<td>' + value.Seatas + '</td>';
                        result += '<td>' + value.handicappedseats + '</td>';
                        result += '<td>' + (parseInt(value.handicappedseats) + parseInt(value.Seatas)) + '</td>';
                        result += "          </tr>";
                    });
                    result += '<tr>';
                    result += '<td></td>';
                    result += '<td colspan="3">Total Seats</td>';
                    result += '<td>' + totalseats1 + '</td>';
                    result += '</tr>';
                    result += '</table>';
                    $("#dynamicvalue").html(result);
                    $("#InformationproModalhdbgcl").modal();
                    $("#dynamicvalue").html(result);
                },
                error: function (data) {
                    alert('Error');
                }
            });


        }


    });

   
