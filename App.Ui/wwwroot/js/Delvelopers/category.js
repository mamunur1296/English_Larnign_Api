import { SendRequest } from '../Utility/SendRequestUtility.js';
import { clearMessage, createActionButtons, initializeDataTable, loger, resetFormValidation, resetValidation, showCreateModal } from '../Utility/helpers.js';
import { notification } from '../Utility/notification.js';
// Dynamically define controller and form elements
const controllerName = "Category";
const createButton = `#Create${controllerName}Btn`;
const formName = `#${controllerName}Form`;
const modalCreateId = `#${controllerName}ModelCreate`;
const saveButtonId = `#btnSave${controllerName}`;
const updateButtonId = `#btnUpdate${controllerName}`;
const dataTableId = `${controllerName}Table`;
const deleteBtn = '#btnDelete';
const deleteModelId = `#delete${controllerName}Model`;
const endpointCreate = `/${controllerName}/Create`;
const endpointGetAll = `/${controllerName}/GetAll`;
const endpointGetById = `/${controllerName}/GetById/`;
const endpointUpdate = `/${controllerName}/Update/`;
const endpointDelete = `/${controllerName}/Delete`;
$(document).ready(async function () {
    await initGitAllList();
    await CreateCategoryBtn(createButton);
});
const initGitAllList = async () => {
    await getCategoryList();
}
// Dynamically fetch data for verbs or other entities
const getCategoryList = async () => {
    try {
        const result = await SendRequest({ endpoint: endpointGetAll });
        if (result.success) {
            await onSuccessEntities(result.data);
            debugger
        } else {
            console.error(`Failed to retrieve ${controllerName} list:`, result.message);
        }
    } catch (error) {
        console.error(`Error fetching ${controllerName} list:`, error);
    }
};

// Handle success and render the entity list
const onSuccessEntities = async (entities) => {

    debugger
    const entitiesList = entities.map((entity) => {
        if (entity) {
            return {
                id: entity?.id,
                name: entity?.name,
            };
        }
        return null;
    }).filter(Boolean);
    debugger
    const entitySchema = [
        { data: null, title: 'Name', render: (data, type, row) => row.name || "N/A" },
        {
            data: null, title: 'Action', render: (data, type, row) => createActionButtons(row, [
                { label: 'Edit', btnClass: 'btn-primary', callback: `update${controllerName}` },
                { label: 'Delete', btnClass: 'btn-danger', callback: `delete${controllerName}` }
            ])
        }
    ];
    debugger
    await initializeDataTable(entitiesList, entitySchema, dataTableId);

};



// Initialize validation
const InitializegetCategoryvalidation = $(formName).validate({
    onkeyup: function (element) {
        $(element).valid();
    },
    rules: {
        Name: {
            required: true,
        }
    },
    messages: {
        Name: {
            required: "Name is required.",
        }
    },
    errorElement: 'div',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});

export const CreateCategoryBtn = async (CreateBtnId) => {
    //Sow Create Model
    $(CreateBtnId).off('click').click(async (e) => {
        e.preventDefault();
        resetFormValidation(formName, InitializegetCategoryvalidation);
        $('#myModalLabelUpdate').hide();
        $('#myModalLabelAdd').show();
        debugger
        showCreateModal(modalCreateId, saveButtonId, updateButtonId);
    });
}
//Save Button


$(saveButtonId).off('click').click(async (e) => {
    e.preventDefault();
    debugger
    try {
        if ($(formName).valid()) {
            const formData = $(formName).serialize();
            const result = await SendRequest({ endpoint: endpointCreate, method: 'POST', data: formData });
            // Clear previous messages
            $('#successMessage').hide();
            $('#UserError').hide();
            $('#EmailError').hide();
            $('#PasswordError').hide();
            $('#GeneralError').hide();
            debugger
            if (result.success && result.status === 201) {
                $(modalCreateId).modal('hide');
                notification({ message: `${controllerName} Created successfully !`, type: "success", title: "Success" });
                await initGitAllList();
            }
        }
    } catch (error) {
        console.error('Error in click handler:', error);
        $(modalCreateId).modal('hide');
        notification({ message: ` ${controllerName} Created failed . Please try again.`, type: "error", title: "Error" });
    }

});




window.updateCategory = async (id) => {
    resetFormValidation(formName, InitializegetCategoryvalidation);
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $(formName)[0].reset();

    const result = await SendRequest({ endpoint: endpointGetById + id });
    if (result.success) {
        $(saveButtonId).hide();
        $(updateButtonId).show();
        //update and buind Entity Id
        $('#Name').val(result.data.name);
        $('#BanglaName').val(result.data.banglaName);
        $('#BaseForm').val(result.data.baseForm);
        $('#ThirdPersonSingular').val(result.data.thirdPersonSingular);
        $('#PastSimple').val(result.data.pastSimple);
        $('#PastParticiple').val(result.data.pastParticiple);
        $('#PresentParticiple').val(result.data.presentParticiple);
        $('#Gerund').val(result.data.gerund);

        $(modalCreateId).modal('show');
        resetValidation(InitializegetCategoryvalidation, formName);
        $(updateButtonId).off('click').on('click', async (e) => {
            e.preventDefault();
            debugger
            const formData = $(formName).serialize();
            const result = await SendRequest({ endpoint: endpointUpdate + id, method: "PUT", data: formData });
            if (result.success) {
                $(modalCreateId).modal('hide');
                notification({ message: `${controllerName} Updated successfully !`, type: "success", title: "Success" });

                await initGitAllList();
            } else {
                $(modalCreateId).modal('hide');
                notification({ message: `${controllerName} Updated failed . Please try again. !`, type: "error", title: "Error" });
            }
        });
    }
    loger(result);
}

////////window.showDetails = async (id) => {
////////    loger("showDetails id " + id);
////////}




window.deleteCategory = async (id) => {
    clearMessage('successMessage', 'globalErrorMessage');
    debugger;
    $(deleteModelId).modal('show');
    $(deleteBtn).off('click').on('click', async (e) => {
        e.preventDefault();
        debugger;
        const result = await SendRequest({ endpoint: endpointDelete, method: "DELETE", data: { id: id } });

        if (result.success) {
            $(deleteModelId).modal('hide');
            notification({ message: `${controllerName} Deleted successfully !`, type: "success", title: "Success" });
            await initGitAllList();

        } else {
            $(deleteModelId).modal('hide');
            notification({ message: result.detail, type: "error", title: "Error" });
        }
    });
}
