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