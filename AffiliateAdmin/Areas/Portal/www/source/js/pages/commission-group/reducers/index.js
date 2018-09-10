import { combineReducers } from "redux";

import { SET_GRIDDATA, SET_PAGING_OPTION, SET_SELECTED_GROUP, SET_SEARCHED_GROUPNAME } from '../actions/types';
import createReducer from '../../../modules/createReducer';
import { defaultPagingOption } from '../modules/constant';
import { isShowLoader } from '../../../reducers/loaderReducer';

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

const searchedGroupName = createReducer('', {
    [SET_SEARCHED_GROUPNAME](prevState, action) {
        return action.data;
    }
})

export const selectedGroup = createReducer(null, {
    [SET_SELECTED_GROUP](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const productList = createReducer([], {});
export const deductionList = createReducer([], {});

export default combineReducers({ gridData, pagingOption, productList, deductionList, selectedGroup, searchedGroupName, isShowLoader });