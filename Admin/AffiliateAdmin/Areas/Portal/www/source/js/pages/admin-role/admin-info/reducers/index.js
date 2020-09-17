import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';
import { SET_ROLE_LIST, SET_PERMISSION_LIST_BY_ROLE, SET_MARKET_LIST_BY_USER } from '../actions/types';

export const roleList = createReducer(null, {
    [SET_ROLE_LIST](prevState, action) {
        return action.data;
    }
})

export const permissionListByRole = createReducer(null, {
    [SET_PERMISSION_LIST_BY_ROLE](prevState, action) {
        return action.data;
    }
})

export const marketListByUser = createReducer(null, {
    [SET_MARKET_LIST_BY_USER](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    roleList,
    permissionListByRole,
    marketListByUser
});