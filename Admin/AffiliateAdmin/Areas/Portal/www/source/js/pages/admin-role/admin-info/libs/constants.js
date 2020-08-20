import { regExp, changePasswordMessages } from '../../../../modules/constants';

export { regExp };

export const messages = {
    ...changePasswordMessages,
    RequiredRole: "Role is required!"
}

export const resources = {
    UserName: 'User Name',
    FirstName: 'First Name',
    LastName: 'Last Name',
    Password: 'Password',
    ConfirmPassword: 'Confirm Password',
    Role: 'Role',
    AccountStatus: 'Account Status'
}

export const initialState = {
    isClickedSubmit: false,
    newUserName: '',
    firstName: '',
    lastName: '',
    newPassword: '',
    confirmPassword: '',
    role: 0,
    isActive: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isEdit: false,
    userInfo: null,
    isChangingPassword: false,
    messagePopup: '',
    isConfirmPopup: true,
    isDeleteUser: false
};