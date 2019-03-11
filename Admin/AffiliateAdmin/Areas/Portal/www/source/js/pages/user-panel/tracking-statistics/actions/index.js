import { SET_PARAM, SET_SELECTED_MONTH } from './types';
import { post } from '../../../../modules/ajaxRequest';

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

export const fetchData = (param) => async () => {
    const url = 'Portal/UserPanel/TrackingStatistics/GetTrackingStatisticsList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const fetchDetailData = (param) => async () => {
    const url = 'Portal/UserPanel/TrackingStatistics/GetTrackingStatisticsDetailList';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}