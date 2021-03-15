import { combineReducers } from 'redux';
import { RESET_REDUCER } from '../actions/types';

import { selectedMenu, adminRoleReducer } from './children-reducers';
import { isShowLoader, userId, userName, hasWriteAdminRoleClaim } from './common-reducers';

const allReducers = combineReducers({
    ...adminRoleReducer,
    selectedMenu,
    isShowLoader,
    userId,
    userName,
    hasWriteAdminRoleClaim
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