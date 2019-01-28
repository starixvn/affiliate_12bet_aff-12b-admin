import { combineReducers } from 'redux';
import { SET_NOTE_LIST, SET_SHOW_POPUP, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

export const noteList = createReducer([], {
    [SET_NOTE_LIST](prevState, action) {
        return [...action.data];
    }
});

export const isShowPopup = createReducer(false, {
    [SET_SHOW_POPUP](prevState, action) {
        return action.data;
    }
});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    isShowPopup,
    noteList,
    param
});