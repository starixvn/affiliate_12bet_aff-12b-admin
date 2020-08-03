import { combineReducers } from 'redux';
import { RESET_REDUCER } from '../actions/types';

import { adminRoleReducer } from './reducer-children';

const allReducers = combineReducers({
    ...adminRoleReducer
});

export const rootReducer = (state, action) => {
    const newState = { ...state };
    if (action.type === RESET_REDUCER) {
        const children = action.data;
        newState[children] = {};
    }

    return allReducers(newState, action);
};

export default rootReducer;