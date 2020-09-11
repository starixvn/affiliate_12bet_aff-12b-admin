import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';
import { SET_ROLE_LIST, SET_IS_EDIT, SET_IS_CREATE_NEW, SET_ROLE_PERMISSION } from '../actions/types';

export const roleList = createReducer(null, {
    [SET_ROLE_LIST](prevState, action) {
        return action.data;
    }
})

export const isEdit = createReducer(false, {
    [SET_IS_EDIT](prevState, action) {
        return action.data;
    }
})

export const isCreateNew = createReducer(true, {
    [SET_IS_CREATE_NEW](prevState, action) {
        return action.data;
    }
})

export const rolePermission = createReducer(null, {
    [SET_ROLE_PERMISSION](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    roleList,
    isEdit,
    isCreateNew,
    rolePermission
});