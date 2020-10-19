export const claimResource = {
    WritePermission : 'Write',
    SecurityQuestion : '[SecurityQA]',
    PhoneNumber : '[AffPhone]',
    Email : '[AffEmail]',
    PaymentInfo : '[AffPaymentInfo]',
    WebsiteUrl : '[AffWebURL]'
}

export default class ClaimPrincipal{
    constructor(claims){
        this.claims = claims;
    }

    hasReadClaim(claimType) {
        if(typeof claimType === 'string')
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
}