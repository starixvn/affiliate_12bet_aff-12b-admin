import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { API_ENDPOINTS } from '../libs/constants';

import {
    SET_SELECTED_MODULE,
    SET_CHECKED_MODULES,
    SHOULD_RESET_SCROLL_POSITION,
    SET_GRID_DATA,
    SHOW_MODAL,
    HIDE_MODAL
} from './types';

export { toggleLoader };

export const setSelectedModule = (data) => {
    return {
        type: SET_SELECTED_MODULE,
        data
    }
}

export const setCheckedModules = (data) => {
    return {
        type: SET_CHECKED_MODULES,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const showModal = (modalType) => {
    return {
        type: SHOW_MODAL,
        data: modalType
    }
}

export const hideModal = () => {
    return {
        type: HIDE_MODAL
    }
}

export const fetchModules = () => async (dispatch) => {
    dispatch(toggleLoader(true));

    try {
        const response = await post(API_ENDPOINTS.GET_MODULE_LIST);
        const data = response && response.status === 200 ? response.data : [];
        const checkedModules = _convertCheckedModules(data);

        dispatch(setGridData(data));
        dispatch(setShouldResetScrollPosition(true));
        dispatch(setCheckedModules(checkedModules));
    } catch (error) {
        console.error('Error fetching modules:', error);
        dispatch(setGridData([]));
        dispatch(setCheckedModules({}));
    } finally {
        dispatch(toggleLoader(false));
    }
}

export const getModuleById = (moduleId) => async () => {
    try {
        const response = await post(API_ENDPOINTS.GET_MODULE_BY_ID, { moduleId });
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error fetching module:', error);
        return null;
    }
}

export const insertModule = (param) => async () => {
    try {
        const response = await post(API_ENDPOINTS.INSERT_MODULE, param);
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error inserting module:', error);
        return null;
    }
}

export const updateModule = (param) => async () => {
    try {
        const response = await post(API_ENDPOINTS.UPDATE_MODULE, param);
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error updating module:', error);
        return null;
    }
}

export const deleteModules = (param) => async () => {
    try {
        const response = await post(API_ENDPOINTS.DELETE_MODULE, param);
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error deleting modules:', error);
        return null;
    }
}

export const validateModuleUniqueness = (moduleName, moduleId = 0) => async () => {
    try {
        const response = await post(API_ENDPOINTS.VALIDATE_UNIQUENESS, { moduleName, moduleId });
        return response && response.status === 200 ? response.data : { IsUnique: false };
    } catch (error) {
        console.error('Error validating uniqueness:', error);
        return { IsUnique: false };
    }
}

export const getModulesByType = (moduleType) => async () => {
    try {
        const response = await post(API_ENDPOINTS.GET_MODULES_BY_TYPE, { moduleType });
        return response && response.status === 200 ? response.data : [];
    } catch (error) {
        console.error('Error fetching modules by type:', error);
        return [];
    }
}

function _convertCheckedModules(dataList) {
    const checkedModules = {};
    for (let item of dataList) {
        const itemId = item.ModuleId;
        checkedModules[itemId] = false;
    }
    return checkedModules;
}
