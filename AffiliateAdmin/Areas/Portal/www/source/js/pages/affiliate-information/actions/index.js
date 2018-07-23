import {
    LEFTPANEL_RECEIVE_AFFINFO,
    LEFTPANEL_UPDATE_LANGUAGE_SUCCESS,
    LEFTPANEL_UPDATE_CLOSED_SUCCESS,
    LEFTPANEL_UPDATE_AFFTYPE_SUCCESS
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

export const getLeftMenuInfo = userName => async dispatch => {
    const url = resolveClientUrl('Portal/Affiliate/GetInfo?username=' + userName);
    const response = await axios.get(url);
    dispatch(receiveLeftMenuInfo(response.data));
}

export const updateGeneralInfo = param => {
    const url = resolveClientUrl('Portal/Affiliate/UpdateInfo');

    return () => axios.post(url, param);
}