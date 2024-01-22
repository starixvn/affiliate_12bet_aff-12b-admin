import { targetCountryStatus } from '../../../../modules/constants';

export const getExcludedTargetCountryList = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Excluded).map(m => m.TargetCountryCode);
}

export const getUsingTargetCountryList = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Using).map(m => m.TargetCountryCode);
}

export const checkExcludedAllTargetCountries = (targetCountryList, excludedTargetCountryList, usingTargetCountryList = []) => {
    return excludedTargetCountryList.length !== 0 && (excludedTargetCountryList.length + usingTargetCountryList.length) === targetCountryList.length;
}