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

const Active = 1
export const accountStatusList = {
    [Active]: 'Active',
    0: 'Closed'
}

export const initialState = {
    isClickedSubmit: false,
    userName: '',
    newPassword: '',
    confirmPassword: '',
    role: 0,
    accountStatus: Active,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isInsert: true
};