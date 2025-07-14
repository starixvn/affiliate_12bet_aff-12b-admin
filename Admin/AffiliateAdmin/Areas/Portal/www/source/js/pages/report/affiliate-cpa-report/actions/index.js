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
        type: types.SET_PARAMS,
        data
    }
}

export const toggleSubmit = () => {
    return {
        type: types.TOGGLE_SUBMIT
    }
}
