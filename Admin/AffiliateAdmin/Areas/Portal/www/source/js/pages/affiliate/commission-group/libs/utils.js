import { deductionNameList, messages, INFINITY_AM, INFINITY_QM } from './constant';

const emptyCommGroup = {
    GroupId: 0,
    GroupName: '',
    Description: '',
    QmTurnover: 100,
    IsApplyBonus: false,
    TierSettingList: [],
    FdmSettingList: [],
    PlatformFeeList: [],
    OperationalFee: {},
    BankingFee: {}
}

export const buildDefaultCommGroup = (deductionList, productList) => {
    const deductionSetting = getDeductionSetting(deductionList);

    const platformFeeParam = { productList, deductionSetting, platformFeeList: [] };
    const platformFeeList = buildPlatformFeeList(platformFeeParam);

    const operationalFee = createDefaultOperationalSetting(deductionSetting.operationalFeeSetting);
    const bankingFee = createDefaultBankingSetting(deductionSetting.bankingFeeSetting);

    return Object.assign({}, emptyCommGroup, {
        PlatformFeeList: platformFeeList,
        OperationalFee: operationalFee,
        BankingFee: bankingFee
    });
}

export const getDeductionSetting = (deductionList) => {
    const deductionObj = convertDeductionList(deductionList);
    const platformFeeSetting = deductionObj[deductionNameList.platformFee];
    const operationalFeeSetting = deductionObj[deductionNameList.operationalFee];
    const bankingFeeSetting = deductionObj[deductionNameList.bankingFee];

    return {
        platformFeeSetting,
        operationalFeeSetting,
        bankingFeeSetting
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


export const buildPlatformFeeList = ({ productList, deductionSetting, productIds, platformFeeList = [] }) => {
    const { platformFeeSetting } = deductionSetting;

    const products = convertProductList(productList, productIds);
    return !!platformFeeList.length ? platformFeeList : [{
        DeductionId: platformFeeSetting.DeductionId,
        ProductIds: products.map(p => p.ProductId).join(','),
        DeductedPercentage: platformFeeSetting.DeductedPercentage
    }];
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
    for (let item of deductionList) {
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

export const validateCommGroup = (selectedGroup, shouldValidateGroupName = true) => {
    const { GroupName, QmTurnover, TierSettingList, FdmSettingList, PlatformFeeList } = selectedGroup;

    const groupNameValidation = validateGroupName(GroupName, shouldValidateGroupName);
    const qmTurnoverValidation = validateQmTurnover(QmTurnover);
    const tierSettingValidation = validateTierSettingList(TierSettingList);
    const fdmSettingValidation = validateFdmSettingList(FdmSettingList);
    const platformFeeListValidation = validatePlatformFeeList(PlatformFeeList);

    const isValid = groupNameValidation.isValid && qmTurnoverValidation.isValid &&
        tierSettingValidation.isValid && fdmSettingValidation.isValid && platformFeeListValidation.isValid;
    const messageList = [...groupNameValidation.messageList, ...qmTurnoverValidation.messageList,
        ...tierSettingValidation.messageList, ...fdmSettingValidation.messageList, ...platformFeeListValidation.messageList
    ];

    return { isValid, messageList };
}

export const validateGroupName = (groupName, shouldValidateGroupName) => {
    let isValid = true;
    const messageList = [];

    if (shouldValidateGroupName && !groupName) {
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

    if (!tierSettingList.length) {
        isValid = false;
        messageList.push(messages.requireTier);
    } else if (!tierSettingList.filter(item => item.MaxAmount === INFINITY_AM && item.MaxQm === INFINITY_QM).length) {
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

export const validateFdmSettingList = (fdmSettingList) => {
    let isValid = true;
    const messageList = [];

    if (!fdmSettingList.length) {
        isValid = true;
    } else if (!fdmSettingList.filter(item => item.MaxFdmDeposit === INFINITY_AM && item.MaxNewFdm === INFINITY_QM).length) {
        isValid = false;
        messageList.push(messages.requireFdmInfinity);
    } else if (fdmSettingList.filter(item => item.MinNewFdm > item.MaxNewFdm).length > 0) {
        isValid = false;
        messageList.push(messages.newFdmConstrain);
    } else if (fdmSettingList.filter(item => item.MinFdmDeposit > item.MaxFdmDeposit).length > 0) {
        isValid = false;
        messageList.push(messages.fdmDepositConstrain);
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