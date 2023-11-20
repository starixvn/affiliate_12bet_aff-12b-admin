import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import {claimsPrincipal} from '../../../../reducers/claimReducer';
import * as types from '../actions/types';

export const targetProductList = createReducer(null, {});
export const languageList = createReducer(null, {});
export const affTypeList = createReducer(null, {});
export const today = createReducer(null, {});

export const bannerRootUrl = createReducer(null, {});

export const sizeList = createReducer(null, {
    [types.SET_SIZE_LIST](prevState, action) {
        return action.data;
    }
});

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const paramFromHome = createReducer(null, {
    [types.SET_PARAM_FROM_HOME](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const bannerDetailUrl = createReducer(null, {
    [types.SET_BANNER_DETAIL_URL](prevState, action) {
        return Object.assign({}, { ...action.data });
    }
})

export const selectedBannerId = createReducer(null, {
    [types.SET_SELECTED_BANNER_ID](prevState, action) {
        return action.data;
    }
})

export const checkedBanners = createReducer({}, {
    [types.SET_CHECKED_BANNERS](prevState, action) {
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
    targetProductList,
    languageList,
    sizeList,
    affTypeList,
    bannerRootUrl,
    today,
    param,
    paramFromHome,
    gridData,
    pagingOption,
    isShowLoader,
    checkedBanners,
    bannerDetailUrl,
    selectedBannerId,
    shouldResetScrollPosition,
    claimsPrincipal
});