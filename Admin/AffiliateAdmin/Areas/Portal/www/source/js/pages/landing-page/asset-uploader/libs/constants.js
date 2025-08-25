export const maxWidthHeight = 501;
export const maxLength = 50;
export const regSpecialCharacters = '[\\\\\/\:\*\?\"\<\>\|\#\%\&\+]';

export const fileUpload = {
    AllowedImageExtensions: [".jpg", ".jpeg", ".png", ".gif", ".webp"],
    MaximumFiles: 10,
    MaxFileSize: 3
}

export const headerText = {
    Index: '#',
    Module: 'Module',
    MediaName: 'Media Name',
    FileName: 'File Name',
    Dimensions: 'Dimensions',
    Images: 'Images',
    Action: 'Action',
}

export const messages = {
    PleaseSelect: 'Please select asset(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected asset(s)?',
    DeleteItemsSuccessfully: 'Delete asset(s) successfully'
}

export const defaultState = {
    files: [],
    inputFileNames: [''],
    isInsert: true,
    isShowMessage: false,
    isSuccess: true,
    message: '',
    isDisabled: true,
    isDisableCurrency: false,
    messageNotificationPopup: ''
}