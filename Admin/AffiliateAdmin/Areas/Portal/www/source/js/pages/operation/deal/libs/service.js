import { INFINITY_MONEY, INFINITY_NUMBER, messages, mappingFields, cpaTierSettingDefault, productGroupDefault, DEAL_CPA, DEAL_REVENUE_SHARE, DEAL_HYBRID } from './constants';
import { validateRevenueShareInfo } from './revenue-share-service';

const commonDealInfo = {
    GroupId: 0,
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
    IsInstant: false,
    Quota: 0,
    CheckedMinFdm: false,
    CheckedMinTotalDeposit: false,
    CheckedMinTurnover: false,
    CPATierSetting: [
        {
            ...cpaTierSettingDefault
        }
    ],
    ProductGroup: [
        {
            ...productGroupDefault
        },
    ]
}

export const buildDefaultDealInfo = () => {
    return Object.assign({}, commonDealInfo);
}

const convertListWithChecked = (list, selectedIdsCSV, idKey, nameKey, checkedItems = []) => {
    const selectedIds = selectedIdsCSV.split(',').filter(Boolean);
    const isCheckAll = selectedIds.length === list.length;

    const disabledSet = new Set(checkedItems.filter(id => !selectedIds.includes(id)));

    return list.map(item => {
        const itemId = item[idKey].toString();
        const isDisabled = disabledSet.has(itemId);
        const isChecked = !isDisabled && (isCheckAll || selectedIds.includes(itemId));

        return {
            [idKey]: item[idKey],
            [nameKey]: item[nameKey],
            Checked: isChecked,
            IsDisabledItem: isDisabled
        };
    });
};


export const convertProductList = (productList, productIds = '', checkedProducts = []) => {
    return convertListWithChecked(productList, productIds, 'ProductId', 'ProductName', checkedProducts);
}

export const convertTargetCountryList = (targetCountryList, targetCountryCodes = '') => {
    return convertListWithChecked(targetCountryList, targetCountryCodes, 'TargetCountryCode', 'TargetCountryName');
}

export const validateDealInfo = (dealInfo) => {
    const { DealType, DealName } = dealInfo;
    let validateResult = { isValid: false, messageList: [] };

    const groupNameValidation = validateGroupName(DealName);

    if (DealType === DEAL_CPA) {
        validateResult = validateCPAInfo(dealInfo);
    } else if (DealType === DEAL_REVENUE_SHARE) {
        validateResult = validateRevenueShareInfo(dealInfo);
    } else {
        validateResult = validateHybridInfo(dealInfo);
    }

    const isValid = groupNameValidation.isValid && validateResult.isValid;
    const messageList = [...groupNameValidation.messageList, ...validateResult.messageList];

    return { isValid, messageList };
}

export const validateCPAInfo = (dealInfo) => {
    const { CPATierSetting, CheckedMinFdm, CheckedMinTotalDeposit, CheckedMinTurnover, ProductGroup } = dealInfo;

    const generalDealInfoValidation = validateGeneralDealInfo(dealInfo);
    const checkBoxValidation = validateCheckBox(CheckedMinFdm, CheckedMinTotalDeposit, CheckedMinTurnover);
    const productGroupValidation = validateProductGroup(ProductGroup, CheckedMinTurnover);
    const tierSettingValidation = validateTierSetting(CPATierSetting, CheckedMinFdm, CheckedMinTotalDeposit, CheckedMinTurnover);

    const isValid = generalDealInfoValidation.isValid && checkBoxValidation.isValid && productGroupValidation.isValid && tierSettingValidation.isValid;
    const messageList = [...generalDealInfoValidation.messageList, ...checkBoxValidation.messageList, ...productGroupValidation.messageList, ...tierSettingValidation.messageList];

    return { isValid, messageList };
}

export const validateHybridInfo = (dealInfo) => {
    const validateCPAInfoResult = validateCPAInfo(dealInfo);
    const validateRevenueShareInfoResult = validateRevenueShareInfo(dealInfo);

    const isValid = validateCPAInfoResult.isValid && validateRevenueShareInfoResult.isValid;
    const messageList = [...validateCPAInfoResult.messageList, ...validateRevenueShareInfoResult.messageList];

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

export const validateGeneralDealInfo = (dealInfo) => {
    const {
        CountryCodes,
        ActiveDays,
        IsActivePlayerHoldingPeriod,
        HoldingTickets,
        HoldingDays,
        IsQuota,
        Quota
    } = dealInfo;

    const messageList = [];

    const addIfInvalid = (condition, message) => {
        if (condition) {
            messageList.push(message);
        }
    };

    addIfInvalid(!CountryCodes, messages.requireCountryCodes);

    addIfInvalid(!ActiveDays || ActiveDays < 1 || ActiveDays > INFINITY_NUMBER, messages.requireActiveDays);

    if (IsActivePlayerHoldingPeriod) {
        addIfInvalid((!HoldingTickets || HoldingTickets < 1 || HoldingTickets > INFINITY_NUMBER)
            || (!HoldingDays || HoldingDays < 1 || HoldingDays > INFINITY_NUMBER),
            messages.requireHoldingTicketsAndHoldingDays);
    }

    if (IsQuota) {
        addIfInvalid(!Quota || Quota < 1 || Quota > INFINITY_NUMBER, messages.requireQuota);
    }

    const isValid = messageList.length === 0;

    return { isValid, messageList };
};

export const validateCheckBox = (checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover) => {
    let isValid = true;
    const messageList = [];

    if (!checkedMinFdm && !checkedMinTotalDeposit && !checkedMinTurnover) {
        isValid = false;
        messageList.push(messages.requireCheckBox);
    }

    return { isValid, messageList };
}

export const validateProductGroup = (productGroup, checkedMinTurnover) => {
    let isValid = true;
    const messageList = [];

    if (!checkedMinTurnover) {
        return { isValid, messageList };
    }

    if (ensureProductGroupAtLeastOne(productGroup)) {
        isValid = false;
        messageList.push(messages.requireProductGroup);

        return { isValid, messageList };
    }

    const productGroupHasSelectedProductValidation = ensureProductGroupHasSelectedProduct(productGroup);
    if (!ensureProductGroupHasSelectedProduct(productGroup).isValid) {
        isValid = false;
        messageList.push(...productGroupHasSelectedProductValidation.messageList);
    }

    const duplicateProductsInGroupsValidation = checkDuplicateProductsInGroups(productGroup);
    if (!duplicateProductsInGroupsValidation.isValid) {
        isValid = false;
        messageList.push(...duplicateProductsInGroupsValidation.messageList);
    }

    return { isValid, messageList };
}

const ensureProductGroupAtLeastOne = (productGroup) => {
    return !productGroup || productGroup.length === 0;
}

const ensureProductGroupHasSelectedProduct = (productGroup) => {
    let isValid = true;
    const messageList = [];

    productGroup.forEach((item, index) => {
        if (!item.ProductIds) {
            isValid = false;
            messageList.push(messages.requireProductGroupHasSelectedProduct.replace('{0}', index + 1));
        }
    });

    return { isValid, messageList };
}

const checkDuplicateProductsInGroups = (productGroups) => {
    const seen = new Set();
    const messageList = [];
    let isValid = true;

    productGroups.forEach((group, index) => {
        const productIds = group.ProductIds.split(',').filter(p => !!p);

        for (const id of productIds) {
            if (seen.has(id)) {
                isValid = false;
                messageList.push(`Product Group ${index + 1} - ${messages.requireDuplicateProductsInGroups}`);
                break;
            }
            seen.add(id);
        }
    });

    return { isValid, messageList };
};

export const validateTierSetting = (tierSetting, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover) => {
    let isValid = true;
    const messageList = [];

    // Validate tier setting at least one
    if (ensureTierSettingAtLeastOne(tierSetting)) {
        isValid = false;
        messageList.push(messages.requireTierSetting);

        return { isValid, messageList };
    }

    const groupedTierSetting = groupTierSettingByGroupProductId(tierSetting);

    // Validate increasing by field
    Object.keys(groupedTierSetting).forEach((groupProductId, index) => {
        const groupTiers = groupedTierSetting[groupProductId];
        const increasingTiersValidation = validateIncreasingTiers(index, groupTiers, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);

        if (!increasingTiersValidation.isValid) {
            isValid = false;
            messageList.push(...increasingTiersValidation.messageList);
        }
    });

    // Validate that all tier settings of all product groups must match the tier settings of product group 1
    const allTierSettingsMatchGroupProduct1Validation = validateAllTierSettingsMatchGroupProduct1(groupedTierSetting, checkedMinFdm, checkedMinTotalDeposit);
    if (!allTierSettingsMatchGroupProduct1Validation.isValid) {
        isValid = false;
        messageList.push(...allTierSettingsMatchGroupProduct1Validation.messageList);
    }

    // Validate value field of tier setting
    const valueFieldValidation = validateValueFieldOfTierSetting(groupedTierSetting, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);
    if (!valueFieldValidation.isValid) {
        isValid = false;
        messageList.push(...valueFieldValidation.messageList);
    }

    return { isValid, messageList };
}

const ensureTierSettingAtLeastOne = (tierSetting) => {
    return !tierSetting || tierSetting.length === 0;
}

const validateIncreasingTiers = (index, groupTiers, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover) => {
    let isValid = true;
    const messageList = [];
    const groupProduct = `Product Group ${index + 1}`;
    const fields = [];

    const validations = [
        { fn: minFdmValidationIncreasing, isChecked: checkedMinFdm && index === 0 },
        { fn: minTotalDepositValidationIncreasing, isChecked: checkedMinTotalDeposit && index === 0 },
        { fn: minTurnoverValidationIncreasing, isChecked: checkedMinTurnover },
        { fn: cpaAmountValidationIncreasing, isChecked: true }
    ];

    for (const { fn, isChecked } of validations) {
        const result = fn(groupTiers, isChecked);
        if (!result.isValid) {
            isValid = false;
            fields.push(mappingFields[result.field]);
        }
    }

    if (!isValid) {
        messageList.push(`${groupProduct} - ${messages.requireIncreasing.replace('{0}', fields.join('/'))}`);
    }

    return { isValid, messageList };
}

// Start validate increasing by field
const validateIncreasingByField = (tiers, field, isChecked) => {

    if (isChecked) {
        for (let i = 1; i < tiers.length; i++) {
            if (tiers[i][field] <= tiers[i - 1][field]) {
                return { isValid: false, field };
            }
        }
    }

    return { isValid: true, field };
};

const minFdmValidationIncreasing = (tiers, isChecked) => {
    return validateIncreasingByField(tiers, 'MinFdm', isChecked);
}

const minTotalDepositValidationIncreasing = (tiers, isChecked) => {
    return validateIncreasingByField(tiers, 'MinTotalDeposit', isChecked);
}

const minTurnoverValidationIncreasing = (tiers, isChecked) => {
    return validateIncreasingByField(tiers, 'MinTO', isChecked);
}

const cpaAmountValidationIncreasing = (tiers, isChecked) => {
    return validateIncreasingByField(tiers, 'CpaAmount', isChecked);
}
// End validate increasing by field

// Start validate all tier settings match group product 1
const validateAllTierSettingsMatchGroupProduct1 = (groupedTierSetting, checkedMinFdm, checkedMinTotalDeposit) => {
    let isValid = true;
    const messageList = [];

    const firstGroupProductId = Object.keys(groupedTierSetting)[0];
    const firstGroupTiers = groupedTierSetting[firstGroupProductId];

    let index = 1;
    Object.keys(groupedTierSetting).forEach(groupProductId => {
        if (groupProductId !== firstGroupProductId) {
            const groupTiers = groupedTierSetting[groupProductId];
            if (!isGroup2MatchGroup1(groupTiers, firstGroupTiers, checkedMinFdm, checkedMinTotalDeposit)) {
                isValid = false;
                messageList.push(messages.requireAllTierSettingsMatchGroupProduct1.replace('{0}', index));
            }
        }
        index++;
    });

    return { isValid, messageList };
}

const isGroup2MatchGroup1 = (group2, group1, checkedMinFdm, checkedMinTotalDeposit) => {
    return group2.every(item1 =>
        group1.some(item2 =>
            (!checkedMinFdm || item1.MinFdm === item2.MinFdm)
            && (!checkedMinTotalDeposit || item1.MinTotalDeposit === item2.MinTotalDeposit)));
};
// End validate all tier settings match group product 1

// Start validate value field of tier setting
const validateValueFieldOfTierSetting = (groupedTierSetting, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover) => {
    let isValid = true;
    const messageList = [];

    Object.keys(groupedTierSetting).forEach((groupProductId, index) => {
        const groupTiers = groupedTierSetting[groupProductId];

        const minValidation = validateValueField('min', index, groupTiers, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);
        const maxValidation = validateValueField('max', index, groupTiers, checkedMinFdm, checkedMinTotalDeposit, checkedMinTurnover);

        if (!minValidation.isValid) {
            isValid = false;
            messageList.push(...minValidation.messageList);
        }

        if (!maxValidation.isValid) {
            isValid = false;
            messageList.push(...maxValidation.messageList);
        }
    });

    return { isValid, messageList };
};

const validateValueField = (
    type, // 'min' | 'max'
    index,
    groupTiers,
    checkedMinFdm,
    checkedMinTotalDeposit,
    checkedMinTurnover
) => {
    let isValid = true;
    const messageList = [];
    const fields = new Set();

    const validateField = (condition, fieldKey) => {
        if (condition && !fields.has(mappingFields[fieldKey])) {
            isValid = false;
            fields.add(mappingFields[fieldKey]);
        }
    };

    groupTiers.forEach((tier) => {
        const { MinFdm, MinTotalDeposit, MinTO, CpaAmount } = tier;

        const isMinCheck = type === 'min';

        const fdmInvalid = isMinCheck ? !MinFdm || MinFdm < 1 : MinFdm >= INFINITY_MONEY;
        const depositInvalid = isMinCheck ? !MinTotalDeposit || MinTotalDeposit < 1 : MinTotalDeposit >= INFINITY_MONEY;
        const turnoverInvalid = isMinCheck ? !MinTO || MinTO < 1 : MinTO >= INFINITY_MONEY;
        const cpaInvalid = isMinCheck ? !CpaAmount || CpaAmount < 1 : CpaAmount >= INFINITY_MONEY;

        if (index === 0) {
            validateField(checkedMinFdm && fdmInvalid, 'MinFdm');
            validateField(checkedMinTotalDeposit && depositInvalid, 'MinTotalDeposit');
        }

        validateField(checkedMinTurnover && turnoverInvalid, 'MinTO');
        validateField(cpaInvalid, 'CpaAmount');
    });

    if (!isValid) {
        const fieldNames = [...fields].join('/');
        const messageTemplate = type === 'min' ? messages.requireMinValue : messages.requireMaxValue;
        messageList.push(`Product Group ${index + 1} - ${messageTemplate.replace('{0}', fieldNames)}`);
    }

    return { isValid, messageList };
};

const groupTierSettingByGroupProductId = (tierSetting) => {
    const groupedTierSetting = {};

    tierSetting.forEach(item => {
        if (!groupedTierSetting[item.GroupProductId]) {
            groupedTierSetting[item.GroupProductId] = [];
        }
        groupedTierSetting[item.GroupProductId].push(item);
    });

    return groupedTierSetting;
}