import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_GRID_DATA, SET_IS_GENERAL, SET_ROLE_LIST, SET_MARKET_ID_BY_USER_LIST, SET_MARKET_BY_USER_LIST, SET_MESSAGE_POPUP } from './types';

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

export const setMarketIdByUserList = (data) => {
    return {
        type: SET_MARKET_ID_BY_USER_LIST,
        data
    }
}

export const setMarketByUserList = (data) => {
    return {
        type: SET_MARKET_BY_USER_LIST,
        data
    }
}

export const setMessagePopup = (data) => {
    return {
        type: SET_MESSAGE_POPUP,
        data
    }
}

export const getPermissionInfoList = () => async (dispatch) => {
    const url = `Portal/GeneralPermissionRole/GetPermissionInfoList`;
    const response = await post(url);

    return response && response.status === 200 ? response.data : null;
}

export const getRoleList = () => async (dispatch) => {
    const url = `Portal/GeneralPermissionRole/GetRoleList`;
    const response = await post(url);

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setRoleList(data));
}

export const getMarketByRoleList = () => async (dispatch) => {
    const url = `Portal/GeneralPermissionRole/GetMarketByRoleList`;
    const response = await post(url);
    
    const data = response && response.status === 200 ? response.data : null;

    return data;
}

export const getMarketByUserList = () => async (dispatch) => {
    const url = `Portal/GeneralPermissionRole/GetMarketByUserList`;
    const response = await post(url);
    
    const data = response && response.status === 200 ? response.data : null;
    const makertIdList = data ? _getMarketIdList(data) : {};

    dispatch(setMarketByUserList(data));
    dispatch(setMarketIdByUserList(makertIdList));
}

export const updatePermissionInfo = (permissionInfo) => async () => {
    const url = 'Portal/GeneralPermissionRole/UpdatePermissionInfo';
    const response = await post(url, { permissionInfo });

    return response && response.status === 200 ? response.data : null;
}

export const updateMarketInfoList = (marketInfoList) => async () => {
    const url = 'Portal/GeneralPermissionRole/UpdateMarketInfoList';
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