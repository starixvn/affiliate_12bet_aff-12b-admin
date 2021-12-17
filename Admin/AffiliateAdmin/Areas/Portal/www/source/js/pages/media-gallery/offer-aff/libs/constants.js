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
    OfferPageHtmlFile: 'Offer Page HTML File',
    Preview: 'Preview',
    Edit: 'Edit'
}

export const defaultState = {
    affNames: '',
    offerPage:'',
    isClickedSubmit: false,
    isShowMessage: false,
    isSuccess: true,
    message: ''
}

export const restrictedCharactersPattern = /[;\/\?:@=&"<>#%{}\|\\^~\[\]`\s+]/;
export const validCharactersPattern = /[0-9a-zA-Z\n\s]/;

export const messages = {
    PleaseSelect: 'Please select offer - affiliate assignment(s) to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected offer - affiliate assignment(s)?',
    DeleteItemsSuccessfully: 'Delete offer - affiliate assignment(s) successfully'
}

export const displayDateFormat = "MM/dd/yyyy HH:mm:ss"