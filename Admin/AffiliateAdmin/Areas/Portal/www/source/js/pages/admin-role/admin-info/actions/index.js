import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { redirectToCreateNew, redirectToAdminUsers } from '../../common/actions';
import { deleteUsers } from '../../admin-users/actions';
import { SET_ROLE_LIST, SET_IS_INFORMATION } from './types';

export { toggleLoader, redirectToCreateNew, redirectToAdminUsers, deleteUsers };

export const setRoleList = (data) => {
    return {
        type: SET_ROLE_LIST,
        data
    }
}

export const setIsInfomation = (data) => {
    return {
        type: SET_IS_INFORMATION,
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