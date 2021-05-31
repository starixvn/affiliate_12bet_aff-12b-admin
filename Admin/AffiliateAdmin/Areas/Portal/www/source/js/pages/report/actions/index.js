import * as types from './types';
import { setPagingOption } from '../../../actions/pagingAction';

export { setPagingOption };

export const setReportTitle = (data) => {
    return {
        type: types.SET_REPORT_TITLE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setViewLevel = (data) => {
    return {
        type: types.SET_VIEW_LEVEL,
        data
    }
}

export const setIsCheckedDatePicker = (data) => {
    return {
        type: types.SET_IS_CHECKED_DATE_PICKER,
        data
    }
}