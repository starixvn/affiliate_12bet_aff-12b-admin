import { combineReducers } from 'redux';

import { pagingOption } from '../../../reducers/pagingReducer';

import createReducer from '../../../modules/createReducer';
import * as actionTypes from '../actions/types';

import { sortByValues, allProduct } from '../modules/constants';

const affiliate = createReducer('', {
    [actionTypes.SET_SEARCHED_AFFILIATE](prevState, action) {
        return action.data
    }
})

const selectedLanguage = createReducer('', {
    [actionTypes.SET_SELECTED_LANGUAGE](prevState, action) {
        return action.data;
    }
})

const now = new Date();
const defaultMonth = { month: now.getMonth() + 1, year: now.getFullYear() };
const fromMonth = createReducer(defaultMonth, {
    [actionTypes.SET_FROM_MONTH](prevState, action) {
        return action.data;
    }
})

const toMonth = createReducer(defaultMonth, {
    [actionTypes.SET_TO_MONTH](prevState, action) {
        return action.data;
    }
})

const isGetAll = createReducer(false, {
    [actionTypes.SET_CHECKED_GETALL](prevState) {
        return !prevState;
    }
})

const sortBy = createReducer(sortByValues.Affiliate, {
    [actionTypes.SET_SORTBY](prevState, action) {
        return action.data;
    }
})

const isSortAscending = createReducer(true, {
    [actionTypes.SET_CHECKED_SORT_ASC](prevState, action) {
        return !prevState;
    }
})

const gridData = createReducer(null, {
    [actionTypes.SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

const selectedProductList = createReducer(allProduct, {
    [actionTypes.SET_SELECTED_PRODUCTLIST](prevState, action) {
        return action.data;
    }
});

const productList = createReducer([], {});

const reportTitle = createReducer('', {
    [actionTypes.SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

const param = createReducer(null, {
    [actionTypes.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export default combineReducers({
    affiliate,
    selectedLanguage,
    fromMonth,
    toMonth,
    isGetAll,
    sortBy,
    isSortAscending,
    productList,
    selectedProductList,
    gridData,
    pagingOption,
    reportTitle,
    param
});