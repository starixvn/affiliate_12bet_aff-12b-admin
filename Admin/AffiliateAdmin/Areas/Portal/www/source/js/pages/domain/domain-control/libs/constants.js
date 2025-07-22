import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const filterDealMode = {
    'Exclude': 1,
    'Include': 0
}

export const headerText = {
    Index: '#',
    TargetCountry: 'Target Country',
    AffDomain: 'Aff Domain',
    AffSubDomain: 'Aff Sub-domain',
    RedirectDomain: 'Redirect Domain',
    MobileOnly: 'Mobile Only',
    Deal: 'Deal',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    Status: 'Active',
    Edit: globalResource.Edit,
    Delete: globalResource.Delete
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    selectedDomain: null,
    targetCountryListByDomains: null,
    dealList: []
}