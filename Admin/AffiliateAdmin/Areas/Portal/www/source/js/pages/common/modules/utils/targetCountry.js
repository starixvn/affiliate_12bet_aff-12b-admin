import { targetCountryStatus } from '../../../../modules/constants';

export const getExcludedTargetCountryList = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Excluded).map(m => m.TargetCountryCode);
}

export const checkExcludedAllTargetCountries = (targetCountryList, excludedTargetCountryList) => {
    return excludedTargetCountryList.length !== 0 && excludedTargetCountryList.length === targetCountryList.length;
}