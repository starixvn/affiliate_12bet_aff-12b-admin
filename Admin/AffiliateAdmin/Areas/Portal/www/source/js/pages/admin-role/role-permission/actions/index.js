import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_GRID_DATA, SET_IS_GENERAL, SET_ROLE_LIST, SET_MARKET_ID_LIST, SET_MARKET_LIST } from './types';

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

export const setMarketList = (data) => {
    return {
        type: SET_MARKET_LIST,
        data
    }
}

export const getPermissionInfoList = () => async (dispatch) => {
    const url = `Portal/RolePermission/GetPermissionInfoList`;
    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const getRoleList = () => async (dispatch) => {
    const url = `Portal/AdminRoleCommon/GetRoleList`;
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setRoleList(data));
}

export const getMarketListByRole = () => async (dispatch) => {
    const url = `Portal/AdminRoleCommon/GetMarketListByRole`;
    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const getMarketList = () => async (dispatch) => {
    const url = `Portal/AdminRoleCommon/GetMarketListByUser`;
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;
    const makertIdList = data ? _getMarketIdList(data) : {};

    dispatch(setMarketList(data));
    dispatch(setMarketIdList(makertIdList));
}

export const updatePermissionInfo = (param) => async () => {
    const url = 'Portal/RolePermission/UpdatePermissionInfo';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateMarketInfoList = (marketInfoList) => async () => {
    const url = 'Portal/RolePermission/UpdateMarketInfoList';
    const response = await post(url, { marketInfoList });

    return response && response.status === 200 ? response.data : null;
}

const _getMarketIdList = (marketList) => {
    const makertIds = {};
    for (let i = 0; i < marketList.length; i++) {
        const makertItem = marketList[i];
        makertIds[makertItem.MarketId] = makertItem.MarketId;
    }

    return makertIds
}