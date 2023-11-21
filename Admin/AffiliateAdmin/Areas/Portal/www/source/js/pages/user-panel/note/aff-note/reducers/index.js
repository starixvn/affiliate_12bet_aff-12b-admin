import { combineReducers } from 'redux';
import * as types from '../actions/types';
import createReducer from '../../../../../modules/createReducer';
import { pagingOption } from '../../../../../reducers/pagingReducer';

import { defaultNote } from '../../common/libs/constants';

export const noteList = createReducer([], {
    [types.SET_NOTE_LIST](prevState, action) {
        return [...action.data];
    }
});

export const isShowPopup = createReducer(false, {
    [types.SET_SHOW_POPUP](prevState, action) {
        return action.data;
    }
});

export const noteInfo = createReducer({ ...defaultNote }, {
    [types.SET_NOTE_INFO](prevState, action) {
        return { ...action.data };
    }
});

export default combineReducers({
    pagingOption,
    isShowPopup,
    noteList,
    noteInfo
});