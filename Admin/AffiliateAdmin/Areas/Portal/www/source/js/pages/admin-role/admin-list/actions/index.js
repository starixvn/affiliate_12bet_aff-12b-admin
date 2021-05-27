import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { redirectToCreateNew, redirectToUserDetail } from '../../common/actions';
import { SET_PARAM, SET_CHECKED_USERS, SET_IS_BACK, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_USER_INFO } from './types';

export { toggleLoader, redirectToCreateNew, redirectToUserDetail };

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

export const setUserInfo = (data) => {
    return {
        type: SET_USER_INFO,
        data
    }
}

export const setShouldResetScrollPosition = (data) => {
    return {
        type: SHOULD_RESET_SCROLL_POSITION,
        data
    }
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

export const getUserPermissionList = (userId) => async () => {
    const url = 'Portal/AdminRole/GetUserPermissionList';
    const response = await post(url, { userId });

    return response && response.status === 200 ? response.data : null;
}

export const deleteUsers = (userIds) => async () => {
    const url = 'Portal/AdminRole/DeleteUsers';
    const response = await post(url, { userIds });

    return response && response.status === 200 ? response.data : null;
}

function _convertCheckedUsers(dataList) {
    const checkedUsers = {};
    for (let item of dataList) {
        checkedUsers[item.UserId] = false;
    }

    return checkedUsers;
}