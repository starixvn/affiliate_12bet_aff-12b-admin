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
    Change: 'Change',
    Language: 'Language',
    AccountStatus: 'Account Status'
}

export const initialState = {
    isClickedSubmit: false,
    newUserName: '',
    firstName: '',
    lastName: '',
    newPassword: '',
    role: 0,
    language: '',
    initialRole: 0,
    isActive: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    initialUserInfo: null,
    initialMarketList: null,
    initialPermissionList: null,
    isChangingPassword: false,
    messagePopup: '',
    isConfirmPopup: true,
    isDeleteUser: false,
    isAfterDeleted: false,
    isEdit: false,
    marketListByRole: null,
    itemDefault: {
        MarketId: 'SetByDefault',
        MarketName: `Set to Role's Default`,
        HasPermission: false
    }
};