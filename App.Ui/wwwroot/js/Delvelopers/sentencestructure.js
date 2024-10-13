import { SendRequest, populateDropdown } from '../Utility/SendRequestUtility.js';
import { clearMessage, createActionButtons, initializeDataTable, loger, resetFormValidation, resetValidation, showCreateModal } from '../Utility/helpers.js';
import { notification } from '../Utility/notification.js';
// Dynamically define controller and form elements
const controllerName = "SentenceStructure";
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
    await CreateSentenceStructureBtn(createButton);
});
const initGitAllList = async () => {
    await getSentenceStructureList();
}
// Dynamically fetch data for verbs or other entities
const getSentenceStructureList = async () => {
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
                bangla: entity?.banglaSentence,
                english: entity?.englistSentence,
            };
        }
        return null;
    }).filter(Boolean);
    debugger
    const entitySchema = [
        { data: null, title: 'English', render: (data, type, row) => row.english || "N/A" },
        { data: null, title: 'Bangla', render: (data, type, row) => row.bangla || "N/A" },
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
const InitializeSentenceStructurevalidation = $(formName).validate({
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

export const CreateSentenceStructureBtn = async (CreateBtnId) => {
    //Sow Create Model
    $(CreateBtnId).off('click').click(async (e) => {
        e.preventDefault();
        resetFormValidation(formName, InitializeSentenceStructurevalidation);
        $('#myModalLabelUpdate').hide();
        $('#myModalLabelAdd').show();
        debugger
        showCreateModal(modalCreateId, saveButtonId, updateButtonId);
        await populateDropdown('/SubCategory/GetAll', '#SubCatagoryDropdown', 'id', 'name', "Select Sub Category");
        await populateDropdown('/SentenceForms/GetAll', '#FormsDropdown', 'id', 'name', "Select Form");
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




window.updateSentenceStructure = async (id) => {
    resetFormValidation(formName, InitializeSentenceStructurevalidation);
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $(formName)[0].reset();
    await populateDropdown('/SubCategory/GetAll', '#SubCatagoryDropdown', 'id', 'name', "Select Sub Category");
    await populateDropdown('/SentenceForms/GetAll', '#FormsDropdown', 'id', 'name', "Select Form");
    const result = await SendRequest({ endpoint: endpointGetById + id });
    if (result.success) {
        $(saveButtonId).hide();
        $(updateButtonId).show();
        //update and buind Entity Id
        $('#EnglistSentence').val(result.data.englistSentence);
        $('#BanglaSentence').val(result.data.banglaSentence);
        $('#SubCatagoryDropdown').val(result.data.subCatagoryID);
        $('#FormsDropdown').val(result.data.formsId);


        $(modalCreateId).modal('show');
        resetValidation(InitializeSentenceStructurevalidation, formName);
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




window.deleteSentenceStructure = async (id) => {
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
