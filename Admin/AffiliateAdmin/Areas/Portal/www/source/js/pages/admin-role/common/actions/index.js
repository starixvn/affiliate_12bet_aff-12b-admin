import { setSelectedMenu, setUserId, setUserName } from '../../core/actions';
import { setIsBack } from '../../admin-list/actions';
import { adminRoleMenuKey } from '../../core/libs/constants';

export const redirectToAdminList = () => (dispatch) => {
    dispatch(setSelectedMenu(adminRoleMenuKey.adminList));
    dispatch(setUserId(null));
    dispatch(setUserName(null));
    dispatch(setIsBack(true));
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