import { SET_NOTE_LIST, SET_SHOW_POPUP_EDIT, SET_PARAM } from './types';

export const setNoteList = (data) => {
    return {
        type: SET_NOTE_LIST,
        data
    }
}

export const setShowPopupEdit = (data) => {
    return {
        type: SET_SHOW_POPUP_EDIT,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}