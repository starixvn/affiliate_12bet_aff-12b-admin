import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { fileUpload } from '../../libs/constants';
import { defaultPagingOption } from '../../../../modules/constants';
import * as types from '../actions/types';

export const statusList = createReducer(null, {});
export const maxFileSize = createReducer(fileUpload.MaxFileSize, {});
export const maximumFiles = createReducer(fileUpload.MaximumFiles, {});
export const bannerRootUrl = createReducer(null, {});
export const allowedExtensions = createReducer(fileUpload.AllowedImageExtensions, {});

export const formNameList = createReducer(null, {
    [types.SET_FORM_NAME_LIST](prevState, action) {
        return action.data;
    }
});
export const targetCountryList = createReducer(null, {
    [types.SET_TARGET_COUNTRY_LIST](prevState, action) {
        return action.data;
    }
});
export const languageList = createReducer(null, {
    [types.SET_LANGUAGE_LIST](prevState, action) {
        return action.data;
    }
});

export const param = createReducer({ PageIndex: defaultPagingOption.PageIndex, PageSize: defaultPagingOption.PageSize }, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const formDetailUrl = createReducer(null, {
    [types.SET_FORM_DETAIL_URL](prevState, action) {
        return Object.assign({}, { ...action.data });
    }
})

export const selectedMediaId = createReducer(null, {
    [types.SET_SELECTED_MEDIA_ID](prevState, action) {
        return action.data;
    }
})

export const checkedForms = createReducer({}, {
    [types.SET_CHECKED_FORMS](prevState, action) {
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
    formNameList,
    targetCountryList,
    languageList,
    statusList,
    maxFileSize,
    maximumFiles,
    allowedExtensions,
    bannerRootUrl,
    param,
    gridData,
    pagingOption,
    isShowLoader,
    checkedForms,
    shouldResetScrollPosition,
    formDetailUrl,
    selectedMediaId,
});