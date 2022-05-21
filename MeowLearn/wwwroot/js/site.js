// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const disableCheckboxes = (disable) => {
    Array.from(document.querySelectorAll("#UsersSelected"))
        .forEach(checkbox => checkbox.disabled = disable);
}

const disableButton = (tag, disable) => {
    document.getElementById(tag).disabled = disable;
}

const disableControls = (buttonTag, disable) => {
    disableCheckboxes(disable);
    disableButton(buttonTag, disable)
}
