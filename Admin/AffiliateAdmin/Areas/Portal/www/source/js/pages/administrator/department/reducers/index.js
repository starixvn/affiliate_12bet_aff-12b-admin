import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import { SET_SELECTED_DEPARTMENT_ID, SET_OPEN_IT_POPUP, SET_OPEN_POLICY_POPUP, SET_OPEN_INSERT_UPDATE_POPUP, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from '../actions/types';

export const selectedDepartmentId = createReducer(0, {
    [SET_SELECTED_DEPARTMENT_ID](prevState, action) {
        return action.data;
    }
})

export const isOpenItPopup = createReducer(false, {
    [SET_OPEN_IT_POPUP](prevState, action) {
        return action.data;
    }
})

export const isOpenPolicyPopup = createReducer(false, {
    [SET_OPEN_POLICY_POPUP](prevState, action) {
        return action.data;
    }
})

export const isOpenInsertUpdatePopup = createReducer(false, {
    [SET_OPEN_INSERT_UPDATE_POPUP](prevState, action) {
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
    selectedDepartmentId,
    isOpenItPopup,
    isOpenPolicyPopup,
    isOpenInsertUpdatePopup,
    shouldResetScrollPosition
});