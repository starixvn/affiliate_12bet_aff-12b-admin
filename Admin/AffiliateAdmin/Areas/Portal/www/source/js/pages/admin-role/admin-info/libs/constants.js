import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const messages = {
    RequiredRole: "Role is required!"
}

export const resources = {
    UserName: 'User Name',
    FirstName: 'First Name',
    LastName: 'Last Name',
    Password: 'Password',
    ConfirmPassword: 'Confirm Password',
    Role: 'Role',
    Language: 'Language',
    AccountStatus: 'Account Status'
}

export const headerText = {
    Index: globalResource.Index,
    Function: 'Function',
    IndividualPermission: 'Individual Permission',
    RolePermission: 'Role Permission',
    Read: 'Read',
    Host: 'Host',
    View: 'View',
    Edit: globalResource.Edit
}

export const initialState = {
    isClickedSubmit: false,
    newUserName: '',
    firstName: '',
    lastName: '',
    newPassword: '',
    role: 0,
    isActive: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    userInfo: null,
    isChangingPassword: false,
    messagePopup: '',
    isConfirmPopup: true,
    isDeleteUser: false,
    isEdit: false
};