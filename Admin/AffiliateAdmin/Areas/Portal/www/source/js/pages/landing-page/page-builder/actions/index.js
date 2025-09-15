import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import * as types from './types';

export { toggleLoader };

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

export const addModuleComponent = () => (dispatch, getState) => {
    const { ui } = getState();
    const newModule = {
        id: Date.now(),
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
        id: Date.now(),
        orderId: 0,
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
        const response =  {data: {IsValid: true}, status: 200};
        
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
        const response = {data: {IsValid: true}, status: 200};
        
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
        
        const isUpdate = formData.pageId && formData.pageId > 0;
        const url = isUpdate && !isReview ? 'Portal/PageBuilder/UpdatePageBuilder' : 'Portal/PageBuilder/InsertPageBuilder';
        
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

export const generatePreviewUrl = (formData, moduleComponents) => async (dispatch) => {
    return await dispatch(saveLandingPage(formData, moduleComponents, true));
};