import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import * as types from '../actions/types';

export const targetCountryList = createReducer(null, {});
export const languageList = createReducer(null, {});
export const bannerRootUrl = createReducer(null, {});
export const selectedFormId = createReducer(null, {
    [types.SET_SELECTED_FORM_ID](prevState, action) {
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
    targetCountryList,
    languageList,
    bannerRootUrl,
    checkedForms,
    selectedFormId,
    shouldResetScrollPosition,
    gridData,
    isShowLoader,
    pagingOption
});