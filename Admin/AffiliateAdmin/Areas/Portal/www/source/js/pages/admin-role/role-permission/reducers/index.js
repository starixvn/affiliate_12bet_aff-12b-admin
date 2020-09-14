import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { SET_GRID_DATA, SET_IS_GENERAL, SET_ROLE_LIST, SET_MARKET_ID_BY_USER_LIST, SET_MARKET_BY_USER_LIST } from '../actions/types';

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

export const roleList = createReducer(null, {
    [SET_ROLE_LIST](prevState, action) {
        return action.data;
    }
})

export const marketIdByUserList = createReducer(null, {
    [SET_MARKET_ID_BY_USER_LIST](prevState, action) {
        return action.data;
    }
})

export const marketByUserList = createReducer(null, {
    [SET_MARKET_BY_USER_LIST](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    gridData,
    isGeneral,
    roleList,
    marketIdByUserList,
    marketByUserList
});