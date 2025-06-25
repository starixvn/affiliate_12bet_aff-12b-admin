import { combineReducers } from 'redux';

import createReducer from '../../../modules/createReducer';
import * as types from '../actions/types';

import { pagingOption } from '../../../reducers/pagingReducer';
import { isShowLoader } from '../../../reducers/loaderReducer';
import { defaultPersonalInfo, defaultAdditionalInfo, defaultGeneralInfo } from '../libs/constants';

const languageCodeList = createReducer({}, {});
const targetCountryCodeList = createReducer({}, {});
const dealOptionsList = createReducer({}, {});
const canAddAffMktAccount = createReducer(false, {});
const defaultAffId = '12BETAFF001';
const defaultParams = {
    ...defaultPersonalInfo,
    ...defaultAdditionalInfo,
    ...defaultGeneralInfo
}

export const params = createReducer(defaultParams, {
    [types.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const isShowAddAffMktAccountPopup = createReducer(false, {
    [types.SET_IS_SHOW_ADD_AF_MKT_ACCOUNT_POPUP](prevState, action) {
        return action.data;
    }
});

export const affId = createReducer(defaultAffId, {
    [types.SET_AFF_ID](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    params,
    pagingOption,
    isShowLoader,
    languageCodeList,
    targetCountryCodeList,
    canAddAffMktAccount,
    isShowAddAffMktAccountPopup,
    affId,
    dealOptionsList
});