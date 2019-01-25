import { combineReducers } from 'redux';
import { SET_NOTE_LIST, SET_SHOW_POPUP_EDIT, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

export const noteList = createReducer([], {
    [SET_NOTE_LIST](prevState, action) {
        return [...action.data];
    }
});

export const isShowPopupEdit = createReducer(false, {
    [SET_SHOW_POPUP_EDIT](prevState, action) {
        return [...action.data];
    }
});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return [...action.data];
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    isShowPopupEdit,
    noteList,
    param
});