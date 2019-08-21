import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { SET_REPORT_TITLE, SET_PARAM, SET_SHOW_POPUP_COMMGROUP, SET_AFFID, SET_COMM_GROUP, SET_SELECTED_COLUMN } from '../actions/types';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { showPopupPassword } from '../../common/reducers';

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

export const columnList = createReducer(0, {
    [SET_SELECTED_COLUMN](prevState, action) {
        return [...action.data];
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
    groupId,
    columnList
});