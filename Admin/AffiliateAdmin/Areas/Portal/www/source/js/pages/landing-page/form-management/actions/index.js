import { post } from '../../../../modules/ajaxRequest';

import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';

import * as types from './types';

export { toggleLoader, setPagingOption };

export const setSelectedFormId = (data) => {
    return {
        type: types.SET_SELECTED_FORM_ID,
        data
    }
}

export const setCheckedForms = (data) => {
    return {
        type: SET_CHECKED_FormS,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: types.SET_GRID_DATA,
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

export const fetchData = () => async (dispatch) => {
    dispatch(toggleLoader(true));

    try {
        const response = await post(API_ENDPOINTS.GET_Form_LIST);
        const data = response && response.status === 200 ? response.data : [];
        const checkedForms = _convertCheckedForms(data);

        dispatch(setGridData(data));
        dispatch(setShouldResetScrollPosition(true));
        dispatch(setCheckedForms(checkedForms));
    } catch (error) {
        console.error('Error fetching Forms:', error);
        dispatch(setGridData([]));
        dispatch(setCheckedForms({}));
    } finally {
        dispatch(toggleLoader(false));
    }
}

export const getFormById = (FormId) => async () => {
    try {
        const response = await post(API_ENDPOINTS.GET_Form_BY_ID, { FormId });
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error fetching Form:', error);
        return null;
    }
}

export const insertForm = (param) => async () => {
    try {
        const response = await post(API_ENDPOINTS.INSERT_Form, param);
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error inserting Form:', error);
        return null;
    }
}

export const updateForm = (param) => async () => {
    try {
        const response = await post(API_ENDPOINTS.UPDATE_Form, param);
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error updating Form:', error);
        return null;
    }
}

export const deleteForms = (param) => async () => {
    try {
        const response = await post(API_ENDPOINTS.DELETE_Form, param);
        return response && response.status === 200 ? response.data : null;
    } catch (error) {
        console.error('Error deleting Forms:', error);
        return null;
    }
}

export const validateFormUniqueness = (FormName, FormId = 0) => async () => {
    try {
        const response = await post(API_ENDPOINTS.VALIDATE_UNIQUENESS, { FormName, FormId });
        return response && response.status === 200 ? response.data : { IsUnique: false };
    } catch (error) {
        console.error('Error validating uniqueness:', error);
        return { IsUnique: false };
    }
}

export const getFormsByType = (FormType) => async () => {
    try {
        const response = await post(API_ENDPOINTS.GET_FormS_BY_TYPE, { FormType });
        return response && response.status === 200 ? response.data : [];
    } catch (error) {
        console.error('Error fetching Forms by type:', error);
        return [];
    }
}

function _convertCheckedForms(dataList) {
    const checkedForms = {};
    for (let item of dataList) {
        const itemId = item.FormId;
        checkedForms[itemId] = false;
    }
    return checkedForms;
}
