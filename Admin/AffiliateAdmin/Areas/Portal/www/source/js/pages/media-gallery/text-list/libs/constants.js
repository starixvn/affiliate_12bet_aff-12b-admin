import { deviceTypes, pcLaptop } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export { deviceTypes, globalResource };

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
    PleaseSelect: 'Please select items to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected text(s)?',
    DeleteItemsSuccessfully: 'Delete Text(s) Successfully',
    PleaseEnter: 'Please input text to update.'
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