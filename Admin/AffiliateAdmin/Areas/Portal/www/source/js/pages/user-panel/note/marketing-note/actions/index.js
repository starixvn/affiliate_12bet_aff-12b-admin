import { SET_MARKETING_NOTE_INFO, SET_MARKETING_NOTE_LIST, SET_SHOW_POPUP } from './types'
import { setSelectedUserPanel, toggleLoaderUserPanel } from '../../../../customer/user-panel/actions';
import { post } from '../../../../../modules/ajaxRequest';
import { setPagingOption } from '../../../../../actions/pagingAction';

export { setSelectedUserPanel, toggleLoaderUserPanel };

export const setMarketingNoteList = (data) => {
    return {
        type: SET_MARKETING_NOTE_LIST,
        data
    }
}

export const getMarketingNoteList = (param) => async (dispatch) => {
    dispatch(toggleLoaderUserPanel(true));

    const url = 'Portal/UserPanel/MarketingNote/GetNoteList';

    const response = await post(url, param);

    if (response && response.status === 200) {
        const { MainData, TotalItem } = response.data;

        dispatch(setMarketingNoteList(MainData));

        const { pageIndex, pageSize } = param;

        const pagingOption = {
            PageIndex: pageIndex,
            PageSize: pageSize,
            TotalItem: TotalItem
        };

        dispatch(setPagingOption(pagingOption));
    }

    dispatch(toggleLoaderUserPanel(false));
}

export const setShowPopup = (data) => {
    return {
        type: SET_SHOW_POPUP,
        data
    }
}

export const setMarketingNoteInfo = (data) => {
    return {
        type: SET_MARKETING_NOTE_INFO,
        data
    }
}

export const getMarketingNoteContentById = (param) => async () => {
    const url = 'Portal/UserPanel/MarketingNote/GetNoteContentById';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const insertMarketingNote = (param) => async () => {
    const url = 'Portal/UserPanel/MarketingNote/InsertNote';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateMarketingNote = (param) => async () => {
    const url = 'Portal/UserPanel/MarketingNote/UpdateNote';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}