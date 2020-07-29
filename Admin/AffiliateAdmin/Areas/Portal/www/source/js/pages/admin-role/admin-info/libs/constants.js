import { regExp, changePasswordMessages } from '../../../../modules/constants';

export { regExp };

export const messages = {
    ...changePasswordMessages,
    RequiredUserName: "User Name is required!"
}

const AsstMarketing = 5;
export const roleList = {
    1: 'Management',
    2: 'Exe. Settlement',
    3: 'Exe. Risk Team',
    4: 'Exe. Marketing',
    [AsstMarketing]: 'Asst. Marketing'
}

export const initialState = {
    isClickedSubmit: false,
    newUserName: '',
    newPassword: '',
    confirmPassword: '',
    role: 0,
    isActive: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isEdit: false,
    messagePopup: '',
    isConfirmPopup: true
};