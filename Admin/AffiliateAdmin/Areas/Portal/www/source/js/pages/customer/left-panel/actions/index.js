import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_UPDATE_LANGUAGE_SUCCESS,
    LEFTPANEL_UPDATE_CLOSED_SUCCESS,
    LEFTPANEL_UPDATE_AFFTYPE_SUCCESS
} from './types';
import { post } from '../../../../modules/ajaxRequest';

const receiveLeftMenuInfo = (data) => {
    return {
        type: LEFTPANEL_RECEIVE_AFFINFO,
        data
    }
}

export const setLanguage = (data) => {
    return {
        type: LEFTPANEL_UPDATE_LANGUAGE_SUCCESS,
        data
    }
}

export const setClosedStatus = (data) => {
    return {
        type: LEFTPANEL_UPDATE_CLOSED_SUCCESS,
        data
    }
}

export const setAffType = (data) => {
    return {
        type: LEFTPANEL_UPDATE_AFFTYPE_SUCCESS,
        data
    }
}

export const getAffiliateInfo = userName => async dispatch => {
    const url = resolveClientUrl('Portal/CustomerLeftPanel/GetAffiliateInfo');
    const param = { affUsername: userName };
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    dispatch(receiveLeftMenuInfo(data));
}

export const updateGeneralInfo = param => {
    const url = resolveClientUrl('Portal/Affiliate/UpdateInfo');

    return () => axios.post(url, param);
}

export const changePassword = param => {
    const url = resolveClientUrl('Portal/Affiliate/ChangePassword');

    return () => axios.post(url, param);
}