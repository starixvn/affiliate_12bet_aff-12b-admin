import { combineReducers } from 'redux';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

import createReducer from '../../../../modules/createReducer';
import * as types from '../actions/types.js';
import { defaultParams } from '../libs/constants';

export const reportTitle = createReducer('', {
    [types.SET_REPORT_TITLE](prevState, action) {
        return action.data;
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

export const memberCpaId = createReducer(null, {
    [types.SET_MEMBER_CPA_ID](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    params,
    reportTitle,
    isSubmit,
    memberCpaId,
    isShowLoader,
    pagingOption,
});