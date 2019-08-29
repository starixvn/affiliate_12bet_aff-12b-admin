import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { SET_PARAM, SET_GRID_DATA, SET_TOGGLE_POPUP, SET_SELECTED_CURRENTCY, SHOULD_RESET_SCROLL_POSITION } from '../actions/types';

const today = createReducer(null, {});

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
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

export const selectedCurrency = createReducer(null, {
    [SET_SELECTED_CURRENTCY](prevState, action) {
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
    param,
    gridData,
    isOpenPopup,
    isShowLoader,
    selectedCurrency,
    shouldResetScrollPosition
});