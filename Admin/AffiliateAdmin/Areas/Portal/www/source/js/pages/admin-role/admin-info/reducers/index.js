import { combineReducers } from 'redux';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';
import { SET_USER_NAME, SET_USER_INFO } from '../actions/types';

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
    userName,
    userInfo,
    isShowLoader
});