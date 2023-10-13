import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    Domain: 'Domain',
    SSL: 'SSL',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    BlockTargetCountry: 'Block Target Country',
    Action: globalResource.Action
}

export const targetCountryStatus = {
    Unblocked: 0,
    Blocked: 1,
    Using: 2
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    usingTargetCountries: [],
    isBlockAllTargetCountries: false,
    selectedDomain: null,
    targetCountryListByDomain: null
}