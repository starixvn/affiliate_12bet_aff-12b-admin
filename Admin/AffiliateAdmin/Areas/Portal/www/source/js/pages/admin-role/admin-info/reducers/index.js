import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';
import { SET_ROLE_LIST, SET_IS_INFORMATION } from '../actions/types';

export const roleList = createReducer(null, {
    [SET_ROLE_LIST](prevState, action) {
        return action.data;
    }
})

export const isInformation = createReducer(true, {
    [SET_IS_INFORMATION](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    roleList,
    isInformation
});