export const permissionResource = {
    UserControl: "[UserControl]",
    Affiliate: "[Affiliate]",
    Payment: "[Payment]",
    Media: "[Media]",
    AffExRate: "[AffEx.Rate]",
    HotNews: "[HotNews]",
    RevenueGroup: "[RevenueGroup]",
    AffReport: "[AffReport]",
    Domain: "[Domain]",
    MemberEmail: "[MemberEmail]",
    Note: "[Note]",
    SecurityQA: "[SecurityQA]",
    EditAffStatus: "[EditAffiliateStatus]",
    EditAffDetails: "[EditAffiliateDetails]",
    TransferPlayerSetting: "[HideTransferPlayerSetting]", //Hide Transfer Player
    TransferPlayerReport: "[HideTransferPlayerReport]" //Hide Transfer Player
}

export class Permission {
    constructor(permissions) {
        this.permissions = permissions
    }

    static get currentPermissions() {
        const { permissions } = window.globalVariables || { permissions: [] };

        return new Permission(permissions);
    }

    getPermissionInfo = (functionName) => {
        return this.permissions.find(p => p.FunctionName === functionName);
    }

    hasPermission = (functionName) => !!this.getPermissionInfo(functionName)
}