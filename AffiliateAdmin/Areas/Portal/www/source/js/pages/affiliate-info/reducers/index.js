import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { SET_REPORT_TITLE, SET_PARAM, SET_SHOW_POPUP_PASSWORD, SET_SHOW_POPUP_COMMGROUP, SET_AFFID, SET_COMM_GROUP } from '../actions/types';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';

export const commGroupList = createReducer({}, {});

export const languageList = createReducer({}, {});

export const groupId = createReducer(0, {});

export const reportTitle = createReducer('', {
    [SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const showPopupPassword = createReducer(false, {
    [SET_SHOW_POPUP_PASSWORD](prevState, action) {
        return action.data;
    }
})

export const showPopupCommGroup = createReducer(false, {
    [SET_SHOW_POPUP_COMMGROUP](prevState, action) {
        return action.data;
    }
})

export const affId = createReducer(0, {
    [SET_AFFID](prevState, action) {
        return action.data;
    }
})

export const commGroup = createReducer(0, {
    [SET_COMM_GROUP](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    pagingOption,
    reportTitle,
    param,
    commGroupList,
    languageList,
    isShowLoader,
    showPopupPassword,
    showPopupCommGroup,
    affId,
    commGroup,
    groupId
});