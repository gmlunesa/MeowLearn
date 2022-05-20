$(function () {
    $("#UserRegistrationModal input[name='AcceptUserAgreement']").click(onAcceptUserAgreementClick);
    $("#UserRegistrationModal button[name='register']").prop("disabled", true);

    function onAcceptUserAgreementClick() {
        if ($(this).is(":checked")) {
            $("#UserRegistrationModal button[name='register']").prop("disabled", false);
        }
        else {
            $("#UserRegistrationModal button[name='register']").prop("disabled", true);
        }
    }

    $("#UserRegistrationModal input[name='Email']").blur(() => {
        let email = $("#UserRegistrationModal input[name='Email']").val();

        let url = `UserAuth/EmailExists?email=${email}`;

        $.ajax({
            type: "GET",
            url: url,
            success: (data) => {
                if (data) {
                    ShowDismissibleAlert("alert_register", "warning", "Email is already registered.");
                    //let alertHtml = `<div class="alert alert-warning alert-dismissible fade show" role="alert">
                    //                    Email is already registered.
                    //                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    //                    <span aria-hidden="true">&times;</span>
                    //                    </button>
                    //                </div>`;
                    //$("#alert_register").html(alertHtml);
                }
                else {
                    CloseAlert("alert_register");
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                let errorBody = `Oops! Received a ${xhr.status} error — ${xhr.statusText}`;

                ShowDismissibleAlert("alert_register", "danger", errorBody);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        })
    });
        
    let registerUserButton = $("#UserRegistrationModal button[name='register']").click(onUserRegisterClick);

    function onUserRegisterClick() {
        let url = "UserAuth/RegisterUser";

        let antiForgeryToken = $("#UserRegistrationModal input[name='__RequestVerificationToken']").val();

        let email = $("#UserRegistrationModal input[name='Email'").val();
        let password = $("#UserRegistrationModal input[name='Password'").val();
        let confirmPassword = $("#UserRegistrationModal input[name='ConfirmPassword'").val();
        let firstName = $("#UserRegistrationModal input[name='FirstName'").val();
        let lastName = $("#UserRegistrationModal input[name='LastName'").val();
        let address = $("#UserRegistrationModal input[name='Address'").val();
        let zipCode = $("#UserRegistrationModal input[name='ZipCode'").val();
        let phoneNumber = $("#UserRegistrationModal input[name='PhoneNumber'").val();

        let user = {
            __RequestVerificationToken: antiForgeryToken,
            Email: email,
            Password: password,
            ConfirmPassword: confirmPassword,
            FirstName: firstName,
            LastName: lastName,
            Address: address,
            ZipCode: zipCode,
            PhoneNumber: phoneNumber,
            AcceptUserAgreement: true,
        }

        $.ajax({
            type: "POST",
            url: url,
            data: user,
            success: (data) => {
                let parsed = $.parseHTML(data);

                let hasErrors = $(parsed).find("input[name='RegistrationInvalid']").val() === 'true';

                if (hasErrors == true) {
                    $("#UserRegistrationModal").html(data);

                    // Rewire button as we refresh modal
                    let userRegisterButton = $("#UserRegistrationModal button[name='register'").click(onUserRegisterClick);

                    $("#UserRegistrationForm").removeData("validator");
                    $("#UserRegistrationForm").removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse("#UserRegistrationForm");
                }
                else {
                    location.href = "/Home/Index";
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                let errorBody = `Oops! Received a ${xhr.status} error — ${xhr.statusText}`;

                ShowDismissibleAlert("alert_register", "danger", errorBody);
                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        })
    }


}
);