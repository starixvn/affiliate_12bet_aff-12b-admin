import { combineReducers } from 'redux';
import { SET_MARKETING_NOTE_INFO, SET_MARKETING_NOTE_LIST, SET_SHOW_POPUP } from '../actions/types';
import createReducer from '../../../../../modules/createReducer';
import { pagingOption } from '../../../../../reducers/pagingReducer';

export const marketingNoteList = createReducer([], {
    [SET_MARKETING_NOTE_LIST](prevState, action) {
        return [...action.data];
    }
})

export const isShowPopup = createReducer(false, {
    [SET_SHOW_POPUP](prevState, action) {
        return action.data;
    }
})

const initMarketingNote = { noteId: 0, noteContent: '' };
export const marketingNoteInfo = createReducer({ ...initMarketingNote }, {
    [SET_MARKETING_NOTE_INFO](prevState, action) {
        return { ...action.data };
    }
})

export default combineReducers({
    marketingNoteList,
    marketingNoteInfo,
    isShowPopup,
    pagingOption
})