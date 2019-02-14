import { SET_PARAM, SET_SELECTED_MONTH } from './types';

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setSelectedMonth = (data) => {
    return {
        type: SET_SELECTED_MONTH,
        data
    }
}