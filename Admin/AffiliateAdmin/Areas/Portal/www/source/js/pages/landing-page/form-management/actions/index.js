import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';

import * as types from './types';

export { toggleLoader, setPagingOption };

export const setParam = (data) => {
    return {
        type: types.SET_PARAM,
        data
    }
}

export const setModuleList = (data) => {
    return {
        type: types.SET_MODULE_LIST,
        data
    }
}
export const setTargetCountryList = (data) => {
    return {
        type: types.SET_TARGET_COUNTRY_LIST,
        data
    }
}
export const setLanguageList = (data) => {
    return {
        type: types.SET_LANGUAGE_LIST,
        data
    }
}

export const setFormDetailUrl = (data) => {
    return {
        type: types.SET_FORM_DETAIL_URL,
        data
    }
}

export const setSelectedMediaId = (data) => {
    return {
        type: types.SET_SELECTED_MEDIA_ID,
        data
    }
}

export const setCheckedForms = (data) => {
    return {
        type: types.SET_CHECKED_FORMS,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: types.SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const setNotification = (data) => {
    return {
        type: types.SET_NOTIFICATION,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: types.SET_GRID_DATA,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/FormManagement/GetFormManagementList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedForms = data ? _convertCheckedForms(data.MainData || []) : {};

    dispatch(setGridData(data ? data.MainData : []));
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedForms(checkedForms));
    dispatch(toggleLoader(false));
}

function _convertCheckedForms(dataList) {
    const checkedForms = {};
    for (let item of dataList) {
        const itemId = item.FormId;
        checkedForms[itemId] = false;
    }

    return checkedForms;
}

export const getFormDetail = (mediaId) => async () => {
    const url = 'Portal/FormManagement/GetFormDetail';
    const response = await post(url, { mediaId });

    return response && response.status === 200 ? response.data : null;
}

export const uploadForms = (param) => () => {
    const url = 'Portal/FormManagement/UploadRegistrationForms';

    return handleExcute(url, param);
}

export const updateForm = (param) => () => {
    const url = 'Portal/FormManagement/UpdateRegistrationForm';

    return handleExcute(url, param);
}

const handleExcute = async (url, param) => {
    const formData = new FormData();
    for (let key in param) {
        key !== 'files' && formData.append(key, param[key]);
    }

    const files = param['files'];

    if (files) {
        for (let file of files) {
            formData.append('files', file);
        }
    }

    const response = await post(url, formData);

    return response && response.status === 200 ? response.data : null;
}

export const deleteForms = (mediaIds) => async () => {
    const url = 'Portal/FormManagement/DeleteForms';
    const response = await post(url, { mediaIds });

    return response && response.status === 200 ? response.data : null;
}

export const toggleFormStatus = (param) => async () => {
    const url = 'Portal/FormManagement/ToggleFormStatus';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}