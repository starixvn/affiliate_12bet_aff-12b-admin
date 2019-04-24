import { combineReducers } from 'redux';
import { SET_USER_ACCOUNT_INFO, SET_MORE_SITE_URLS_ARRAY, SET_SELECTED_PAYMENT_METHOD } from '../actions/types';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import createReducer from '../../../../modules/createReducer';

const affId = createReducer(null, {});

const userAccountInfo = createReducer(null, {
    [SET_USER_ACCOUNT_INFO](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

const moreSiteUrlsArray = createReducer([], {
    [SET_MORE_SITE_URLS_ARRAY](prevState, action) {
        return [...action.data];
    }
});

const selectedPaymentMethod = createReducer('Player', {
    [SET_SELECTED_PAYMENT_METHOD](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    isShowLoader,
    affId,
    userAccountInfo,
    moreSiteUrlsArray,
    selectedPaymentMethod
});