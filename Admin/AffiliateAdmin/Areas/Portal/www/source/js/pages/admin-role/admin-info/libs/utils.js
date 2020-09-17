import { deepClone, updateObjectInArray } from '../../../../modules/dataHandler';

export const convertMarketListByRole = (marketListByRole, isInitial) => {

    const selectedItemKey = [];

    const convertMarketList =  marketListByRole.map(item => {
        if (item.HasPermission) {
            selectedItemKey.push(item.MarketId);
        }

        return {
            key: item.MarketId,
            name: item.MarketName,
            checked: isInitial ? item.HasPermission : false
        };
    });

    return {
        selectedItemKey: selectedItemKey.join(','),
        convertMarketList
    }
}

export const convertRoleList = (roleList) => {
    const newRoleList = {};
    for (let i = 0; i < roleList.length; i++) {
        const item = roleList[i];
        newRoleList[item.RoleId] = item.RoleName;
    }

    return newRoleList;
}

export const getSelectedItemKey = (dataSource) => {
    const selectedItemKey = [];

    for (let i = 0; i < dataSource.length; i++) {
        const item = dataSource[i];

        if (item.checked) {
            selectedItemKey.push(item.key)
        }
    }

    return selectedItemKey.join(",");
}

export const combineArrayObject = (initialArray, updatedArray) => {

    let newArray = deepClone(initialArray);

    initialArray.map((item, index) => {
        const updatedItem = updatedArray.find(i => i.key === item.key);

        if(updatedItem) {
            newArray = updateObjectInArray(newArray, index, updatedItem);
        }
    })

    return newArray;
}