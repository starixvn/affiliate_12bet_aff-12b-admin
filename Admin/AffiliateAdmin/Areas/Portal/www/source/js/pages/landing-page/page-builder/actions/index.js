import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { 
    SET_CHECKED_LANDING_PAGES, 
    SET_SELECTED_LANDING_PAGE_ID, 
    SHOULD_RESET_SCROLL_POSITION, 
    SET_GRID_DATA,
    SET_FORM_DATA,
    UPDATE_FORM_FIELD,
    SET_VALIDATION_ERRORS,
    CLEAR_VALIDATION_ERRORS,
    ADD_MODULE_COMPONENT,
    REMOVE_MODULE_COMPONENT,
    UPDATE_MODULE_COMPONENT,
    SET_MODULE_COMPONENTS,
    SET_CAMPAIGNS,
    SET_DEALS,
    SET_MODULES,
    SET_SOURCES,
    SET_PREVIEW_MODAL,
    SET_COPY_LINK_MODAL,
    SET_LOADING_STATE,
    VALIDATE_PAGE_NAME_SUCCESS,
    VALIDATE_PAGE_NAME_ERROR,
    VALIDATE_PAGE_CODE_SUCCESS,
    VALIDATE_PAGE_CODE_ERROR
} from './types';

export { toggleLoader };

// Legacy actions (keep for backward compatibility)
export const setSelectedLandingPageId = (data) => {
    return {
        type: SET_SELECTED_LANDING_PAGE_ID,
        data
    }
}

export const setCheckedLandingPages = (data) => {
    return {
        type: SET_CHECKED_LANDING_PAGES,
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

// Page Builder Form Actions
export const setFormData = (data) => ({
    type: SET_FORM_DATA,
    data
});

export const updateFormField = (field, value) => ({
    type: UPDATE_FORM_FIELD,
    field,
    value
});

export const setValidationErrors = (errors) => ({
    type: SET_VALIDATION_ERRORS,
    errors
});

export const clearValidationErrors = () => ({
    type: CLEAR_VALIDATION_ERRORS
});

// Module Component Actions
export const addModuleComponent = () => (dispatch, getState) => {
    const { ui } = getState();
    const newModule = {
        id: Date.now(), // Use timestamp as unique ID
        order: ui.nextModuleOrder,
        moduleId: null,
        sourceId: null
    };
    
    dispatch({
        type: ADD_MODULE_COMPONENT,
        module: newModule
    });
};

export const removeModuleComponent = (moduleId) => ({
    type: REMOVE_MODULE_COMPONENT,
    moduleId
});

export const updateModuleComponent = (moduleId, field, value) => ({
    type: UPDATE_MODULE_COMPONENT,
    moduleId,
    field,
    value
});

export const setModuleComponents = (modules) => ({
    type: SET_MODULE_COMPONENTS,
    modules
});

// Dropdown Data Actions
export const setCampaigns = (campaigns) => ({
    type: SET_CAMPAIGNS,
    campaigns
});

export const setDeals = (deals) => ({
    type: SET_DEALS,
    deals
});

export const setModules = (modules) => ({
    type: SET_MODULES,
    modules
});

export const setSources = (sources) => ({
    type: SET_SOURCES,
    sources
});

// UI State Actions
export const setPreviewModal = (isOpen) => ({
    type: SET_PREVIEW_MODAL,
    isOpen
});

export const setCopyLinkModal = (isOpen) => ({
    type: SET_COPY_LINK_MODAL,
    isOpen
});

export const setLoadingState = (isLoading) => ({
    type: SET_LOADING_STATE,
    isLoading
});

export const fetchData = () => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = 'Portal/LandingPageList/GetLandingPageList';
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;
    const checkedLandingPages = data ? _convertCheckedLandingPages(data) : {};

    dispatch(setGridData(data ? data : []));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedLandingPages(checkedLandingPages));
    dispatch(toggleLoader(false));
}

function _convertCheckedLandingPages(dataList) {
    const checkedLandingPages = {};
    for (let item of dataList) {
        const itemId = item.PageId;
        checkedLandingPages[itemId] = false;
    }

    return checkedLandingPages;
}

export const getLandingPageDetail = (selectedLandingPageId) => async () => {
    const url = 'Portal/LandingPageList/GetLandingPageItem';
    const response = await post(url, { pageId: selectedLandingPageId });

    return response && response.status === 200 ? response.data : null;
}

export const getTargetCountryListByLandingPage = (param) => async () => {
    const url = 'Portal/LandingPageList/GetTargetCountryListByLandingPage';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const insertLandingPage = (param) => async () => {
    const url = 'Portal/LandingPageList/InsertLandingPage';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateLandingPage = (param) => async () => {
    const url = 'Portal/LandingPageList/UpdateLandingPage';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const deleteLandingPages = (landingPageIds) => async () => {
    const url = 'Portal/LandingPageList/DeleteLandingPage';
    const response = await post(url, { pageIds: landingPageIds });

    return response && response.status === 200 ? response.data : null;
}

// Page Builder API Actions
export const fetchCampaigns = () => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/GetCampaigns';
        const response = await post(url);
        
        if (response && response.status === 200) {
            dispatch(setCampaigns(response.data || []));
        }
    } catch (error) {
        console.error('Error fetching campaigns:', error);
    }
};

export const fetchDeals = (campaignId) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/GetDeals';
        const response = await post(url, { campaignId });
        
        if (response && response.status === 200) {
            dispatch(setDeals(response.data || []));
        }
    } catch (error) {
        console.error('Error fetching deals:', error);
    }
};

export const fetchModuleSources = (moduleId) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/GetModuleSources';
        const response = await post(url, { moduleId });
        
        if (response && response.status === 200) {
            dispatch(setSources(response.data || []));
        }
    } catch (error) {
        console.error('Error fetching module sources:', error);
    }
};

export const validatePageName = (pageName, pageId = null) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/ValidatePageName';
        const response = await post(url, { pageName, pageId });
        
        if (response && response.status === 200) {
            const isValid = response.data && response.data.IsValid;
            
            if (isValid) {
                dispatch({ type: VALIDATE_PAGE_NAME_SUCCESS });
            } else {
                dispatch({ 
                    type: VALIDATE_PAGE_NAME_ERROR, 
                    error: response.data ? response.data.Message : 'Page name already exists' 
                });
            }
            
            return isValid;
        }
    } catch (error) {
        dispatch({ type: VALIDATE_PAGE_NAME_ERROR, error: 'Error validating page name' });
        return false;
    }
};

export const validatePageCode = (pageCode, pageId = null) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/ValidatePageCode';
        const response = await post(url, { pageCode, pageId });
        
        if (response && response.status === 200) {
            const isValid = response.data && response.data.IsValid;
            
            if (isValid) {
                dispatch({ type: VALIDATE_PAGE_CODE_SUCCESS });
            } else {
                dispatch({ 
                    type: VALIDATE_PAGE_CODE_ERROR, 
                    error: response.data ? response.data.Message : 'Page code already exists' 
                });
            }
            
            return isValid;
        }
    } catch (error) {
        dispatch({ type: VALIDATE_PAGE_CODE_ERROR, error: 'Error validating page code' });
        return false;
    }
};

export const saveLandingPage = (formData, moduleComponents) => async (dispatch) => {
    try {
        dispatch(setLoadingState(true));
        
        const url = 'Portal/PageBuilder/SaveLandingPage';
        const payload = {
            ...formData,
            moduleComponents
        };
        
        const response = await post(url, payload);
        
        if (response && response.status === 200) {
            return response.data;
        }
        
        return null;
    } catch (error) {
        console.error('Error saving landing page:', error);
        return null;
    } finally {
        dispatch(setLoadingState(false));
    }
};

export const generatePreviewUrl = (formData, moduleComponents) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/GeneratePreviewUrl';
        const payload = {
            ...formData,
            moduleComponents
        };
        
        const response = await post(url, payload);
        
        if (response && response.status === 200) {
            return response.data ? response.data.PreviewUrl : null;
        }
        
        return null;
    } catch (error) {
        console.error('Error generating preview URL:', error);
        return null;
    }
};