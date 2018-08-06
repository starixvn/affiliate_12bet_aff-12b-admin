import { combineReducers } from "redux";

import { SET_GRIDDATA, SET_SELECTEDGROUP } from '../actions/types';
import createReducer from '../../../modules/createReducer';

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

export default combineReducers({ gridData, selectedGroup });