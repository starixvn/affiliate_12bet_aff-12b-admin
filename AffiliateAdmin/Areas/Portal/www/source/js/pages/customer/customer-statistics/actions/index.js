import { SET_MONTHDATE } from './types';

export const setMonthDate = (data) => {
    return {
        type: SET_MONTHDATE,
        data
    }
}