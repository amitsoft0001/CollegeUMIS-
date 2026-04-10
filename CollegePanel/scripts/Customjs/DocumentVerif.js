$(document).ready(function () {
    $("#submitapp").click(function (event) {
        // debugger;
        var res = $("#hid").val();
        ///alert(res);
        if (res == "")
            res = 0;
        showloader();
        $.ajax({
            url: "/College/Home/DocumentVerifyForStudent/",
            data: { id: res },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (data) {
                hideloader();
                if (data.Status) {
                    alert(data.Msg);
                    window.location.reload();
                    //location.replace('/College/Home/DocumentVerifyList');
                }
                else {
                    alert(data.Msg);
                }
                // console.log(data);

            },
            error: function (err) {
                hideloader();
                alert('error');

                return false;
            }
        });
    });
    $("#submitappreject").click(function (event) {
        // debugger;
        var res = $("#hid").val();
        if (res == "")
            res = 0;
        var a = confirm('Are you sure to Reject this Student !!');
        var txt;
        if (a) {
            var person = prompt("Please Enter Reason:", "");
            if (person == null || person == "") {
                return false;
            }
            else {
                showloader();
                $.ajax({
                    url: "/College/Home/DocumentVerifyForStudentrejects/",
                    data: { id: res, reason: person },
                    cache: false,
                    type: "POST",
                    dataType: "json",
                    success: function (data) {
                        hideloader();
                        if (data.Status) {
                            alert(data.Msg);
                            window.location.reload();
                            //location.replace('/College/Home/DocumentVerifyList');
                        }
                        else {
                            alert(data.Msg);
                        }
                        // console.log(data);

                    },
                    error: function (err) {
                        hideloader();
                        alert('error');

                        return false;
                    }
                });

            }
        }
        else {
            return false;
        }


    });
});
