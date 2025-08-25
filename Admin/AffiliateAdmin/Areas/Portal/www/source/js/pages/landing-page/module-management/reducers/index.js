import { combineReducers } from 'redux';
import { TOGGLE_LOADER } from '../../../../actions/types';
import {
    SET_SELECTED_MODULE,
    SET_CHECKED_MODULES,
    SHOULD_RESET_SCROLL_POSITION,
    SET_GRID_DATA,
    SHOW_MODAL,
    HIDE_MODAL
} from '../actions/types';

const isShowLoader = (state = false, action) => {
    switch (action.type) {
        case TOGGLE_LOADER:
            return action.data;
        default:
            return state;
    }
};

const selectedModule = (state = null, action) => {
    switch (action.type) {
        case SET_SELECTED_MODULE:
            return action.data;
        default:
            return state;
    }
};

const checkedModules = (state = {}, action) => {
    switch (action.type) {
        case SET_CHECKED_MODULES:
            return action.data;
        default:
            return state;
    }
};

const shouldResetScrollPosition = (state = false, action) => {
    switch (action.type) {
        case SHOULD_RESET_SCROLL_POSITION:
            return action.data;
        default:
            return state;
    }
};

const gridData = (state = [], action) => {
    switch (action.type) {
        case SET_GRID_DATA:
            return action.data;
        default:
            return state;
    }
};

const isShowModal = (state = false, action) => {
    switch (action.type) {
        case SHOW_MODAL:
            return true;
        case HIDE_MODAL:
            return false;
        default:
            return state;
    }
};

const modalType = (state = '', action) => {
    switch (action.type) {
        case SHOW_MODAL:
            return action.data;
        case HIDE_MODAL:
            return '';
        default:
            return state;
    }
};

// Preserve initial state from window
const moduleTypes = (state = [], action) => {
    return state;
};

const navigationTargets = (state = [], action) => {
    return state;
};

const permissions = (state = { canCreate: false, canEdit: false, canDelete: false }, action) => {
    return state;
};

const totalModules = (state = 0, action) => {
    switch (action.type) {
        case SET_GRID_DATA:
            return action.data.length;
        default:
            return state;
    }
};

export default combineReducers({
    isShowLoader,
    selectedModule,
    checkedModules,
    shouldResetScrollPosition,
    gridData,
    isShowModal,
    modalType,
    moduleTypes,
    navigationTargets,
    permissions,
    totalModules
});
