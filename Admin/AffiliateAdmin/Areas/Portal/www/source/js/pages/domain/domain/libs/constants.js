import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    Domain: 'Domain',
    SSL: 'SSL',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    ExcludedTargetCountry: 'Block Target Country',
    Action: globalResource.Action
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    usingTargetCountries: [],
    isExcludedAllTargetCountries: false,
    selectedDomain: null,
    targetCountryList: null
}