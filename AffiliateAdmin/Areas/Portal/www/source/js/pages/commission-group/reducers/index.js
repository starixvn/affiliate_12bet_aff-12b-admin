import { combineReducers } from "redux";

import { SET_GRIDDATA, SET_SELECTEDGROUP, SET_PAGING_OPTION, INSERT_COMM_GROUP_SUCCESS } from '../actions/types';
import createReducer from '../../../modules/createReducer';
import { defaultPagingOption } from '../modules/constant';

const gridData = createReducer([], {
    [SET_GRIDDATA](prevState, action) {
        return action.data;
    },
    [INSERT_COMM_GROUP_SUCCESS](prevState, action) {
        return [...prevState, action.data];
    }
})

const selectedGroup = createReducer(null, {
    [SET_SELECTEDGROUP](prevState, action) {
        return action.data;
    }
})

const pagingOption = createReducer(defaultPagingOption, {
    [SET_PAGING_OPTION](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

const productList = createReducer([], {});

export default combineReducers({ gridData, selectedGroup, pagingOption, productList });