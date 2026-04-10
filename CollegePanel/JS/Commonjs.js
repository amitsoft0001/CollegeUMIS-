function Onlynumericvalue(textbox) {
     textbox.value = textbox.value.replace(/[^0-9.]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}
function OnlyIntvalue(textbox) {
    textbox.value = textbox.value.replace(/[^0-9]/g, ''); textbox.value = textbox.value.replace(/(\..*)\./g, '$1');
}

function checkEmail(textbox) {
    debugger;
    var email = textbox.value;
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email)) {
        alert('Please provide a valid email address');
        // email.focus;
        $(textbox).val("");
        return false;
    }
    else {
    }
}