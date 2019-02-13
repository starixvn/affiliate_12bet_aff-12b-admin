import { combineReducers } from 'redux';
import { SET_PARAM, SET_MONTH } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { isShowLoader } from '../../../../reducers/loaderReducer';

export const trackingCodeList = createReducer({}, {});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const month = createReducer('', {
    [SET_MONTH](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    isShowLoader,
    trackingCodeList,
    param,
    month
});