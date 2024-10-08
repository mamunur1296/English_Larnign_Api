import { notification } from '../Utility/notification.js';
import { createActionButtons, displayNotification, initializeDataTable, loger, resetValidation, showCreateModal, showExceptionMessage } from '../utility/helpers.js';
import { SendRequest, populateDropdown } from '../utility/sendrequestutility.js';

$(document).ready(async function () {
    loger("This is menu");
    debugger
});
$('#CreateAuthorizationBtn').click(async () => {
    debugger
    showCreateModal('AuthorizationModelCreate', 'btnSave', 'btnUpdate');
    await populateDropdown('/Menu/GetAll', '#MenuDropdown', 'id', 'name', "Select Ment");
});

$('#btnSaveMenu').off("click").click(async () => {

    try {
        // Create FormData object to handle complex structures
        const formData = new FormData($('#AssignActionsForm')[0]);
        // Optionally: Convert FormData to a JSON object if needed
        const formObject = {};
        formData.forEach((value, key) => {
            if (!formObject[key]) {
                formObject[key] = value;
            } else {
                if (!Array.isArray(formObject[key])) {
                    formObject[key] = [formObject[key]];
                }
                formObject[key].push(value);
            }
        });
        debugger;
        const result = await SendRequest({
            endpoint: '/Authorization/SaveRoleMenuMapping',
            method: 'POST',
            data: formData,

        });
        debugger;
        if (result.success && result.status === 201) {
            $('#AuthorizationModelCreate').modal('hide');
            notification({ message: "SubMenu Updated successfully !", type: "success", title: "Success" });
           
        }
    } catch (error) {
        console.error('Error in click handler:', error);
        displayNotification({
            formId: '#AssignActionsForm',
            modalId: '#modelCreate',
            messageElementId: '#globalErrorMessage',
            message: 'Failed to save role menu mapping. Please try again.'
        });
    }
});
document.getElementById('selectAllMenus').addEventListener('change', function () {
    let checkboxes = document.querySelectorAll('.menu-checkbox, .submenu-checkbox, .action-checkbox');
    checkboxes.forEach(function (checkbox) {
        checkbox.checked = event.target.checked;
    });
});