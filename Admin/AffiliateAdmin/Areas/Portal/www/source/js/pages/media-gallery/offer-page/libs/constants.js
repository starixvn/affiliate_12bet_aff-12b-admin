import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    FileName: 'HTML File',
    FileSize: 'File Size (KB)',
    Language: 'Language',
    UploadDate: 'Upload Date',
    FromDate: 'Effective From',
    ToDate: 'To',
    Affiliate: 'Affiliate',
    Preview: 'Preview',
    Edit: 'Edit'
}

const now = new Date();
const firstDate = new Date(now.getFullYear(), now.getMonth() + 1, 1);
const lastDate = new Date(now.getFullYear(), now.getMonth() + 2, 0);

export const defaultState = {
    fileName: '',
    fromDate: firstDate,
    toDate: lastDate,
    language:'English',
    fileSize:0,
    selectedFile: null,
    isClickedSubmit: false,
    isInsert: true,
    isShowMessage: false,
    iniatalFileName: "",
    messageNotificationPopup:"",
    isSuccess: true,
    message: ''
}

export const restrictedCharactersPattern = /[;\/\?:@=&"<>#%{}\|\\^~\[\]`\s+]/;
export const maxLength = 50;

export const messages = {
    PleaseSelect: 'Please select Offer page(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected Offer page(s)?',
    DeleteItemsSuccessfully: 'Delete Offer page(s) successfully'
}

export const maxFileSize = 3 * 1024 * 1024;

export const displayDateFormat = "MM/dd/yyyy HH:mm:ss"