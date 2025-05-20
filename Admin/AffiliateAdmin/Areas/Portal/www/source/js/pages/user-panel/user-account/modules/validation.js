import {
    messageList,
    regexEmail,
    countriesList,
    requiredFieldList,
    requiredBankTranferList,
    playerMethod,
    moneybookersMethod,
    wireMethod,
    contactAppTypes
} from './constants'
import { getRegexByApp } from './utils'

const getMessage = (fieldName, value) => {
    return value === '' ? messageList[fieldName] + "<br />" : '';
}

const validateMoreSiteUrls = (moreSiteUrlsList) => {
    const moreSiteUrls = moreSiteUrlsList.map(siteUrl => siteUrl.trim());
    const emptySiteUrlIndex = moreSiteUrls.indexOf('');
    const hasEmptySiteUrl = emptySiteUrlIndex !== -1;
    const message = hasEmptySiteUrl ? `Website URL ${emptySiteUrlIndex + 2} is required<br />` : '';

    return {
        message,
        isValid: !hasEmptySiteUrl
    }
}

const validatePaymentMethod = (userAccountInfo) => {
    let message = '';
    let isValid = true;

    switch (userAccountInfo.PreferredPayment) {
        case playerMethod:
            message = getMessage('PlayerUsername', userAccountInfo.PlayerUsername);
            isValid = userAccountInfo.PlayerUsername !== '';
            break;
        case moneybookersMethod:
            message = getMessage('MoneybookersEmail', userAccountInfo.MoneybookersEmail);
            isValid = userAccountInfo.MoneybookersEmail !== '';
            break;
        case wireMethod:
            message = requiredBankTranferList.reduce((accumulator, field) => accumulator + getMessage(field, userAccountInfo[field]), '');
            isValid = userAccountInfo.BankName !== '' && userAccountInfo.BankAddress !== '' &&
                userAccountInfo.BankAccountName !== '' && userAccountInfo.BankAccountNumber !== '';
            break;
    }

    return {
        message,
        isValid
    };
}

export const validateUserAccount = (userAccountInfo, moreSiteUrlsList, licenseeName) => {
    const { MarketingAccount } = userAccountInfo;
    let message = '';
    const checkEmail = new RegExp(regexEmail);
    const isValidGender = MarketingAccount || userAccountInfo.Gender != null;
    const isValidEmail = !userAccountInfo.HasHostAffPersonalInfoPermission || (MarketingAccount && !userAccountInfo.Email ? true : checkEmail.test(userAccountInfo.Email));
    const country = countriesList[userAccountInfo.Country] === undefined ? "" : userAccountInfo.Country;
    const selectedField = userAccountInfo.HasWebSite ? 'SiteUrl' : `NoSiteDescription${licenseeName}`;

    const newRequiredFieldList = [...requiredFieldList, selectedField];
    message = isValidGender ? message : message + messageList.Gender + "<br />";
    message = isValidEmail ? message : message + messageList.Email + "<br />";
    message += MarketingAccount ? '' : getMessage('Country', country);

    const resultValidateContactApps = validateAppContactList(userAccountInfo.ContactAppList, MarketingAccount);
    message += resultValidateContactApps.isValid ? '' : resultValidateContactApps.message;

    message += MarketingAccount ? '' : newRequiredFieldList.reduce((accumulator, field) => { return accumulator + getMessage(field, userAccountInfo[field]) }, '');

    let isValid = MarketingAccount || (isValidGender && isValidEmail && userAccountInfo.FirstName !== '' && userAccountInfo.LastName !== '' &&
        userAccountInfo.Phone !== '' && userAccountInfo.Address !== '' && userAccountInfo.City !== '' &&
        userAccountInfo.Postcode !== '' && userAccountInfo.Country !== '' && userAccountInfo[selectedField] !== '');

    const resultValidateMoreSiteUrls = (!MarketingAccount && userAccountInfo.HasWebSite && moreSiteUrlsList) ? validateMoreSiteUrls(moreSiteUrlsList) : ({ message: '', isValid: true });
    const resultValidatePaymentMethod = MarketingAccount ? validatePaymentMethod(userAccountInfo) : ({ message: '', isValid: true });

    return {
        message: message + resultValidateMoreSiteUrls.message + resultValidatePaymentMethod.message,
        isValid: isValid && resultValidateMoreSiteUrls.isValid && resultValidatePaymentMethod.isValid && resultValidateContactApps.isValid
    };
}

const getAppMessage = (appName, suffixMsg) => {
    const name = appName == contactAppTypes.Skype ? contactAppTypes.Teams : appName;

    return `${name} ${suffixMsg}`;
}

const validateContactValue = (contactApp) => {
    const MAX_LENGTH = 50;
    let message = '';
    let isValid = true;

    if (contactApp.Value.length > MAX_LENGTH) {
        message = getAppMessage(contactApp.Name, "no more than 50 characters<br />");
        isValid = false;
    }

    if ((contactApp.Name === contactAppTypes.Telegram ||
        contactApp.Name === contactAppTypes.Line ||
        contactApp.Name === contactAppTypes.WhatsApp) &&
        !new RegExp(getRegexByApp(contactApp.Name)).test(contactApp.Value)) {
        message = getAppMessage(contactApp.Name, "is invalid<br />");
        isValid = false;
    }

    return {
        message,
        isValid
    };
}

export const validateAppContactList = (contactAppList, MarketingAccount) => {
    let message = '';
    let isValid = true;

    if (!MarketingAccount && (!contactAppList || contactAppList.length === 0)) {
        message = 'At least one IM account is required<br />';
        isValid = false;
    }

    const firstItem = contactAppList[0];
    if (!MarketingAccount && !firstItem.Value) {
        message = getAppMessage(firstItem.Name, "is required<br />");
        isValid = false;
    } else {
        const firstItemValidation = validateContactValue(firstItem);
        if (!firstItemValidation.isValid) {
            message += firstItemValidation.message;
            isValid = false;
        }
    }

    // Validate remaining contacts
    for (let i = 1; i < contactAppList.length; i++) {
        const contactApp = contactAppList[i];
        if (contactApp.Value) {
            const validationResult = validateContactValue(contactApp);
            if (!validationResult.isValid) {
                message += validationResult.message;
                isValid = false;
            }
        }
    }

    return {
        message,
        isValid
    };
}