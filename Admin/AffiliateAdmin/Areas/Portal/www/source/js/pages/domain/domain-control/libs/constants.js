import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: '#',
    Market: 'Market',
    AffDomain: 'Aff Domain',
    AffSubDomain: 'Aff Sub-domain',
    RedirectDomain: 'Redirect Domain',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    Status: 'Status',
    Edit: globalResource.Edit,
    Delete: globalResource.Delete
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    selectedDomain: null,
    marketListByDomains: null
}