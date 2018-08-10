import axios from 'axios';
import { resolveClientUrl } from '../../../modules/utils';

import { SET_GRIDDATA, SET_PAGING_OPTION, SET_SELECTED_GROUP } from './types';

export const fetchGroupSettingData = (param) => {
    const url = resolveClientUrl('Portal/CommissionGroup/GetCommissionSettingList');

    return axios.post(url, param);
}

const setGridData = (data) => {
    return {
        type: SET_GRIDDATA,
        data
    }
}

export const getGroupSettingList = (param) => async(dispatch) => {
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

export const setPagingOption = (data) => {
    return {
        type: SET_PAGING_OPTION,
        data
    }
}

export const insertCommissionGroup = (param) => {
    const url = resolveClientUrl('Portal/CommissionGroup/InsertCommissionGroup');

    return () => axios.post(url, param);
}

export const setSelectedGroup = (data) => {
    return {
        type: SET_SELECTED_GROUP,
        data
    }
}