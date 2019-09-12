import { minDate } from '../../../modules/constants';
import { globalResource } from '../../../modules/globalResource';

export { minDate };

export const all = -1;
export const normal = 0;
export const important = 1;
export const insertMessage = 0;
export const currentMessage = -1;

export const searchByList = {
    [normal]: 'Normal',
    [important]: 'Important'
}

export const resource = {
    CurrentMessage: 'Current Message',
    Select: 'Select:',
    None: 'None',
    AllDate: 'All Date',
    ViewDetail: 'View Detail',
    Add: globalResource.Add,
    All: globalResource.All,
    From: globalResource.From,
    To: globalResource.To,
    SearchBy: globalResource.SearchBy,
    Search: globalResource.Search,
    Edit: globalResource.Edit,
    Update: globalResource.Update,
    Delete: globalResource.Delete,
    Cancel: globalResource.Cancel,
    NoInfo: globalResource.NoInfo,
    Important: searchByList[important],
}

export const headerText = {
    Index: '#',
    Creator: 'Creator',
    PostedDate: 'Posted Date',
    Message: 'Message',
    Important: searchByList[important],
    UpdatedBy: 'Updated By',
    UpdatedDate: 'Updated Date'
}

export const messages = {
    PleaseSelect: 'Please select items to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected emails?',
    DeleteItemsSuccessfully: 'Delete Emails Successfully',
    AreYouSureDelete: 'Are you sure to delete the selected email?',
    DeleteItemSuccessfully: 'Delete Email Successfully',
}

export const languages = {
    en: 'English',
    cn: 'Chinese Simplified',
    tw: 'Chinese Traditional',
    es: 'Thai',
    th: 'Spanish',
    vi: 'VietNamese',
    id: 'Indonesian',
    kr: 'Korean',
    jp: 'Japanese'
}

export const popupTitle = {
    Add: 'Add New Message',
    Current: 'Current Message Detail',
    Detail: 'Message Detail'
}