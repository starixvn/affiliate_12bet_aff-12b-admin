import { globalResource } from '../../../../modules/globalResource';
import { assetStatus } from '../libs/constants';

/**
 * Page Management Filter Configuration
 */
export const pageManagementFilterConfig = {
    filterFields: () => [
        {
            name: 'campaignName',
            label: 'Campaign Name',
            type: 'text',
            placeholder: 'Enter campaign name...'
        },
        {
            name: 'landingPageName',
            label: 'Landing Page Name',
            type: 'text',
            placeholder: 'Enter page name...'
        }
    ],
    
    multiSelectFields: (props) => [
        {
            name: 'dealIds',
            label: 'Deal',
            dataSource: props.dealList || [],
            updateFunction: props.setDealList
        },
        {
            name: 'targetCountryCodes',
            label: 'Target Country',
            dataSource: props.targetCountryList || [],
            updateFunction: props.setTargetCountryList
        },
        {
            name: 'languageCodes',
            label: 'Language',
            dataSource: props.languageList || [],
            updateFunction: props.setLanguageList
        },
        {
            name: 'statusIds',
            label: 'Status',
            dataSource: props.statusList || [],
            updateFunction: props.setStatusList
        }
    ],
    
    initialFilters: (props) => ({
        landingPageName: '',
        campaignName: '',
        dealIds: props.dealList ? props.dealList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        targetCountryCodes: props.targetCountryList ? props.targetCountryList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        languageCodes: props.languageList ? props.languageList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        statusIds: props.statusList ? props.statusList.filter(item => item.checked).map(item => item.key).join(',') : 'all'
    })
};

/**
 * Asset Uploader Filter Configuration
 */
export const assetUploaderFilterConfig = {
    filterFields: () => [
        {
            name: 'mediaName',
            label: 'Media Name',
            type: 'text',
            placeholder: 'Enter media name...'
        }
    ],
    
    multiSelectFields: (props) => [
        {
            name: 'moduleIds',
            label: 'Module',
            dataSource: props.moduleList || [],
            updateFunction: props.setModuleList
        },
        {
            name: 'targetCountryCodes',
            label: 'Target Country',
            dataSource: props.targetCountryList || [],
            updateFunction: props.setTargetCountryList
        },
        {
            name: 'languageCodes',
            label: 'Language',
            dataSource: props.languageList || [],
            updateFunction: props.setLanguageList
        }
    ],
    
    filterFields: (props) => [
        {
            name: 'mediaName',
            label: 'Media Name',
            type: 'text',
            placeholder: 'Enter media name...'
        },
        {
            name: 'status',
            label: 'Status',
            type: 'select',
            options: [
                { value: assetStatus.active, label: 'Active' },
                { value: assetStatus.inactive, label: 'Inactive' },
                { value: assetStatus.all, label: 'All' }
            ]
        }
    ],
    
    initialFilters: (props) => ({
        mediaName: '',
        moduleIds: props.moduleList ? props.moduleList.filter(item => item.checked).map(item => item.key).join(',') : '',
        targetCountryCodes: props.targetCountryList ? props.targetCountryList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        languageCodes: props.languageList ? props.languageList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        status: assetStatus.active
    })
};

/**
 * Form Management Filter Configuration
 */
export const formManagementFilterConfig = {
    multiSelectFields: (props) => [
        {
            name: 'moduleIds',
            label: 'Module',
            dataSource: props.moduleList || [],
            updateFunction: props.setModuleList
        },
        {
            name: 'targetCountryCodes',
            label: 'Target Country',
            dataSource: props.targetCountryList || [],
            updateFunction: props.setTargetCountryList
        },
        {
            name: 'languageCodes',
            label: 'Language',
            dataSource: props.languageList || [],
            updateFunction: props.setLanguageList
        }
    ],
    
    filterFields: () => [
        {
            name: 'status',
            label: 'Status',
            type: 'select',
            options: [
                { value: assetStatus.active, label: 'Active' },
                { value: assetStatus.inactive, label: 'Inactive' },
                { value: assetStatus.all, label: 'All' }
            ]
        }
    ],
    
    initialFilters: (props) => ({
        moduleIds: props.moduleList ? props.moduleList.filter(item => item.checked).map(item => item.key).join(',') : '',
        targetCountryCodes: props.targetCountryList ? props.targetCountryList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        languageCodes: props.languageList ? props.languageList.filter(item => item.checked).map(item => item.key).join(',') : 'all',
        status: assetStatus.active
    })
};
