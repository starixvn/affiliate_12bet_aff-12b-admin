import { SET_SHOW_STATISTICS_DETAIL, SET_PARAM, SET_DATE } from './types';

export const setShowStatisticsDetail = (data) => {
    return {
        type: SET_SHOW_STATISTICS_DETAIL,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setDate = (data) => {
    return {
        type: SET_DATE,
        data
    }
}