import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { fileUpload } from '../libs/constants';
import * as types from '../actions/types';

export const moduleList = createReducer(null, {});
export const targetCountryList = createReducer(null, {});
export const languageList = createReducer(null, {});
export const maxFileSize = createReducer(fileUpload.MaxFileSize, {});
export const maximumFiles = createReducer(fileUpload.MaximumFiles, {});
export const bannerRootUrl = createReducer(null, {});
export const allowedExtensions = createReducer(fileUpload.AllowedImageExtensions, {});

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const assetDetailUrl = createReducer(null, {
    [types.SET_ASSET_DETAIL_URL](prevState, action) {
        return Object.assign({}, { ...action.data });
    }
})

export const selectedAssetId = createReducer(null, {
    [types.SET_SELECTED_ASSET_ID](prevState, action) {
        return action.data;
    }
})

export const checkedBanners = createReducer({}, {
    [types.SET_CHECKED_ASSETS](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [types.SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [types.SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    moduleList,
    targetCountryList,
    languageList,
    maxFileSize,
    maximumFiles,
    allowedExtensions,
    bannerRootUrl,
    param,
    gridData,
    pagingOption,
    isShowLoader,
    checkedBanners,
    shouldResetScrollPosition,
    assetDetailUrl,
    selectedAssetId,
});