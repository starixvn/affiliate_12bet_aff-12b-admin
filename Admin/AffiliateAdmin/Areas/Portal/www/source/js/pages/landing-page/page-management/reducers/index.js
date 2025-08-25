import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import * as types from '../actions/types';

export const targetCountryList = createReducer(null, {});
export const languageList = createReducer(null, {});
export const dealList = createReducer(null, {});
export const statusList = createReducer(null, {});
export const landingPageUrl = createReducer(null, {});

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const landingPageDetailUrl = createReducer(null, {
    [types.SET_LANDING_PAGE_DETAIL_URL](prevState, action) {
        return Object.assign({}, { ...action.data });
    }
})

export const selectedLandingPageId = createReducer(null, {
    [types.SET_SELECTED_LANDING_PAGE_ID](prevState, action) {
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
    targetCountryList,
    languageList,
    dealList,
    statusList,
    param,
    gridData,
    pagingOption,
    isShowLoader,
    shouldResetScrollPosition,
});