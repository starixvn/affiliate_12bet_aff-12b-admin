import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';
import { SET_PARAM, SET_CHECKED_USERS, SET_IS_BACK, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_USER_INFO } from '../actions/types';

export const isBack = createReducer(false, {
    [SET_IS_BACK](prevState, action) {
        return action.data;
    }
})

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

export const userInfo = createReducer(null, {
    [SET_USER_INFO](prevState, action) {
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
    param,
    checkedUsers,
    gridData,
    userInfo,
    shouldResetScrollPosition
});