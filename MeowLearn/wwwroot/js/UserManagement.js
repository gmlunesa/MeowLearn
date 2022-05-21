
disableButton("SaveSelectedUsers", true);
document.getElementById("UsersCheckList").innerHTML = "";

const selectElement = document.querySelector('#CategoryId');

selectElement.addEventListener('change', async (event) => {

    let selectedCategoryId = event.target.value;

    if (selectedCategoryId != 0) {

        try {
            let url = `/Admin/UserManagement/GetUserCategoriesPerCategory?categoryId=${selectedCategoryId}`;

            let data = await getRequest(url);

            document.getElementById("UsersCheckList").innerHTML = await data;
            disableButton("SaveSelectedUsers", false);

        } catch (error) {
            ShowDismissibleAlert("alert_usermgmt", "danger", "Oops, we encountered an error!");
        }

    }
    else {
        document.getElementById("UsersCheckList").innerHTML = "";
        disableControls("SaveSelectedUsers", true);
    }


});

let saveButtonElement = document.getElementById('SaveSelectedUsers');

saveButtonElement.addEventListener('click', async (event) => {
    let url = "/Admin/UserManagement/SaveSelectedUsers";
    let categoryId = document.getElementById("CategoryId").value;
    let antiForgeryToken = document.querySelector('input[name="__RequestVerificationToken"]').value;

    let usersSelected = Array.from(document.querySelectorAll('#UsersSelected:checked')).map(item => ({
        Id: item.value
    }));

    let usersSelectedInput = {
        __RequestVerificationToken: antiForgeryToken,
        CategoryId: parseInt(categoryId),
        UsersSelected: usersSelected
    }

    ShowDismissibleAlert("alert_usermgmt", "primary", "Saving...");

    // Disable Save button
    disableControls("SaveSelectedUsers", true);

    try {

        $.ajax({
            type: "POST",
            url: url,
            data: usersSelectedInput,
            success: (data) => {
                document.getElementById("UsersCheckList").innerHTML = data;

                ShowDismissibleAlert("alert_usermgmt", "success", "Saved successfully.");
                disableButton("SaveSelectedUsers", false);
                disableCheckboxes(false);
            },
            error: (xhr, ajaxOptions, thrownError) => {
                let errorBody = `Oops! Received a ${xhr.status} error — ${xhr.statusText}`;

                ShowDismissibleAlert("alert_usermgmt", "danger", errorBody);
                disableControls("SaveSelectedUsers", false);

                console.error(thrownError + "\r\n" + xhr.statusText + "\r\n" + xhr.responseText);
            }
        });


    } catch (error) {
        ShowDismissibleAlert("alert_usermgmt", "danger", "Oops, we encountered an error!");
        disableControls("SaveSelectedUsers", false);
    }

});


const getRequest = async (url) => {
    let response = await fetch(`${window.location.origin}${url}`);
    let data = await response.text();
    
    return data;
};

const postRequest = async (url, requestBody) => {
    let response = await fetch(`${window.location.origin}${url}`, {
        headers: { "Content-Type": "application/x-www-form-urlencoded; charset=utf-8" },
        method: 'POST',
        credentials: 'include',
        body: requestBody
    });

    let data = await response.text();

    return data;
}

