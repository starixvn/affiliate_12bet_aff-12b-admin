import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { SET_DATE, SET_GRID_DATA, SET_TOGGLE_POPUP, SET_SELECTED_EXCHANGE_RATE, SHOULD_RESET_SCROLL_POSITION } from '../actions/types';

const today = createReducer(null, {});
const hasPermission = createReducer(null, {});

export const date = createReducer(null, {
    [SET_DATE](prevState, action) {
        return action.data;
    }
});

export const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
});

export const isOpenPopup = createReducer(false, {
    [SET_TOGGLE_POPUP](prevState, action) {
        return action.data;
    }
});

export const selectedExchangeRate = createReducer(null, {
    [SET_SELECTED_EXCHANGE_RATE](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const shouldResetScrollPosition = createReducer(true, {
    [SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    today,
    hasPermission,
    date,
    gridData,
    isOpenPopup,
    isShowLoader,
    selectedExchangeRate,
    shouldResetScrollPosition
});