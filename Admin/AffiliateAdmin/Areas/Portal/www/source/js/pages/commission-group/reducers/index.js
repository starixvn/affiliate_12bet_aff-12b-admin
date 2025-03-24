import { combineReducers } from "redux";

import * as types from '../actions/types';
import createReducer from '../../../modules/createReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { pagingOption } from '../../../reducers/pagingReducer';

export const gridData = createReducer(null, {
    [types.SET_GRIDDATA](prevState, action) {
        return action.data;
    }
})

export const searchedGroupName = createReducer('', {
    [types.SET_SEARCHED_GROUPNAME](prevState, action) {
        return action.data;
    }
})

export const selectedGroup = createReducer(null, {
    [types.SET_SELECTED_GROUP](prevState, action) {
        return !action.data ? null : Object.assign({}, prevState, action.data);
    }
})

export const productList = createReducer([], {});

export const deductionList = createReducer([], {});

export const defaultCurrency = createReducer(null, {});

export default combineReducers({
    gridData,
    pagingOption,
    productList,
    deductionList,
    selectedGroup,
    searchedGroupName,
    isShowLoader,
    defaultCurrency,
});