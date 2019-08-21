import { post } from '../../../modules/ajaxRequest';
import { toggleLoader } from '../../../actions/loaderAction';
import { setPagingOption } from '../../../actions/pagingAction';

import { SET_GRIDDATA, SET_PAGING_OPTION, SET_SELECTED_GROUP, SET_SEARCHED_GROUPNAME } from './types';

export { toggleLoader, setPagingOption };

export const fetchGroupSettingData = (param) => {
    const url = 'Portal/CommissionGroup/GetCommissionGroupList';

    return post(url, param);
}

export const setGridData = (data) => {
    return {
        type: SET_GRIDDATA,
        data
    }
}

export const getGroupSettingList = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));

    if (!param.GroupName) {
        dispatch(setSearchedGroupName(''));
    }

    const response = await fetchGroupSettingData(param);
    if (response && response.status === 200) {
        const data = response.data;

        dispatch(setGridData(data ? data.CommissionSettingList : null));

        const { PageIndex, PageSize } = param;
        const pagingOption = {
            PageIndex,
            PageSize,
            TotalItem: data ? data.TotalRows : 0
        };
        dispatch(setPagingOption(pagingOption));
    }
    dispatch(toggleLoader(false));
}

export const setSelectedGroup = (data) => {
    return {
        type: SET_SELECTED_GROUP,
        data
    }
}

export const setSearchedGroupName = (data) => {
    return {
        type: SET_SEARCHED_GROUPNAME,
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

export const getSelectedCommGroup = (param) => async (dispatch) => {
    const url = 'Portal/CommissionGroup/GetCommissionGroup';

    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setSelectedGroup(data))
}

export const deleteCommGroup = (param) => {
    const url = 'Portal/CommissionGroup/DeleteCommissionGroup';

    return () => post(url, param);
}