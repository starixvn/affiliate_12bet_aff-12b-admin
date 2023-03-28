import { minDate } from '../../../modules/constants';
import { globalResource } from '../../../modules/globalResource';

export { minDate };

export const all = '0';
export const normal = '1';
export const important = '2';
export const messageInsertId = 0;
export const messageCurrentId = -1;

const english = 'MessageContentEn';
const bangladesh = 'MessageContentBn';
const chineseTraditional = 'MessageContentTw';
const chineseSimplified = 'MessageContentCn';
const spanish = 'MessageContentEs';
const thai = 'MessageContentTh';
const vietNamese = 'MessageContentVi';
const indonesian = 'MessageContentId';
const korean = 'MessageContentKr';
const japanese = 'MessageContentJp';
const cambodian = 'MessageContentKh';

export const defaultMessageContentList =
{
    MessageId: 0,
    [english]: '',
    [bangladesh]: '',
    [chineseTraditional]: '',
    [chineseSimplified]: '',
    [spanish]: '',
    [thai]: '',
    [vietNamese]: '',
    [indonesian]: '',
    [cambodian]: '',
    [korean]: '',
    [japanese]: ''
};

export const languages = {
    [english]: 'English',
    [bangladesh]: 'English (Bangladesh)',
    [chineseTraditional]: 'Chinese Traditional',
    [chineseSimplified]: 'Chinese Simplified',
    [spanish]: 'Spanish',
    [thai]: 'Thai',
    [vietNamese]: 'VietNamese',
    [indonesian]: 'Indonesian',
    [cambodian]: 'Cambodian',
    [korean]: 'Korean',
    [japanese]: 'Japanese'
}

export const searchByList = {
    [all]: 'All',
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
    Index: globalResource.Index,
    Creator: 'Creator',
    PostedDate: 'Posted Date',
    Message: 'Message',
    Important: searchByList[important],
    UpdatedBy: 'Updated By',
    UpdatedDate: 'Updated Date'
}

export const messages = {
    PleaseSelect: 'Please select items to delete',
    AreYouSureDeleteItems: 'Are you sure to delete the selected item(s)?',
    DeleteItemsSuccessfully: 'Delete message(s) Successfully',
    PleaseEnter: 'Please enter the content of message, one language at least.',
    AreYouWant: 'Are you sure you want to update this message?'
}

export const popupTitle = {
    Add: 'Add New Message',
    Current: 'Current Message Detail',
    Detail: 'Message Detail'
}