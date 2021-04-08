import { deepClone, updateObjectInArray } from '../../../../modules/dataHandler';

export const convertMarketListByRole = (marketListByRole, isInitial) => {

    return  marketListByRole.map(item => {
        return {
            ...item,
            HasPermission: isInitial ? item.HasPermission : false
        };
    });
}

export const convertRoleList = (roleList) => {
    const newRoleList = {};
    for (let i = 0; i < roleList.length; i++) {
        const item = roleList[i];
        newRoleList[item.RoleId] = item.RoleName;
    }

    return newRoleList;
}

export const getSelectedMarketName = (dataSource) => {
    const selectedItemKey = [];

    for (let i = 0; i < dataSource.length; i++) {
        const item = dataSource[i];

        if (item.HasPermission) {
            selectedItemKey.push(item.MarketName)
        }
    }

    return selectedItemKey.join(", ");
}

export const validateLanguage = (marketListByRole) => {
    const selectedMarket = marketListByRole ? marketListByRole.filter(item => item.HasPermission) : [];

    return !!selectedMarket.length;
}