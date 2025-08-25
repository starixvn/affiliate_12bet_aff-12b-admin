export const MODULE_TYPES = {
    IMAGE: 1,
    CONTACT: 2,
    FORM: 3,
    LINK: 4
};

export const NAVIGATION_TARGETS = {
    NONE: 0,
    REGISTER_FORM: 9,
    LOGIN_FORM: 10,
    CONTACT_PAGE: 11
};

export const MODAL_TYPES = {
    CREATE: 'create',
    EDIT: 'edit',
    DELETE: 'delete'
};

export const messages = {
    PleaseSelect: 'Please select module(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected module(s)?',
    DeleteItemsSuccessfully: 'Delete module(s) successfully',
    CreateModuleSuccess: 'Module created successfully',
    UpdateModuleSuccess: 'Module updated successfully',
    ModuleNameRequired: 'Module name is required',
    ModuleTypeRequired: 'Module type is required',
    InvalidModuleType: 'Invalid module type selected',
    ModuleNameTooLong: 'Module name must not exceed 200 characters',
    RemarkTooLong: 'Remark must not exceed 200 characters',
    ModuleNameExists: 'A module with this name already exists'
};

export const headerText = {
    Index: '#',
    ModuleName: 'Module Name',
    ModuleType: 'Type',
    Remark: 'Description',
    NavigateTo: 'Navigation',
    IsRequired: 'Required',
    IsUnique: 'Unique',
    IsMultipleUpload: 'Multiple Upload',
    CreatedDate: 'Created',
    Actions: 'Actions'
};

export const defaultFormState = {
    moduleId: 0,
    moduleName: '',
    moduleType: MODULE_TYPES.IMAGE,
    remark: '',
    navigateTo: NAVIGATION_TARGETS.NONE,
    isMultipleUpload: false,
    isRequired: false,
    isUnique: false,
    isSubmitting: false,
    errors: {}
};

export const validationRules = {
    moduleNameMaxLength: 200,
    remarkMaxLength: 200,
    restrictedCharacters: /[<>"'&]/,
    moduleTypeMin: 1,
    moduleTypeMax: 4
};

export const API_ENDPOINTS = {
    GET_MODULE_LIST: 'Portal/LandingPageModule/GetModuleList',
    GET_MODULE_BY_ID: 'Portal/LandingPageModule/GetModuleById',
    INSERT_MODULE: 'Portal/LandingPageModule/InsertModule',
    UPDATE_MODULE: 'Portal/LandingPageModule/UpdateModule',
    DELETE_MODULE: 'Portal/LandingPageModule/DeleteModule',
    GET_MODULES_BY_TYPE: 'Portal/LandingPageModule/GetModulesByType',
    VALIDATE_UNIQUENESS: 'Portal/LandingPageModule/ValidateModuleUniqueness'
};
