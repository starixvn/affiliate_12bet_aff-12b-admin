import { combineReducers } from 'redux';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

import createReducer from '../../../../modules/createReducer';
import * as types from '../actions/types.js';
import { defaultParams } from '../libs/constants';

export const reportTitle = createReducer('', {
    [types.SET_REPORT_TITLE](prevState, action) {
        return `PLAYER CPA REPORT - Deal Name contains '${dealName}' - View By ${viewBy} - FROM ${fromDate} TO ${toDate}`;
    }
})

export const params = createReducer(defaultParams, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const isSubmit = createReducer(false, {
    [types.TOGGLE_SUBMIT](prevState, action) {
        return !prevState;
    }
})

export default combineReducers({
    params,
    reportTitle,
    isSubmit,
    isShowLoader,
    pagingOption,
});