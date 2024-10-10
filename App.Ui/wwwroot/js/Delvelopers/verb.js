import { SendRequest, populateDropdown } from '../Utility/SendRequestUtility.js';
import { clearMessage, createActionButtons, displayNotification, initializeDataTable, loger, resetFormValidation, resetValidation, showCreateModal } from '../Utility/helpers.js';
import { notification } from '../Utility/notification.js';
// Dynamically define controller and form elements
const controllerName = "Verb";
const createButton = `#Create${controllerName}Btn`;
const formName = `#${controllerName}Form`;
const modalCreateId = `${controllerName}ModelCreate`;
const saveButtonId = `#btnSave${controllerName}`;
const updateButtonId = `#btnUpdate${controllerName}`;
const endpointCreate = `/${controllerName}/Create`;
const endpointGetAll = `/${controllerName}/GetAll`;
const dataTableId = `${controllerName}Table`;

$(document).ready(async function () {
    await getVerbList();
    await initializeCreateButton(createButton);
});

// Dynamically fetch data for verbs or other entities
const getVerbList = async () => {
    try {
        const result = await SendRequest({ endpoint: endpointGetAll });
        if (result.success) {
            await onSuccessEntities(result.data);
        } else {
            console.error(`Failed to retrieve ${controllerName} list:`, result.message);
        }
    } catch (error) {
        console.error(`Error fetching ${controllerName} list:`, error);
    }
};

// Handle success and render the entity list
const onSuccessEntities = async (entities) => {
    if (!entities || entities.length === 0) {
        console.warn(`No ${controllerName} items found.`);
        return;
    }

    const entitiesList = entities.map((entity) => {
        if (entity) {
            return {
                id: entity?.id,
                name: entity?.name,
                banglaName: entity?.banglaName,
                baseForm: entity?.baseForm,
                thirdPersonSingular: entity?.thirdPersonSingular,
                pastSimple: entity?.pastSimple,
                pastParticiple: entity?.pastParticiple,
                presentParticiple: entity?.presentParticiple,
                gerund: entity?.gerund
            };
        }
        return null;
    }).filter(Boolean);

    const entitySchema = [
        { data: null, title: 'Name', render: (data, type, row) => row.name || "N/A" },
        { data: null, title: 'Bangla', render: (data, type, row) => row.banglaName || "N/A" },
        { data: null, title: 'Base Form', render: (data, type, row) => row.baseForm || "N/A" },
        { data: null, title: 'TPSingular', render: (data, type, row) => row.thirdPersonSingular || "N/A" },
        { data: null, title: 'P S', render: (data, type, row) => row.pastSimple || "N/A" },
        { data: null, title: 'P P', render: (data, type, row) => row.pastParticiple || "N/A" },
        { data: null, title: 'Present P', render: (data, type, row) => row.presentParticiple || "N/A" },
        { data: null, title: 'Gerund', render: (data, type, row) => row.gerund || "N/A" },
        { data: null, title: 'Action', render: (data, type, row) => createActionButtons(row, [{ label: 'Edit', btnClass: 'btn-primary', callback: 'updateVerb' }, { label: 'Delete', btnClass: 'btn-danger', callback: 'deleteVerb' }]) }
    ];

    await initializeDataTable(entitiesList, entitySchema, dataTableId);
};

// Initialize validation dynamically
const initializeValidation = $(formName).validate({
    onkeyup: function (element) {
        $(element).valid();
    },
    rules: {
        Name: { required: true },
        Url: { required: true }
    },
    messages: {
        Name: { required: "Name is required." },
        Url: { required: "URL is required." }
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


// Dynamically initialize the create button and show modal
const initializeCreateButton = async (createBtnId) => {
    $(createBtnId).off('click').click(async (e) => {
        e.preventDefault();
        resetFormValidation(formName, initializeValidation);

        $('#myModalLabelUpdate').hide();
        showCreateModal(modalCreateId, saveButtonId, updateButtonId);
    });
};

// Handle saving the form
$(saveButtonId).off('click').click(async (e) => {
    e.preventDefault();
    clearMessage('successMessage', 'globalErrorMessage');

    try {
        if ($(formName).valid()) {
            const formData = $(formName).serialize();
            const result = await SendRequest({ endpoint: endpointCreate, method: 'POST', data: formData });

            if (result.success && result.status === 201) {
                $(modalCreateId).modal('hide');
                notification({ message: `${controllerName} Created successfully!`, type: "success", title: "Success" });
                await getVerbList(); // Refresh list
            }
        }
    } catch (error) {
        console.error(`Error in ${controllerName} save handler:`, error);
        $(modalCreateId).modal('hide');
        notification({ message: `${controllerName} creation failed. Please try again!`, type: "error", title: "Error" });
    }
});


//window.updateSubMenu = async (id) => {
//    resetFormValidation('#SubMenuForm', isMenuValidae);
//    clearMessage('successMessage', 'globalErrorMessage');
//    debugger
//    $('#myModalLabelUpdate').show();
//    $('#myModalLabelAdd').hide();
//    $('#SubMenuForm')[0].reset();

//    const result = await SendRequest({ endpoint: '/SubMenu/GetById/' + id });
//    if (result.success) {
//        $('#btnSaveSubMenu').hide();
//        $('#btnUpdateSubMenu').show();


//        $('#Name').val(result.data.name);
//        $('#Url').val(result.data.url);



//        $('#SubMenuModelCreate').modal('show');
//        resetValidation(isMenuValidae, '#SubMenuForm');
//        $('#btnUpdateSubMenu').off('click').on('click', async (e) => {
//            e.preventDefault();
//            debugger
//            const formData = $('#SubMenuForm').serialize();
//            const result = await SendRequest({ endpoint: '/SubMenu/Update/' + id, method: "PUT", data: formData });
//            if (result.success) {
//                $('#SubMenuModelCreate').modal('hide');
//                notification({ message: "SubMenu Updated successfully !", type: "success", title: "Success" });

//                await getSubMenuList(); // Update the user list
//            } else {
//                $('#SubMenuModelCreate').modal('hide');
//                notification({ message: " SubMenu Updated failed . Please try again. !", type: "error", title: "Error" });
//            }
//        });
//    }
//    loger(result);
//}

////////window.showDetails = async (id) => {
////////    loger("showDetails id " + id);
////////}




//window.deleteSubMenu = async (id) => {
//    clearMessage('successMessage', 'globalErrorMessage');
//    debugger;
//    $('#deleteAndDetailsModel').modal('show');
//    $('#companyDetails').empty();
//    $('#DeleteErrorMessage').hide();
//    $('#DeleteErrorMessage').hide(); // Hide error message initially
//    $('#btnDelete').off('click').on('click', async (e) => {
//        e.preventDefault();
//        debugger;
//        const result = await SendRequest({ endpoint: '/SubMenu/Delete', method: "DELETE", data: { id: id } });

//        if (result.success) {
//            $('#deleteAndDetailsModel').modal('hide');
//            notification({ message: "SubMenu Deleted successfully !", type: "success", title: "Success" });
//            await getSubMenuList(); // Update the category list

//        } else {
//            $('#deleteAndDetailsModel').modal('hide');
//            notification({ message: result.detail, type: "error", title: "Error" });
//        }
//    });
//}
