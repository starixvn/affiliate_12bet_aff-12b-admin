import { post } from '../../../modules/ajaxRequest';

import { SET_GRIDDATA, SET_PAGING_OPTION, SET_SELECTED_GROUP } from './types';

const fetchGroupSettingData = (param) => {
    const url = 'Portal/CommissionGroup/GetCommissionGroupList';

    return post(url, param);
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

export const setSelectedGroup = (data) => {
    return {
        type: SET_SELECTED_GROUP,
        data
    }
}

export const insertCommissionGroup = (param) => {
    const url = 'Portal/CommissionGroup/CreateCommissionGroup';

    return () => post(url, param);
}

export const updateCommissionGroup = (param) => {
    const url = 'Portal/CommissionGroup/UpdateCommissionGroup';

    return () => post(url, param);
}

export const getSelectedCommGroup = (param) => async(dispatch) => {
    const url = 'Portal/CommissionGroup/GetCommissionGroup';

    const response = await post(url, param);

    dispatch(setSelectedGroup(response.data))
}

export const deleteCommGroup = (param) => {
    const url = 'Portal/CommissionGroup/DeleteCommissionGroup';

    return () => post(url, param);
}