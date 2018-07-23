import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_UPDATE_LANGUAGE_SUCCESS
} from './types';
import axios from 'axios';

import {
    resolveClientUrl
} from '../../../modules/utils';

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

export const getLeftMenuInfo = userName => async dispatch => {
    const url = resolveClientUrl('Portal/Affiliate/GetInfo?username=' + userName);
    const response = await axios.get(url);
    dispatch(receiveLeftMenuInfo(response.data));
}

export const updateLanguage = preferredLanguage => {
    const url = resolveClientUrl('Portal/Affiliate/UpdateInfo')
    const param = {
        preferredLanguage
    }

    return () => axios.post(url, param);
}