import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { SET_GRID_DATA, SET_IS_GENERAL } from '../actions/types';

export const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export const isGeneral = createReducer(true, {
    [SET_IS_GENERAL](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    gridData,
    isGeneral
});