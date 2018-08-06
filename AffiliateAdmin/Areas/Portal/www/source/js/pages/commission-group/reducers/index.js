import { combineReducers } from "redux";

import { SET_GRIDDATA, SET_SELECTEDGROUP, SET_PAGING_OPTION } from '../actions/types';
import createReducer from '../../../modules/createReducer';
import { defaultPagingOption } from '../modules/constant';

const gridData = createReducer([], {
    [SET_GRIDDATA](state, action) {
        return action.data;
    }
})

const selectedGroup = createReducer({}, {
    [SET_SELECTEDGROUP](state, action) {
        return action.data;
    }
})

const pagingOption = createReducer(defaultPagingOption, {
    [SET_PAGING_OPTION](state, action) {
        return Object.assign({}, state, action.data);
    }
})

export default combineReducers({ gridData, selectedGroup, pagingOption });