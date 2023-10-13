import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import * as types from '../actions/types';
import { claimsPrincipal } from '../../../../reducers/claimReducer';

const targetCountryList = createReducer(null, {})

export const shouldResetScrollPosition = createReducer(true, {
    [types.SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [types.SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export const selectedTargetCountryId = createReducer('', {
    [types.SET_SELECTED_TARGET_COUNTRY_ID](prevState, action) {
        return action.data;
    }
})

export const selectedDomainId = createReducer(null, {
    [types.SET_SELECTED_DOMAIN_ID](prevState, action) {
        return action.data;
    }
})

export const affDomainList = createReducer(null, {
    [types.SET_AFF_DOMAIN_LIST](prevState, action) {
        return action.data;
    }
})

export const redirectDomainList = createReducer(null, {
    [types.SET_REDIRECT_DOMAIN_LIST](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    gridData,
    isShowLoader,
    targetCountryList,
    selectedTargetCountryId,
    selectedDomainId,
    affDomainList,
    redirectDomainList,
    shouldResetScrollPosition,
    claimsPrincipal
});