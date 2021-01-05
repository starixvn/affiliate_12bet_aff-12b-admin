import { regExp, changePasswordMessages } from '../../../modules/constants';

export { regExp };

export const messages = {
    ...changePasswordMessages,
    RequiredOldPassword: "Old Password is required!"
}

export const initialState = {
    isClickedSubmit: false,
    oldPassword: '',
    newPassword: '',
    confirmPassword: '',
    isShowMessage: false,
    isSuccess: true,
    message: ''
};