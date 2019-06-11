import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { SET_PARAM, SET_SHOW_POPUP } from '../actions/types';

export const today = createReducer(null, {});

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const isShowPopup = createReducer(false, {
    [SET_SHOW_POPUP](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    pagingOption,
    isShowLoader,
    isShowPopup,
    today,
    param
});