import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';
import { SET_USER_NAME, SET_USER_INFO, SET_USER_ID } from '../actions/types';

export const roleList = createReducer(null, {})

export const userId = createReducer('', {
    [SET_USER_ID](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const userName = createReducer('', {
    [SET_USER_NAME](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const userInfo = createReducer(null, {
    [SET_USER_INFO](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    userId,
    userName,
    userInfo,
    isShowLoader,
    roleList
});