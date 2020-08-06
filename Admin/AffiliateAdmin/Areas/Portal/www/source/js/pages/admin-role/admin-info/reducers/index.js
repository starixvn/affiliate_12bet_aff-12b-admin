import { combineReducers } from 'redux';
import createReducer from '../../../../modules/createReducer';
import { SET_ROLE_LIST } from '../actions/types';

export const roleList = createReducer(null, {
    [SET_ROLE_LIST](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    roleList
});