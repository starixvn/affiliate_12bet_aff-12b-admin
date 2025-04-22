import { targetCountryStatus } from '../../../../modules/constants';

export const getBlockedTargetCountryList = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Blocked).map(m => m.TargetCountryCode);
}

export const checkBlockedAllTargetCountries = (targetCountryList, blockedTargetCountryList) => {
    return blockedTargetCountryList.length !== 0 && blockedTargetCountryList.length === targetCountryList.length;
}