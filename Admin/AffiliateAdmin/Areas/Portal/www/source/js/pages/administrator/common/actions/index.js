import { post } from '../../../../modules/ajaxRequest';

export const getUserDetail = async (selectedUserId) => {
    const url = 'Portal/Administrator/GetUserDetail';
    const response = await post(url, { selectedUserId });

    return response && response.status === 200 ? response.data : null;
}

export const insertUser = async (param) =>  {
    const url = 'Portal/Administrator/InsertUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateUser = async (param) => {
    const url = 'Portal/Administrator/UpdateUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}