import { deductionNameList, messages, INFINITY_AM, INFINITY_QM } from './constant';

const emptyCommGroup = {
    GroupId: 0,
    GroupName: '',
    Description: '',
    QmTurnover: 100,
    TierSettingList: [],
    PlatformFeeList: [],
    OperationalFee: {},
    BankingFee: {},
    ExchangeWinLossShare: {},
    ExchangeCommissionShare: {}
}

export const buildDefaultCommGroup = (deductionList, productList) => {
    const deductionSetting = getDeductionSetting(deductionList);

    const platformFeeParam = { productList, deductionSetting, platformFeeList: [] };
    const platformFeeList = buildPlatformFeeList(platformFeeParam);

    const operationalFee = createDefaultOperationalSetting(deductionSetting.operationalFeeSetting);
    const bankingFee = createDefaultBankingSetting(deductionSetting.bankingFeeSetting);
    const exchangeWinLossShare = createDefaultWinLossShareSetting(deductionSetting.exchangeWinLossShareSetting);
    const exchangeCommissionShare = createDefaultCommissionShareSetting(deductionSetting.exchangeCommissionShareSetting);

    return Object.assign({}, emptyCommGroup, {
        PlatformFeeList: platformFeeList,
        OperationalFee: operationalFee,
        BankingFee: bankingFee,
        ExchangeWinLossShare: exchangeWinLossShare,
        ExchangeCommissionShare: exchangeCommissionShare
    });
}

export const getDeductionSetting = (deductionList) => {
    const deductionObj = convertDeductionList(deductionList);
    const platformFeeSetting = deductionObj[deductionNameList.platformFee];
    const operationalFeeSetting = deductionObj[deductionNameList.operationalFee];
    const bankingFeeSetting = deductionObj[deductionNameList.bankingFee];
    const exchangeWinLossShareSetting = deductionObj[deductionNameList.exchangeWinLossShare];
    const exchangeCommissionShareSetting = deductionObj[deductionNameList.exchangeCommissionShare];

    return {
        platformFeeSetting,
        operationalFeeSetting,
        bankingFeeSetting,
        exchangeWinLossShareSetting,
        exchangeCommissionShareSetting
    }
}

export const createDefaultOperationalSetting = (operationalFeeSetting) => {
    return {
        DeductionId: operationalFeeSetting.DeductionId,
        ProductIds: 0,
        DeductedPercentage: operationalFeeSetting.DeductedPercentage
    }
}

export const createDefaultBankingSetting = (bankingFeeSetting) => {
    return {
        DeductionId: bankingFeeSetting.DeductionId,
        ProductIds: 0,
        DeductedPercentage: bankingFeeSetting.DeductedPercentage
    }
}

export const createDefaultWinLossShareSetting = (exchangeWinLossShareSetting) => {
    return {
        DeductionId: exchangeWinLossShareSetting.DeductionId,
        ProductIds: 19,
        DeductedPercentage: exchangeWinLossShareSetting.DeductedPercentage
    }
}

export const createDefaultCommissionShareSetting = (exchangeCommissionShareSetting) => {
    return {
        DeductionId: exchangeCommissionShareSetting.DeductionId,
        ProductIds: 19,
        DeductedPercentage: exchangeCommissionShareSetting.DeductedPercentage
    }
}


export const buildPlatformFeeList = ({ productList, deductionSetting, productIds, platformFeeList = [] }) => {
    const { platformFeeSetting } = deductionSetting;

    const products = convertProductList(productList, productIds);
    const convertedPlatformFeeList = platformFeeList.length ? platformFeeList : [{
        DeductionId: platformFeeSetting.DeductionId,
        ProductIds: products.map(p => p.ProductId).join(','),
        DeductedPercentage: platformFeeSetting.DeductedPercentage
    }];

    return convertedPlatformFeeList;
}

export const createDefaultPlatformfee = (productList, platformFeeSetting) => {
    return {
        deductionId: platformFeeSetting.DeductionId,
        productList: convertProductList(productList),
        commission: platformFeeSetting.DeductedPercentage
    };
}

export const convertDeductionList = (deductionList) => {
    const deductions = {};
    for (let i = 0; i < deductionList.length; i++) {
        const item = deductionList[i];
        deductions[item.DeductionName] = item;
    }

    return deductions;
}

export const convertProductList = (productList, productIds = '') => {
    const idList = productIds.split(',').filter(p => !!p);
    const isCheckAll = idList.length === productList.length;

    return productList.map(item => {
        return {
            ProductId: item.ProductId,
            ProductName: item.ProductName,
            Checked: isCheckAll || idList.filter(i => Number(i) === item.ProductId).length > 0
        }
    })
}

export const validate = (selectedGroup) => {
    const { GroupName, QmTurnover, TierSettingList, PlatformFeeList } = selectedGroup;

    const groupNameValidation = validateGroupName(GroupName);
    const qmTurnoverValidation = validateQmTurnover(QmTurnover);
    const tierSettingValidation = validateTierSettingList(TierSettingList);
    const platformFeeListValidation = validatePlatformFeeList(PlatformFeeList);

    const isValid = groupNameValidation.isValid && qmTurnoverValidation.isValid &&
        tierSettingValidation.isValid && platformFeeListValidation.isValid;
    const messageList = [...groupNameValidation.messageList, ...qmTurnoverValidation.messageList,
    ...tierSettingValidation.messageList, ...platformFeeListValidation.messageList
    ];

    return { isValid, messageList };
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

export const validateQmTurnover = (qmTurnover) => {
    let isValid = true;
    const messageList = [];

    if (isNaN(qmTurnover) || typeof qmTurnover !== 'number') {
        isValid = false;
        messageList.push(messages.requireQmTurnover);
    }

    return { isValid, messageList };
}

export const validateTierSettingList = (tierSettingList) => {
    let isValid = true;
    const messageList = [];

    if (tierSettingList.length === 0) {
        isValid = false;
        messageList.push(messages.requireTier);
    } else if (tierSettingList.filter(item => item.MaxAmount === INFINITY_AM && item.MaxQm === INFINITY_QM).length === 0) {
        isValid = false;
        messageList.push(messages.requireInfinity);
    } else if (tierSettingList.filter(item => item.MinAmount > item.MaxAmount).length > 0) {
        isValid = false;
        messageList.push(messages.amountConstrain);
    } else if (tierSettingList.filter(item => item.MinQm > item.MaxQm).length > 0) {
        isValid = false;
        messageList.push(messages.qmConstrain);
    }

    return { isValid, messageList };
}

export const validatePlatformFeeList = (platformFeeList) => {
    let isValid = true;
    const messageList = [];

    if (platformFeeList.filter(item => !item.ProductIds).length > 0) {
        isValid = false;
        messageList.push(messages.requireSelectedProduct);
    }

    return { isValid, messageList };
}