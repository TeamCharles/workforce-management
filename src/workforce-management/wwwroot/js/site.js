$(document).ready(function () {
    if (location.pathname.includes("Edit")) {
        checkEmployeeEditStatus();
        $("#employeeEdit input").on("change", checkEmployeeEditStatus);
        $("#employeeEdit select").on("change", checkEmployeeEditStatus);
        function checkEmployeeEditStatus() {
            if ($("#Employee_FirstName").val() && $("#Employee_LastName").val() && $("#Employee_StartDate").val() && $("#Employee_DepartmentId").val()) {
                $("#employeeSubmit").attr("disabled", false);
            } else {
                $("#employeeSubmit").attr("disabled", true);
            }
        }
    } else {
        checkEmployeeFormStatus();
        $("#employeeAdd input").on("change", checkEmployeeFormStatus);
        $("#employeeAdd select").on("change", checkEmployeeFormStatus);

        // Checks whether all fields on the Employee Add/Edit form have been filled in
        function checkEmployeeFormStatus() {
            if ($("#Employee_FirstName").val() && $("#Employee_LastName").val() && $("#Employee_StartDate").val() && $("#Employee_DepartmentId").val() && $("#Employee_ComputerId").val()) {
                $("#employeeSubmit").attr("disabled", false);
            } else {
                $("#employeeSubmit").attr("disabled", true);
            }
        }
    }

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

    $(".departmentName").on("change", entryCheck);

    $(".departmentDescription").on("change", entryCheck);
    
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
