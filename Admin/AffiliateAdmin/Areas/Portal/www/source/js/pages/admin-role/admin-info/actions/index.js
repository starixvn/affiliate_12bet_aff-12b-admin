import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { redirectToCreateNew, redirectToAdminList } from '../../common/actions';
import { deleteUsers } from '../../admin-list/actions';
import { SET_ROLE_LIST, SET_IS_CREATE_NEW, SET_IS_EDIT, SET_ROLE_PERMISSION } from './types';

export { toggleLoader, redirectToCreateNew, redirectToAdminList, deleteUsers };

export const setRoleList = (data) => {
    return {
        type: SET_ROLE_LIST,
        data
    }
}

export const setIsEdit = (data) => {
    return {
        type: SET_IS_EDIT,
        data
    }
}

export const setIsCreateNew = (data) => {
    return {
        type: SET_IS_CREATE_NEW,
        data
    }
}

export const setRolePermission = (data) => {
    return {
        type: SET_ROLE_PERMISSION,
        data
    }
}

export const fetchData = (userId) => async () => {
    const url = 'Portal/AdminInfo/GetUserInfo';
    const response = await post(url, { userId });

    return response && response.status === 200 ? response.data : null;
}

export const getRoleList = () => async (dispatch) => {
    const url = 'Portal/AdminInfo/GetRoleList';
    const response = await post(url);

    const data = response && response.status === 200 ? convertRoleList(response.data) : null;

    dispatch(setRoleList(data));
}

export const getRolePermission = (roleId) => async (dispatch) => {
    const url = 'Portal/AdminInfo/GetPermissionListByRole';
    const response = await post(url, { roleId });

    const data = response && response.status === 200 ? response.data : null;

    dispatch(setRolePermission(data));
}

export const getMarketListByRole = (roleId) => async (dispatch) => {
    const url = 'Portal/AdminInfo/GetMarketListByRole';
    const response = await post(url, { roleId });

    return response && response.status === 200 ? response.data : null;
}

const convertRoleList = (roleList) => {
    const newRoleList = {};
    for (let i = 0; i < roleList.length; i++) {
        const item = roleList[i];
        newRoleList[item.RoleId] = item.RoleName;
    }

    return newRoleList;
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