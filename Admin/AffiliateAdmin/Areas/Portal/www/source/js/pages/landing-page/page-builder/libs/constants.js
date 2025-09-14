import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

// Legacy constants (keep for backward compatibility)
export const headerText = {
    Index: '#',
    BlockTargetCountry: 'Block Target Country',
    LandingPageName: 'Landing Page Name (Shown in dropdown)',
    LandingPageCode: 'Landing Page Code (Contained in links)'
}

export const defaultState = {
    isBlockAllTargetCountries: false,
    selectedLandingPage: { pageName: '', pageCode: '', blockTargetCountry: '' },
    targetCountryList: null,
    isClickedSubmit: false,
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}

export const restrictedCharactersPattern = /[;\/\?:@=&"<>#%{}\|\\^~\[\]`\s+]/;
export const maxLength = 50;

export const messages = {
    requiredErrorMessage: 'This field is required',
    maxLengthErrorMessage: 'This field must not exceed 50 characters',
    PleaseSelect: 'Please select landing page(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected landing page(s)?',
    DeleteItemsSuccessfully: 'Delete landing page(s) Successfully'
}

// Page Builder Constants
export const pageBuilderLabels = {
    campaignName: 'Campaign Name',
    targetCountry: 'Target Country',
    language: 'Language',
    deal: 'Deal',
    pageName: 'Page Name',
    pageCode: 'Page Code',
    status: 'Status',
    moduleComponents: 'Module Components',
    orderId: 'Order',
    module: 'Module',
    source: 'Source',
    preview: 'Preview',
    create: 'Create',
    update: 'Update',
    reset: 'Reset',
    copyLink: 'Copy Link',
    add: 'Add +',
    remove: 'Remove'
};

export const pageBuilderDefaultState = {
    formData: {
        pageId: 0,
        campaignId: null,
        targetCountryCode: '',
        languageCode: '',
        dealIds: [],
        pageName: '',
        pageCode: '',
        status: 1
    },
    moduleComponentsSetting: [
        {
            id: 1,
            orderId: 1,
            moduleId: null,
            mediaId: null
        }
    ],
    validation: {
        errors: {},
        isValid: false,
        isValidating: false
    },
    ui: {
        isPreviewOpen: false,
        isCopyLinkOpen: false,
        isLoading: false,
        nextModuleOrder: 2,
        hasPreviewedOrCreated: false,
        previewUrl: null
    }
};

export const validationMessages = {
    campaignRequired: 'Campaign Name is required',
    pageNameRequired: 'Page Name is required',
    pageNameExists: 'Page Name already exists. Please enter a different name.',
    pageCodeRequired: 'Page Code is required',
    pageCodeExists: 'Page Code already exists. Please enter a different code.',
    pageCodeInvalid: 'Page Code can only contain letters, numbers, hyphens and underscores',
    moduleRequired: 'Module is required',
    sourceRequired: 'Source is required',
    atLeastOneModule: 'At least one module component is required'
};

export const pageCodePattern = /^[a-zA-Z0-9-_]+$/;