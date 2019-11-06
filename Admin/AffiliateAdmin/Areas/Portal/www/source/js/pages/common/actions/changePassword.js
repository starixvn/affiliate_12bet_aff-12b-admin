import { post } from '../../../modules/ajaxRequest';
import { SET_SHOW_POPUP_PASSWORD } from './types';

export const setShowPopupPassword = (data) => {
    return {
        type: SET_SHOW_POPUP_PASSWORD,
        data
    }
}

export const updateChangeAffPasswword = async(param) => {
    const url = 'Portal/Affiliate/ChangePassword';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateChangeAdminPasswword = async (param) => {
    const url = 'Portal/Administrator/ChangePassword';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}