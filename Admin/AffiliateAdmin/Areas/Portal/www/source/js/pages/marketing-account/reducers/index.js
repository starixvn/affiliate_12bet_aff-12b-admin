import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import * as types from '../actions/types';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';

const languageCodeList = createReducer({}, {});
const targetCountryCodeList = createReducer({}, {});
const canAddAffMktAccount = createReducer(false, {});

export const param = createReducer(null, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const isShowAddAffMktAccountPopup = createReducer(false, {
    [types.SET_IS_SHOW_ADD_AF_MKT_ACCOUNT_POPUP](prevState, action) {
        return action.data;
    }
});

export const affId = createReducer(null, {
    [types.SET_AFF_ID](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    param,
    pagingOption,
    isShowLoader,
    languageCodeList,
    targetCountryCodeList,
    canAddAffMktAccount,
    isShowAddAffMktAccountPopup,
    affId
});