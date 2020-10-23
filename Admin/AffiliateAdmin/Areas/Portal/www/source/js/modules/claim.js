import { Language } from "../pages/customer/left-panel/components/general-info/Language";

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
    AffAccountStatus: '[AffAccountStatus]',
    Domain: '[Domain]'
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
        return this.claims[claim.type] !== undefined 
            && (claim.value === undefined || this.claims[claim.type] === claim.value)
    }
}