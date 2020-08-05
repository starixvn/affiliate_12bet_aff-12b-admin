import { combineReducers } from 'redux';
import { RESET_REDUCER } from '../actions/types';

import { selectedMenu, adminRoleReducer } from './children-reducers';
import { isShowLoader, userId, userName } from './common-reducers';

const allReducers = combineReducers({
    ...adminRoleReducer,
    selectedMenu,
    isShowLoader,
    userId,
    userName
});

export const rootReducer = (state, action) => {
    const newState = { ...state };
    if (action.type === RESET_REDUCER) {
        const children = action.data;
        newState[children] = undefined;
    }

    return allReducers(newState, action);
};

export default rootReducer;