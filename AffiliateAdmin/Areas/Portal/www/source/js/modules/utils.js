const setItem = (index, dataList, newValue) => {
    return dataList.map((item, i) => {
        if (i === index) {
            return Object.assign({}, dataList[index], newValue);
        }

        return Object.assign({}, item);;
    });
}

const removeItem = (index, dataList) => {
    return dataList.filter((item, i) => {
        return i !== index;
    })
}

const deepClone = (data) => {
    return JSON.parse(JSON.stringify(data));
}

export {
    setItem,
    removeItem,
    deepClone
}