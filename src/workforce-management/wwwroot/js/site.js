$(document).ready(function () {

    function entryCheck() {
        if ($(".departmentName").val() && $(".departmentDescription").val()) {
            $("#departmentBtn").prop("disabled", false)
        } else {
            $("#departmentBtn").prop("disabled", true)
        }
    }

    $("#departmentBtn").prop("disabled", true);

    $(".departmentName").on("change",function () {
        entryCheck();
    });

    $(".departmentDescription").on("change", function () {
        entryCheck();
    });

});

