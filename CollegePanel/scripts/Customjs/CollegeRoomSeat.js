$(document).ready(function () {

    $("#addRow").click(function (event) {
        debugger;
        var str = "";
        var c = $('#table tbody tr.MyClass').length;
        
        var roomid = $('input[name="roomid"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < roomid.length; i++) {
            if (roomid[i] != "") {
                str = str + roomid[i] + ',';
            }

        }
        var RoomNoIDval = $('Select[name="RoomNoID"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < RoomNoIDval.length; i++) {
            if (RoomNoIDval[i] != "") {
                str = str + RoomNoIDval[i] + ',';
            }

        }
        var RoomRowVal = $('input[name="RoomRow"]').map(function () {
            return this.value
        }).get();
        var RoomColumnVal = $('input[name="RoomColumn"]').map(function () {
            return this.value
        }).get();
        if (RoomNoIDval.length > 0) {
            for (var i = 0; i < RoomNoIDval.length; i++) {
                if (RoomNoIDval[i].length == 0) {
                    alert('Please Select Room No before entering next Choice !!');
                    return;
                }
            }
        }        

        for (var i = 0; i < RoomNoIDval.length; i++) {
            if (RoomNoIDval[i] != "") {
                str = str + RoomNoIDval[i] + ',';
            }

        }

        
        if (RoomRowVal.length > 0) {
            for (var i = 0; i < RoomRowVal.length; i++) {
                if (RoomRowVal[i].length == 0) {
                    alert('Please enter No of rows entering next Choice !!');
                    return;
                }
            }
        }
        if (RoomColumnVal.length > 0) {
            for (var i = 0; i < RoomColumnVal.length; i++) {
                if (RoomColumnVal[i].length == 0) {
                    alert('Please enter No of columns entering next Choice !!');
                    return;
                }
            }
        }
        var j = parseInt(c) + 1;

        var html555 = '';
        $('#divtableinsert').removeAttr("style");
        $("#divtableinsert").attr("style", "display: block;");
        showloader();
        $.ajax({
            url: "/College/Home/RoomSeatlist/",
            data: { id: str },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
                hideloader();
                showloader();
                var saved = '';
                //console.log(result);
                $.each(result, function (key, value) {
                    saved += '<option value="' + value.ID + '">' + value.RoomNo + '</option>';
                });
                // debugger;
                html555 += '<tr class="MyClass"><td class="sno">' + j + '</td>';
                html555 += '<td><select class="form-control custom-select-value RoomNoID " id="RoomNoID" name="RoomNoID" required="required" style="width:200px;">';
                html555 += ' <option value="">--Select Room No--</option>';
                html555 += '' + saved + '';
                html555 += '</select>';
                html555 += '</td>';
                html555 += '  <td><input type="text" class="form-control textdesign" name="Description" id="Description" placeholder ="Description" readonly="readonly" /></td>';
                html555 += '  <td><input type="text" class="form-control textdesign RoomRow" name="RoomRow" id="RoomRow" placeholder ="Rows"  oninput = "Onlynumericvalue(this);" maxlength = "5"/></td>';
                html555 += '  <td><input type="text" class="form-control textdesign RoomColumn" name="RoomColumn" id="RoomColumn" placeholder ="Column" oninput = "Onlynumericvalue(this);" maxlength = "5"/></td>';
                html555 += '  <td><input type="text" class="form-control textdesign Capacity" name="Capacity" id="Capacity" placeholder ="Capacity" readonly="readonly"/></td>';
                html555 += '  <td><input type="button" class="btn btn-primary " name="Remove" value="Remove" onclick="removeRow(this);" /><input type="hidden" class="form-control " name="Roomid"  placeholder ="" value=""  /></td>';

                html555 += '</tr>';


                $("#tbodyins").append(html555);

                $('#newbtn').removeAttr("style");
                $('#newbtn').attr("style", "display:block");
                $('#savebtn').removeAttr("style");
                $('#savebtn').attr("style", "display:block");
                hideloader();
            },
            error: function () {
                hideloader();
                $('#divtableinsert').removeAttr("style");
                $("#divtableinsert").attr("style", "display: none;");               // alert("error")
                $('#newbtn').removeAttr("style");
                $('#newbtn').attr("style", "display:none");
                $('#savebtn').removeAttr("style");
                $('#savebtn').attr("style", "display:none");
            }
        });
    });
    $("#save").click(function (event) {
        var str = "";
        debugger;
        var RoomNoIDval = $('Select[name="RoomNoID"]').map(function () {
            return this.value
        }).get();

        var RoomRowVal = $('input[name="RoomRow"]').map(function () {
            return this.value
        }).get();
        var RoomColumnVal = $('input[name="RoomColumn"]').map(function () {
            return this.value
        }).get();
        var RoomCapacityVal = $('input[name="Capacity"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < RoomRowVal.length; i++) {
            if (RoomNoIDval[i] == "0") {
                return;
            }
            if (RoomRowVal[i] == "0") {
                return;
            }
            if (RoomColumnVal[i] == "0") {
                return;
            }
            if (RoomCapacityVal[i] == "0") {
                return;
            }
        }
        var Roomidlist = '';;
        var RowList = '';
        var ColList = '';
        var CapacityList = '';
        for (var i = 0; i < RoomNoIDval.length; i++) {
            if (RoomNoIDval[i] != "") {
                str = str + RoomNoIDval[i] + ',';
            }

        }
        if (RoomNoIDval.length > 0) {
            for (var i = 0; i < RoomNoIDval.length; i++) {
                if (RoomNoIDval[i].length == 0) {
                    alert('Please Select All Room No !!');
                    return;
                }
                else {
                    Roomidlist += (RoomNoIDval[i] == "" ? "0" : RoomNoIDval[i]) + ',';
                }
                if (RoomRowVal[i].length == 0) {

                    alert('Please Fill All Row values   !!');
                    return;

                }
                else {
                    RowList += (RoomRowVal[i] == "" ? "0" : RoomRowVal[i]) + ',';

                }
                if (RoomColumnVal[i].length == 0) {

                    alert('Please Fill All Column values   !!');
                    return;

                }
                else {
                    ColList += (RoomColumnVal[i] == "" ? "0" : RoomColumnVal[i]) + ',';

                }
                if (RoomCapacityVal[i].length == 0) {

                    alert('Please Fill All Capacity values   !!');
                    return;

                }
                else {
                    CapacityList += (RoomCapacityVal[i] == "" ? "0" : RoomCapacityVal[i]) + ',';

                }

            }
        }


        if (window.FormData !== undefined) {

            var fromData = new FormData();
            fromData.append('Roomidlist', Roomidlist);
            fromData.append('RowList', RowList);
            fromData.append('ColList', ColList);
            fromData.append('CapacityList', CapacityList);

            showloader();
            $.ajax({
                url: '/College/Home/SaveRoomSeats',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fromData,
                success: function (result) {
                    // debugger;
                    //  console.log(result);
                    if (result.Status == true) {

                        hideloader();
                        alert(result.Msg);
                        location.replace('/College/Home/CollegeRoomSeat');
                    }
                    else {

                        alert(result.Msg);
                        hideloader();
                    }
                    return false;
                },
                error: function (err) {
                    //debugger;
                    alert(err.statusText);
                    hideloader();
                    return false;
                }
            });
        }
        else {
            alert("FormData is not supported.");

            return false;
        }


        return;
    });
   
        $('.RColumn').change(function () {
        debugger;
        var col = $("#RColumn").val();
        var row = $("#RRow").val();
      
        if (col == "0" || col == "") {
            alert('Please Enter No Of column !!!');
            //$("#RRow").val('');
            $(this).val('');
            $(this).focus();
            $("#RCapacity").val('');
            return;
        }
        else if (row == "0" || row == "") {
            alert('Please Enter No Of Row !!!');
            //$("#RColumn").val('');
            $(this).val('');
            $("#RRow").focus();
            $("#RCapacity").val('');
            return;
        }
        else {
            var capacity = (parseInt(col) * parseInt(row));

           $("#RCapacity").val(capacity);
        }


    });
});
$(document).on('change', '.RoomNoID', function () {
    //$('Select[name="RoomNoID"]').change(function (event) {
    debugger;
    var tr = $(this).closest("tr");
    var tm = tr.find("#Description");
    var res = $(this).val();
    if (res == "") {
        res = 0;
        tm.val('');
        return;

    }
   
  
   
    $.ajax({
        url: "/College/Home/RoomSeatDescription/",
        data: { id: res },
        cache: false,
        type: "POST",
        dataType: "json",
        headers: { 'Authorization': 'Basic ' + btoa(username + ':' + password) },
        success: function (data) {
            debugger;
            //if(data.Status)
            //console.log(data);
            tm.val(data.Description);
        }
    });
});
$(document).on('change', '.RoomColumn', function () {
//$('.RoomColumn').change(function () {
    debugger;
    var tr = $(this).closest("tr");
    var row = tr.find("#RoomRow").val();
    var col = tr.find("#RoomColumn").val();
    if ($(this).val() == "0" || $(this).val() == "") {
        alert('Please Enter No Of column !!!');
       // tr.find("#RoomRow").val('');
        $(this).val('');
        $(this).focus();
        tr.find("#Capacity").val('');
        return;
    }
    //else if (row == "0" || row == "") {
    //    alert('Please Enter No Of Row !!!');
    //    tr.find("#RoomRow").val('');
    //    $(this).val('');
    //    tr.find("#RoomRow").focus();
    //    return;
    //}
    else {
        var capacity = 0;
        if (row == "")
            capacity = 0;
        else
            capacity = (parseInt(col) * parseInt(row));

        tr.find("#Capacity").val(capacity);
    }


});

$(document).on('change', '.RoomRow', function () {
    //$('.RoomColumn').change(function () {
    debugger;
    var tr = $(this).closest("tr");
    var row = tr.find("#RoomRow").val();
    var col = tr.find("#RoomColumn").val();
    //if (col == "0" || col == "") {
    //    alert('Please Enter No Of column !!!');
    //    tr.find("#RoomRow").val('');
    //    $(this).val('');
    //    $(this).focus();
    //    tr.find("#Capacity").val('');
    //    return;
    //}
    //else
    if (row == "0" || row == "") {
        alert('Please Enter No Of Row !!!');
        //tr.find("#RoomRow").val('');
        $(this).val('');
       $(this).focus();
        tr.find("#Capacity").val('');
        return;
    }
    else {
        var capacity = 0;
        if (col == "")
            capacity = 0;
        else
         capacity = (parseInt(col) * parseInt(row));

        tr.find("#Capacity").val(capacity);
    }


});
    function removeRow(oButton) {
        var empTab = document.getElementById('table');
        empTab.deleteRow(oButton.parentNode.parentNode.rowIndex);
    }
    $(document).on('click', '.myBtn', function () {
        debugger;
        var id = $(this).data("id");
        var RoomMasterID = $(this).data("roommasterid");
        var Row = $(this).data("row");
        var Column = $(this).data("column");
        var Capacity = $(this).data("capacity");
        var RoomNo = $(this).data("roomno");
        var Description = $(this).data("description");
       // alert(Description);
        $("#RoomSeat").modal();
        if (id == "") {
            $("#RoomNo").val('');
            $("#Description").val('');
            $("#RRow").val('');
            $("#RColumn").val('');
            $("#RCapacity").val('');      
            
            return;
        }
        else {
            $("#RoomNo").val(RoomNo);
            $("#Description").val(Description);
            $("#RRow").val(Row);
            $("#RColumn").val(Column);
            $("#RCapacity").val(Capacity);
            $("#hid").val(id);
            $("#RoomID").val(RoomMasterID);
            
        }


    });

   



function updatefee() {
    //debugger;
    if ($("#Description").val() == "") {
        alert("Please Enter Description !!");
        $("#Description").focus();
        return;
    }
    if ($("#RRow").val() == "") {
        alert("Please Enter Row !!");
        $("#RRow").focus();
        return;
    }
    if ($("#RColumn").val() == "") {
        alert("Please Enter Column !!");
        $("#RColumn").focus();
        return;
    }
    if ($("#RCapacity").val() == "") {
        alert("Please Enter Capacity !!");
        $("#RCapacity").focus();
        return;

    }
   
    var ID = 0;
    if ($("#hid").val() == "") {
        alert("Network Error !!");
        $("#Description").focus();
        return;
    }
    else {
        ID = $("#hid").val();
    }

    var obj = {
        ID: ID,
        Description: $("#Description").val(),
        Row: $("#RRow").val(),
       Column: $("#RColumn").val(),
       Capacity: $("#RCapacity").val(),
       RoomMasterID: $("#RoomMasterID").val(),
    }


    showloader();
    $.ajax({
        url: '/College/Home/UpdateRoomSeat',
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {

            hideloader();
            // console.log(result);
            if (result.Status == true) {
                alert(result.Msg);
                showloader();
                window.location.reload();
            }
            else {
                alert(result.Msg);
            }
            return false;
        },
        error: function (err) {

            hideloader();
            alert(err.statusText);
            return false;

        }
    });
}

function confirm1(id) {
    var a = confirm('Are you sure you want to delete this record !!');
    var txt;
    if (a) {
        window.location = "/Administrator/Home/deleteAdmissionFee/" + id + "";
        return true;

    }
    else {
        return false;
    }
}


