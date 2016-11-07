$(document).ready(function () {

    /**
     * Purpose: Checks to see if the input fields within the department are null or !null.
     * Arguments:
     *    N/A
     * Return:
     *     N/A
     **/
    function entryCheck() {
        if ($(".departmentName").val() && $(".departmentDescription").val()) {
            $("#departmentBtn").prop("disabled", false)
        } else {
            $("#departmentBtn").prop("disabled", true)
        }
    }

    $(".departmentName").on("change",entryCheck);

    $(".departmentDescription").on("change", entryCheck);

});

