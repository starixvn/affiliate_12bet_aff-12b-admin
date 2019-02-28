import { SET_REPORT_TITLE, SET_PARAM, SET_VIEW_LEVEL } from './types';
import { setPagingOption } from '../../../actions/pagingAction';

export { setPagingOption };

export const setReportTitle = (data) => {
    return {
        type: SET_REPORT_TITLE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setViewLevel = (data) => {
    return {
        type: SET_VIEW_LEVEL,
        data
    }
}