import { globalResource } from '../../../../modules/globalResource';
import { selectedAllProduct } from '../../../../modules/constants';

export { globalResource, selectedAllProduct };

const defaultLanguage = 'Chinese Simplified';
export const Asian = 2;
export const maxWidthHeight = 501;
export const maxLength = 50;
export const regSpecialCharacters = '[\\\\\/\:\*\?\"\<\>\|\#\%\&\+]';
export const validExtensions = ['jpg', 'gif', 'png', 'jpeg'];

export const headerText = {
    Index: globalResource.Index,
    Images: 'Images',
    FileName: 'File Name',
    TargetProduct: 'Target Product',
    Language: 'Language',
    Size: 'Size',
    CopiedFrom: 'Copied From',
    NumberOfCopies: 'Number Of Copies',
    AffType: 'Affiliate Type'
}

export const messages = {
    PleaseSelect: 'Please select banner(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected banner(s)?',
    DeleteItemsSuccessfully: 'Delete banner(s) Successfully'
}

export const defaultState = {
    files: [],
    inputFileNames: [''],
    language: defaultLanguage,
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isDisabled: true,
    messageNotificationPopup: ''
}