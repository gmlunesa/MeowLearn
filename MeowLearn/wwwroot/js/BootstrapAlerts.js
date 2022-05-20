function ShowDismissibleAlert(alertId, alertType = "info", alertBody) {
    let alertHtml = `<div class="alert alert-${alertType} alert-dismissible fade show" role="alert">
                        ${alertBody}
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                        </button>
                    </div>`;

    $(`#${alertId}`).html(alertHtml);
}

function CloseAlert(alertId) {
    $(`#${alertId}`).html("");
}