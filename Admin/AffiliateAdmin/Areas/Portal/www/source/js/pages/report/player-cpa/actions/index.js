import * as types from './types';
import { post } from '../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../actions/pagingAction';

export { setPagingOption };

export const setReportTitle = (data) => {
    return {
        type: types.SET_REPORT_TITLE,
        data
    }
}

export const setParams = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const toggleSubmit = () => {
    return {
        type: types.TOGGLE_SUBMIT,
    }
}

export const fetchData = (param) => async () => {
    const url = 'Portal/PlayerCpaReport/GetPlayerCpaReportData';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}