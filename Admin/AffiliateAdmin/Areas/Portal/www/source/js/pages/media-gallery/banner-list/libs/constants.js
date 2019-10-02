import { deviceTypes, pcLaptop } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export { deviceTypes, globalResource };

export const Casino = 'Casino';
export const Baccarat = 'Baccarat';
export const Sportsbook = 'Sportsbook';

const defaultLanguage = 'Chinese Simplified';
const defaultCurrency = 'EURO';
const defaultImageType = 'Flash';
export const Asian = 2;
export const defaultDevice = pcLaptop;

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
    fileName: '',
    inputFileName: '',
    category: Casino,
    subCategory: Baccarat,
    language: defaultLanguage,
    currency: defaultCurrency,
    imageType: defaultImageType,
    affType: Asian,
    deviceType: deviceTypes[defaultDevice],
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isDisabled: true
}