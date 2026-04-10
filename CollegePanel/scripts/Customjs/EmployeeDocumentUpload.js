$(document).ready(function () {
   // alert(Id);
    var yearList = '';
    $.ajax({
        url: "/College/Home/GetYear/",
        cache: false,
        type: "POST",
        dataType: "json",       
        success: function (data) {
            $.each(data.data, function (key, value) {
                yearList += '<option value="' + value.ID + '">' + value.year + '</option>';
            });
           // console.log(yearList);
        }
    });
    $("#addRow").click(function (event) {
       // debugger;
        var html555 = '';
        var itemvalues = $('Select[name="Document"]').map(function () {
            return this.value
        }).get();
        if (itemvalues.length > 0) {
            for (var i = 0; i < itemvalues.length; i++) {
                if (itemvalues[i].length == 0) {
                    alert('Please Select Document before entering next Choice !!');
                    return;
                }
            }
        }
        var narrationvalues = $('input[name="Narration"]').map(function () {
            return this.value
        }).get();
        if (narrationvalues.length > 0) {
            for (var i = 0; i < narrationvalues.length; i++) {
                if (narrationvalues[i].length == 0) {
                    alert('Please Enter Document No before entering next Choice !!');
                    return;
                }
            }
        }
        var filevalues = $('input[name="photo"]').map(function () {
            return this.value
        }).get();
        if (filevalues.length > 0) {
            for (var i = 0; i < filevalues.length; i++) {
                if (filevalues[i].length == 0) {
                    alert('Please Upload Document File before entering next Choice !!');
                    return;
                }
            }
        }

        var res = $(this).val();
        var tr = $(this).closest("tr");
        var str = '';
        var DocumentID = $('select[name="Document"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < DocumentID.length; i++) {
            if (DocumentID[i] != "") {
                str += DocumentID[i] + ',';
            }
        }
        var id = $('#EID').val();
        showloader();
        $.ajax({
            url: "/College/Home/DocumentID_bindDynamic/",
            data: { res: str, id: id },
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
                hideloader();
                showloader();

               // console.log(result);
                var str = '';
                var saved = '';
                $.each(result, function (key, value) {
                    saved += '<option value="' + value.DocID + '">' + value.DocTitle + '</option>';
                });
                html555 += '<tr>';
                html555 += '<td><select class="form-control custom-select-value Document textdesign" id="Document" name="Document" required="required">';
                html555 += ' <option value="">--Select Document--</option>';
               html555 += '' + saved + '';
                html555 += '</select>';
                html555 += '</td>';
                html555 += '  <td><input type="text" class="form-control textdesign" name="Narration"  placeholder ="Narration" /></td>';
                html555 += '<td><input type="file" id="file" onchange="check(this);" name="photo"></td>'
                html555 += '  <td><input type="button" class="btn btn-primary " name="Remove" value="Remove" onclick="removeRow(this);" /></td>';
                html555 += '</tr>';
                $("#Documenttable").append(html555);
                tr.next('tr').find('.Document ').html(str);
                $('#addRow').removeAttr("style");
                $('#addRow').attr("style", "display:block");
                hideloader();
            },
            error: function () {
                hideloader();
                $('#addRow').removeAttr("style");
                $('#addRow').attr("style", "display:none");
                // alert("error")
            }
        });
    });
    $("#savedoc").click(function (event) {
        //debugger;
        var Documentitemvalues = $('Select[name="Document"]').map(function () {
            return this.value
        }).get();

        var narrationvalues = $('input[name="Narration"]').map(function () {
            return this.value
        }).get();
        var filevalues = $('input[name="photo"]').map(function () {
            return this.value
        }).get();
       
        var UserID = $("#UserList").val();
        if (UserID == "")
        {
            alert('Please Select User');
            return;
        }
        var Documentidlist = '';;
        var narrationList = '';
        var filevalueslist = '';
        var Compulsory1_subjectidlist = '';
        var Compulsory2_subjectidlist = '';
        if (Documentitemvalues.length > 0) {
            for (var i = 0; i < Documentitemvalues.length; i++) {
                if (Documentitemvalues[i].length == 0) {

                    alert('Please Fill All Above Document choice   !!');
                    return;

                }
                else {
                    Documentidlist += (Documentitemvalues[i] == "" ? "0" : Documentitemvalues[i]) + ',';

                }
                if (narrationvalues[i].length == 0) {

                    alert('Please Fill All Above Document Remarks  !!');
                    return;

                }
                else {
                    narrationList += (narrationvalues[i] == "" ? "0" : narrationvalues[i]) + ',';

                }
                if (filevalues[i].length == 0) {

                    alert('Please Fill All Above Document File  !!');
                    return;

                }
                else {
                    filevalueslist += (filevalues[i] == "" ? "0" : filevalues[i]) + ',';

                }
            }
        }
        //alert(narrationvalues);
        //alert(filevalues);
        //console.log(Documentidlist);
        //console.log(narrationList);
        //console.log(filevalueslist);
        if (window.FormData !== undefined) {

            var fileUpload = $('input[name="photo"]').map(function () {
                return this
            }).get();
            var fileData = new FormData();
            for (var l = 0 ; l < fileUpload.length; l++)
            {
                var files = fileUpload[l].files;
                for (var i = 0; i < files.length; i++) {
                    fileData.append("photo", files[i]);
                }
            }
           
            fileData.append('filevalueslist', filevalueslist);
            fileData.append('narrationList', narrationList);
            fileData.append('Documentidlist', Documentidlist);
            fileData.append('UserID', UserID);
            showloader();
            $.ajax({
                url: '/College/Home/save_Documentstype1',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (result) {
                   debugger;
                  //  console.log(result);
                    if (result.Status == true) {
                       
                        hideloader();
                        alert(result.Message);
                       // location.replace('/College/Home/DocumentListEmployee');
                        window.location = '/College/Home/DocumentListEmployee/?id=' + Id;
                    }
                    else {
                       
                        alert(result.Message);
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
     $("#addRowType2").click(function (event) {
        //debugger;
        var html555 = '';
        var itemvalues = $('Select[name="Documenttype"]').map(function () {
            return this.value
        }).get();
        if (itemvalues.length > 0) {
            for (var i = 0; i < itemvalues.length; i++) {
                if (itemvalues[i].length == 0) {
                    alert('Please Select Document  before entering next Choice !!');
                    return;
                }
            }
        }
        var narrationvalues = $('input[name="Narrationtype"]').map(function () {
            return this.value
        }).get();
        if (narrationvalues.length > 0) {
            for (var i = 0; i < narrationvalues.length; i++) {
                if (itemvalues[i] != "7") {
                    if (narrationvalues[i].length == 0) {
                        alert('Please Enter Document No before entering next Choice !!');
                        return;
                    }
                }

            }
        }
        var Yearvalues = $('Select[name="Year"]').map(function () {
            return this.value
        }).get();
        if (Yearvalues.length > 0) {
            for (var i = 0; i < Yearvalues.length; i++) {
                if (itemvalues[i]!='7'){
                if (Yearvalues[i].length == 0) {
                    alert('Please Select Passing Year  before entering next Choice !!');
                    return;
                }
                }
            }
        }
        var Gradevalues = $('Select[name="Grade"]').map(function () {
            return this.value
        }).get();
        if (Gradevalues.length > 0) {
            for (var i = 0; i < Gradevalues.length; i++) {
                if (itemvalues[i] != '7') {
                    if (Gradevalues[i].length == 0) {
                        alert('Please Enter Grade before entering next Choice !!');
                        return;
                    }
                }
            }
        }

       
        var filevalues = $('input[name="filetype"]').map(function () {
            return this.value
        }).get();
        if (filevalues.length > 0) {
            for (var i = 0; i < filevalues.length; i++) {
                if (filevalues[i].length == 0) {
                    alert('Please Upload Document File before entering next Choice !!');
                    return;
                }
            }
        }
        var res = $(this).val();
        var tr = $(this).closest("tr");
        var str = '';
        var DocumentID = $('select[name="Documenttype"]').map(function () {
            return this.value
        }).get();
        for (var i = 0; i < DocumentID.length; i++) {
            if (DocumentID[i] != "") {
                str += DocumentID[i] + ',';
            }
        }
        var id = $('#EID').val();
        
        showloader();
        $.ajax({
            url: "/College/Home/DocumentID_bindDynamictype2/",
            data: { res: str, id :id},
            cache: false,
            type: "POST",
            dataType: "json",
            success: function (result) {
                hideloader();
                showloader();
               
                var str = '';
                var saved = '';
              
                $.each(result, function (key, value) {
                    saved += '<option value="' + value.DocID + '">' + value.DocTitle + '</option>';
                });
               
                html555 += '<tr>';
                html555 += '<td><select class="form-control custom-select-value Documenttype textdesign" id="Documenttype" name="Documenttype" required="required">';
                html555 += ' <option value="">--Select Document--</option>';
                html555 += '' + saved + '';
                html555 += '</select>';
                html555 += '</td>';
                html555 += '  <td><input type="text" class="form-control textdesign" name="Narrationtype"  placeholder ="Narration" /></td>';
                html555 += '<td><select class="form-control custom-select-value Documenttype textdesign" id="Year" name="Year" required="required">';
                html555 += ' <option value="">--Select Passing Year--</option>';
                html555 += '' + yearList + '';
                html555 += '</select>';
                html555 += '</td>';
                html555 += '  <td><input type="text" class="form-control textdesign" name="Grade"  placeholder ="Grade" /></td>';
                html555 += '<td><input type="file" id="filetype" onchange="check(this);" name="filetype"></td>'
                html555 += '  <td><input type="button" class="btn btn-primary " name="Remove" value="Remove" onclick="removeRowtype2(this);" /></td>';
                html555 += '</tr>';
                $("#Documenttabletype2").append(html555);
                tr.next('tr').find('.Documenttype ').html(str);
                $('#addRowType2').removeAttr("style");
                $('#addRowType2').attr("style", "display:block");
                hideloader();
            },
            error: function () {
                hideloader();
                $('#addRowType2').removeAttr("style");
                $('#addRowType2').attr("style", "display:none");
                // alert("error")
            }
        });
    });
    $("#savedoctype2").click(function (event) {
       // debugger;
        var Documentitemvalues = $('Select[name="Documenttype"]').map(function () {
            return this.value
        }).get();

        var narrationvalues = $('input[name="Narrationtype"]').map(function () {
            return this.value
        }).get();
        var filevalues = $('input[name="filetype"]').map(function () {
            return this.value
        }).get();

        var Yearvalues = $('Select[name="Year"]').map(function () {
            return this.value
        }).get();
       
        var Gradevalues = $('input[name="Grade"]').map(function () {
            return this.value
        }).get();
       
        var UserID = $("#UserList").val();
        if (UserID == "") {
            alert('Please Select User');
            return;
        }
      //  debugger;
        var Documentidlist = '';;
        var narrationList = '';
        var filevalueslist = '';
        var Compulsory1_subjectidlist = '';
        var Compulsory2_subjectidlist = '';
        var YearvaluesList = '';
        var GradevaluesList='';
        if (Documentitemvalues.length > 0) {
            for (var i = 0; i < Documentitemvalues.length; i++) {
                if (Documentitemvalues[i].length == 0) {

                    alert('Please Fill All Above Document choice   !!');
                    return;

                }
                else {
                    Documentidlist += (Documentitemvalues[i] == "" ? "0" : Documentitemvalues[i]) + ',';

                }
                
                    if (narrationvalues[i].length == 0) {
                        if (Documentitemvalues[i] != "7") {
                        alert('Please Fill All Above Document Remarks  !!');
                        return;

                    }
                }
                    else {
                        narrationList += (narrationvalues[i] == "" ? "0" : narrationvalues[i]) + ',';

                    }
                   
                        if (Yearvalues[i].length == 0) {
                            if (Documentitemvalues[i] != '7') {
                                alert('Please Select All Above Passing Year !!');
                                return;
                            }
                        }
                        else {
                            YearvaluesList += (Yearvalues[i] == "" ? "0" : Yearvalues[i]) + ',';

                        }                      
                        if (Gradevalues[i].length == 0) {
                            if (Documentitemvalues[i] != '7') {

                                alert('Please Fill All Above Grade Choice   !!');
                                return;
                            }
                            }
                            else {
                                GradevaluesList += (Gradevalues[i] == "" ? "0" : Gradevalues[i]) + ',';

                            }
                if (filevalues[i].length == 0) {

                    alert('Please Fill All Above Document File  !!');
                    return;

                }
                else {
                    filevalueslist += (filevalues[i] == "" ? "0" : filevalues[i]) + ',';

                }
            }
        }
        //alert(narrationvalues);
        //alert(filevalues);
        //console.log(Documentidlist);
        //console.log(narrationList);
        //console.log(filevalueslist);
        if (window.FormData !== undefined) {

            var fileUpload = $('input[name="filetype"]').map(function () {
                return this
            }).get();
            var fileData = new FormData();
            for (var l = 0 ; l < fileUpload.length; l++) {
                var files = fileUpload[l].files;
                for (var i = 0; i < files.length; i++) {
                    fileData.append("filetype", files[i]);
                }
            }



            //var fileUpload2 = $("#sign").get(0);
            //var files2 = fileUpload2.files;
            //for (var i = 0; i < files2.length; i++) {
            //    fileData.append("sign", files2[i]);
            //}
            fileData.append('filevalueslist', filevalueslist);
            fileData.append('narrationList', narrationList);
            fileData.append('Documentidlist', Documentidlist);
            fileData.append('YearvaluesList', YearvaluesList);
            fileData.append('GradevaluesList', GradevaluesList);
            fileData.append('UserID', UserID);
            showloader();
            $.ajax({
                url: '/College/Home/save_Documentstype2',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (result) {
                    //debugger;
                    //console.log(result);
                    if (result.Status == true) {

                        hideloader();
                        alert(result.Message);
                        // location.replace('/College/Home/DocumentListEmployee');
                        window.location = '/College/Home/DocumentListEmployee/?id=' + Id;
                    }
                    else {

                        alert(result.Message);
                        hideloader();
                    }
                    return false;
                },
                error: function (err) {
                  //  debugger;
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
    //Edit
    $("#Edit").click(function (event) {
       // debugger;      
        var DocType = $("#DocType").val()
        var Narration = $("#Narration").val();;
        var editfile = $("#newfile").val();
        var hfile = $("#hfile").val();
        var UserID = $("#UserList").val();
        var ID = $("#hid").val();
        var Year = $("#Year").val();
        var Grade = $("#Grade").val();
        if (UserID == "") {
            alert('Please Select User');
            return;
        }
        if ($("#DocType").val() == "") {
            alert('Please Select  Document Type !!');
            $("#DocType").focus();
            return false;
        }
        if ($("#newfile").val() == "" && $("#hfile").val() == "") {
            alert('Please Upload Document File !!');
            $("#DocType").focus();
            return false;
        }
        if (DocType != '7') {
            if (Year == '') {
                alert('Please Select Year !!');
                $("#Year").focus();
                return false;
            }
        }
        if (DocType != '7') {
            if (Grade == '') {
                alert('Please Enter Grade !!');
                $("#Grade").focus();
                return false;
            }
        }
        if (Grade == undefined)
        {
            Grade = '';
        }
        if (Year == undefined) {
            Year = '';
        }

       
      
        if (window.FormData !== undefined) {

            var fileUpload = $("#newfile").get(0);
            var files = fileUpload.files;
            var fileData = new FormData();

            if (files != null)
            {
                for (var i = 0; i < files.length; i++) {
                    fileData.append("photo", files[i]);
                }
            }
           



           
            fileData.append('DocType', DocType);
            fileData.append('Narration', Narration);
            fileData.append('editfile', editfile);
            fileData.append('UserID', UserID);
            fileData.append('Grade', Grade);
            fileData.append('Year', Year);
            fileData.append('ID', ID);
            showloader();
            $.ajax({
                url: '/College/Home/Update_documentUpload',
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: fileData,
                success: function (result) {
                   // debugger;
                   // console.log(result);
                    if (result.Status == true) {

                        hideloader();
                        alert(result.Message);
                        window.location = '/College/Home/DocumentListEmployee/?id=' + Id;
                      // location.replace('/College/Home/DocumentListEmployee');
                    }
                    else {

                        alert(result.Message);
                        hideloader();
                    }
                    return false;
                },
                error: function (err) {
                   // debugger;
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
});

$('.Document').change(function () {

    
    var res = $(this).val();
    var alltr = $(this).closest('tr').nextAll('tr').find('.Subject').html('<option value=' + "" + '>' + "--Select Document--" + '</option>');

    var tr = $(this).closest("tr");
    var str = '';
    var DocumentID = $('select[name="Document"]').map(function () {

        return this.value
    }).get();
    for (var i = 0; i < DocumentID.length; i++) {
        if (DocumentID[i] != "") {
            str += DocumentID[i] + ',';
        }

    }
    showloader();

    $.ajax({
        url: "/College/Home/DocumentID_bindDynamic/",
        data: { res: str },
        cache: false,
        type: "POST",
        dataType: "json",
        success: function (result) {

            var str = '';
            str += '<option value=' + "" + '>' + "--Select Document--" + '</option>';
            for (var i = 0; i < result.length; i++) {

                str += '<option value=' + result[i].DocID + '>' + result[i].DocTitle + '</option>';


            }
            tr.next('tr').find('.Document ').html(str);

            hideloader();

        },
        error: function (errormessage) {
            hideloader();
            alert(errormessage.responseText);
        }

    });

});
check = function (photo) {

    var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png", ".pdf"];
    var arrInputs = document.getElementsByTagName("input");
   
    
 //  debugger;
    for (var i = 0; i < arrInputs.length; i++) {
        var oInput = arrInputs[i];
       
     
        if (oInput.type == "file") {
            var sFileName = oInput.value;
            if (oInput.files[0] != undefined) {
               var sFileNamesize = oInput.files[0].size;
            }
          
          
            
            if (sFileName.length > 0) {
                var blnValid = false;
                for (var j = 0; j < _validFileExtensions.length; j++) {
                    var sCurExtension = _validFileExtensions[j];
                    if (sFileName.substr(sFileName.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                        blnValid = true;                       
                    }
                }


                if (!blnValid) {
                    oInput.value = "";                 
                    alert("Sorry, file is invalid ");
                    return false;
                }
                else {
                    if (sFileNamesize > 512000) {
                        oInput.value = "";                      
                        alert("File is too big!");
                        break;
                    }
                }

            }
       
        }
    }

    return true;
}
function removeRow(oButton) {
    var empTab = document.getElementById('Documenttable');
    empTab.deleteRow(oButton.parentNode.parentNode.rowIndex);       
}
function removeRowtype2(oButton) {
    var empTab = document.getElementById('Documenttabletype2');
    empTab.deleteRow(oButton.parentNode.parentNode.rowIndex);       
}


