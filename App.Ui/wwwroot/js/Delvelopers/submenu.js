import { SendRequest, populateDropdown } from '../Utility/SendRequestUtility.js';
import { clearMessage, createActionButtons, displayNotification, initializeDataTable, loger, resetFormValidation, resetValidation, showCreateModal } from '../Utility/helpers.js';
import { notification } from '../Utility/notification.js';

$(document).ready(async function () {
    await getSubMenuList();
    await CreateSubMenuBtn('#CreateSubMenuBtn');
});

const getSubMenuList = async () => {
    try {
        const result = await SendRequest({ endpoint: '/SubMenu/GetAll' });
        if (result.success) {
            await onSuccessUsers(result.data);
        } else {
            console.error('Failed to retrieve menu list:', result.message);
        }
    } catch (error) {
        console.error('Error fetching menu list:', error);
    }
};

const onSuccessUsers = async (menus) => {
    if (!menus || menus.length === 0) {
        console.warn('No menu items found.');
        return; // Handle empty response gracefully
    }
    debugger
    const menusitem = menus.map((menu) => {
        if (menu) {
            return {
                id: menu?.id,
                name: menu?.name ?? "N/A",
                url: menu?.url ?? "N/A",
                Actions: menu.subMenuActions?.map(subMenu => subMenu.name).join(', ') || "N/A"
            };
        }
        return null;
    }).filter(Boolean);

    // Define the schema for the DataTable columns
    const userSchema = [
        {
            data: null,
            title: 'Name',
            render: (data, type, row) => row.name || "N/A"
        },
        {
            data: null,
            title: 'Url',
            render: (data, type, row) => row.url || "N/A"
        },
        {
            data: null,
            title: 'Action\'s',
            render: (data, type, row) => row.Actions || "N/A"
        },
        {
            data: null,
            title: 'Action',
            render: (data, type, row) => createActionButtons(row, [
                { label: 'Edit', btnClass: 'btn-primary', callback: 'updateSubMenu' },
                { label: 'Assain Action', btnClass: 'btn-primary', callback: 'AssainAction' },
                { label: 'Delete', btnClass: 'btn-danger', callback: 'deleteSubMenu' }
            ])
        }
    ];

    await initializeDataTable(menusitem, userSchema, 'SubMenuTable');
};

// Initialize validation
const isSubMenuValidae = $('#SubMenuForm').validate({
    onkeyup: function (element) {
        $(element).valid();
    },
    rules: {
        Name: {
            required: true,
        },
        Url: {
            required: true,
        }
    },
    messages: {
        Name: {
            required: "Name is required.",
        },
        Url: {
            required: "URL is required.",
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

export const CreateSubMenuBtn = async (CreateBtnId) => {
    //Sow Create Model
    $(CreateBtnId).off('click').click(async (e) => {
        e.preventDefault();
        resetFormValidation('#SubMenuForm', isSubMenuValidae);
        $('#myModalLabelUpdate').hide();
        debugger
        showCreateModal('SubMenuModelCreate', 'btnSaveSubMenu', 'btnUpdateSubMenu');
        await populateDropdown('/Menu/GetAll', '#MenuDropdown', 'id', 'name', "Select Ment");
    });
}
//Save Button


$('#btnSaveSubMenu').off('click').click(async (e) => {
    e.preventDefault();
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    try {
        if ($('#SubMenuForm').valid()) {
            const formData = $('#SubMenuForm').serialize();
            const result = await SendRequest({ endpoint: '/SubMenu/Create', method: 'POST', data: formData });
            // Clear previous messages
            $('#successMessage').hide();
            $('#UserError').hide();
            $('#EmailError').hide();
            $('#PasswordError').hide();
            $('#GeneralError').hide();
            debugger
            if (result.success && result.status === 201) {
                $('#SubMenuModelCreate').modal('hide');
                notification({ message: "SubMenu Created successfully !", type: "success", title: "Success" });
                await getSubMenuList(); // Update the user list
            }
        }
    } catch (error) {
        console.error('Error in click handler:', error);
        $('#SubMenuModelCreate').modal('hide');
        notification({ message: " Branch Created failed . Please try again. !", type: "error", title: "Error" });
    }

});

window.updateSubMenu = async (id) => {
    resetFormValidation('#SubMenuForm', isMenuValidae);
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $('#SubMenuForm')[0].reset();

    const result = await SendRequest({ endpoint: '/SubMenu/GetById/' + id });
    if (result.success) {
        $('#btnSaveSubMenu').hide();
        $('#btnUpdateSubMenu').show();


        $('#Name').val(result.data.name);
        $('#Url').val(result.data.url);



        $('#SubMenuModelCreate').modal('show');
        resetValidation(isMenuValidae, '#SubMenuForm');
        $('#btnUpdateSubMenu').off('click').on('click', async (e) => {
            e.preventDefault();
            debugger
            const formData = $('#SubMenuForm').serialize();
            const result = await SendRequest({ endpoint: '/SubMenu/Update/' + id, method: "PUT", data: formData });
            if (result.success) {
                $('#SubMenuModelCreate').modal('hide');
                notification({ message: "SubMenu Updated successfully !", type: "success", title: "Success" });

                await getSubMenuList(); // Update the user list
            } else {
                $('#SubMenuModelCreate').modal('hide');
                notification({ message: " SubMenu Updated failed . Please try again. !", type: "error", title: "Error" });
            }
        });
    }
    loger(result);
}

//////window.showDetails = async (id) => {
//////    loger("showDetails id " + id);
//////}
const isAssainActionValidae = $('#AssainActionForm').validate({
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
window.AssainAction = async (id) => {
    resetFormValidation('#AssainActionForm', isAssainActionValidae);
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $('#AssainActionForm')[0].reset();

    const result = await SendRequest({ endpoint: '/SubMenu/GetById/' + id });
    if (result.success) {
        $('#btnSaveSubMenu').hide();
        $('#btnUpdateSubMenu').show();


        $('#SubMenuName').val(result.data.name);  
        $('#SubMenuId').val(result.data.id); 


        $('#AssainActionModelCreate').modal('show');
        resetValidation(isAssainActionValidae, '#AssainActionForm');
        $('#AssainActionBtn').off('click').on('click', async (e) => {
            e.preventDefault();
            debugger
            const formData = $('#AssainActionForm').serialize();
            const result = await SendRequest({ endpoint: '/SubMenu/AssainActions/' + id, method: "POST", data: formData });
            if (result.success) {
                $('#AssainActionModelCreate').modal('hide');
                notification({ message: "SubMenu Updated successfully !", type: "success", title: "Success" });

                await getSubMenuList(); // Update the user list
            } else {
                $('#AssainActionModelCreate').modal('hide');
                notification({ message: " SubMenu Updated failed . Please try again. !", type: "error", title: "Error" });
            }
        });
    }
}


window.deleteSubMenu = async (id) => {
    clearMessage('successMessage', 'globalErrorMessage');
    debugger;
    $('#deleteAndDetailsModel').modal('show');
    $('#companyDetails').empty();
    $('#DeleteErrorMessage').hide();
    $('#DeleteErrorMessage').hide(); // Hide error message initially
    $('#btnDelete').off('click').on('click', async (e) => {
        e.preventDefault();
        debugger;
        const result = await SendRequest({ endpoint: '/SubMenu/Delete', method: "DELETE", data: { id: id } });

        if (result.success) {
            $('#deleteAndDetailsModel').modal('hide');
            notification({ message: "SubMenu Deleted successfully !", type: "success", title: "Success" });
            await getSubMenuList(); // Update the category list

        } else {
            $('#deleteAndDetailsModel').modal('hide');
            notification({ message: result.detail, type: "error", title: "Error" });
        }
    });
}
