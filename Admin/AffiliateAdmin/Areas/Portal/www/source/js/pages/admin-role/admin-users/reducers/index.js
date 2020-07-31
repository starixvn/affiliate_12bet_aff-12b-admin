import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';
import { SET_PARAM, SET_CHECKED_USERS, SET_GRID_DATA, SHOULD_RESET_SCROLL_POSITION } from '../actions/types';

export const isBack = createReducer(false, {})

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const checkedUsers = createReducer({}, {
    [SET_CHECKED_USERS](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    isBack,
    isShowLoader,
    param,
    gridData,
    checkedUsers,
    shouldResetScrollPosition
});