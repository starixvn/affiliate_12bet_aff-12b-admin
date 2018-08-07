import { combineReducers } from "redux";

import { SET_GRIDDATA, SET_SELECTEDGROUP, SET_PAGING_OPTION, GET_PRODUCT_LIST } from '../actions/types';
import createReducer from '../../../modules/createReducer';
import { defaultPagingOption } from '../modules/constant';

const gridData = createReducer([], {
    [SET_GRIDDATA](state, action) {
        return action.data;
    }
})

const selectedGroup = createReducer(null, {
    [SET_SELECTEDGROUP](state, action) {
        return action.data;
    }
})

const pagingOption = createReducer(defaultPagingOption, {
    [SET_PAGING_OPTION](state, action) {
        return Object.assign({}, state, action.data);
    }
})

const productList = createReducer([], {});

export default combineReducers({ gridData, selectedGroup, pagingOption, productList });