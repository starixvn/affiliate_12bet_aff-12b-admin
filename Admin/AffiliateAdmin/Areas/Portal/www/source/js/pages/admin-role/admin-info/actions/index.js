import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { redirectToCreateNew, redirectToAdminList } from '../../common/actions';
import { deleteUsers } from '../../admin-list/actions';
import { SET_ROLE_LIST, SET_PERMISSION_LIST_BY_ROLE, SET_MARKET_LIST_BY_USER } from './types';
import { convertRoleList, convertMarketListByRole } from '../libs/utils';

export { toggleLoader, redirectToCreateNew, redirectToAdminList, deleteUsers };

export const setRoleList = (data) => {
    return {
        type: SET_ROLE_LIST,
        data
    }
}

export const setPermissionListByRole = (data) => {
    return {
        type: SET_PERMISSION_LIST_BY_ROLE,
        data
    }
}

export const setMarketListByUser = (data) => {
    return {
        type: SET_MARKET_LIST_BY_USER,
        data
    }
}

export const fetchData = (userId) => async () => {
    const url = 'Portal/AdminInfo/GetUserInfo';
    const response = await post(url, { userId });

    return response && response.status === 200 ? response.data : null;
}

export const getRoleList = () => async (dispatch) => {
    const url = 'Portal/AdminRoleCommon/GetRoleList';
    const response = await post(url);

    const data = response && response.status === 200 ? convertRoleList(response.data) : null;

    dispatch(setRoleList(data));
}

export const getPermissionListByRole = (roleId) => async (dispatch) => {
    const url = 'Portal/AdminInfo/GetPermissionListByRole';
    const response = await post(url, { roleId });

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setPermissionListByRole(data));
}

export const getMarketListByRole = (roleId, isDefaultByRole) => async () => {
    const url = 'Portal/AdminRoleCommon/GetMarketListByRole';
    const response = await post(url, { roleId });

    return response && response.status === 200 ? convertMarketListByRole(response.data, isDefaultByRole) : null;
}

export const getMarketListByUser = (userId) => async (dispatch) => {
    const url = 'Portal/AdminRoleCommon/GetMarketListByUser';
    const response = await post(url, { userId });

    const data = response && response.status === 200 ? convertMarketListByRole(response.data) : [];

    dispatch(setMarketListByUser(data));
}

export const createUser = (userInfo) => async () => {
    const url = 'Portal/AdminInfo/CreateUser';
    const response = await post(url, userInfo);

    return response && response.status === 200 ? response.data : null;
}

export const updateUser = (userInfo) => async () => {
    const url = 'Portal/AdminInfo/UpdateUser';
    const response = await post(url, userInfo);

    return response && response.status === 200 ? response.data : null;
}

export const updateStatus = (param) => async () => {
    const url = 'Portal/AdminInfo/UpdateStatus';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}