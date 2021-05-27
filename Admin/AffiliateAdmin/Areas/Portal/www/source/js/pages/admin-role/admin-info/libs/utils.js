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
    for (let item of roleList) {
        newRoleList[item.RoleId] = item.RoleName;
    }

    return newRoleList;
}

export const getSelectedMarketName = (dataSource) => {
    const selectedItemKey = [];

    for (let item of dataSource) {
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