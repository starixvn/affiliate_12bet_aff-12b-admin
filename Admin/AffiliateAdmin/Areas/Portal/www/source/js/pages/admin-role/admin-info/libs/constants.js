import { regExp, changePasswordMessages } from '../../../../modules/constants';

export { regExp };

export const messages = {
    ...changePasswordMessages,
    RequiredRole: "Role is required!"
}

export const roleList = {
    Management: 'Management',
    ExeSettlement: 'Exe. Settlement',
    ExeRiskTeam: 'Exe. Risk Team',
    ExeMarketing: 'Exe. Marketing',
    AsstMarketing: 'Asst. Marketing'
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
    userInfo: null,
};