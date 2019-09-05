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