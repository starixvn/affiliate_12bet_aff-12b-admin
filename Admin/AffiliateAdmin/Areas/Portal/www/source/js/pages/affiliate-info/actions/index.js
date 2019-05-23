import { SET_REPORT_TITLE, SET_PARAM, SET_SHOW_POPUP_COMMGROUP, SET_AFFID, SET_COMM_GROUP, SET_SELECTED_COLUMN } from './types';
import { setPagingOption } from '../../../actions/pagingAction';
import { setShowPopupPassword } from '../../common/actions/changePassword';

export { setPagingOption, setShowPopupPassword };

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

export const setShowPopupCommGroup = (data) => {
    return {
        type: SET_SHOW_POPUP_COMMGROUP,
        data
    }
}

export const setAffId = (data) => {
    return {
        type: SET_AFFID,
        data
    }
}

export const setCommGroup = (data) => {
    return {
        type: SET_COMM_GROUP,
        data
    }
}

export const setSelectedColumn = (data) => {
    return {
        type: SET_SELECTED_COLUMN,
        data
    }
}