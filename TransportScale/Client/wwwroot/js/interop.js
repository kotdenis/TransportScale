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

function saveWeighing() {
    $("#buttonAccept").removeAttr("disabled");
    $('#inputWeighing').val('');
    $('#inputCarBrand').val('');
    $('#inputCarPlate').val('');
    $('#inputCargo').val('');
    $('#inputWeighing').css('background-color', '#008080');
    $('#modalsave').show();
}

function closeModal() {
    $('#modalsave').hide();
}

function test(value) {
    alert(value);
}

function showTest() {

}