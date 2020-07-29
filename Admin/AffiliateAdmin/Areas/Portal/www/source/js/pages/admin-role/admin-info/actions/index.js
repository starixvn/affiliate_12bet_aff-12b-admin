import { post } from '../../../../modules/ajaxRequest';
import { toggleLoader } from '../../../../actions/loaderAction';
import { SET_USER_NAME, SET_USER_INFO } from './types';

export { toggleLoader };

export const setUserName = (data) => {
    return {
        type: SET_USER_NAME,
        data
    }
}

export const setUserInfo = (data) => {
    return {
        type: SET_USER_INFO,
        data
    }
}

export const fetchData = (param) => async (dispatch) => {
    dispatch(toggleLoader(true));
    const url = 'Portal/AdminInfo/GetUserInfo';
    //const response = await post(url, param);
    const response = await {};

    const data = response && response.status === 200 ? response.data : {};

    dispatch(setUserInfo(data));
    dispatch(toggleLoader(false));
}

export const createUser = (userInfo) => async (dispatch) => {
    const url = 'Portal/AdminInfo/CreateUser';
    const response = await post(url, userInfo);

    if (response && response.status === 200) {
        dispatch(setUserName(userInfo.UserName));

        return response.data;
    }

    return null;
}

export const updateUser = (userInfo) => async () => {
    const url = 'Portal/AdminInfo/UpdateUser';
    const response = await post(url, userInfo);

    return response && response.status === 200 ? response.data : null;
}