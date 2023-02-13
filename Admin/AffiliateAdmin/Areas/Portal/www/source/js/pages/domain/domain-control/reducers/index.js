import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import * as types from '../actions/types';
import { claimsPrincipal } from '../../../../reducers/claimReducer';

const marketList = createReducer(null, {})

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

export const selectedMarketId = createReducer('', {
    [types.SET_SELECTED_MARKET_ID](prevState, action) {
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
    marketList,
    selectedMarketId,
    selectedDomainId,
    affDomainList,
    redirectDomainList,
    shouldResetScrollPosition,
    claimsPrincipal
});