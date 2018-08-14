import { combineReducers } from "redux";

import { SET_GRIDDATA, SET_PAGING_OPTION, SET_SELECTED_GROUP } from '../actions/types';
import createReducer from '../../../modules/createReducer';
import { defaultPagingOption } from '../modules/constant';

const gridData = createReducer([], {
    [SET_GRIDDATA](prevState, action) {
        return action.data;
    }
})

const pagingOption = createReducer(defaultPagingOption, {
    [SET_PAGING_OPTION](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const selectedGroup = createReducer(null, {
    [SET_SELECTED_GROUP](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const productList = createReducer([], {});
export const deductionList = createReducer([], {});

export default combineReducers({ gridData, pagingOption, productList, deductionList, selectedGroup });