import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    Domain: 'Domain',
    SSL: 'SSL',
    Creator: 'Creator',
    CreatedDate: 'Created Date',
    BlockMarket: 'Block Market',
    Action: globalResource.Action
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