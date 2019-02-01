import { SET_SHOW_STATISTICS_DETAIL, SET_PARAM } from './types';

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