import { targetCountryStatus } from '../../../../modules/constants';

export const getExcludedTargetCountries = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Excluded).map(m => m.TargetCountryCode);
}

export const getUsingTargetCountries = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Using).map(m => m.TargetCountryCode);
}

export const checkExcludedAllTargetCountries = (targetCountryList, usingTargetCountries, excludedTargetCountries) => {
    return excludedTargetCountries.length !== 0 && (usingTargetCountries.length + excludedTargetCountries.length) === targetCountryList.length;
}