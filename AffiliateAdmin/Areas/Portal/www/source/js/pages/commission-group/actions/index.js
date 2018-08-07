import axios from 'axios';
import { resolveClientUrl } from '../../../modules/utils';

import { SET_GRIDDATA, SET_SELECTEDGROUP, SET_PAGING_OPTION } from './types';

const fetchGroupSettingData = (param) => {
    const url = resolveClientUrl('Portal/CommissionGroup/GetCommissionSettingList');

    return axios.post(url, param);
}

const setGridData = (data) => {
    return {
        type: SET_GRIDDATA,
        data: data
    }
}

export const getGroupSettingList = (param = {}) => async(dispatch) => {
    const response = await fetchGroupSettingData(param);
    const { CommissionSettingList, TotalRows } = response.data;

    dispatch(setGridData(CommissionSettingList));

    const { PageIndex, PageSize } = param;
    const pagingOption = {
        PageIndex,
        PageSize,
        TotalItem: TotalRows
    };
    dispatch(setPagingOption(pagingOption));
}

export const setSelectedGroup = (data) => {
    return {
        type: SET_SELECTEDGROUP,
        data: data
    }
}

export const setPagingOption = (data) => {
    return {
        type: SET_PAGING_OPTION,
        data: data
    }
}