import { combineReducers } from 'redux';
import { SET_SELECTED_SITE_URL, SET_PARAM } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

const today = createReducer(null, {});

export const selectedSiteUrl = createReducer('', {
    [SET_SELECTED_SITE_URL](prevState, action) {
        return action.data;
    }
});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    today,
    selectedSiteUrl,
    param
});