import { deviceTypes, pcLaptop } from '../../../../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

export { deviceTypes, globalResource };

const Asian = 'Asian';
const Media = 'Media';
const European = 'European';

export const affTypes = {
    [Asian]: 2,
    [Media]: 3,
    [European]: 4
}

const Flash = 'Flash';
export const imageTypes = {
    Flash,
    Image: 'Image'
}

const defaultCategory = 'Basketball';
const defaultSubCategory = 'Baccarat';
const defaultLanguage = 'Chinese Simplified';
const defaultCurrency = 'EURO';
const defaultImageType = imageTypes[Flash];
export const defaultAffType = affTypes[Asian];
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
    fileName: '',
    category: defaultCategory,
    subCategory: defaultSubCategory,
    language: defaultLanguage,
    currency: defaultCurrency,
    imageType: defaultImageType,
    affType: defaultAffType,
    deviceType: deviceTypes[defaultDevice],
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}