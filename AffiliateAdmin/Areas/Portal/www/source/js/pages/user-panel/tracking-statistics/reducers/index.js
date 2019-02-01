import { combineReducers } from 'redux';
import { SET_SHOW_STATISTICS_DETAIL, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { isShowLoader } from '../../../../reducers/loaderReducer';

export const trackingCodeList = createReducer({}, {});

export const isShowStatisticsDetail = createReducer(false, {
    [SET_SHOW_STATISTICS_DETAIL](prevState, action) {
        return action.data;
    }
});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    isShowLoader,
    isShowStatisticsDetail,
    trackingCodeList,
    param
});