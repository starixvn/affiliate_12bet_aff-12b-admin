import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SET_PARAM, SET_CHECKED_ITEMS, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_IS_GENERAL } from '../actions/types';

export const param = createReducer({ PageIndex: 1, PageSize: 20 }, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const checkedItems = createReducer({}, {
    [SET_CHECKED_ITEMS](prevState, action) {
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

export const isGeneral = createReducer(true, {
    [SET_IS_GENERAL](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    pagingOption,
    isShowLoader,
    checkedItems,
    shouldResetScrollPosition,
    gridData,
    param,
    isGeneral
});