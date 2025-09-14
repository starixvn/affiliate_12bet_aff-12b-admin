import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import * as types from '../actions/types';
import { pageBuilderDefaultState } from '../libs/constants';

// Legacy reducers (keep for backward compatibility)
export const landingPageUrl = createReducer(null, {});

export const selectedLandingPageId = createReducer(null, {
    [types.SET_SELECTED_LANDING_PAGE_ID](prevState, action) {
        return action.data;
    }
})

export const checkedLandingPages = createReducer({}, {
    [types.SET_CHECKED_LANDING_PAGES](prevState, action) {
        return action.data;
    }
})

export const shouldResetScrollPosition = createReducer(true, {
    [types.SHOULD_RESET_SCROLL_POSITION](prevState, action) {
        return action.data;
    }
})

export const gridData = createReducer(null, {
    [types.SET_GRID_DATA](prevState, action) {
        return action.data;
    }
})

// Page Builder Form Reducer
export const formData = createReducer(pageBuilderDefaultState.formData, {
    [types.SET_FORM_DATA](prevState, action) {
        return { ...prevState, ...action.data };
    },
    [types.UPDATE_FORM_FIELD](prevState, action) {
        return { ...prevState, [action.field]: action.value };
    },
    [types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL](prevState, action) {
        const detail = action.pageBuilderDetail;
        if (!detail) return prevState;
        
        return {
            ...prevState,
            pageId: detail.PageId,
            campaignId: detail.CampaignId,
            targetCountryCode: detail.TargetCountryCode,
            languageCode: detail.LanguageCode,
            dealIds: detail.DealIds ? detail.DealIds.split(',').map(id => parseInt(id.trim())) : [],
            pageName: detail.PageName,
            pageCode: detail.PageCode,
            status: detail.StatusId
        };
    }
});

// Module Components Reducer
export const moduleComponents = createReducer(pageBuilderDefaultState.moduleComponentsSetting, {
    [types.ADD_MODULE_COMPONENT](prevState, action) {
        return [...prevState, action.module];
    },
    [types.INSERT_MODULE_AFTER](prevState, action) {
        const newState = [...prevState];
        newState.splice(action.insertIndex, 0, action.module);
        // Reorder after insertion
        return newState.map((module, index) => ({
            ...module,
            orderId: index + 1
        }));
    },
    [types.REMOVE_MODULE_COMPONENT](prevState, action) {
        const filteredState = prevState.filter(module => module.id !== action.moduleId);
        // Reorder after removal
        return filteredState.map((module, index) => ({
            ...module,
            orderId: index + 1
        }));
    },
    [types.REORDER_MODULE_COMPONENTS](prevState, action) {
        return prevState.map((module, index) => ({
            ...module,
            orderId: index + 1
        }));
    },
    [types.UPDATE_MODULE_COMPONENT](prevState, action) {
        return prevState.map(module => 
            module.id === action.moduleId 
                ? { ...module, [action.field]: action.value }
                : module
        );
    },
    [types.SET_MODULE_COMPONENTS](prevState, action) {
        return action.modules;
    },
    [types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL](prevState, action) {
        const detail = action.pageBuilderDetail;
        if (!detail || !detail.ModuleComponentsSetting) return prevState;
        
        console.log('Original ModuleComponentsSetting:', detail.ModuleComponentsSetting);
        
        // Group ModuleComponentsSetting by ModuleId and OrderId to combine multi-select items
        // Example input: 
        // [
        //   { ModuleId: 1, OrderId: 1, MediaId: 101, ExtraInfo: '' },
        //   { ModuleId: 1, OrderId: 1, MediaId: 102, ExtraInfo: '' },  // Same module, different media
        //   { ModuleId: 2, OrderId: 2, MediaId: 201, ExtraInfo: 'test' }
        // ]
        // Expected output:
        // [
        //   { id: 1, orderId: 1, moduleId: 1, mediaId: [101, 102], extraInfo: '' },
        //   { id: 2, orderId: 2, moduleId: 2, mediaId: 201, extraInfo: 'test' }
        // ]
        const groupedComponents = {};
        
        detail.ModuleComponentsSetting.forEach(item => {
            const key = `${item.ModuleId}_${item.OrderId}`;
            
            if (!groupedComponents[key]) {
                groupedComponents[key] = {
                    moduleId: item.ModuleId,
                    orderId: item.OrderId,
                    mediaIds: [],
                    extraInfo: item.ExtraInfo || ''
                };
            }
            
            // Add MediaId to the array (handle both single values and arrays)
            if (item.MediaId !== null && item.MediaId !== undefined) {
                if (Array.isArray(item.MediaId)) {
                    groupedComponents[key].mediaIds.push(...item.MediaId);
                } else {
                    groupedComponents[key].mediaIds.push(item.MediaId);
                }
            }
        });
        
        console.log('Grouped components:', groupedComponents);
        
        // Convert grouped components to our module components format
        const result = Object.values(groupedComponents)
            .sort((a, b) => a.orderId - b.orderId) // Sort by order
            .map((group, index) => ({
                id: index + 1, // Generate unique ID
                orderId: group.orderId,
                moduleId: group.moduleId,
                // For single select: use single value, for multi-select: use array
                mediaId: group.mediaIds.length === 1 ? group.mediaIds[0] : group.mediaIds,
                extraInfo: group.extraInfo
            }));
            
        console.log('Final module components:', result);
        return result;
    }
});

// Dropdown Data Reducers
export const targetCountryList = createReducer([], {
    [types.SET_TARGET_COUNTRY_LIST](prevState, action) {
        return action.targetCountries;
    }
});

export const languageList = createReducer([], {
    [types.SET_LANGUAGE_LIST](prevState, action) {
        return action.languages;
    }
});

export const campaignList = createReducer([], {
    [types.SET_CAMPAIGN_LIST](prevState, action) {
        return action.campaigns;
    }
});

export const dealList = createReducer([], {
    [types.SET_DEAL_LIST](prevState, action) {
        return action.deals;
    }
});

export const moduleList = createReducer([], {
    [types.SET_MODULE_LIST](prevState, action) {
        return action.modules;
    }
});

export const statusList = createReducer([], {
    [types.SET_STATUS_LIST](prevState, action) {
        return action.statuses;
    }
});

export const sources = createReducer({}, {
    [types.SET_SOURCES](prevState, action) {
        return {
            ...prevState,
            [action.moduleId]: action.sources
        };
    }
});

// Validation Reducer
export const validation = createReducer(pageBuilderDefaultState.validation, {
    [types.SET_VALIDATION_ERRORS](prevState, action) {
        return { ...prevState, errors: action.errors, isValid: Object.keys(action.errors).length === 0 };
    },
    [types.CLEAR_VALIDATION_ERRORS](prevState, action) {
        return { ...prevState, errors: {}, isValid: true };
    },
    [types.VALIDATE_PAGE_NAME_SUCCESS](prevState, action) {
        const newErrors = { ...prevState.errors };
        delete newErrors.pageName;
        return { ...prevState, errors: newErrors, isValid: Object.keys(newErrors).length === 0 };
    },
    [types.VALIDATE_PAGE_NAME_ERROR](prevState, action) {
        return { 
            ...prevState, 
            errors: { ...prevState.errors, pageName: action.error },
            isValid: false 
        };
    },
    [types.VALIDATE_PAGE_CODE_SUCCESS](prevState, action) {
        const newErrors = { ...prevState.errors };
        delete newErrors.pageCode;
        return { ...prevState, errors: newErrors, isValid: Object.keys(newErrors).length === 0 };
    },
    [types.VALIDATE_PAGE_CODE_ERROR](prevState, action) {
        return { 
            ...prevState, 
            errors: { ...prevState.errors, pageCode: action.error },
            isValid: false 
        };
    }
});

// UI State Reducer
export const ui = createReducer(pageBuilderDefaultState.ui, {
    [types.SET_PREVIEW_MODAL](prevState, action) {
        return { ...prevState, isPreviewOpen: action.isOpen };
    },
    [types.SET_COPY_LINK_MODAL](prevState, action) {
        return { ...prevState, isCopyLinkOpen: action.isOpen };
    },
    [types.SET_LOADING_STATE](prevState, action) {
        return { ...prevState, isLoading: action.isLoading };
    },
    [types.SET_HAS_PREVIEWED_OR_CREATED](prevState, action) {
        return { ...prevState, hasPreviewedOrCreated: action.hasPreviewedOrCreated };
    },
    [types.SET_PREVIEW_URL](prevState, action) {
        return { ...prevState, previewUrl: action.previewUrl };
    },
    [types.ADD_MODULE_COMPONENT](prevState, action) {
        return { ...prevState, nextModuleOrder: prevState.nextModuleOrder + 1 };
    }
});

// Page Builder Detail Reducer
export const pageBuilderDetail = createReducer(null, {
    [types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL](prevState, action) {
        return action.pageBuilderDetail || null;
    }
});

// Edit Mode Reducer
export const isEditMode = createReducer(false, {
    [types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL](prevState, action) {
        return action.pageBuilderDetail && action.pageBuilderDetail.PageId > 0;
    }
});

export default combineReducers({
    // Legacy state
    landingPageUrl,
    gridData,
    isShowLoader,
    checkedLandingPages,
    selectedLandingPageId,
    shouldResetScrollPosition,
    
    // Page Builder state
    formData,
    moduleComponents,
    campaignList,
    moduleList,
    targetCountryList,
    languageList,
    dealList,
    statusList,
    sources,
    validation,
    ui,
    pageBuilderDetail,
    isEditMode
});