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

export const setIsFromAnotherPage = (data) => {
    return {
        type: types.SET_IS_FROM_ANOTHER_PAGE,
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

    const url = 'Portal/OfferAff/GetOfferAffList';
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
        checkedIds[item.AffId] = false;
    }

    return checkedIds;
}

export const insertOfferAff = (param) => async () => {
    const url = 'Portal/OfferAff/InsertOfferAff';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : submitIsFailed;
}

export const deleteOfferAffs = (selectedIds) => async () => {
    const url = 'Portal/OfferAff/DeleteOfferAffs';
    const response = await post(url, { AffIds: selectedIds });

    return response && response.status === 200 ? response.data : submitIsFailed;
}