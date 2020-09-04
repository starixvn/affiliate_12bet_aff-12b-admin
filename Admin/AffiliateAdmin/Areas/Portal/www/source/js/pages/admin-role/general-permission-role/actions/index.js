import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_GRID_DATA, SET_IS_GENERAL, SET_ROLE_LIST, SET_MARKET_ID_LIST } from './types';

export { toggleLoader };

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const setIsGeneral = (data) => {
    return {
        type: SET_IS_GENERAL,
        data
    }
}

export const setRoleList = (data) => {
    return {
        type: SET_ROLE_LIST,
        data
    }
}

export const setMarketIdList = (data) => {
    return {
        type: SET_MARKET_ID_LIST,
        data
    }
}

export const fetchData = () => async (dispatch) => {
    dispatch(toggleLoader(true));

    const url = `Portal/GeneralPermissionRole/GetPermissionInfoList`;
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setGridData(data));
    dispatch(toggleLoader(false));
}

export const getRoleList = () => async (dispatch) => {
    const url = `Portal/GeneralPermissionRole/GetRoleList`;
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setRoleList(data));
}

export const getMarketList = () => async (dispatch) => {
    const url = `Portal/GeneralPermissionRole/GetMarketList`;
    const response = await post(url);
    
    const data = response && response.status === 200 ? response.data : null;
    const makertIdList = data ? _getMarketIdList(data) : {};

    dispatch(setRoleList(makertIdList));

    return data;
}

const _getMarketIdList = (marketList) => {
    const makertIds = {};
    for (let i = 0; i < marketList.length; i++) {
        const makertItem = marketList[i];
        makertIds[makertItem.MakertId] = makertItem.MakertName;
    }

    return makertIds
}

export const fakeFunctionList = [
    { RoleId: 1, RoleName: 'Management', ParentId: 1, FuncId: 1, FuncName: 'Home', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 2, FuncId: 2, FuncName: 'Customer', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 2, FuncId: 3, FuncName: 'Statement History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 2, FuncId: 4, FuncName: 'Adjusment', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 5, FuncName: 'Affiliate', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 6, FuncName: 'Affiliate Info', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 7, FuncName: 'Payment Request', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 8, FuncName: 'Email History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 5, FuncId: 9, FuncName: 'Revenue Group', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 10, FuncName: 'Admin Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 11, FuncName: 'Admin List', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 12, FuncName: 'General Permission Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 12, FuncId: 13, FuncName: 'General', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 12, FuncId: 14, FuncName: 'makert', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 1, RoleName: 'Management', ParentId: 10, FuncId: 15, FuncName: 'Admin Info', Policy: 3, IsRead: true, IsHost: true },

    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 1, FuncId: 1, FuncName: 'Home', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 2, FuncId: 2, FuncName: 'Customer', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 2, FuncId: 3, FuncName: 'Statement History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 2, FuncId: 4, FuncName: 'Adjusment', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 5, FuncName: 'Affiliate', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 6, FuncName: 'Affiliate Info', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 7, FuncName: 'Payment Request', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 8, FuncName: 'Email History', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 5, FuncId: 9, FuncName: 'Revenue Group', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 10, FuncName: 'Admin Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 11, FuncName: 'Admin List', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 12, FuncName: 'General Permission Role', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 12, FuncId: 13, FuncName: 'General', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 12, FuncId: 14, FuncName: 'makert', Policy: 3, IsRead: true, IsHost: true },
    { RoleId: 2, RoleName: 'Exe. Settlement', ParentId: 10, FuncId: 15, FuncName: 'Admin Info', Policy: 3, IsRead: true, IsHost: true },
]