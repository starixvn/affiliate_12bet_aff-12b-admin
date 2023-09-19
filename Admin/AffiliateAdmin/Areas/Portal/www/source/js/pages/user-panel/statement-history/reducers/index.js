import { combineReducers } from 'redux';
import * as types from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return { ...action.data };
    }
});

export const transactionId = createReducer(null, {
    [types.TRANSACTION_ID](prevState, action) {
        return action.data;
    }
});

export const expandMonthDateList = createReducer({}, {
    [types.SET_EXPAND_MONTH_DATE_LIST](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    pagingOption,
    param,
    transactionId,
    expandMonthDateList
});