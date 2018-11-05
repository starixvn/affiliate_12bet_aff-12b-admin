import { SET_REPORT_TITLE, SET_PARAM, SET_SHOW_POPUP, SET_AFFID } from './types';
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

export const setShowPopup = (data) => {
    return {
        type: SET_SHOW_POPUP,
        data
    }
}

export const setAffId = (data) => {
    return {
        type: SET_AFFID,
        data
    }
}