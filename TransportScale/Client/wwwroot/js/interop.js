function showDemo() {
    if ($('#inputCarBrand').val()) {
        $("#buttonAccept").attr("disabled", "true")
        $("#buttonSave").attr("disabled", "true")
    }
}

function endWeighing() {
    $("#buttonSave").removeAttr("disabled");
    $('#inputWeighing').css('background-color', '#32CD32');
}

function weighingOff() {
    $("#buttonWeghing").attr("disabled", "true");
}

function saveWeighing() {
    $("#buttonAccept").removeAttr("disabled");
    $("#buttonWeghing").removeAttr("disabled");
    $('#inputWeighing').val('');
    $('#inputCarBrand').val('');
    $('#inputCarPlate').val('');
    $('#inputCargo').val('');
    $('#inputWeighing').css('background-color', '#008080');
    $('#modalsave').show();
}


function showModal() {
    $('#modalsave').show();
}

function closeModal() {
    $('#modalsave').hide();
}

function errorShow() {
    $('#modalError').show();
}

function hideError() {
    $('#modalError').hide();
}

function delHide() {
    $('#modalDelete').hide();
}

function demo1() {
    alert("demo");
}

function test(value) {
    $("#modalDelete").show();
}

