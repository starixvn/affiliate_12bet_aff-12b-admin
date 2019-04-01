import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_RECEIVE_ONLINELIST
} from './types';
import { post } from '../../../../modules/ajaxRequest';

const receiveLeftMenuInfo = (data) => {
    return {
        type: LEFTPANEL_RECEIVE_AFFINFO,
        data
    }
}

const getOnLineList = (data) => {
    return {
        type: LEFTPANEL_RECEIVE_ONLINELIST,
        data
    }
}

export const getAffiliateInfo = userName => async dispatch => {
    const url = 'Portal/CustomerLeftPanel/GetAffiliateInfo';
    const param = { affUsername: userName };
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    dispatch(receiveLeftMenuInfo(data));
}

export const updateGeneralInfo = param => {
    const url = 'Portal/CustomerLeftPanel/UpdateInfo';

    return () => axios.post(url, param);
}

export const changePassword = param => {
    const url = 'Portal/CustomerLeftPanel/ChangePassword';

    return () => axios.post(url, param);
}