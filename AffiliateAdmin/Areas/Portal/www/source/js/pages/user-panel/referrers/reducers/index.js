import { combineReducers } from 'redux';
import { SET_DEVICE, SET_FROM_DATE, SET_TO_DATE, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

export const device = createReducer(0, {
    [SET_DEVICE](prevState, action) {
        return action.data;
    }
});

const today = new Date();
const defaultFromDate = `${today.getMonth()}/01/${today.getFullYear()}`;
const defaultToDate = `${today.getMonth()}/${today.getDate()}/${today.getFullYear()}`;

export const fromDate = createReducer(defaultFromDate, {
    [SET_FROM_DATE](prevState, action) {
        return action.data;
    }
});

export const toDate = createReducer(defaultToDate, {
    [SET_TO_DATE](prevState, action) {
        return action.data;
    }
});

const defaultParam = {
    Device: 0,
    FromDate: defaultFromDate,
    ToDate: defaultToDate
}
export const param = createReducer(defaultParam, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    device,
    fromDate,
    toDate,
    param
});