function uptest () {
    alert("test");
}

function savetest() {
    $('#modalsave').show();
}

function deltest() {
    $('#modaldel').show();
}

function delhide() {
    $('#modaldel').hide();
}

function weighthide() {
    $('#buttonWeghing').hide();
    $('#buttonSave').hide();
}

function weightshow() {
    if ($('#inputCarBrand').val()) {
        $('#buttonWeghing').show();
    }
}