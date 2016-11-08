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

    $("#departmentBtn").prop("disabled", true);

    $(".departmentName").on("change", function () {
        entryCheck();
    });

    $(".departmentDescription").on("change", function () {
        entryCheck();
    });
    
    /**
     * Purpose: Checks to see if the input fields within the computer add form are null or !null.
     **/

    function validateAddComputerForm() {
        if (validateAddComputerFields()) {
            $("#computerAddBtn").removeAttr("disabled")
        } else {
            $("#computerAddBtn").attr("disabled", true)
        }
    }

    /**
     * Purpose: Checks to see if the input fields within the add computer view are null or !null.
     * Return:
     *     bool - if the computer add field is filled out completely
     **/
    function validateAddComputerFields() {
        return $(".computerAddSerialNumber").val().trim().length > 0 &&
               $(".computerAddMake").val().trim().length > 0 &&
               $(".computerAddModel").val().trim().length > 0;
    }

    /**
     * Purpose: Adds event listeners to the computer add form fields to validate the form
     **/
    (function addListenersToAddComputerFields() {
        var computerFieldItems = [
            $(".computerAddSerialNumber"),
            $(".computerAddMake"),
            $(".computerAddModel")
        ];

        computerFieldItems.forEach(field => {
            field.on("change", validateAddComputerForm);
        });
    })();
});
