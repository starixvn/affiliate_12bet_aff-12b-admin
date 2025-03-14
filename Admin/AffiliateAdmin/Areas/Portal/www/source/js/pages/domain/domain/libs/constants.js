import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: '#',
    Domain: 'Domain',
    SSL: 'SSL',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    BlockMarket: 'Block Market',
    Edit: globalResource.Edit,
    Delete: globalResource.Delete
}

export const marketStatus = {
    Unblocked: 0,
    Blocked: 1,
    Using: 2
}

export const defaultState = {
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    usingMarkets: [],
    isBlockAllMarkets: false,
    selectedDomain: null,
    marketListByDomain: null
}