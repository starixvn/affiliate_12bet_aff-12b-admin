import {
    messageList,
    regexEmail,
    countriesList,
    requiredFieldList,
    requiredBankTranferList,
    playerMethod,
    moneybookersMethod,
    wireMethod
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
    let message = '';
    const checkEmail = new RegExp(regexEmail);
    const isValidGender = userAccountInfo.Gender != null;
    const isValidEmail = !userAccountInfo.HasHostAffPersonalInfoPermission || checkEmail.test(userAccountInfo.Email);
    const country = countriesList[userAccountInfo.Country] === undefined ? "" : userAccountInfo.Country;
    const selectedField = userAccountInfo.HasWebSite ? 'SiteUrl' : `NoSiteDescription${licenseeName}`;

    const newRequiredFieldList = [...requiredFieldList, selectedField];
    message = isValidGender ? message : message + messageList.Gender + "<br />";
    message = isValidEmail ? message : message + messageList.Email + "<br />";
    message += getMessage('Country', country);

    const resultValidateContactApps = validateAppContactList(userAccountInfo.ContactAppList);
    message += resultValidateContactApps.isValid ? '' : resultValidateContactApps.message + '<br />';

    message += newRequiredFieldList.reduce((accumulator, field) => { return accumulator + getMessage(field, userAccountInfo[field]) }, '');

    let isValid = isValidGender && isValidEmail && userAccountInfo.FirstName !== '' && userAccountInfo.LastName !== '' &&
        userAccountInfo.Phone !== '' && userAccountInfo.Address !== '' && userAccountInfo.City !== '' &&
        userAccountInfo.Postcode !== '' && userAccountInfo.Country !== '' && userAccountInfo[selectedField] !== '';

    const resultValidateMoreSiteUrls = (userAccountInfo.HasWebSite && moreSiteUrlsList) ? validateMoreSiteUrls(moreSiteUrlsList) : ({ message: '', isValid: true });
    const resultValidatePaymentMethod = validatePaymentMethod(userAccountInfo);

    return {
        message: message + resultValidateMoreSiteUrls.message + resultValidatePaymentMethod.message,
        isValid: isValid && resultValidateMoreSiteUrls.isValid && resultValidatePaymentMethod.isValid && resultValidateContactApps.isValid
    };
}

const getAppMessage = (appName, suffixMsg) => {
    const name = appName == "Skype" ? "Teams" : appName;

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

    if ((contactApp.Name === 'Telegram' || 
         contactApp.Name === 'Line' || 
         contactApp.Name === 'WhatsApp') && 
        !new RegExp(getRegexByApp(contactApp.Name)).test(contactApp.Value)) {
        message = getAppMessage(contactApp.Name, "is invalid<br />");
        isValid = false;
    }

    return {
        message,
        isValid
    };
}

export const validateAppContactList = (contactAppList) => {
    let message = '';
    let isValid = true;

    if (!contactAppList || contactAppList.length === 0) {
        message = 'At least one IM account is required<br />';
        isValid = false;
    }

    const firstItem = contactAppList[0];
    if (!firstItem.Value) {
        message = getAppMessage(firstItem.Name, "is required<br />");
        isValid = false;
    }

    const firstItemValidation = validateContactValue(firstItem);
    if (!firstItemValidation.isValid) {
        message += firstItemValidation.message;
        isValid = false;
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