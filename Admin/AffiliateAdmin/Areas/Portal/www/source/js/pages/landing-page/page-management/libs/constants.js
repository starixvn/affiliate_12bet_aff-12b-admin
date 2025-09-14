import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const headerText = {
    Index: '#',
    CampaignCode: 'Campaign Code',
    CampaignName: 'Campaign Name',
    PageName: 'Page Name',
    PageCode: 'Page Code',
    TargetCountry: 'Target Country',
    Language: 'Language',
    Deal: 'Deal',
    CreatedDate: 'Created Date',
    CreatedBy: 'Created By',
    LastModifiedDate: 'Last Modified Date',
    LastModifiedBy: 'Last Modified By',
    Status: 'Status',
    Action: 'Action'
}

export const defaultState = {
    isBlockAllTargetCountries: false,
    selectedLandingPage: { pageName: '', pageCode: '', blockTargetCountry: '' },
    targetCountryList: null,
    isClickedSubmit: false,
    isInsert: true,
    isShowMessage: false,
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

export const messageSwitchStatus = {
    SwitchStatusSuccessfully: 'Status changed successfully',
    FailedToSwitchStatus: 'Failed to change status'
}