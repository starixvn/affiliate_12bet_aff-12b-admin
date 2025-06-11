import { messages } from './constants';


const commonDealInfo = {
    DealId: 0,
    DealName: '',
    DealType: 3,
    Description: '',
    SubCommRate: 0.05,
    CountryCodes: '',
    ActiveDays: '',
    IsActivePlayerHoldingPeriod: false,
    HoldingTickets: 0,
    HoldingDays: 0,
    IsQuota: false,
    IsInstantPayment: false,
    Quota: 0,
    CheckedMinFdm: false,
    CheckedMinTotalDeposit: false,
    CheckedMinWagering: false,
    CPATierSetting: [
        {
            TierId: 1,
            GroupProductId: 1,
            MinFdm: 0,
            MinTotalDeposit: 0,
            MinWagering: 0,
            CpaAmount: 1
        }
    ],
    ProductGroup: [
        {
            ProductIds: '',
            GroupProductId: 1,
        },
    ]
}

export const buildDefaultDealInfo = () => {
    return Object.assign({}, commonDealInfo);
}

const convertListWithChecked = (list, selectedIdsCSV, idKey, nameKey) => {
    const selectedIds = selectedIdsCSV.split(',').map(id => id.trim()).filter(Boolean).map(id => id.toString());
    const isCheckAll = selectedIds.length === list.length;

    return list.map(item => ({
        [idKey]: item[idKey],
        [nameKey]: item[nameKey],
        Checked: isCheckAll || selectedIds.includes(item[idKey])
    }));
};

export const convertProductList = (productList, productIds = '') => {
    return convertListWithChecked(productList, productIds, 'ProductId', 'ProductName');
}

export const convertTargetCountryList = (targetCountryList, targetCountryCodes = '') => {
    return convertListWithChecked(targetCountryList, targetCountryCodes, 'TargetCountryCode', 'TargetCountryName');
}

export const validateDealInfo = (dealInfo) => {
    const { DealName, CPATierSetting } = dealInfo;

    const groupNameValidation = validateGroupName(DealName);

}

export const validateGroupName = (groupName) => {
    let isValid = true;
    const messageList = [];

    if (!groupName) {
        isValid = false;
        messageList.push(messages.requireGroupName);
    }

    return { isValid, messageList };
}

export const validateGeneralDealInfo = (dealInfo) => {

}