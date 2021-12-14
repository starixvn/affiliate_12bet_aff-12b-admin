import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import {claimsPrincipal} from '../../../../reducers/claimReducer';
import * as types from '../actions/types';

export const exclusivePageList = createReducer(null, {});
export const previewLink = createReducer(null, {});

export const selectedId = createReducer(null, {
    [types.SET_SELECTED_ID](prevState, action) {
        return action.data;
    }
})

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const checkedIds = createReducer({}, {
    [types.SET_CHECKED_IDS](prevState, action) {
        return action.data;
    }
})

export const selectedPreviewId = createReducer(null, {
    [types.SET_SELECTED_PREVIEW_ID](prevState, action) {
        return action.data;
    }
})

export const isFromAnotherPage = createReducer(false, {
    [types.SET_IS_FROM_ANOTHER_PAGE](prevState, action) {
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
    gridData,
    isShowLoader,
    checkedIds,
    selectedId,
    pagingOption,
    shouldResetScrollPosition,
    selectedPreviewId,
    isFromAnotherPage,
    exclusivePageList,
    previewLink,
    param,
    claimsPrincipal
});