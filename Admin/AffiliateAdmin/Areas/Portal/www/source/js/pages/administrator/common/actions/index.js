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

export const insertExistedUser = async (param) =>  {
    const url = 'Portal/Administrator/InsertExistedUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateUser = async (param) => {
    const url = 'Portal/Administrator/UpdateUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const getAccountPolicyList = async (param) => {
    const url = 'Portal/Administrator/GetAccountPolicyList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedPolicies = data ? _convertCheckedAccountPolicy(data) : {};

    return { data, checkedPolicies };
}

export const getRolePolicy = async (param) => {
    const url = 'Portal/Department/GetRolePolicy';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedPolicies = data ? _convertCheckedRolePolicy(data) : {};

    return { data, checkedPolicies };
}

function _convertCheckedAccountPolicy(dataList) {
    const checkedPolicies = {};
    for (let i = 0; i < dataList.length; i++) {
        const itemId = dataList[i].FunctionId;
        checkedPolicies[itemId] = {
            individualPermission: {
                HasRead: !!dataList[i].Policy,
                HasHost: dataList[i].Policy === 3
            },
            groupPermission: {
                HasRead: !!dataList[i].GroupPolicy,
                HasHost: dataList[i].GroupPolicy === 3
            }
        };
    }

    return checkedPolicies;
}

function _convertCheckedRolePolicy(dataList) {
    const checkedPolicies = {};
    for (let i = 0; i < dataList.length; i++) {
        const itemId = dataList[i].FunctionId;
        checkedPolicies[itemId] = {
            individualPermission: {},
            groupPermission: {
                HasRead: !!dataList[i].Policy,
                HasHost: dataList[i].Policy === 3
            }
        };
    }

    return checkedPolicies;
}