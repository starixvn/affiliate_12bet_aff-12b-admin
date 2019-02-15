import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_DEVICE_TYPE, SET_FROM_DATE, SET_TO_DATE, SET_SELECTED_SITE_URL, SET_PARAM } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setDeviceType = (data) => {
    return {
        type: SET_DEVICE_TYPE,
        data
    }
}

export const setFromDate = (data) => {
    return {
        type: SET_FROM_DATE,
        data
    }
}

export const setToDate = (data) => {
    return {
        type: SET_TO_DATE,
        data
    }
}

export const setSelectedSiteUrl = (data) => {
    return {
        type: SET_SELECTED_SITE_URL,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}