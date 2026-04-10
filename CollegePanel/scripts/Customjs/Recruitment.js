$(document).on('click', '#MARITprocessStart', function () {
   // debugger;
    var res = $(this).val();
    //console.log(res);
    showloader();
    $.ajax({
        url: "/College/Home/Recruitmentstartmerit",
        data: { CasteCategory: genCasteCategory, CourseCategoryid: CourseCategoryid, Admissioncategoryid: Admissioncategoryid },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            //debugger;
            hideloader();
            if (data.status == true) {
                alert(data.Msg);
                showloader();
                window.location.reload();
            }
            else {
                alert(data.Msg);
            }
        },
        error: function (data) {
           // debugger;
            hideloader();
            alert('Error');
        }
    });
});
$(document).on('click', '.othercast', function () {
    var res = $(this).val();
    //console.log(res);
    showloader();
    $.ajax({
        url: "/College/Home/Recruitmentstartohtercast",
        data: { CasteCategory: res, CourseCategoryid: CourseCategoryid, Admissioncategoryid: Admissioncategoryid },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            debugger;
            hideloader();
            if (data.status == true) {
                alert(data.Msg);
                showloader();
                window.location.reload();
            }
            else {
                alert(data.Msg);
            }
        },
        error: function (data) {
            debugger;
            hideloader();
            alert('Error');
        }
    });
});

$(document).on('click', '.otherquota', function () {
    var res = $(this).val();
    //console.log(res);
    showloader();
    $.ajax({
        url: "/College/Home/Recruitmentstartquota",
        data: { CasteCategory: res, CourseCategoryid: CourseCategoryid, Admissioncategoryid: Admissioncategoryid },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (data) {
            debugger;
            hideloader();
            if (data.status == true) {
                alert(data.Msg);
                showloader();
                window.location.reload();
            }
            else {
                alert(data.Msg);
            }
        },
        error: function (data) {
            debugger;
            hideloader();
            alert('Error');
        }
    });
});

function rollback()
{
    var a = confirm('Are You Sure to Rollback of B.A. Recruitment  !!');
    if(a==true)
    {
       
        showloader();
        $.ajax({
            url: "/College/Home/Recruitmentrollback",
            data: { CourseCategoryid: CourseCategoryid, Admissioncategoryid: Admissioncategoryid },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (data) {
                console.log(data);
                debugger;
                hideloader();
                if (data.status == true) {
                    alert(data.Msg);
                    showloader();
                    window.location.reload();
                }
                else {
                    alert(data.Msg);
                }
            },
            error: function (data) {
                debugger;
                hideloader();
                alert('Error');
            }
        });
    }

}
function generatewaitinglist() {
    var a = true;//confirm('Are You Sure to Rollback of B.A. Recruitment  !!');
    if (a == true) {

        showloader();
        $.ajax({
            url: "/College/Home/Recruitmentgeneratewaitinglist",
            data: { CourseCategoryid: CourseCategoryid, Admissioncategoryid: Admissioncategoryid },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (data) {
                console.log(data);
                debugger;
                hideloader();
                if (data.status == true) {
                    alert(data.Msg);
                    showloader();
                    window.location.reload();
                }
                else {
                    alert(data.Msg);
                }
            },
            error: function (data) {
                debugger;
                hideloader();
                alert('Error');
            }
        });
    }

}