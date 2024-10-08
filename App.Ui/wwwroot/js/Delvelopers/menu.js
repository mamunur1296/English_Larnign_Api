import { SendRequest } from '../Utility/SendRequestUtility.js';
import { clearMessage, createActionButtons, displayNotification, initializeDataTable, loger, resetFormValidation, resetValidation, showCreateModal } from '../Utility/helpers.js';
import { GetLoginUserMenuItems, getSubmenuActionListByName } from '../Utility/menuitems.js';
import { notification } from '../Utility/notification.js';

$(document).ready(async function () {
    await getMenuList();
    const menuAction = await getSubmenuActionListByName("Menu");
    if (menuAction && menuAction.some(action => action.ActionName === "create")) {
        await CreateMenuBtn('#CreateMenuBtn');
        $('#CreateMenuBtn').show(); 
    } else {
        $('#CreateMenuBtn').hide(); 
    }
});


const getMenuList = async () => {
    try {
        const result = await SendRequest({ endpoint: '/Menu/GetAll' });
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

    // Map and structure the menu items, and handle async calls with await
    const menusitem = await Promise.all(menus.map(async (menu) => {
        if (menu) {
            const menuAction = await getSubmenuActionListByName("Menu"); // Await the async function
            return {
                id: menu.id,
                name: menu.name ?? "N/A",
                url: menu.url ?? "N/A",
                sub: menu.subMenus?.map(subMenu => subMenu.name).join(', ') || "N/A",
                actions: menuAction || [] // Ensure actions is always an array
            };
        }
        return null;
    }));

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
            title: 'Sub Menu\'s',
            render: (data, type, row) => row.sub || "N/A"
        },
        
        {
            data: null,
            title: 'Action',
            render: (data, type, row) => {
                if (Array.isArray(row.actions)) {
                    const populateItem = ["Update", "Delete", "GetById"];
                    const actionButtons = row.actions
                        .filter(action => populateItem.includes(action.ActionName)) 
                        .map(action => ({
                            label: action.ActionName, 
                            btnClass: action.ActionName === "Delete" ? 'btn-danger' : 'btn-primary',
                            callback: action.ActionName + "Menu"
                        }));
                    return actionButtons.length > 0 ? createActionButtons(row, actionButtons) : 'No Valid Actions';
                }
                return 'No Actions'; 
            }
        }
    ];

    // Initialize the DataTable with the menu items and schema
    await initializeDataTable(menusitem, userSchema, 'MenuTable');
};


// Initialize validation
const isMenuValidae = $('#MenuForm').validate({
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

export const CreateMenuBtn = async (CreateBtnId) => {
    //Sow Create Model
    $(CreateBtnId).off('click').click(async (e) => {
        e.preventDefault();
        resetFormValidation('#MenuForm', isMenuValidae);
        $('#myModalLabelUpdate').hide();
        debugger
        showCreateModal('MenuModelCreate', 'btnSaveMenu', 'btnUpdateMenu');
    });
}
 //Save Button


$('#btnSaveMenu').off('click').click(async (e) => {
    e.preventDefault();
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    try {
        if ($('#MenuForm').valid()) {
            const formData = $('#MenuForm').serialize();
            const result = await SendRequest({ endpoint: '/Menu/Create', method: 'POST', data: formData });
            // Clear previous messages
            $('#successMessage').hide();
            $('#UserError').hide();
            $('#EmailError').hide();
            $('#PasswordError').hide();
            $('#GeneralError').hide();
            debugger
            if (result.success && result.status === 201) {
                $('#MenuModelCreate').modal('hide');
                notification({ message: "Branch Created successfully !", type: "success", title: "Success" });
                await getMenuList(); // Update the user list
            }
        }
    } catch (error) {
        console.error('Error in click handler:', error);
        $('#MenuModelCreate').modal('hide');
        notification({ message: " Branch Created failed . Please try again. !", type: "error", title: "Error" });
    }

});

window.UpdateMenu = async (id) => {
    resetFormValidation('#MenuForm', isMenuValidae);
    clearMessage('successMessage', 'globalErrorMessage');
    debugger
    $('#myModalLabelUpdate').show();
    $('#myModalLabelAdd').hide();
    $('#MenuForm')[0].reset();
   
    const result = await SendRequest({ endpoint: '/Menu/GetById/' + id });
    if (result.success) {
        $('#btnSaveMenu').hide();
        $('#btnUpdateMenu').show();


        $('#Name').val(result.data.name);
        $('#Url').val(result.data.url);



        $('#MenuModelCreate').modal('show');
        resetValidation(isMenuValidae, '#MenuForm');
        $('#btnUpdateMenu').off('click').on('click', async (e) => {
            e.preventDefault();
            debugger
            const formData = $('#MenuForm').serialize();
            const result = await SendRequest({ endpoint: '/Menu/Update/' + id, method: "PUT", data: formData });
            if (result.success) {
                $('#MenuModelCreate').modal('hide');
                notification({ message: "Category Updated successfully !", type: "success", title: "Success" });

                await getMenuList(); // Update the user list
            } else {
                $('#MenuModelCreate').modal('hide');
                notification({ message: " Category Updated failed . Please try again. !", type: "error", title: "Error" });
            }
        });
    }
    loger(result);
}

//////window.showDetails = async (id) => {
//////    loger("showDetails id " + id);
//////}


window.DeleteMenu = async (id) => {
    clearMessage('successMessage', 'globalErrorMessage');
    debugger;
    $('#deleteAndDetailsModel').modal('show');
    $('#companyDetails').empty();
    $('#DeleteErrorMessage').hide();
    $('#DeleteErrorMessage').hide(); // Hide error message initially
    $('#btnDelete').off('click').on('click', async (e) => {
        e.preventDefault();
        debugger;
        const result = await SendRequest({ endpoint: '/Menu/Delete', method: "DELETE", data: { id: id } });

        if (result.success) {
            $('#deleteAndDetailsModel').modal('hide');
            notification({ message: "Category Deleted successfully !", type: "success", title: "Success" });
            await getMenuList(); // Update the category list

        } else {
            $('#deleteAndDetailsModel').modal('hide');
            notification({ message: result.detail, type: "error", title: "Error" });
        }
    });
}
