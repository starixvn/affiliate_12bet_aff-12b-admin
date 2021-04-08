import { combineReducers } from 'redux';
import { SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return {...action.data };
    }
});

export default combineReducers({
    pagingOption,
    param
});