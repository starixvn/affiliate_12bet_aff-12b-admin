import { combineReducers } from 'redux';
import * as types from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';

export const selectedSiteDetailFilter = createReducer(null, {
    [types.SET_SELECTED_SITE_DETAIL_FILTER](prevState, action) {
        return action.data ? { ...action.data } : null;;
    }
});

export const param = createReducer({}, {
    [types.SET_PARAM](prevState, action) {
        return { ...action.data };
    }
});

export default combineReducers({
    pagingOption,
    selectedSiteDetailFilter,
    param
});