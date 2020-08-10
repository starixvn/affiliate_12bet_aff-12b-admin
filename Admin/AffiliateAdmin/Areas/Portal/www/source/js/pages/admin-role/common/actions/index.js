import { setSelectedMenu, setUserId, setUserName } from '../../core/actions';
import { setIsBack } from '../../admin-users/actions';
import { adminRoleMenuKey } from '../../core/libs/constants';

export const redirectToAdminUsers = () => (dispatch) => {
    dispatch(setSelectedMenu(adminRoleMenuKey.adminUsers));
    dispatch(setUserId(null));
    dispatch(setUserName(null));
    dispatch(setIsBack(true));
}

export const redirectToAdminInfo = () => (dispatch) => {
    dispatch(setSelectedMenu(adminRoleMenuKey.adminInfo));
}

export const redirectToPermissionRole = () => (dispatch) => {
    dispatch(setSelectedMenu(adminRoleMenuKey.permissionRole));
}

export const redirectToCreateNew = () => (dispatch) => {
    dispatch(setUserId(null));
    dispatch(setUserName(null));
    dispatch(setSelectedMenu(adminRoleMenuKey.adminInfo));
}

export const redirectToUserDetail = (userId, userName) => (dispatch) => {
    dispatch(setUserId(userId));
    dispatch(setUserName(userName));
    dispatch(setSelectedMenu(adminRoleMenuKey.adminInfo));
}