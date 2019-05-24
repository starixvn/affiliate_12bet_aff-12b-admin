import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_NOTE_LIST, SET_SHOW_POPUP, SET_NOTE_INFO } from './types';
import { setPagingOption } from '../../../../actions/pagingAction';

export { toggleLoader, post };

export const setNoteList = (data) => {
    return {
        type: SET_NOTE_LIST,
        data
    }
}

export const getNoteList = (param) => async(dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/UserPanel/Note/GetNoteList';
    const response = await post(url, param);

    if (response && response.status === 200) {
        const { MainData, TotalItem } = response.data;

        dispatch(setNoteList(MainData));

        const { pageIndex, pageSize } = param;
        const pagingOption = {
            PageIndex: pageIndex,
            PageSize: pageSize,
            TotalItem: TotalItem
        };
        dispatch(setPagingOption(pagingOption));
    }
    dispatch(toggleLoader(false));
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

export const getNoteContentById = (param) => async() => {
    const url = 'Portal/UserPanel/Note/GetNoteContentById';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const insertNote = (param) => async() => {
    const url = 'Portal/UserPanel/Note/InsertNote';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateNote = (param) => async() => {
    const url = 'Portal/UserPanel/Note/UpdateNote';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}