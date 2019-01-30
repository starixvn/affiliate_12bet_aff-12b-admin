import { SET_NOTE_LIST, SET_SHOW_POPUP, SET_NOTE_INFO } from './types';

import { setPagingOption } from '../../../../actions/pagingAction';

export { setPagingOption };

export const setNoteList = (data) => {
    return {
        type: SET_NOTE_LIST,
        data
    }
}

export const setShowPopup = (data) => {
    return {
        type: SET_SHOW_POPUP,
        data
    }
}

export const setNoteInfo = (data) => {
    return {
        type: SET_NOTE_INFO,
        data
    }
}