export const claimResource = {
    WritePermission: 'Write',

    //Main menu permission
    MediaGalleryMenu: '[Media]',
    MessageMenu: '[HotNews]',
    ExchangeRateMenu: '[AffEx.Rate]',
    AdminRoleMenu: '[UserControl]',
    DomainMenu: '[Domain]',

    AffiliateInfo: '[Affiliate]',
    PaymentRequest: '[Payment]',
    EmailHistory: '[EmailHistory]',
    ViewLog: '[ViewLog]',
    CommissionGroup: '[CommissionGroup]',
    AssignCommGroup: '[AssignCommGroup]',
    MemberDetail: '[AffMemberDetail]',
    ActiveMembers: '[ActiveMembers]',
    RegisteredMembers: '[RegisteredMembers]',
    MemberName: '[AffMemberName]',
    TurnOver: '[AffTurnover]',
    Note: '[Note]',
    EditAffiliate: '[AffCustomerEdit]',
    SecurityQA: '[SecurityQA]',
    AffEmail: '[AffEmail]',
    AffPhone : '[AffPhone]',
    AffPaymentInfo : '[AffPaymentInfo]',
    AffWebUrl : '[AffWebURL]',
    AffAdjustment: '[AffAdjustment]',
    AffAccountStatus: '[AffAccountStatus]',
    AffPlayerDetail: '[AffPlayerDetail]',
    AffPassword: '[AffPassword]',
    LanguageAmendment: '[AffMarket]',
    AffPersonalInfo: "[AffPersonalInfo]",
    ProcRequestMarketingRole: '[ProcessingRequestMarketingRole]',
    ProcRequestManagementRole: "[ProcessingRequestManagementRole]",

    //Export Excel
    ExportAffInfoExcel: '[ExportAffInfoExcel]',
    ExportPlayerDetailExcel: '[ExportPlayerDetailExcel]',
    ExportReportExcel: '[ExportReportExcel]',
}

export class Claim {
    constructor(type, value){
        this.type = type;
        this.value = value;
    }
}

export default class ClaimPrincipal{
    constructor(claims){
        this.claims = claims;
    }

    static get currentClaimPrincipal() {
        let claims = window.globalVariables.claims || window.initialState.claims || {};
        return new ClaimPrincipal(claims);
    }

    hasReadClaim(claimType) {
        if (typeof claimType === 'string')
        {
            return this.claims[claimType] !== undefined
        }
        return false;
    }

    hasWriteClaim(claimType){
        if( typeof claimType === 'string' && this.claims[claimType] !== undefined){
            const found = this.claims[claimType].find(c => c === claimResource.WritePermission);
            return !!found;
        }
        return false;
    }

    hasClaim(claim){
        if (typeof claim === 'string')
        {
            return this.hasReadClaim(claim);
        }
        return this.claims[claim.type] !== undefined
            && (claim.value === undefined || !!this.claims[claim.type].find(value => value === claim.value))
    }
}


