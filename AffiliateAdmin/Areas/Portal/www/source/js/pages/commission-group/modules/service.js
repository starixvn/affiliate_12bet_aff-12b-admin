import { deductionNameList } from '../modules/constant';

const emptyCommGroup = {
    GroupId: 0,
    GroupName: '',
    Description: '',
    QmTurnover: 0,
    TierSettingList: [],
    PlatformFeeList: [],
    OperationalFee: {},
    BankingFee: {}
}

export const buildDefaultCommGroup = (deductionList, productList) => {
    const deductionSetting = getDeductionSetting(deductionList);
    const remainingDeduction = convertRemainingDeduction([], deductionSetting);

    const platformFeeParam = { productList, deductionSetting, platformFeeList: [] };
    const platformFeeList = buildPlatformFeeList(platformFeeParam);

    return Object.assign({}, emptyCommGroup, remainingDeduction, { PlatformFeeList: platformFeeList });
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

export const convertRemainingDeduction = (RemainingFeeList, deductionSetting) => {
    const { operationalFeeSetting, bankingFeeSetting } = deductionSetting;

    const operationalFeeItem = RemainingFeeList.filter(item => item.DeductionId === operationalFeeSetting.DeductionId);
    const OperationalFee = operationalFeeItem.length ? operationalFeeItem[0] : {
        DeductionId: operationalFeeSetting.DeductionId,
        ProductIds: 0,
        DeductedPercentage: operationalFeeSetting.DeductedPercentage
    };

    const bankingFeeItem = RemainingFeeList.filter(item => item.DeductionId === bankingFeeSetting.DeductionId);
    const BankingFee = bankingFeeItem.length ? bankingFeeItem[0] : {
        DeductionId: bankingFeeSetting.DeductionId,
        ProductIds: 0,
        DeductedPercentage: bankingFeeSetting.DeductedPercentage
    };

    return {
        OperationalFee,
        BankingFee
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