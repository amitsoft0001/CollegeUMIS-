    $(document).ready(function () {
       
        $("#EducationTypeID").change(function (event) {

            $('#CourseCategoryID').find("option").remove();
            $("#CourseCategoryID").append($("<option></option>").val("").html("--Select Course--"));
            var res = $(this).val();
            if (res == "")
            { res = 0;
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
            if (res == "")
            { res = 0;
                return;}
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
        $(function () {
            $("#btnPrint").click(function () {
                var contents = $("#dvContents").html();
                console.log(contents);
                var frame1 = $('<iframe />');
                frame1[0].name = "frame1";
                frame1.css({ "position": "absolute", "top": "-1000000px" });
                $("body").append(frame1);
                var frameDoc = frame1[0].contentWindow ? frame1[0].contentWindow : frame1[0].contentDocument.document ? frame1[0].contentDocument.document : frame1[0].contentDocument;
                frameDoc.document.open();
                //Create a new HTML document.
                frameDoc.document.write('<html><head><title>.</title>');

                frameDoc.document.write('</head><body>');
                //Append the external CSS file.
                frameDoc.document.write('<link href="style.css" rel="stylesheet" type="text/css" />');
                //Append the DIV contents.
                frameDoc.document.write(contents);
                frameDoc.document.write('</body></html>');
                frameDoc.document.close();
                setTimeout(function () {
                    window.frames["frame1"].focus();
                    window.frames["frame1"].print();
                    frame1.remove();
                }, 500);
            });
        });
    
        $(function ()
        {
            $("#session option").each(function ()
            {
                if ($(this).val() ==((CuSession)))
                {
                    $(this).attr('selected', 'selected');
                }
            });
        });
    });
       


    

    
     
   
