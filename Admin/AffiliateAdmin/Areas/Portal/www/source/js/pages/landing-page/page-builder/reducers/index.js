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
    }
});

// Module Components Reducer
export const moduleComponents = createReducer(pageBuilderDefaultState.moduleComponents, {
    [types.ADD_MODULE_COMPONENT](prevState, action) {
        return [...prevState, action.module];
    },
    [types.REMOVE_MODULE_COMPONENT](prevState, action) {
        return prevState.filter(module => module.id !== action.moduleId);
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
    }
});

// Dropdown Data Reducers
export const campaigns = createReducer([], {
    [types.SET_CAMPAIGNS](prevState, action) {
        return action.campaigns;
    }
});

export const deals = createReducer([], {
    [types.SET_DEALS](prevState, action) {
        return action.deals;
    }
});

export const modules = createReducer([], {
    [types.SET_MODULES](prevState, action) {
        return action.modules;
    }
});

export const sources = createReducer([], {
    [types.SET_SOURCES](prevState, action) {
        return action.sources;
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
    [types.ADD_MODULE_COMPONENT](prevState, action) {
        return { ...prevState, nextModuleOrder: prevState.nextModuleOrder + 1 };
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
    campaigns,
    deals,
    modules,
    sources,
    validation,
    ui
});