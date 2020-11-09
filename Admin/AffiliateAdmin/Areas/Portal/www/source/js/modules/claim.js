export const claimResource = {
    WritePermission : 'Write',
    SecurityQuestion : '[SecurityQA]',
    PhoneNumber : '[AffPhone]',
    Email : '[AffEmail]',
    PaymentInfo : '[AffPaymentInfo]',
    WebsiteUrl : '[AffWebURL]',
    Note : '[Note]',
    Adjustment: '[AffAdjustment]',
    LanguageAmendment: '[AffMarket]',
    CommGroup: '[CommissionGroup]',
    AffCommGroup: '[AssignCommGroup]',
    AffAccountStatus: '[AffAccountStatus]',
    Domain: '[Domain]',
    HotNews: '[HotNews]',
    AffExRate: '[AffEx.Rate]',
    MemberMenu: '[AffMember]',
    AffMember: '[AffMemberDetail]',
    ActiveMembers: '[ActiveMembers]',
    AffPlayerDetail: '[AffPlayerDetail]',
    AffMemberName: '[AffMemberName]',
    TurnOver: '[AffTurnover]',
    AffMemberName: '[AffMemberName]',
    ExportExcel: '[ExportExcel]',
    AffInfo: '[Affiliate]',
    Payment: '[Payment]',
    MediaGallery: '[Media]',
    AffPassword: '[AffPassword]'
}

export class Claim{
    constructor(type, value){
        this.type = type;
        this.value = value;
    }
}

export default class ClaimPrincipal{
    constructor(claims){
        this.claims = claims;
    }

    static currentClaimPrincipal =  new ClaimPrincipal(window.globalVariables.claims);

    hasReadClaim(claimType) {
        if (typeof claimType === 'string')
        {
            return this.claims[claimType] !== undefined
        }
        return false;
    }

    hasWriteClaim(claimType){
        if( typeof claimType === 'string' && this.claims[claimType] !== undefined){
            return this.claims[claimType] === claimResource.WritePermission;
        }
        return false;
    }

    hasClaim(claim){
        if (typeof claim === 'string')
        {
            return this.hasReadClaim(claim);
        }

        return this.claims[claim.type] !== undefined 
            && (claim.value === undefined || this.claims[claim.type] === claim.value)
    }
}


