import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { SET_FILE, SET_IS_CLICK_UPLOAD } from '../actions/types';
import { isShowLoader } from '../../../../reducers/loaderReducer';

export const file = createReducer(null, {
    [SET_FILE](prevState, action){
        return action.data;
    }
});

export const isClickUpload = createReducer(false, {
    [SET_IS_CLICK_UPLOAD](prevState, action){
        return action.data;
    }
});

export default combineReducers({
    file,
    isShowLoader,
    isClickUpload
});