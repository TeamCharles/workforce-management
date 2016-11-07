// Write your Javascript code.
$(document).ready(function () {
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
});