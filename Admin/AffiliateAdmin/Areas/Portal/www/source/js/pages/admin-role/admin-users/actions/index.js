import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { setPagingOption } from '../../../../actions/pagingAction';
import { redirectToCreateNew, redirectToUserDetail } from '../../common/actions';
import { SET_PARAM, SET_CHECKED_USERS, SET_IS_BACK, SHOULD_RESET_SCROLL_POSITION, SET_GRID_DATA, SET_USER_INFO } from './types';

export { toggleLoader, setPagingOption, redirectToCreateNew, redirectToUserDetail };

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
    dispatch(setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize }));
    dispatch(setShouldResetScrollPosition(true));
    dispatch(setCheckedUsers(checkedUsers));
    dispatch(toggleLoader(false));
}

export const getRoleByUser = (userId) => () => {
    //const url = 'Portal/AdminRole/GetRoleByUser';
    //const response = await post(url, { userId });

    //return response && response.status === 200 ? response.data : null;

    return [
        { FunctionDescription: 'User Control', IndividualPermission: 1, RolePermission: 3 },
        { FunctionDescription: 'Affiliate', IndividualPermission: 3, RolePermission: 0 },
        { FunctionDescription: 'Payment', IndividualPermission: 3, RolePermission: 0 },
        { FunctionDescription: 'Media', IndividualPermission: 0, RolePermission: 0 },
        { FunctionDescription: 'Affiliate Exchange', IndividualPermission: 1, RolePermission: 1 },
        { FunctionDescription: 'Hot News', IndividualPermission: 0, RolePermission: 0 },
        { FunctionDescription: 'Revenue Share Group', IndividualPermission: 0, RolePermission: 1 },
        { FunctionDescription: 'Affiliate Report', IndividualPermission: 3, RolePermission: 3 }
    ]
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