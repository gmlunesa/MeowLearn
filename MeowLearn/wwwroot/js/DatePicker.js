$(function () {
    function ConfigDatePicker() {
        const monthAllowance = 2;
        var currDate = new Date();

        $(".datepicker").datepicker({
            dateFormat: "yy-mm-dd",
            minDate: computeMonths(currDate, (-monthAllowance)),
            maxDate: computeMonths(currDate, monthAllowance)
        });
    }
    ConfigDatePicker();
});