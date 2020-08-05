import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setSelectedMenu, setUserId, setUserName } from '../../core/actions';
import { SET_PARAM, SET_CHECKED_USERS, SET_IS_BACK, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA } from './types';
import { adminRoleMenuKey } from '../../core/libs/constants';

export { toggleLoader };

export const setParam = (data) => {
    return {
        type: SET_PARAM,
        data
    }
}

export const setIsBack = (data) => {
    return {
        type: SET_IS_BACK,
        data
    }
}

export const setCheckedUsers = (data) => {
    return {
        type: SET_CHECKED_USERS,
        data
    }
}

export const setGridData = (data) => {
    return {
        type: SET_GRID_DATA,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
}

export const redirectToCreateNew = () => (dispatch) => {
    dispatch(toggleLoader(true));
    dispatch(setUserId(null));
    dispatch(setUserName(null));
    dispatch(setSelectedMenu(adminRoleMenuKey.adminInfo));
    dispatch(toggleLoader(false));
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/AdminRole/GetUserList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedUsers = data ? _convertCheckedUsers(data) : {};

    dispatch(setGridData(data ? data : []));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedUsers(checkedUsers));
    dispatch(toggleLoader(false));
}

export const deleteUsers = (userIdList) => async () => {
    const url = 'Portal/AdminRole/DeleteUsers';
    const response = await post(url, { userIdList });

    return response && response.status === 200 ? response.data : null;
}

function _convertCheckedUsers(dataList) {
    const checkedUsers = {};
    for (let i = 0; i < dataList.length; i++) {
        const userId = `${dataList[i].UserId}`;
        checkedUsers[userId] = false;
    }

    return checkedUsers;
}