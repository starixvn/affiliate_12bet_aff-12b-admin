import { combineReducers } from 'redux';
import { SET_USER_ACCOUNT_INFO, SET_MORE_SITE_URLS_LIST } from '../actions/types';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const userAccountInfo = createReducer(null, {
    [SET_USER_ACCOUNT_INFO](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

const moreSiteUrlsList = createReducer([], {
    [SET_MORE_SITE_URLS_LIST](prevState, action) {
        return [...action.data];
    }
});

export default combineReducers({
    isShowLoader,
    userAccountInfo,
    moreSiteUrlsList
});