import { combineReducers } from 'redux';
import { SET_SELECTED_PAGE, SET_PARAM, SET_SHOW_DETAIL } from '../actions/types';
import createReducer from '../../../../modules/createReducer';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';

export const today = createReducer('', {});

export const languageList = createReducer({}, {});

export const selectedPage = createReducer({}, {
    [SET_SELECTED_PAGE](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const param = createReducer({}, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
});

export const isShowDetail = createReducer(false, {
    [SET_SHOW_DETAIL](prevState, action) {
        return action.data;
    }
});

export default combineReducers({
    pagingOption,
    isShowLoader,
    today,
    languageList,
    selectedPage,
    param,
    isShowDetail
});