import { combineReducers } from 'redux';
import { SET_MAIN_VIEW_PARAM, SET_DETAIL_VIEW_PARAM, SET_SELECTED_AFF } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

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

export const selectedAff = createReducer('', {
    [SET_SELECTED_AFF](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    today,
    hasViewLogPermission,
    pagingOption,
    isShowLoader,
    selectedAff,
    mainViewParam,
    detailViewParam
});