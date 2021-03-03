import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import {claimsPrincipal} from '../../../../reducers/claimReducer';
import { SET_CHECKED_NEWSLETTER_PAGES, SET_SELECTED_NEWSLETTER_PAGE_ID, SET_SELECTED_PREVIEW_PAGE_ID, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_PARAM } from '../actions/types';

export const languageList = createReducer(null, {});

export const previewLink = createReducer(null, {});

export const selectedNewsLetterPageId = createReducer(null, {
    [SET_SELECTED_NEWSLETTER_PAGE_ID](prevState, action) {
        return action.data;
    }
})

export const param = createReducer(null, {
    [SET_PARAM](prevState, action) {
        return Object.assign({}, prevState, action.data);
    }
})

export const checkedNewsLetterPages = createReducer({}, {
    [SET_CHECKED_NEWSLETTER_PAGES](prevState, action) {
        return action.data;
    }
})

export const selectedPreviewPageId = createReducer(null,{
    [SET_SELECTED_PREVIEW_PAGE_ID](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    gridData,
    isShowLoader,
    selectedNewsLetterPageId,
    checkedNewsLetterPages,
    shouldResetScrollPosition,
    selectedPreviewPageId,
    languageList,
    previewLink,
    param,
    claimsPrincipal
});