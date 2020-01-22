import { deviceTypes, pcLaptop, mobile } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export { deviceTypes, globalResource, pcLaptop, mobile };

export const Casino = 'Casino';
export const Baccarat = 'Baccarat';
export const Sportsbook = 'Sportsbook';

const defaultLanguage = 'Chinese Simplified';
const defaultCurrency = 'EURO';
const defaultImageType = 'Image';
export const Asian = 2;
export const maxWidthHeight = 501;
export const maxLength = 50;

export const headerText = {
    Index: '#',
    Images: 'Images',
    FileName: 'File Name',
    Category: 'Category',
    SubCategory: 'Sub Category',
    Language: 'Language',
    Currency: 'Currency',
    Type: 'Type',
    Size: 'Size',
    AffType: 'Affiliate Type',
    Device: 'Device',
    ImageType: 'Image Type'
}

export const messages = {
    PleaseSelect: 'Please select banner(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected banner(s)?',
    DeleteItemsSuccessfully: 'Delete banner(s) Successfully'
}

export const defaultState = {
    file: null,
    inputFileName: '',
    category: Casino,
    subCategory: Baccarat,
    language: defaultLanguage,
    currency: defaultCurrency,
    imageType: defaultImageType,
    affType: Asian,
    deviceType: 0,
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isDisabled: true,
    messageNotificationPopup: ''
}