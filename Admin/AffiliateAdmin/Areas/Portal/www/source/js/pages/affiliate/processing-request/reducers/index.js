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
    today,
    pagingOption,
    reportTitle,
    param,
    isShowLoader,
    selectedRequestId,
    gridData,
    shouldResetScrollPosition,
    claimsPrincipal,
});