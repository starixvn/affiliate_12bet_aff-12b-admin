import { SET_PARAM, SET_MONTH } from './types';

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setMonth = (data) => {
    return {
        type: SET_MONTH,
        data
    }
}