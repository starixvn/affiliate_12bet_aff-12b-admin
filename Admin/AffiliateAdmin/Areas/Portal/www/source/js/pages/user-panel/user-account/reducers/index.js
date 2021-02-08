import { combineReducers } from 'redux';
import * as types from '../actions/types';
import createReducer from '../../../../modules/createReducer';

export const userAccountInfo = createReducer(null, {
    [types.SET_USER_ACCOUNT_INFO](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const moreSiteUrlsList = createReducer(null, {
    [types.SET_MORE_SITE_URLS_LIST](prevState, action) {
        return [...action.data];
    }
});

export const hasWriteEditAffClaim = createReducer(false, {
    [types.SET_HAS_WRITE_EDIT_AFF_CLAIM](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    userAccountInfo,
    moreSiteUrlsList,
    hasWriteEditAffClaim
});