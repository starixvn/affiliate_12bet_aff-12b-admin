import { combineReducers } from 'redux';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';

import createReducer from '../../../modules/createReducer';
import { SET_REPORT_TITLE, SET_PARAM } from '../actions/types';

const productList = createReducer([], {});

const languageList = createReducer({}, {});

const reportTitle = createReducer('', {
    [SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export default combineReducers({
    productList,
    pagingOption,
    reportTitle,
    param,
    languageList,
    isShowLoader
});