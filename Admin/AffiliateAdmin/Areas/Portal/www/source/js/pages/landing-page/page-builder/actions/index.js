import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import * as types from './types';

export { toggleLoader };

// Legacy actions (keep for backward compatibility)
export const setSelectedLandingPageId = (data) => {
    return {
        type: types.SET_SELECTED_LANDING_PAGE_ID,
        data
    }
}

export const setCheckedLandingPages = (data) => {
    return {
        type: types.SET_CHECKED_LANDING_PAGES,
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

// Page Builder Form Actions
export const setFormData = (data) => ({
    type: types.SET_FORM_DATA,
    data
});

export const updateFormField = (field, value) => ({
    type: types.UPDATE_FORM_FIELD,
    field,
    value
});

export const initializeFromPageBuilderDetail = (pageBuilderDetail) => ({
    type: types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL,
    pageBuilderDetail
});

export const setValidationErrors = (errors) => ({
    type: types.SET_VALIDATION_ERRORS,
    errors
});

export const clearValidationErrors = () => ({
    type: types.CLEAR_VALIDATION_ERRORS
});

// Module Component Actions
export const addModuleComponent = () => (dispatch, getState) => {
    const { ui } = getState();
    const newModule = {
        id: Date.now(), // Use timestamp as unique ID
        orderId: ui.nextModuleOrder,
        moduleId: null,
        sourceId: null
    };
    
    dispatch({
        type: types.ADD_MODULE_COMPONENT,
        module: newModule
    });
};

export const insertModuleAfter = (afterModuleId) => (dispatch, getState) => {
    const { moduleComponents } = getState();
    const afterIndex = moduleComponents.findIndex(m => m.id === afterModuleId);
    
    const newModule = {
        id: Date.now(), // Use timestamp as unique ID
        orderId: 0, // Will be set in reducer based on position
        moduleId: null,
        mediaId: null
    };
    
    dispatch({
        type: types.INSERT_MODULE_AFTER,
        module: newModule,
        afterModuleId,
        insertIndex: afterIndex + 1
    });
};

export const removeModuleComponent = (moduleId) => ({
    type: types.REMOVE_MODULE_COMPONENT,
    moduleId
});

export const reorderModuleComponents = () => ({
    type: types.REORDER_MODULE_COMPONENTS
});

export const updateModuleComponent = (moduleId, field, value) => ({
    type: types.UPDATE_MODULE_COMPONENT,
    moduleId,
    field,
    value
});

export const setModuleComponents = (modules) => ({
    type: types.SET_MODULE_COMPONENTS,
    modules
});

// Dropdown Data Actions
export const setCampaignList = (campaigns) => ({
    type: types.SET_CAMPAIGN_LIST,
    campaigns
});

export const setDealList = (deals) => ({
    type: types.SET_DEAL_LIST,
    deals
});

export const setModuleList = (modules) => ({
    type: types.SET_MODULE_LIST,
    modules
});

export const setSources = (moduleId, sources) => ({
    type: types.SET_SOURCES,
    moduleId,
    sources
});

export const setStatusList = (statusList) => ({
    type: types.SET_STATUS_LIST,
    statusList
});

// UI State Actions
export const setPreviewModal = (isOpen) => ({
    type: types.SET_PREVIEW_MODAL,
    isOpen
});

export const setCopyLinkModal = (isOpen) => ({
    type: types.SET_COPY_LINK_MODAL,
    isOpen
});

export const setLoadingState = (isLoading) => ({
    type: types.SET_LOADING_STATE,
    isLoading
});

export const setHasPreviewedOrCreated = (hasPreviewedOrCreated) => ({
    type: types.SET_HAS_PREVIEWED_OR_CREATED,
    hasPreviewedOrCreated
});

export const setPreviewUrl = (previewUrl) => ({
    type: types.SET_PREVIEW_URL,
    previewUrl
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
        const url = 'Portal/PageBuilder/GetCampaignList';
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
        const url = 'Portal/PageBuilder/GetDealList';
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
        const url = 'Portal/PageBuilder/GetMediaSources';
        const response = await post(url, { moduleId });
        
        if (response && response.status === 200) {
            dispatch(setSources(moduleId, response.data || []));
        }
    } catch (error) {
        console.error('Error fetching module sources:', error);
    }
};

export const validatePageName = (pageName, pageId = null) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/ValidatePageName';
        const response =  {data: {IsValid: true}, status: 200};//await post(url, { pageName, pageId });
        
        if (response && response.status === 200) {
            const isValid = response.data && response.data.IsValid;
            
            if (isValid) {
                dispatch({ type: types.VALIDATE_PAGE_NAME_SUCCESS });
            } else {
                dispatch({ 
                    type: types.VALIDATE_PAGE_NAME_ERROR, 
                    error: response.data ? response.data.Message : 'Page name already exists' 
                });
            }
            
            return isValid;
        }
    } catch (error) {
        dispatch({ type: types.VALIDATE_PAGE_NAME_ERROR, error: 'Error validating page name' });
        return false;
    }
};

export const validatePageCode = (pageCode, pageId = null) => async (dispatch) => {
    try {
        const url = 'Portal/PageBuilder/ValidatePageCode';
        const response = {data: {IsValid: true}, status: 200};//await post(url, { pageCode, pageId });
        
        if (response && response.status === 200) {
            const isValid = response.data && response.data.IsValid;
            
            if (isValid) {
                dispatch({ type: types.VALIDATE_PAGE_CODE_SUCCESS });
            } else {
                dispatch({ 
                    type: types.VALIDATE_PAGE_CODE_ERROR, 
                    error: response.data ? response.data.Message : 'Page code already exists' 
                });
            }
            
            return isValid;
        }
    } catch (error) {
        dispatch({ type: types.VALIDATE_PAGE_CODE_ERROR, error: 'Error validating page code' });
        return false;
    }
};

export const saveLandingPage = (formData, moduleComponents, isReview = false) => async (dispatch) => {
    try {
        dispatch(setLoadingState(true));
        
        // Format module components as ModuleComponentsSetting JSON
        // If component.mediaId is an array, create a new object for each item
        let moduleComponentsSetting = [];
        moduleComponents.forEach(component => {
            if (Array.isArray(component.mediaId)) {
                component.mediaId.forEach(mediaIdItem => {
                    moduleComponentsSetting.push({
                        ModuleId: component.moduleId,
                        OrderId: component.orderId,
                        MediaId: mediaIdItem,
                        ExtraInfo: component.extraInfo,
                    });
                });
            } else {
                moduleComponentsSetting.push({
                    ModuleId: component.moduleId,
                    OrderId: component.orderId,
                    MediaId: component.mediaId,
                    ExtraInfo: component.extraInfo,
                });
            }
        });
        
        // Determine if this is an update (pageId > 0) or insert (pageId = 0)
        const isUpdate = formData.pageId && formData.pageId > 0;
        const url = isUpdate ? 'Portal/PageBuilder/UpdatePageBuilder' : 'Portal/PageBuilder/InsertPageBuilder';
        
        const payload = {
            PageId: formData.pageId || 0,
            CampaignId: formData.campaignId,
            PageName: formData.pageName,
            PageCode: formData.pageCode,
            TargetCountryCode: formData.targetCountryCode,
            LanguageCode: formData.languageCode,
            DealIds: Array.isArray(formData.dealIds) ? formData.dealIds.join(',') : formData.dealIds,
            StatusId: formData.status,
            ModuleComponentsSetting: moduleComponentsSetting,
            IsPreview: isReview
        };
        
        const response = await post(url, payload);
        
        if (response && response.status === 200) {
            return response.data;
        }
        
        return null;
    } catch (error) {
        return null;
    } finally {
        dispatch(setLoadingState(false));
    }
};

// Preview now uses the same saveLandingPage function with isReview=true
export const generatePreviewUrl = (formData, moduleComponents) => async (dispatch) => {
    return await dispatch(saveLandingPage(formData, moduleComponents, true));
};