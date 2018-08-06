import axios from 'axios';
import { resolveClientUrl } from '../../../modules/utils';

import { SET_GRIDDATA, SET_SELECTEDGROUP } from './types';

export const fetchGroupSettingData = () => {
    const url = resolveClientUrl('Portal/CommissionGroup/GetCommissionSettingList');

    return () => axios.post(url);
}

export const setGridData = (data) => {
    return {
        type: SET_GRIDDATA,
        data: data
    }
}

export const setSelectedGroup = (data) => {
    return {
        type: SET_SELECTEDGROUP,
        data: data
    }
}