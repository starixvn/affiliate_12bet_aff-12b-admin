import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    Market: 'Market',
    AffDomain: 'Aff Domain',
    RedirectDomain: 'Redirect Domain',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    Status: 'Status',
    Action: globalResource.Action
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    selectedDomain: null,
    marketListByDomains: null
}