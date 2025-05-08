import { targetCountryStatus } from '../../../../modules/constants';

export const getBlockTargetCountryList = (targetCountryList) => {
    return targetCountryList.filter(m => m.Status === targetCountryStatus.Blocked).map(m => m.TargetCountryCode);
}

export const checkBlockAllTargetCountries = (targetCountryList, blockTargetCountryList) => {
    return blockTargetCountryList.length !== 0 && blockTargetCountryList.length === targetCountryList.length;
}