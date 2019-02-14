import { combineReducers } from 'redux';
import { SET_DEVICE_TYPE, SET_FROM_DATE, SET_TO_DATE, SET_SITE_URL, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

export const deviceType = createReducer(0, {
    [SET_DEVICE_TYPE](prevState, action) {
        return action.data;
    }
});

//const today = new Date();
//const defaultFromDate = `${today.getMonth() + 1}/01/${today.getFullYear()}`;
//const defaultToDate = `${today.getMonth() + 1}/${today.getDate()}/${today.getFullYear()}`;

export const fromDate = createReducer('', {
    [SET_FROM_DATE](prevState, action) {
        return action.data;
    }
});

export const toDate = createReducer('', {
    [SET_TO_DATE](prevState, action) {
        return action.data;
    }
});

export const siteUrl = createReducer('', {
    [SET_SITE_URL](prevState, action) {
        return action.data;
    }
});

//const defaultParam = {
//    Device: 0,
//    FromDate: defaultFromDate,
//    ToDate: defaultToDate
//}
export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    deviceType,
    fromDate,
    toDate,
    siteUrl,
    param
});