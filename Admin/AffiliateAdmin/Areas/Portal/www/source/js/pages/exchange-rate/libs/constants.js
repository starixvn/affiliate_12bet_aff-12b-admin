import { globalResource } from '../../../modules/globalResource';
import { minDate, money } from '../../../modules/constants';

export { minDate, globalResource, money };

export const resource = {
    Date: 'Date',
    Submit: 'Submit',
    Title: 'Ex.Rate',
    Currency: 'Currency',
    US$Rate: 'US$ Rate',
    Buying: 'Buying',
    Search: globalResource.Search,
    Cancel: globalResource.Cancel,
}

export const headerText = {
    Index: '#',
    Currency: 'Currency',
    Selling: 'Selling',
    Buying: 'Buying'
}

export const message = {
    Required: 'Exchange rate is required.',
    Invalid: 'Exchange rate must be greater than zero.',
    Success: 'Updated successfully!',
    InputDigit: 'Please input digit only.',
    ExceedLimit: 'Exchange rate exceeds the allowable limit.'
}

export const regExp = {
    checkRate: "^(\\d+\\.?\\d*)?$",
    shouldFormatRate: "^\\d+$",
    validRate: "^\\d+(\\.\\d{1,})?$"
}

export const rateType = {
    ActualRate: 0,
    BuyingRate: 1
}

export const maxLength = 25;
export const exchangeUs$Id = 3;
export const charCodes = {
    KeyEnter: 13,
    KeyDot: 46,
    Key0: 48,
    Key9: 57,
    KeyUnitSeparator: 31
}