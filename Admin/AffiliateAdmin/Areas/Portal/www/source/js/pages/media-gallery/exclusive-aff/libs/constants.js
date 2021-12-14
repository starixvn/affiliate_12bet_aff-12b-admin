import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: globalResource.Index,
    Search: globalResource.Search,
    FileName: 'HTML File',
    FileSize: 'File Size (KB)',
    FromDate: 'Effective From',
    ToDate: 'To',
    Affiliate: 'Affiliate',
    ExclusiveHtmlFile: 'Exclusive HTML File',
    Preview: 'Preview',
    Edit: 'Edit'
}

export const defaultState = {
    affNames: '',
    exclusivePage:'',
    isClickedSubmit: false,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}

export const restrictedCharactersPattern = /[;\/\?:@=&"<>#%{}\|\\^~\[\]`\s+]/;

export const messages = {
    PleaseSelect: 'Please select exclusive - affiliate assignment(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected exclusive - affiliate assignment(s)?',
    DeleteItemsSuccessfully: 'Delete exclusive - affiliate assignment(s) successfully'
}

export const displayDateFormat = "MM/dd/yyyy HH:mm:ss"