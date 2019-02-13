import { setPagingOption } from '../../../../actions/pagingAction';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_DEVICE, SET_FROM_DATE, SET_TO_DATE, SET_PARAM } from '../actions/types';

export { setPagingOption, toggleLoader };

export const setDevice = (data) => {
    return {
        type: SET_DEVICE,
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

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}