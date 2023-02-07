import { combineReducers } from 'redux';
import * as types from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';

export const pageList = createReducer([], {});

export const selectedPageDetailFilter = createReducer(null, {
    [types.SET_SELECTED_PAGE_DETAIL_FILTER](prevState, action) {
        return action.data ? Object.assign({}, action.data) : null;
    }
});

export const param = createReducer({}, {
    [types.SET_PARAM](prevState, action) {
        return { ...action.data };
    }
});

export const shortCodeLanguageList = createReducer({}, {
    [types.SET_SHORT_CODE_LANGUAGE_LIST](prevState, action) {
        return { ...action.data };
    }
});

export const isGroupByCountry = createReducer(true, {
    [types.SET_IS_GROUP_BY_COUNTRY](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    pagingOption,
    isGroupByCountry,
    shortCodeLanguageList,
    pageList,
    selectedPageDetailFilter,
    param
});