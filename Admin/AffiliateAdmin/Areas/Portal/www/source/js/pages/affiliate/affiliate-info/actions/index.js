import * as type from './types';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { setShowPopupPassword } from '../../../common/actions/changePassword';

export { toggleLoader, setPagingOption, setShowPopupPassword };

export const setLanguageList = (data) => {
    return{
        type: type.SET_LANGUAGE_LIST,
        data
    }
}

export const setCommGroupList = (data) => {
    return {
        type: type.SET_COMM_GROUP_LIST,
        data
    }
}

export const setReportTitle = (data) => {
    return {
        type: type.SET_REPORT_TITLE,
        data
    }
}

export const setParam = (data) => {
    return {
        type: type.SET_PARAM,
        data
    }
}

export const setShowPopupCommGroup = (data) => {
    return {
        type: type.SET_SHOW_POPUP_COMMGROUP,
        data
    }
}

export const setAffId = (data) => {
    return {
        type: type.SET_AFFID,
        data
    }
}

export const setCommGroup = (data) => {
    return {
        type: type.SET_COMM_GROUP,
        data
    }
}

export const setSelectedColumn = (data) => {
    return {
        type: type.SET_SELECTED_COLUMN,
        data
    }
}

export const setIsClickSearch = (data) => {
    return {
        type: type.SET_IS_CLICK_SEARCH,
        data
    }
}