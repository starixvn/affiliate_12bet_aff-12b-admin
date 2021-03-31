import { combineReducers } from 'redux';
import { SET_MAIN_VIEW_PARAM, SET_DETAIL_VIEW_PARAM, SET_SELECTED_AFF } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';

const today = createReducer(null, {});
const hasViewLogPermission = createReducer(false, {});

export const mainViewParam = createReducer(null, {
    [SET_MAIN_VIEW_PARAM](prevState, action) {
        return {...action.data };
    }
});

export const detailViewParam = createReducer(null, {
    [SET_DETAIL_VIEW_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const selectedAff = createReducer(null, {
    [SET_SELECTED_AFF](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    today,
    hasViewLogPermission,
    pagingOption,
    selectedAff,
    mainViewParam,
    detailViewParam
});