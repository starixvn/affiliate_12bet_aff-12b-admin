import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SET_PARAM, SET_IS_FROM_OTHER_PAGES, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from '../actions/types';

const affName = createReducer(null, {})

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const isFromOtherPages = createReducer(false, {
    [SET_IS_FROM_OTHER_PAGES](prevState, action) {
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
    param,
    affName,
    gridData,
    pagingOption,
    isShowLoader,
    isFromOtherPages,
    shouldResetScrollPosition
});