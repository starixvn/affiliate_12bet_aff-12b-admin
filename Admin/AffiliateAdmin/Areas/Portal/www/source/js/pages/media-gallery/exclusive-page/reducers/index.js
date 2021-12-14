import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import {claimsPrincipal} from '../../../../reducers/claimReducer';
import * as types from '../actions/types';

export const languageList = createReducer(null, {});
export const previewLink = createReducer(null, {});

export const selectedId = createReducer(null, {
    [types.SET_SELECTED_ID](prevState, action) {
        return action.data;
    }
})

const defaultParam = {
    pageIndex: 1,
    pageSize: 20
}

export const param = createReducer(defaultParam, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const checkedIds = createReducer({}, {
    [types.SET_CHECKED_IDS](prevState, action) {
        return action.data;
    }
})

export const selectedPreviewId = createReducer(null,{
    [types.SET_SELECTED_PREVIEW_ID](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [types.SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer([], {
    [types.SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    gridData,
    isShowLoader,
    selectedId,
    checkedIds,
    pagingOption,
    shouldResetScrollPosition,
    selectedPreviewId,
    languageList,
    previewLink,
    param,
    claimsPrincipal
});