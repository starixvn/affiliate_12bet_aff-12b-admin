import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import {claimsPrincipal} from '../../../../reducers/claimReducer';
import * as types from '../actions/types';

export const userNames = createReducer('', {
    [types.SET_USER_NAMES](prevState, action) {
        return action.data;
    }
})

export const message = createReducer('', {
    [types.SET_MESSAGE](prevState, action) {
        return action.data;
    }
})

export const memberNotExist = createReducer('', {
    [types.SET_MEMBER_NOT_EXIST](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [types.SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const activatedGridData = createReducer('', {
    [types.SET_ACTIVATED_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export const unactivatedGridData = createReducer('', {
    [types.SET_UNACTIVATED_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    isShowLoader,
    userNames,
    message,
    memberNotExist,
    activatedGridData,
    unactivatedGridData,
    shouldResetScrollPosition,
    claimsPrincipal
});