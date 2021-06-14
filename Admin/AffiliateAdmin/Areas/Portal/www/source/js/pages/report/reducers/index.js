import { combineReducers } from 'redux';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import {claimsPrincipal} from '../../../reducers/claimReducer';

import createReducer from '../../../modules/createReducer';
import * as types from '../actions/types';
import { viewLevel } from '../libs/constants';

const productList = createReducer([], {});

const languageList = createReducer({}, {});

const today = createReducer('', {});

export const reportTitle = createReducer('', {
    [types.SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const currentViewLevel = createReducer(viewLevel.main, {
    [types.SET_VIEW_LEVEL](prevState, action) {
        return action.data;
    }
})

export const isCheckedDatePicker = createReducer(false, {
    [types.SET_IS_CHECKED_DATE_PICKER](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    productList,
    pagingOption,
    reportTitle,
    param,
    languageList,
    isShowLoader,
    today,
    currentViewLevel,
    claimsPrincipal,
    isCheckedDatePicker
});