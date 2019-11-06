import { post } from '../../../../modules/ajaxRequest';

export const getUserDetail = async (selectedUserId) => {
    const url = 'Portal/Administrator/GetUserDetail';
    const response = await post(url, { selectedUserId });

    return response && response.status === 200 ? response.data : null;
}

export const insertUser = async (param) => {
    const url = 'Portal/Administrator/InsertUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const insertExistedUser = async (param) => {
    const url = 'Portal/Administrator/InsertExistedUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateUser = async (param) => {
    const url = 'Portal/Administrator/UpdateUser';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateAccountPolicy = async (param) => {
    const url = 'Portal/Administrator/UpdateAccountPolicy';
    const response = await post(url, param);

    return response && response.status === 200 ? response.data : null;
}

export const updateRolePolicy = async (param) => {
    const url = 'Portal/Department/UpdateRolePolicy';
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

export const getPolicyListByGroupIds = async (param) => {
    const url = 'Portal/Administrator/GetPolicyListByGroupIds';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : null;
    const checkedPolicies = data ? _convertCheckedAccountPolicy(data) : {};

    return { data, checkedPolicies };
}

export const fetchData = async (param) => {
    const url = 'Portal/Administrator/GetUserList';
    const response = await post(url, param);

    const data = response && response.status === 200 ? response.data : [];
    const checkedUsers = data ? _convertCheckedUsers(data) : {};

    return { data, checkedUsers };
}

function _convertCheckedUsers(dataList) {
    const checkedUsers = {};
    for (let i = 0; i < dataList.length; i++) {
        const userId = `${dataList[i].UserId}`;
        checkedUsers[userId] = false;
    }

    return checkedUsers;
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

export const convertCheckedPolicies = (checkedPolicies, selectedId, isAccountPolicy, isGetByIdList) => {
    if (isGetByIdList) {
        const isList = selectedId.split(',');
        return isList.reduce((accumulator, id) => {
            return accumulator = accumulator + getStringConvertCheckedPolicies(checkedPolicies, id, isAccountPolicy);
        }, '');
    }

    return getStringConvertCheckedPolicies(checkedPolicies, selectedId, isAccountPolicy);
}

const getStringConvertCheckedPolicies = (checkedPolicies, selectedId, isAccountPolicy) => {
    return Object.keys(checkedPolicies).reduce((accumulator, key) => {
        const policy = `${selectedId},${key}`;
        const item = checkedPolicies[key];
        const permission = isAccountPolicy ? item.individualPermission : item.groupPermission;

        if (permission.HasHost && permission.HasRead) {
            return accumulator = accumulator + policy + ',3;';
        }
        if (permission.HasHost) {
            return accumulator = accumulator + policy + ',2;';
        }
        if (permission.HasRead) {
            return accumulator = accumulator + policy + ',1;';
        }

        return accumulator = accumulator + policy + ',0;';
    }, '');
}

export const validateUserName = (userName) => {
    const regexCheckSpecialChar = new RegExp("^[a-zA-Z0-9]{1,20}$");

    return regexCheckSpecialChar.test(userName);
}