import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SHOULD_RESET_SCROLL_POSITION, SET_SELECTED_DOMAIN_ID, SET_GRID_DATA } from '../actions/types';

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

export const selectedDomainId = createReducer(0, {
    [SET_SELECTED_DOMAIN_ID](prevState, action) {
        return action.data;
    }
})

export default combineReducers({
    gridData,
    isShowLoader,
    selectedDomainId,
    shouldResetScrollPosition
});