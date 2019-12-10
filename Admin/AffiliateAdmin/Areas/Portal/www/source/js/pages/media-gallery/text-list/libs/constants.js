import { deviceTypes, pcLaptop, mobile } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export { deviceTypes, globalResource, mobile };

const defaultCategory = 'Basketball';
const defaultLanguage = 'Chinese Simplified';
const defaultCurrency = 'EURO';
export const defaultDevice = pcLaptop;

export const headerText = {
    Index: '#',
    Content: 'Content',
    Category: 'Category',
    Language: 'Language',
    Currency: 'Currency',
    Device: 'Device'
}

export const messages = {
    PleaseSelect: 'Please select text(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected text(s)?',
    DeleteItemsSuccessfully: 'Delete Text(s) Successfully'
}

export const defaultState = {
    textContent: '',
    category: defaultCategory,
    language: defaultLanguage,
    currency: defaultCurrency,
    deviceType: deviceTypes[defaultDevice],
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}