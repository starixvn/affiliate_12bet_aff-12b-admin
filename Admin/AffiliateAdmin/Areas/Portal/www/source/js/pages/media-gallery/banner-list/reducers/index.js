import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SET_PARAM, SET_BANNER_DETAIL_URL, SET_CHECKED_BANNERS, SET_SELECTED_BANNER_ID, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from '../actions/types';

export const categoryList = createReducer(null, {});
export const subCategoryCasinoList = createReducer(null, {});
export const subCategorySportsbookList = createReducer(null, {});
export const languageList = createReducer(null, {});
export const currencyList = createReducer(null, {});
export const imageTypeList = createReducer(null, {});
export const sizeList = createReducer(null, {});
export const affTypeList = createReducer(null, {});
export const bannerUrl = createReducer(null, {});

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const bannerDetailUrl = createReducer(null, {
    [SET_BANNER_DETAIL_URL](prevState, action) {
        console.log(action.data);
        return Object.assign({}, { ...action.data });
    }
})

export const selectedBannerId = createReducer(null, {
    [SET_SELECTED_BANNER_ID](prevState, action) {
        return action.data;
    }
})

export const checkedBanners = createReducer({}, {
    [SET_CHECKED_BANNERS](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    categoryList,
    subCategoryCasinoList,
    subCategorySportsbookList,
    languageList,
    currencyList,
    imageTypeList,
    sizeList,
    affTypeList,
    bannerUrl,
    param,
    gridData,
    pagingOption,
    isShowLoader,
    checkedBanners,
    bannerDetailUrl,
    selectedBannerId,
    shouldResetScrollPosition
});