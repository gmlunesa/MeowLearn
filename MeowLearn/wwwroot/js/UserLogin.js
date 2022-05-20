$(function () {
    let userLoginButton = $("#UserLoginModal button[name='login']").click(onUserLoginClick);

    function onUserLoginClick() {
        let url = "UserAuth/Login";
        let antiForgeryToken = $("#UserLoginModal input[name='__RequestVerificationToken'").val();

        let email = $("#UserLoginModal input[name='Email'").val();
        let password = $("#UserLoginModal input[name='Password'").val();
        let rememberMe = $("#UserLoginModal input[name='RememberMe'").prop('checked');

        let userInput = {
            __RequestVerificationToken: antiForgeryToken,
            Email: email,
            Password: password,
            RememberMe: rememberMe
        };

        $.ajax({
            type: "POST",
            url: url,
            data: userInput,
            success: (data) => {
                // Parse response received from UserAuthController.Login
                let parsed = $.parseHTML(data);

                let hasErrors = $(parsed).find("input[name='LoginInvalid']").val() === "true";

                if (hasErrors == true) {
                    $("#UserLoginModal").html(data);

                    userLoginButton = $("#UserLoginModal button[name='login'").click(onUserLoginClick);

                    let form = $("#UserLoginForm");

                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);
                }
                else {
                    location.href = "Home/Index";
                }
            },
            error: (xhr, ajaxOptions, thrownError) => {
                let errorBody = `Oops! Received a ${xhr.status} error — ${xhr.statusText}`;

                ShowDismissibleAlert("alert_login", "danger", errorBody);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });
    }
});