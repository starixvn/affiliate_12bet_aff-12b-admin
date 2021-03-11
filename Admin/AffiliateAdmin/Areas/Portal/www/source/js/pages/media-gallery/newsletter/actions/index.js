import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { SET_CHECKED_NEWSLETTER_PAGES, SET_SELECTED_NEWSLETTER_PAGE_ID, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_SELECTED_PREVIEW_PAGE_ID } from './types';

export { toggleLoader, setPagingOption };

export const setSelectedNewsLetterPageId = (data) => {
    return {
        type: SET_SELECTED_NEWSLETTER_PAGE_ID,
        data
    }
}

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setSelectedPreviewPageId = data => {
    return {
        type: SET_SELECTED_PREVIEW_PAGE_ID,
        data
    }
}

export const setCheckedNewsLetterPages = (data) => {
    return {
        type: SET_CHECKED_NEWSLETTER_PAGES,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/NewsLetter/GetNewsLetterList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedNewsLetterPages = data ? _convertCheckedNewsLetterPages(data.MainData) : {};

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedNewsLetterPages(checkedNewsLetterPages));
    dispatch(toggleLoader(false));
}

function _convertCheckedNewsLetterPages(dataList) {
    const checkedNewsLetterPages = {};
    for (let i = 0; i < dataList.length; i++) {
        const itemId = dataList[i].NewsLetterId;
        checkedNewsLetterPages[itemId] = false;
    }

    return checkedNewsLetterPages;
}

export const getNewsLetterPageDetail = (selectedNewsLetterPageId) => async () => {
    const url = 'Portal/NewsLetter/GetNewsLetterDetailById';
    const response = await post(url, { id: selectedNewsLetterPageId });

    return response && response.status === 200 ? response.data : null;
}

export const insertNewsLetterPage = (param) => async () => {
    const url = 'Portal/NewsLetter/InsertNewsLetter';
    return await handleExcute(url, param);
}

export const updateNewsLetterPage = (param) => async () => {
    const url = 'Portal/NewsLetter/UpdateNewsLetter';
    return await handleExcute(url, param);

}

export const deleteNewsLetterPages = (newsLetterPageIds) => async () => {
    const url = 'Portal/NewsLetter/DeleteNewsLetters';
    const response = await post(url, { ids: newsLetterPageIds });

    return response && response.status === 200 ? response.data : null;
}

const handleExcute = async (url, param) => {
    const formData = new FormData();
    for (let key in param ) {
        formData.append(key, param[key]);
    }

    const response = await post(url, formData);

    return response && response.status === 200 ? response.data : null;
}