import { combineReducers } from 'redux';

import createReducer from '../../../../modules/createReducer';
import { isShowLoader } from '../../../../reducers/loaderReducer';
import * as types from '../actions/types';
import { pageBuilderDefaultState } from '../libs/constants';

export const landingPageUrl = createReducer(null, {});
export const bannerRootUrl = createReducer(null, {});

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

export const moduleComponents = createReducer(pageBuilderDefaultState.moduleComponentsSetting, {
    [types.ADD_MODULE_COMPONENT](prevState, action) {
        return [...prevState, action.module];
    },
    [types.INSERT_MODULE_AFTER](prevState, action) {
        const newState = [...prevState];
        newState.splice(action.insertIndex, 0, action.module);
        return newState.map((module, index) => ({
            ...module,
            orderId: index + 1
        }));
    },
    [types.REMOVE_MODULE_COMPONENT](prevState, action) {
        const filteredState = prevState.filter(module => module.id !== action.moduleId);
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
        const moduleList = action.moduleList || [];
        
        if (!detail || !detail.ModuleComponentsSetting) return prevState;

        // Helper function to determine if a module supports multiple selection
        const isModuleMultiSelect = (moduleId) => {
            const moduleInfo = moduleList.find(m => m.ModuleId === moduleId);
            if (!moduleInfo) return false;
            
            const isMultipleUpload = moduleInfo.IsMultipleUpload;
            const forceMultiSelectModules = ['hero image', 'favorite games', 'provider', 'promo list'];
            const shouldBeMultiSelect = forceMultiSelectModules.some(name => 
                moduleInfo.ModuleName?.toLowerCase().includes(name.toLowerCase())
            );
            
            return isMultipleUpload || shouldBeMultiSelect;
        };

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
            
            if (item.MediaId !== null && item.MediaId !== undefined) {
                if (Array.isArray(item.MediaId)) {
                    groupedComponents[key].mediaIds.push(...item.MediaId);
                } else {
                    groupedComponents[key].mediaIds.push(item.MediaId);
                }
            }
        });
        
        const result = Object.values(groupedComponents)
            .sort((a, b) => a.orderId - b.orderId)
            .map((group, index) => {
                const isMultiSelect = isModuleMultiSelect(group.moduleId);
                let mediaId;
                
                if (group.mediaIds.length === 0) {
                    mediaId = null;
                } else if (isMultiSelect) {
                    // Multi-select modules should always have arrays
                    mediaId = group.mediaIds;
                } else {
                    // Single-select modules should have single values
                    mediaId = group.mediaIds[0] || null;
                }
                
                
                return {
                    id: index + 1,
                    orderId: group.orderId,
                    moduleId: group.moduleId,
                    mediaId: mediaId,
                    extraInfo: group.extraInfo
                };
            });
            
        return result;
    }
});

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
    },
    [types.ADD_CAMPAIGN_TO_LIST](prevState, action) {
        return [...prevState, action.campaign];
    }
});

export const dealList = createReducer([], {
    [types.SET_DEAL_LIST](prevState, action) {
        return action.deals;
    },
    [types.ADD_DEAL_TO_LIST](prevState, action) {
        // Add the new deal to the existing dealList
        return [...prevState, action.deal];
    }
});

export const normalDealList = createReducer([], {
    [types.SET_NORMAL_DEAL_LIST](prevState, action) {
        return action.normalDeals;
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

export const ui = createReducer(pageBuilderDefaultState.ui, {
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

export const pageBuilderDetail = createReducer(null, {
    [types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL](prevState, action) {
        return action.pageBuilderDetail || null;
    }
});

export const isEditMode = createReducer(false, {
    [types.INITIALIZE_FROM_PAGE_BUILDER_DETAIL](prevState, action) {
        return action.pageBuilderDetail && action.pageBuilderDetail.PageId > 0;
    }
});

export default combineReducers({
    landingPageUrl,
    isShowLoader,
    bannerRootUrl,
    formData,
    moduleComponents,
    campaignList,
    moduleList,
    targetCountryList,
    languageList,
    dealList,
    normalDealList,
    statusList,
    sources,
    validation,
    ui,
    pageBuilderDetail,
    isEditMode
});