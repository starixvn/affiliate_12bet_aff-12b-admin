import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import * as type from '../actions/types';

import { pagingOption } from '../../../../reducers/pagingReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { claimsPrincipal } from '../../../../reducers/claimReducer';

const today = createReducer(null, {});
export const reportTitle = createReducer('', {
    [type.SET_REPORT_TITLE](prevState, action) {
        return action.data;
    }
})

export const param = createReducer(null, {
    [type.SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const selectedRequestId = createReducer(0, {
    [type.SET_SELECTED_REQUEST_ID](prevState, action) {
        return action.data;
    }
})

export const selectedRequest = createReducer(null,{
    [type.SET_SELECTED_PROC_REQUEST](prevState, action){
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [type.SET_SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [type.SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export const isClickSearch = createReducer(false, {
    [type.SET_IS_CLICK_SEARCH](prevState, action){
        return action.data;
    }
})

export const dataSummary = createReducer(null, {
    [type.SET_PROC_REQUEST_SUMMARY](prevState, action){
        return action.data;
    }
})

export const languageList = createReducer(null,{
    [type.SET_LANGUAGE_LIST](prevState, action){
        return action.data;
    }
})

export default combineReducers({
    today,
    pagingOption,
    reportTitle,
    param,
    isShowLoader,
    selectedRequestId,
    selectedRequest,
    gridData,
    shouldResetScrollPosition,
    claimsPrincipal,
    isClickSearch,
    dataSummary,
    languageList
});