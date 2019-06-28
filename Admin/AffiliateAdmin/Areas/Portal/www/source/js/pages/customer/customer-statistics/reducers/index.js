import { combineReducers } from 'redux';
import { SET_MONTHDATE, TOGGLE_LOADER_CUSTOMER_STATISTICS } from '../actions/types';

import createReducer from '../../../../modules/createReducer';

export const monthDate = createReducer(null, {
    [SET_MONTHDATE](prevState, action) {
        return action.data;
    }
})

export const isShowLoader = createReducer(false, {
    [TOGGLE_LOADER_CUSTOMER_STATISTICS](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    monthDate,
    isShowLoader
});