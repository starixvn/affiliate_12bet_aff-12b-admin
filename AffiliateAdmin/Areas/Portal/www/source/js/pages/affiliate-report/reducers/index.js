import { combineReducers } from 'redux';

import { pagingOption } from '../../../reducers/pagingReducer';

import createReducer from '../../../modules/createReducer';
import { SET_GRID_DATA, SET_REPORT_TITLE, SET_PARAM } from '../actions/types';

const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

const productList = createReducer([], {});

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
    gridData,
    pagingOption,
    reportTitle,
    param
});