import { globalResource } from '../../../../modules/globalResource';

export { globalResource };

export const colorByStatus = {
    Draft: "#000000",
    Submitted: "#0000FF",
    Approved: "#008000",
    Rejected: "#FF0000"
}

export const statusList = {
    'Draft': { value: 1, shouldShow: (status) => !status || status === 'Draft' },
    'Submitted': { value: 2, shouldShow: (status) => !status || status === 'Draft' || status === 'Submitted' },
    'Approved': { value: 3, shouldShow: (status) => !!status && status !== 'Draft' },
    'Rejected': { value: 4, shouldShow: (status) => !!status && status !== 'Draft' },
}

export const headerText = {
    Index: globalResource.Index,
    UserName: 'Username',
    CreatedBy: 'Created By',
    CreationDate: 'Creation Date',
    LastModified: 'Last Modified',
    SubmittedDate: 'Submitted',
    Deadline: 'Deadline',
    ApprovedBy: 'Approved By',
    ApprovalDate: 'Approval Date',
    Status: 'Status',
    Action: 'Action',
}

export const resource = {
    Search: globalResource.Search,
    Space: ' - ',
    ReportName: 'PROCESSING REQUEST REPORT',
    AllStatuses: 'All STATUSES',
    UsernameContains: 'USERNAME CONTAINS ',
    StatusCount: 'Status count',
    Apostrophe: "'",
    ExceedMaxLength: 'Exceed max length',
    FieldRequeired: 'This field is required.',
    InsertSuccess: 'The processing request has been saved.',
    UpdateSuccess: 'The processing request has been saved.',
    InvalidDeadline: 'Invalid Deadline'
}

export const field = {
    'AgentName': 'Agent Name',
    'PromotionMethod': 'Promotion Method',
    'Remark': 'Remark',
    'ServerCountry': 'Country (Server)',
    'TargetMarket' : 'Target Market',
    'WebAdvAff' : 'Adv/Aff on the Website/SM',
    'WebRank': 'Website Rank',
    'WebRegDate': 'Website Register Date',
    'WebSMLanguage': 'Website/SM Language',
    'WebSMQuality': 'Quality of Website/SM',
    'WebSMType': 'Type of Website/SM'
}

export const playerMethod = 'PlayerAccount';
export const wireMethod = 'BankTranfer';
