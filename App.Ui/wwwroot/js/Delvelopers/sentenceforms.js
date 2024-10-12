import { SendRequest, populateDropdown } from '../Utility/SendRequestUtility.js';
import { clearMessage, createActionButtons, initializeDataTable, loger, resetFormValidation, resetValidation, showCreateModal } from '../Utility/helpers.js';
import { notification } from '../Utility/notification.js';
// Dynamically define controller and form elements
const controllerName = "SentenceForms";
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
    await CreateSentenceFormsBtn(createButton);
});
const initGitAllList = async () => {
    await getSentenceFormsList();
}
// Dynamically fetch data for verbs or other entities
const getSentenceFormsList = async () => {
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
                sentenceStructures: entity.sentenceStructures?.map(ss => ss.englistSentence).join(", ") || "null"
            };
        }
        return null;
    }).filter(Boolean);
    debugger
    const entitySchema = [
        { data: null, title: 'Name', render: (data, type, row) => row.name || "N/A" },
        { data: null, title: 'Sentence Structures', render: (data, type, row) => row.sentenceStructures || "N/A" },
        {
            data: null, title: 'Action', render: (data, type, row) => createActionButtons(row, [
                { label: 'Edit', btnClass: 'btn-primary', callback: `update${controllerName}` },
                { label: 'Assain Structure', btnClass: 'btn-primary', callback: `AssainStructure${controllerName}` },
                { label: 'Delete', btnClass: 'btn-danger', callback: `delete${controllerName}` }
            ])
        }
    ];
    debugger
    await initializeDataTable(entitiesList, entitySchema, dataTableId);

};



// Initialize validation
const InitializeSentenceFormsvalidation = $(formName).validate({
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

export const CreateSentenceFormsBtn = async (CreateBtnId) => {
    //Sow Create Model
    $(CreateBtnId).off('click').click(async (e) => {
        e.preventDefault();
        resetFormValidation(formName, InitializeSentenceFormsvalidation);
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




window.updateSentenceForms = async (id) => {
    resetFormValidation(formName, InitializeSentenceFormsvalidation);
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $(formName)[0].reset();
    await populateDropdown('/Category/GetAll', '#CategoryDropdown', 'id', 'name', "Select Category");
    const result = await SendRequest({ endpoint: endpointGetById + id });
    if (result.success) {
        $(saveButtonId).hide();
        $(updateButtonId).show();
        //update and buind Entity Id
        $('#Name').val(result.data.name);
        

        $(modalCreateId).modal('show');
        resetValidation(InitializeSentenceFormsvalidation, formName);
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

window.AssainStructureSentenceForms = async (id) => {
    loger(id);
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $('#AssainStructureForm')[0].reset();
    await populateDropdown("/SentenceStructure/GetAll", '#StructureDropdown', 'id', 'englistSentence', );
    const result = await SendRequest({ endpoint: '/SentenceForms/GetById/' + id });
    if (result.success) {
        $('#btnSaveAssainStructure').hide();
        $('#btnUpdateAssainStructure').show();


        $('#formateID').val(result.data.id);
        $('#SentenceForms').val(result.data.name); 


        $('#AssainStructureModelCreate').modal('show');
        //resetValidation(isMenuValidae, '#AssainStructureForm');
        $('#btnUpdateAssainStructure').off('click').on('click', async (e) => {
            await initGitAllList();
            debugger
            const formData = $('#AssainStructureForm').serialize();
            const result = await SendRequest({ endpoint: '/SentenceForms/AssainStructure/' + id, method: "POST", data: formData });
            if (result.success) {
                $('#AssainStructureModelCreate').modal('hide');
                notification({ message: "Category Updated successfully !", type: "success", title: "Success" });

                await initGitAllList();
            } else {
                $('#AssainStructureModelCreate').modal('hide');
                notification({ message: " Category Updated failed . Please try again. !", type: "error", title: "Error" });
            }
        });
    }
}




window.deleteSentenceForms = async (id) => {
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
