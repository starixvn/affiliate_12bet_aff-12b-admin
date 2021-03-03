import { globalResource } from '../../../../modules/globalResource';
import {formatDateTime, convertToTimeOffset} from '../../../../modules/utils';

const getCurrentDateInServerString = () => {
    let date = new Date();
    var serverDate = convertToTimeOffset(date, -4);
    return formatDateTime(serverDate, dateFormat);
}

export const dateFormat = "yyyy-MM-ddThh:mm";

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    FileName: 'HTML File',
    FileSize: 'File Size (KB)',
    Language: 'Language',
    UploadDate: 'Upload Date',
    FromDate: 'Effective From',
    ToDate: 'To',
    Preview: 'Preview',
    Edit: 'Edit'
}

export const defaultState = {
    fileName: '',
    fromDate: getCurrentDateInServerString(),
    toDate: getCurrentDateInServerString(),
    language:'en-US',
    fileSize:0,
    selectedFile: null,
    isClickedSubmit: false,
    isInsert: true,
    isShowMessage: false,
    messageNotificationPopup:"",
    isSuccess: true,
    message: ''
}

export const restrictedCharactersPattern = /[;\/\?:@=&"<>#%{}\|\\^~\[\]`\s+]/;
export const maxLength = 50;

export const messages = {
    requiredErrorMessage: 'This field is required',
    maxLengthErrorMessage: 'This field must not exceed 50 characters',
    PleaseSelect: 'Please select landing page(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected landing page(s)?',
    DeleteItemsSuccessfully: 'Delete landing page(s) Successfully'
}

export const maxFileSize = 3 * 1024 * 1024;