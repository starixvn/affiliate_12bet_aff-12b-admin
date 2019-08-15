import { combineReducers } from 'redux';
import { SET_MONTHDATE, TOGGLE_LOADER_CUSTOMER_STATISTICS } from '../actions/types';

import createReducer from '../../../../modules/createReducer';

export const monthDate = createReducer(null, {
    [SET_MONTHDATE](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    monthDate
});