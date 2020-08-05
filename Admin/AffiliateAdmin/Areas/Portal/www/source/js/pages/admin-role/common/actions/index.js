import { toggleLoader } from '../../../../actions/loaderAction';
import { setSelectedMenu, setUserId, setUserName } from '../../core/actions';
import { setIsBack } from '../../admin-users/actions';
import { adminRoleMenuKey } from '../../core/libs/constants';

export const redirectToAdminUsers = () => (dispatch) => {
    dispatch(toggleLoader(true));
    dispatch(setSelectedMenu(adminRoleMenuKey.adminUsers));
    dispatch(setUserId(null));
    dispatch(setUserName(null));
    dispatch(setIsBack(true));
    dispatch(toggleLoader(false));
}

export const redirectToAdminInfo = () => (dispatch) => {
    dispatch(toggleLoader(true));
    dispatch(setSelectedMenu(adminRoleMenuKey.adminInfo));
    dispatch(toggleLoader(false));
}

export const redirectToPermissionRole = () => (dispatch) => {
    dispatch(toggleLoader(true));
    dispatch(setSelectedMenu(adminRoleMenuKey.permissionRole));
    dispatch(toggleLoader(false));
}