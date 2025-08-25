import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';

import * as types from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setAssetDetailUrl = (data) => {
    return {
        type: types.SET_ASSET_DETAIL_URL,
        data
    }
}

export const setSelectedAssetId = (data) => {
    return {
        type: types.SET_SELECTED_ASSET_ID,
        data
    }
}

export const setCheckedAssets = (data) => {
    return {
        type: types.SET_CHECKED_ASSETS,
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

export const getAssetList = (param) => async (dispatch) => {
    const url = 'Portal/LandingPage/GetAssetList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : [];

    dispatch(setAssetList(data));
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/LandingPage/GetAssetList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedAssets = data ? _convertCheckedAssets(data.MainData) : {};

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedAssets(checkedAssets));
    dispatch(toggleLoader(false));
}

function _convertCheckedAssets(dataList) {
    const checkedAssets = {};
    for (let item of dataList) {
        const itemId = item.AssetId;
        checkedAssets[itemId] = false;
    }

    return checkedAssets;
}

export const getAssetDetail = (selectedAssetId) => async () => {
    const url = 'Portal/LandingPage/GetAssetDetail';
    const response = await post(url, { selectedAssetId });

    return response && response.status === 200 ? response.data : null;
}

export const insertAsset = (param) => () => {
    const url = 'Portal/LandingPage/InsertAsset';

    return handleExcute(url, param);
}

export const updateAsset = (param) => () => {
    const url = 'Portal/LandingPage/UpdateAsset';

    return handleExcute(url, param);
}

const handleExcute = async (url, param) => {
    const formData = new FormData();
    for (let key in param) {
        key !== 'files' && formData.append(key, param[key]);
    }

    const files = param['files'];

    if (files) {
        for (let file of files) {
            formData.append('files', file);
        }
    }

    const response = await post(url, formData);

    return response && response.status === 200 ? response.data : null;
}

export const deleteAssets = (param) => async () => {
    const url = 'Portal/LandingPage/DeleteAssets';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}