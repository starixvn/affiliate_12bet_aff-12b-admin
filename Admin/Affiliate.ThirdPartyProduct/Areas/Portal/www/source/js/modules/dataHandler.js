export const removeItem = (index, dataList) => {
    return dataList.filter((item, i) => {
        return i !== index;
    })
}

export const deepClone = (data) => {
    return JSON.parse(JSON.stringify(data));
}

export const updateObjectInArray = (array, indexItem, updatedItem) => {
    return array.map((item, index) => {
        if (index !== indexItem) {
            return item;
        }
        return {
            ...item,
            ...updatedItem
        };
    });
}