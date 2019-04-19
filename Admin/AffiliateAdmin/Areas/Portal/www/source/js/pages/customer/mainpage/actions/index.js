import { SET_AFF_COMMON_INFO } from './types';

export const setAffCommonInfo = (data) => {
    return {
        type: SET_AFF_COMMON_INFO,
        data
    }
}