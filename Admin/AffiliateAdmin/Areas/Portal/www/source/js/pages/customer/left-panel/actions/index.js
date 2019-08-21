import { post } from '../../../../modules/ajaxRequest';
import { getAffiliateInfo } from '../../mainpage/actions/loadingDataAction';
import { SET_AFF_NAME, SET_IS_VALID_USERNAME, TOGGLE_LOADER_LEFT_PANEL } from './types';

export { getAffiliateInfo };

export const setAffName = (data) => {
    return {
        type: SET_AFF_NAME,
        data
    }
}

export const setIsValidUserName = (data) => {
    return {
        type: SET_IS_VALID_USERNAME,
        data
    }
}

export const toggleLoaderLeftPanel = (data) => {
    return {
        type: TOGGLE_LOADER_LEFT_PANEL,
        data
    }
}

export const getOnLineList = (affName) => async (dispatch) => {
    const url = 'Portal/CustomerLeftPanel/getOnLineList';
    const param = { affUsername: affName };
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateLanguage = (param) => async () => {
    const url = 'Portal/CustomerLeftPanel/UpdateLanguage';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateAffType = (param) => async () => {
    const url = 'Portal/CustomerLeftPanel/UpdateAffType';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateStatus = (param) => async () => {
    const url = 'Portal/CustomerLeftPanel/UpdateStatus';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateClosedStatus = (param) => async () => {
    const url = 'Portal/CustomerLeftPanel/UpdateClosedStatus';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}