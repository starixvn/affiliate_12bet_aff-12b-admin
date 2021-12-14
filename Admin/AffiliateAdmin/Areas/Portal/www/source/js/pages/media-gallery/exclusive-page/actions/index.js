import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { submitIsFailed } from '../../../../modules/constants';
import * as types from './types';

export { toggleLoader, setPagingOption };

export const setSelectedId = (data) => {
    return {
        type: types.SET_SELECTED_ID,
        data
    }
}

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setSelectedPreviewId = data => {
    return {
        type: types.SET_SELECTED_PREVIEW_ID,
        data
    }
}

export const setCheckedIds = (data) => {
    return {
        type: types.SET_CHECKED_IDS,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: types.SET_GRID_DATA,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/ExclusivePage/GetExclusivePageList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedIds = data ? _convertCheckedIds(data.MainData) : {};

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedIds(checkedIds));
    dispatch(toggleLoader(false));
}

function _convertCheckedIds(dataList) {
    const checkedIds = {};
    for (let item of dataList) {
        checkedIds[item.ExclusivePageId] = false;
    }

    return checkedIds;
}

export const getExclusivePageDetail = (selectedId) => async () => {
    const url = 'Portal/ExclusivePage/GetExclusivePageDetailById';
    const response = await post(url, { exclusivePageId: selectedId });

    return response && response.status === 200 ? response.data : submitIsFailed;
}

export const insertExclusivePage = (param) => async () => {
    const url = 'Portal/ExclusivePage/InsertExclusivePage';
    return handleExcute(url, param);
}

export const updateExclusivePage = (param) => async () => {
    const url = 'Portal/ExclusivePage/UpdateExclusivePage';
    return handleExcute(url, param);
}

export const deleteExclusivePages = (selectedIds) => async () => {
    const url = 'Portal/ExclusivePage/DeleteExclusivePages';
    const response = await post(url, { exclusivePageIds: selectedIds });

    return response && response.status === 200 ? response.data : submitIsFailed;
}

const handleExcute = async (url, param) => {
    const formData = new FormData();
    for (let key in param ) {
        formData.append(key, param[key]);
    }

    const response = await post(url, formData);

    return response && response.status === 200 ? response.data : submitIsFailed;
}